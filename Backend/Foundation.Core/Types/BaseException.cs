using FluentValidation.Results;

namespace Foundation.Core.Types
{
    public abstract class BaseException : Exception
    {
        protected BaseException(string actionName, string entityName, object id, string message): base(message)
        {
            ActionName = actionName;
            EntityName = entityName;
            Id = id;
        }

        protected BaseException(string actionName, string entityName, string message): base(message)
        {
            ActionName = actionName;
            EntityName = entityName;
        }

        public string ActionName { get; set; }
        public string EntityName { get; set; }
        public object? Id { get; set; }
    
    }

    public class ObjectNotFoundException : BaseException
    {

        public ObjectNotFoundException(string actionName, string entity, object id) : base(actionName, entity, id,
            $"{actionName}: {entity} \"{id}\" not found.")
        {
        }
    
        public ObjectNotFoundException(string actionName, string entity, object id, Func<object, string> idFormatter) : base(actionName, entity, id,
            $"{actionName}: {entity} \"{idFormatter(id)}\" not found.")
        {
        }
    }

    public class InvalidOperationException : BaseException
    {
        public InvalidOperationException(string actionName, string entity, object id, string message) : base(actionName, entity, id, string.Format("{0}: {3} for {1} \"{2}\".", actionName, entity, id, message))
        {
        }
    
        public InvalidOperationException(string actionName, string entity, object id, string message, Func<object, string> idFormatter) : base(actionName, entity, id, string.Format("{0}: {3} for {1} \"{2}\".", actionName, entity, idFormatter(id), message))
        {
        }
    
        public InvalidOperationException(string actionName, string entity, string message) : base(actionName, entity, string.Format("{0}: {2} for {1}.", actionName, entity, message))
        {
        }
    }

    public class ValidationException : FluentValidation.ValidationException
    {
        public ValidationException(string actionName, IEnumerable<ValidationFailure> errors) : base(
            $"{actionName}: Validation failed. {BuildErrorMessage(errors)}", errors)
        {
            ActionName = actionName;
        }

        private static string BuildErrorMessage(IEnumerable<ValidationFailure> errors) {
            var arr = errors.Select(x => $"{Environment.NewLine} -- {x.PropertyName}: {x.ErrorMessage}");
            return string.Join(string.Empty, arr);
        }
        
        public string ActionName { get; set; }
    }
}