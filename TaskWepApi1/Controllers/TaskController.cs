using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskWepApi1.TaskModel.TaskRequestModels;
using TaskWepApi1.TaskModel.TaskResponseModels;
using Microsoft.AspNetCore.Mvc;
using Task = TaskWepApi1.TaskModel.TaskEntities.Task;

namespace TaskWepApi1.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TaskController : Controller
    {
        public List<TaskModel.TaskEntities.Task> _tasks { get; set; } /*List<Task> yaparken farklı bir task a gidiyor. Entities içindekine dikkat et*/ 
        public TaskController(List<Task> tasks)
        {
            _tasks = tasks;
        }
        
       // [Route("[createTask]")]       Hata aldırıyor.
        [HttpPost("CreateTask")]
        public IActionResult CreateTask([FromBody] CreateTaskDto createTaskDto)
        {
           var task = new TaskModel.TaskEntities.Task()
            {
                TaskId = new Guid(),
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                Department = createTaskDto.Department,
                DeveloperId = createTaskDto.DeveloperId,
                Status =   createTaskDto.Status
            };
            
            _tasks.Add(task);

            var response = new TaskCreateResponse()
            {
                Id = task.TaskId
            };
            return Ok(response);
        }

        [HttpGet ("SearchTask")]
        public IActionResult SearchTask([FromQuery] SearchTaskDto searchTaskDto)
        {
            var tasks = _tasks.Where(x =>
                x.Title.Contains(searchTaskDto.Title, StringComparison.OrdinalIgnoreCase));
            if (tasks.Any())
            {
                return NotFound();
            }

            return Ok(tasks);
        }
        
    }
}