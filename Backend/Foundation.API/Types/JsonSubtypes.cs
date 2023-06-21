using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Foundation.API.Types
{
    public interface IJsonSubtypes
    {
        Type GetType(JsonDocument jObject, Type parentType);
        bool CanConvert(Type toType);
    }

    public class JsonSubtypesBuilder<T>
    {
        private readonly string _jsonDiscriminatorPropertyName;
        private readonly IDictionary<object, Type> _subTypeMappings;
        
        public JsonSubtypesBuilder(string jsonDiscriminatorPropertyName)
        {
            _jsonDiscriminatorPropertyName = jsonDiscriminatorPropertyName;
            _subTypeMappings = new Dictionary<object, Type>();
        }

        public JsonSubtypesBuilder<T> Add<TE>(object value)
        {
            _subTypeMappings.Add(value, typeof(TE));
            return this;
        }
        
        public JsonSubtypes<T> Build() => new JsonSubtypes<T>(_jsonDiscriminatorPropertyName, _subTypeMappings);
    }
    
    public static class JsonSubtypes
    {
        public static JsonSubtypesBuilder<T> Of<T>(string jsonDiscriminatorPropertyName) => new JsonSubtypesBuilder<T>(jsonDiscriminatorPropertyName);
    }
    
    public class JsonSubtypes<T> : JsonConverter<T>, IJsonSubtypes
    {
        private readonly string _jsonDiscriminatorPropertyName;
        private readonly IDictionary<object, Type> _subTypeMappings;
        private readonly IDictionary<Type, object> _subTypeMappingsReverse;

        public JsonSubtypes(string jsonDiscriminatorPropertyName, IDictionary<object, Type> subTypeMappings)
        {
            _jsonDiscriminatorPropertyName = jsonDiscriminatorPropertyName;
            _subTypeMappings = subTypeMappings;
            _subTypeMappingsReverse = subTypeMappings.ToDictionary(t => t.Value, t => t.Key);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(T);
        }
        

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions serializer)
        {
            WriteJson(writer, value, serializer);
        }

        private void WriteJson(Utf8JsonWriter writer, T value, JsonSerializerOptions serializer)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }
            writer.WriteStartObject();
            var type = value.GetType();
            writer.WritePropertyName(_jsonDiscriminatorPropertyName);
            var discriminatorValue = _subTypeMappingsReverse[type];
            JsonSerializer.Serialize(writer, discriminatorValue, discriminatorValue.GetType(), serializer);
            
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in properties)
            {
                writer.WritePropertyName(serializer.PropertyNamingPolicy.ConvertName(property.Name));
                var propertyValue = property.GetValue(value);
                if (property.PropertyType == typeof(object))
                {
                    if (propertyValue == null)
                    {
                        writer.WriteNullValue();
                        continue;
                    }
                    if (propertyValue is JsonElement jsonElement)
                    {
                        if (jsonElement.ValueKind == JsonValueKind.Undefined)
                        {
                            writer.WriteNullValue();
                            continue;
                        }
                        jsonElement.WriteTo(writer);
                        continue;
                    }
                    if (propertyValue is JsonDocument jsonDocument)
                    {
                        jsonDocument.WriteTo(writer);
                        continue;
                    }
                }
                JsonSerializer.Serialize(writer, propertyValue, property.PropertyType, serializer);
            }
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (var field in fields)
            {
                writer.WritePropertyName(serializer.PropertyNamingPolicy.ConvertName(field.Name));
                var propertyValue = field.GetValue(value);
                JsonSerializer.Serialize(writer, propertyValue, field.FieldType, serializer);
            }
            writer.WriteEndObject();
        }

        public override T Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions serializer)
        {
            return ReadJson(ref reader, objectType, serializer);
        }

        private T ReadJson(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions serializer)
        {
            while (reader.TokenType == JsonTokenType.Comment)
                reader.Read();

            T value;
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    value = default;
                    break;
                case JsonTokenType.StartObject:
                    value = ReadObject(ref reader, objectType, serializer);
                    break;
                case JsonTokenType.StartArray:
                    {
                        var elementType = GetElementType(objectType);
                        if (elementType == null)
                        {
                            throw CreateJsonReaderException(ref reader, $"Impossible to read JSON array to fill type: {objectType.Name}");
                        }
                        value = (T)ReadArray(ref reader, objectType, elementType, serializer);
                        break;
                    }
                default:
                    throw CreateJsonReaderException(ref reader, $"Unrecognized token: {reader.TokenType}");
            }

            return value;
        }

        private static InvalidOperationException CreateJsonReaderException(ref Utf8JsonReader reader, string message)
        {
            return new InvalidOperationException(message);
        }

        private IList ReadArray(ref Utf8JsonReader reader, Type targetType, Type elementType, JsonSerializerOptions serializer)
        {
            var list = CreateCompatibleList(targetType, elementType);
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                list.Add(ReadJson(ref reader, elementType, serializer));
            }

            if (!targetType.IsArray)
                return list;

            var array = Array.CreateInstance(elementType, list.Count);
            list.CopyTo(array, 0);
            return array;
        }

        private static IList CreateCompatibleList(Type targetContainerType, Type elementType)
        {
            var typeInfo = ToTypeInfo(targetContainerType);
            if (typeInfo.IsArray || typeInfo.IsAbstract)
            {
                return (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
            }

            return (IList)Activator.CreateInstance(targetContainerType);
        }

        private static Type GetElementType(Type arrayOrGenericContainer)
        {
            if (arrayOrGenericContainer.IsArray)
            {
                return arrayOrGenericContainer.GetElementType();
            }

            var genericTypeArguments = GetGenericTypeArguments(arrayOrGenericContainer);
            return genericTypeArguments.FirstOrDefault();
        }

        private T ReadObject(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions serializer)
        {
            // Copy the current state from reader (it's a struct)
            var readerAtStart = reader;

            JsonDocument jObject;
            try
            {
                jObject = JsonDocument.ParseValue(ref reader);
            }
            catch
            {
                return default;
            }

            var targetType = GetType(jObject, objectType, serializer);
            if (targetType is null)
            {
                return default;
                // throw new JsonException($"Could not create an instance of type {objectType.FullName}. Type is an interface or abstract class and cannot be instantiated. Position: {reader.Position.GetInteger()}.");
            }

            return (T)JsonSerializer.Deserialize(ref readerAtStart, targetType, serializer);
        }

        Type IJsonSubtypes.GetType(JsonDocument jObject, Type parentType)
        {
            var resolvedType = GetTypeFromDiscriminatorValue(jObject, parentType);

            return resolvedType ?? null;
        }

        private Type GetType(JsonDocument jObject, Type parentType, JsonSerializerOptions serializer)
        {
            var targetType = parentType;
            IJsonSubtypes lastTypeResolver = null;
            var currentTypeResolver = GetTypeResolver(ToTypeInfo(targetType), serializer.Converters.OfType<IJsonSubtypes>());
            var visitedTypes = new HashSet<Type> { targetType };

            var jsonConverterCollection = serializer.Converters.OfType<IJsonSubtypes>().ToList();
            while (currentTypeResolver != null && currentTypeResolver != lastTypeResolver)
            {
                targetType = currentTypeResolver.GetType(jObject, targetType);
                if (targetType == null || !visitedTypes.Add(targetType))
                {
                    break;
                }
                lastTypeResolver = currentTypeResolver;
                jsonConverterCollection = jsonConverterCollection.Where(c => c != currentTypeResolver).ToList();
                currentTypeResolver = GetTypeResolver(ToTypeInfo(targetType), jsonConverterCollection);
            }

            return targetType;
        }

        private IJsonSubtypes GetTypeResolver(TypeInfo targetType, IEnumerable<IJsonSubtypes> jsonConverterCollection)
        {
            if (targetType == null)
            {
                return null;
            }

            return jsonConverterCollection
                .FirstOrDefault(c => c.CanConvert(ToType(targetType)));
        }

        private Type GetTypeFromDiscriminatorValue(JsonDocument jObject, Type parentType)
        {
            if (!TryGetValueInJson(jObject, _jsonDiscriminatorPropertyName, out var discriminatorValue))
                return null;
            
            if (_subTypeMappings.Any())
            {
                return GetTypeFromMapping(_subTypeMappings, discriminatorValue);
            }

            return GetTypeByName(discriminatorValue.GetString(), ToTypeInfo(parentType));
        }

        private static bool TryGetValueInJson(JsonDocument jObject, string propertyName, out JsonElement value)
        {
            if (jObject.RootElement.TryGetProperty(propertyName, out value))
            {
                return true;
            }

            var objectEnumerator = jObject
                .RootElement
                .EnumerateObject();
            foreach (var jsonProperty in objectEnumerator.Where(jsonProperty => string.Equals(jsonProperty.Name, propertyName, StringComparison.OrdinalIgnoreCase)))
            {
                value = jsonProperty.Value;
                return true;
            }
            return false;
        }

        private static Type GetTypeByName(string typeName, TypeInfo parentType)
        {
            if (typeName == null)
            {
                return null;
            }

            var insideAssembly = parentType.Assembly;

            var parentTypeFullName = parentType.FullName;

            var typeByName = insideAssembly.GetType(typeName);
            if (parentTypeFullName != null && typeByName == null)
            {
                var searchLocation = parentTypeFullName.Substring(0, parentTypeFullName.Length - parentType.Name.Length);
                typeByName = insideAssembly.GetType(searchLocation + typeName, false, true);
            }

            var typeByNameInfo = ToTypeInfo(typeByName);
            if (typeByNameInfo != null && parentType.IsAssignableFrom(typeByNameInfo))
            {
                return typeByName;
            }

            return null;
        }

        private static Type GetTypeFromMapping(IDictionary<object, Type> typeMapping, JsonElement discriminatorToken)
        {
            if (discriminatorToken.ValueKind == JsonValueKind.Null)
            {
                typeMapping.TryGetValue(null, out var targetType);

                return targetType;
            }

            var key = typeMapping.Keys.FirstOrDefault();
            if (key != null)
            {
                var targetLookupValueType = key.GetType();
                var lookupValue = JsonSerializer.Deserialize(discriminatorToken.GetRawText(), targetLookupValueType);

                if (typeMapping.TryGetValue(lookupValue, out var targetType))
                {
                    return targetType;
                }
            }

            return null;
        }
        
        private static IEnumerable<Type> GetGenericTypeArguments(Type type)
        {
            return type.GenericTypeArguments;
        }

        private static TypeInfo ToTypeInfo(Type type)
        {
            return type?.GetTypeInfo();
        }

        private static Type ToType(TypeInfo typeInfo)
        {
            return typeInfo?.AsType();
        }
    }
}
