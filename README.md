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

## Host Builder

Host builder is an object and used to add some features in the app. To add host builder, you need `CreateDefaultBuilder()` method. When you add host builder, you will get all these things by default:

1. Dependency Injection (DI)
2. Configuration - appSettings.json, environment variables, user secrets, etc.
3. Logging - console, debug, etc.
4. Set the content root path to the result of `GetCurrentDirectory()` method

## ConfigureWebHostDefaults

- provides support for HTTP
- sets the Kestrel server as the web server
- enables the IIS integration

```c#
// Program.cs
namespace MyApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
```

```c#
namespace MyApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyApplication", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // if the error is occurred, it will show on web browser
                app.UseDeveloperExceptionPage();

                // use Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyApplication v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
```

## Controller

- A controller class in Web API has "controller" suffix.
- The controller class must be inherited from `ControllerBase` which provides many method and properties to handle the HTTP request.
- Use `ApiController` attribute on the controller which handle the client error (400 status code), bind the incoming data with the parameters.
- Use attribute routing

## Middleware

middleware is a code that is used in the HTTP request pipeline which can do something before send back the response. These are all middleware that essential for Web API:

- Routing
- Authentication
- Add exception page

Each middleware will works from top line to bottom line, respectively.

```c#
// Startup.cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyApplication v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
```

### Method in middleware

- Run() - complete the middleware execution
- Use() - insert a new middleware in the pipeline
- Next() - pass the execution to the next middleware
- Map() - map the middleware to a specific URL

```c#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.Use(async context =>
        {
            // execute these command
            await context.Response.WriteAsync("Use 1")

            // pass to next middleware which is app.Run below
            // if you don't use next(), the next middleware will not work
            await next();

            // after finish app.Run, this line will be executed
            await context.Response.WriteAsync("Use 2")

        });

        app.Run(async context =>
        {
            // execute these command
            await context.Response.WriteAsync("Run 1")
            await next();
        });

        // execute CustomCode when the route is matched
        app.Map("/project", CustomCode);

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
```

## Routing

Routing is the process of mapping the incoming http request (URL) to a specific resource or the action method.

- In ASP.NET Core Web app, you can enable routing through middleware.
- Need to insert two middleware in the HTTP pipeline:
  - UseRouting()
  - UseEndpoint()

```c#
// TodoListController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyApplication.Controllers
{
    [ApiController]

    // This is base route at the controller level
    // such as todolist/GetTodo
    [Route("[controller]/[action]")]

    // you can define more than one route
    [Route("gettodo")]
    public class TodoListController : ControllerBase
    {
      [Route("todo")]
       public string GetTodo()
       {
           return "Todo";
       }

       [Route("todolist")]
       public string GetTodoList()
       {
           return "Todo List";
       }

       // dynamic route
       [Route("todo/{id}")]
       public string GetById(int id)
       {
           return "Todo " + id;
       }

       // get params from url
       [Route("search")]
       public string SearchTodo(int id, string name)
       {
           return "Search: " + id + " " + name;
       }
    }
}

```

### Route constraints

You can validate the route variables with route constraints. For example, you want the route to accept only integer (route is string by default). These are route constraints:

- Type - int, bool, datetime, double, float, etc.
- Min - min(number)
- Max - max(number)
- MinLength - minlength(number)
- MaxLength - maxlength(number)
- Length - length(number)
- Range - range(min,max)
- Alpha - receive only alphabet
- Required - required
- Regex - regex(expression)

```c#
// receive only id is integer and greater than or equal to 10
[Route("todo/{id:int:min(10)}")]
public string GetById(int id)
{
    return "Todo " + id;
}

// if the id is string or less than 10, this route will be executed
[Route("todo/{id}")]
public string GetById(int id)
{
    return "Todo " + id;
}

```

## Action method return types

### Specific Type

Use to return only data not status code

