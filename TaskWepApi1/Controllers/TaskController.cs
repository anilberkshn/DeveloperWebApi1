using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskWepApi1.Model.Entities;
using TaskWepApi1.Model.ResponseModel;
using TaskWepApi1.Model.TaskRequestModels;
using TaskWepApi1.Repository;
using TaskWepApi1.Services;

namespace TaskWepApi1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/task")]

    public class TaskController : ControllerBase
    {
     //   public List<TaskModel.TaskEntities.Task> _tasks { get; set; } /*List<Task> yaparken farklı bir task a gidiyor. Entities içindekine dikkat et*/ 
     private readonly ITaskService _taskService;
        
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        
       // [Route("[createTask]")]       Hata aldırıyor.
        [HttpPost]
        public IActionResult CreateTask([FromBody] CreateTaskDto createTaskDto)
        {
            var task = new TaskProperties()
            {
                Id = new Guid(),
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                Department = createTaskDto.Department,
                DeveloperId = createTaskDto.DeveloperId,
                Status =   createTaskDto.Status
            };

            _taskService.InsertTaskAsync(task);

            var response = new CreateResponse()
            {
                Id = task.Id
            };
            return Ok(response);
        }

      //  [HttpGet("{taskId}", Name = "taskId")]
        [HttpGet("taskId")]
       
        public IActionResult GetById(Guid guid)
        {
            var findOne = _taskService.GetById(guid);
            return Ok(findOne);
        }

        [HttpGet("GetAllTask")]
        public IActionResult GetAll()
        {
            var getAllTask = _taskService.GetAllAsync();
            return Ok(getAllTask);
        }

        [HttpDelete("HardDelete")]
        public IActionResult HardDelete(Guid guid)
        {
            var task = _taskService.GetById(guid);
            _taskService.Delete(task.Id);
            return Ok(guid);
        }
        
        [HttpPut]
        public IActionResult UpdateDeveloper(Guid taskId, [FromBody] UpdateTaskDto updateTaskDto)
        {
            var developer = _taskService.GetById(taskId);
            _taskService.Update(taskId, updateTaskDto);
            return Ok(developer);
        }

        
        [HttpPut("SoftDelete")]
        public IActionResult SoftDelete(Guid guid, [FromBody] SoftDeleteDto softDeleteDto)
        {
            var taskProperties = _taskService.GetById(guid);
            _taskService.SoftDelete(taskProperties.Id, softDeleteDto);
            return Ok(guid);
        }
    }
}