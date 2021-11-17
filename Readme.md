Posts application:

Prerequisites:

To run dotnet server api
1. .Net Core 5.0 or Docker

To run client
2. nodejs installed


How to run dotnet server application:

1. Navigate to: /PostsApp/Api/WebApi in file manager
2. Open console and run if dotnet is available: 
'dotnet run'
3. If docker is installed the application can run with command: 
'docker run --rm -d  -p 5000:5000/tcp webapi:latest'
4. Open browser on address: `http://localhost:5000/swaggere.html`

5. Navigate to /PostsApp/client in file manager
6. Open new console and run: 'npm install' and then 'npm start'
'npm start' will use proxy to make requests to `http://localhost:5000/`
7. Open browser on address: `http://localhost:4200/`

Run unit tests:

1. Navigate to: /PostsApp/tests
2. Run 'dotnet test'


