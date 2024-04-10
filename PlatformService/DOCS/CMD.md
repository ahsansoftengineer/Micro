## COMMANDS

### DOTNET CORE CLI
```bash
dotnet new webapi -n PlatformService

dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

### DOTNET CORE DOCKER
```bash
docker build -t ahsansoftengineer/platformservice .
docker run -p 8080:80 -d ahsansoftengineer/platformservice
docker ps # -a 
docker container stop container_id
docker container start container_id
docker push ahsansoftnegineer/platformservice
```