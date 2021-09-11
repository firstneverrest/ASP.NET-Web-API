using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

       [Route("todo/{id}")]
       public string GetById(int id)
       {
           return "Todo " + id;
       }

       [Route("search")]
       public string SearchTodo(int id, string name)
       {
           return "Search: " + id + " " + name;
       }
    }
}
