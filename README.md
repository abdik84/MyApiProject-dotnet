# MyApiProject

Simple ASP.NET Core application demonstrating:

### REST API endpoint
```
http://localhost:5051/api/ping
```

### WebSocket Test
Copy and paste following code in browser console.
```
let ws = new WebSocket("ws://localhost:5051/ws");
ws.onmessage = (e) => console.log(e.data);
ws.onopen = () => ws.send("Hello Server");
````

### Linux deployment readiness (Simple Mode)
```
dotnet publish -c Release -o ./publish
cd publish
nohup ASPNETCORE_URLS=http://0.0.0.0:5000 dotnet MyApiProject.dll  
```

Check:
```
ps aux | grep dotnet
```

Stop:
```
kill <PID>
```




---
