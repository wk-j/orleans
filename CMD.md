## Commands

```bash
dotnet new console --language C# --output src/Server
dotnet add src/Server/Server.csproj package Microsoft.Orleans.Server
dotnet add src/Server/Server.csproj package Microsoft.Orleans.Client
dotnet add src/Server/Server.csproj package Microsoft.Extensions.Logging.Console
dotnet add src/Server/Server.csproj package Microsoft.Orleans.OrleansCodeGenerator.Build
dotnet run --project src/Server/Server.csproj

dotnet add src/Client/Client.csproj package Microsoft.Orleans.Client
```

## Grain and Silo

```
dotnet add src/GrainAndSilo package Microsoft.Orleans.Core
dotnet add src/GrainAndSilo package Microsoft.Orleans.OrleansCodeGenerator.Build
```