```c#
// define custom type - object
public EmployeeModel GetEmployees()
{
    return new EmployModel() { id = 1, name = "James"};
}

// or use IEnumerable, the result is the same
public IEnumerable<EmployeeModel> GetEmployees()
{
    return new List<EmployeeModel>() {
        new EmployModel() { id = 1, name = "James"};
        new EmployModel() { id = 2, name = "Boy"}
    };
}
```

### IActionResult

Use to return status code like Ok() or NotFound()

```c#
[Route("{id}")]
public IActionResult GetEmployees()
{
    if (id == 0)
    {
        return NotFound();
    }
    return Ok(new List<EmployeeModel>() {
        new EmployModel() { id = 1, name = "James"};
        new EmployModel() { id = 2, name = "Boy"}
    });
}
```

### ActionResult<T>

Use to return both status code and data

```c#
[Route("{id}/basic")]
public ActionResult<EmployeeModel> GetEmployees()
{
    if (id == 0)
    {
        return NotFound();
    }

    return new EmployeeModel() {id: 1, name: "John"};
}
```

## Return status code

- Ok() - 200 status code
- Created() - 201
- Accepted() - 202
- BadRequest() - 400
-

```c#
public IActionResult GetAnimals()
{
    var animals = new List<AnimalModel>()
    {
        new AnimalModel() { id = 1, name = "Dog" }
        new AnimalModel() { id = 2, name = "Cat" }
    };
    // return both Ok and response data
    return Ok(animals);
}
```

## Model Binder

- the process of binding the HTTP request data to the parameters of application Controllers or Properties
- There are lots of built-in methods and attributes
- Can create custom model binder

### BindProperty

BindPropery is an attribute and used to map the incoming form-data to the public properties.

```c#
public class AnimalsController : ControllerBase
{
    [BindProperty]
    public string name { get; set; }
    [BindProperty]
    public int age { get; set; }

    [HttpPost("")]
    public IActionResult AddAnimal()
    {
        return Ok($"Name = {this.name}, Age = {this.age}");
    }
}
```

Separate model to other file

```c#
// Models/AnimalsModel.cs
namespace MyApplication.Models
{
    public class AnimalsModel
    {
        public string name { get; set; }
        public int age { get; set; }
    }
}

// Controllers/AnimalsController.cs
public class AnimalsController : ControllerBase
{
    [BindProperty]
    public AnimalsModel Animal { get; set; }

    [HttpPost("")]
    public IActionResult AddAnimal()
    {
        return Ok($"Name = {this.Animal.name}, Age = {this.Animal.age}");
    }
}
```

### BindProperties

BindProperties has the same meaning like BindProperty but used for binding multiple variables.

- works on simple (int, string, etc.) and complex data objects
- does not work for HTTP GET request by default
- is applied on the controller level

```c#
// set to support GET request
[BindProperties(SupportsGet = true)]
public class AnimalsController : ControllerBase
{
    public string name { get; set; }
    public int age { get; set; }

    [HttpPost("")]
    public IActionResult AddAnimal()
    {
        return Ok($"Name = {this.name}, Age = {this.age}");
    }
}
```

## FromQuery

FormQuery attribute is used to bind the data available in query string.

```c#
[HttpGet("country")]
public IActionResult AddCountry([FromQuery]string name)
{
    return Ok($"Name = {name}");
}
```

## FromRoute

FormRoute attribute is used to bind the data available in route (URL).

```c#
[HttpGet("country/{name}")]
public IActionResult AddCountry([FromRoute]string name)
{
    return Ok($"Name = {name}");
}
```

## FromBody

FormBody bind the data from body request.

```c#
[HttpPost("")]
public IActionResult AddCountry([FromBody]string name)
{
    return Ok($"Name = {name}");
}
```

## FormHeader

FormHeader bind the header from request.

```c#
[HttpPost("{id}")]
public IActionResult AddCountry([FormHeader]string developer)
{
    return Ok($"Name = {developer}");
}
```
