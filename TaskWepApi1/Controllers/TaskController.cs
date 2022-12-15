using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;



namespace TaskWepApi1.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TaskController : Controller
    {
        public List<Task> _tasks { get; set; }

        public TaskController(List<Task> tasks)
        {
            _tasks = tasks;
        }
        
        
    }
}