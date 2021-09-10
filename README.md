# ASP.NET Web API

ASP.NET is a framework for building web apps and services with .NET and C# which is free, cross-platform and open source. [visit official website here](https://dotnet.microsoft.com/apps/aspnet).

## What is the different between SDK and runtime

SDK is stand for software develop kit which include everything needed for building .NET application. On the other hand, Runtime is used for executing dot net application. Therefore, if you would like to develop .NET application: SDK is the option for you.

## Installation and Setup

1. First, you need to install .NET Core CLI which supports C#, F# and Visual Basic. You can download [.NET 5.0 here](https://dotnet.microsoft.com/download).
2. use `dotnet new webapi --name myproject` to set up Web API
3. use `dotnet build` to build project
4. use `dotnet run` to run project
5. the project will open at localhost:5001. You can go to localhost:5001/swagger to open GUI which is installed by default or you can go to localhost:5001/`<controller_name>` like localhost:5001/WeatherForecast.

## Web Application Templates

Use `dotnet new --list` to list all dotnet templates. These are the examples of dotnet short name templates.

1. mvc - ASP.NET Core Web App (MVC)
2. web app - ASP.NET Core Web App
3. webapi - ASP.NET Core Web API
4. angular - ASP.NET Core with Angular
5. react - ASP.NET Core with React
6. reactredux - ASP.NET Core with React and Redux

## Convention-Based Routing using the routing table

Routing table tells about request url such as HTTP request name (GET, POST, PUT, DELETE), Path, Controller, Action Method, Parameters, etc.

## Folder Structure

1. Controllers - create API and communicate with database
2. Properties/launchSettings.json - config port, swagger, application url and other environment
3. appsettings.Development.json and appsetting.json - config application
4. MyApplication.csproj - define SDK and framework version that be used in the project
5. Program.cs - main program
6. Startup.cs - configure the HTTP request pipeline and add services to the container
7. WeatherForecast.cs - define data model

## Concepts

### Host Builder

Host builder is an object and used to add some features in the app. To add host builder, you need `CreateDefaultBuilder()` method. When you add host builder, you will get all these things by default:

1. Dependency Injection (DI)
2. Configuration - appSettings.json, environment variables, user secrets, etc.
3. Logging - console, debug, etc.
4. Set the content root path to the result of `GetCurrentDirectory()` method

### ConfigureWebHostDefaults

- provides support for HTTP
- sets the Kestrel server as the web server
- enables the IIS integration

###
