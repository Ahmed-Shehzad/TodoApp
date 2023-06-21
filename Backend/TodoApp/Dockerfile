#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TodoApp/TodoApp.csproj", "TodoApp/"]
COPY ["Foundation.API/Foundation.API.csproj", "Foundation.API/"]
COPY ["Foundation.Core/Foundation.Core.csproj", "Foundation.Core/"]
COPY ["TodoApp.Service/TodoApp.Service.csproj", "TodoApp.Service/"]
COPY ["Foundation.Data/Foundation.Data.csproj", "Foundation.Data/"]
COPY ["TodoApp.Data/TodoApp.Data.csproj", "TodoApp.Data/"]
COPY ["TodoApp.Domain/TodoApp.Domain.csproj", "TodoApp.Domain/"]
RUN dotnet restore "TodoApp/TodoApp.csproj"
COPY . .
WORKDIR "/src/TodoApp"
RUN dotnet build "TodoApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TodoApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoApp.dll"]