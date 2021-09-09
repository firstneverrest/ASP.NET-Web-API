# ASP.NET Web API

ASP.NET is a framework for building web apps and services with .NET and C# which is free, cross-platform and open source. [visit official website here](https://dotnet.microsoft.com/apps/aspnet).

## What is the different between SDK and runtime

SDK is stand for software develop kit which include everything needed for building .NET application. On the other hand, Runtime is used for executing dot net application. Therefore, if you would like to develop .NET application: SDK is the option for you.

## Installation and Setup

1. First, you need to install .NET Core CLI which supports C#, F# and Visual Basic. You can download [.NET 5.0 here](https://dotnet.microsoft.com/download).
2. use `dotnet new webapi` to set up Web API
3. use `dotnet run` to run project

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
