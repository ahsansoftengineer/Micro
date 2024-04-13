```bash
docker build -t ahsansoftengineer/commandservice .
docker push ahsansoftengineer/commandservice
docker run -p 8081:8080 -d ahsansoftengineer/commandservice

```