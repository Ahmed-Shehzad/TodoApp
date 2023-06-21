# README

## Prerequisites

- Docker Desktop
- Visual Studio
- .NET 6

## Generate Certificate

Open Powershell Terminal as administrator and run

```bash
dotnet dev-certs https -ep "$env:USERPROFILE\.aspnet\https\aspnetapp.pfx"  -p P@ssword

dotnet dev-certs https --trust
```

## Deployment

To deploy this project run

```bash
docker-compose up -d
```

## Database Migrations

To Create Database and update Migrations navigate to Backend direcotry and run following command in your Terminal

```bash
# dotnet ef migrations add initial --project TodoApp.Data
dotnet ef database update --project TodoApp.Data
```

## API Verification

Open browser and search <https://localhost:5001/swagger/index.html> for Swagger API Playground
