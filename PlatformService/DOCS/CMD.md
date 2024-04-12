## COMMANDS

### DOTNET CORE CLI
```bash
dotnet new webapi -n PlatformService

dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet run
```

### DOTNET CORE DOCKER
```bash
docker build -t ahsansoftengineer/platformservice .
docker run -p 8081:8080 -d ahsansoftengineer/platformservice
# http://localhost:8081/api/platform
# http://localhost:8081/swagger/index.html
docker ps # -a 
docker container stop container_id
docker container start container_id
docker push ahsansoftnegineer/platformservice
```