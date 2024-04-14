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
### MIGRATIONS
```bash

```
### RUNNING PROJECT
```bash
dotnet run
# http://localhost:5001/swagger/index.html
# http://localhost:5001/api/platform
```

### MIGRATIONS
```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet tool list -g
dotnet tool install --global dotnet-ef
export PATH="$PATH:$HOME/.dotnet/tools"

dotnet ef migrations add initialmigration

docker build -t ahsansoftengineer/platformservice .
docker push ahsansoftengineer/platformservice
kubectl get deployments
kubectl rollout restart deployments platforms-depl
```
- To Work the Migration you have to comment InMemo
```c#
// if (_env.IsProduction())
// {
    Console.WriteLine("--> Using SQL DB");
    services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlServer(Configuration.GetConnectionString("PlatformsConn")));
// }
// else
// {
//     Console.WriteLine("--> Using InMem DB");
//     services.AddDbContext<AppDbContext>(opt =>
//          opt.UseInMemoryDatabase("InMem"));
// }

// PrepDb.PrepPopulation(app, env.IsProduction()); 
```