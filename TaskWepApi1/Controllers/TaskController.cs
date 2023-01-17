using System;
using Microsoft.AspNetCore.Mvc;
using TaskWepApi1.Model.Entities;
using TaskWepApi1.Model.ResponseModel;
using TaskWepApi1.Model.TaskRequestModels;
using TaskWepApi1.Repository;

namespace TaskWepApi1.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TaskController : Controller
    {
     //   public List<TaskModel.TaskEntities.Task> _tasks { get; set; } /*List<Task> yaparken farklı bir task a gidiyor. Entities içindekine dikkat et*/ 
     private readonly IRepository _repository;
        
        public TaskController(IRepository repository)
        {
            _repository = repository;
        }
        
       // [Route("[createTask]")]       Hata aldırıyor.
        [HttpPost("CreateTask")]
        public IActionResult CreateTask([FromBody] CreateTaskDto createTaskDto)
        {
            var task = new TaskProperties()
            {
                TaskId = new Guid(),
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                Department = createTaskDto.Department,
                DeveloperId = createTaskDto.DeveloperId,
                Status =   createTaskDto.Status
            };
            
            _repository.Insert(task);

            var response = new CreateResponse()
            {
                Id = task.TaskId
            };
            return Ok(response);
        }

        [HttpGet("{taskId}", Name = "taskId")]
        //[HttpGet ("SearchTask")]
        public IActionResult GetById(Guid guid)
        {
            var findOne = _repository.GetById(guid);
            return Ok(findOne);
        }

        [HttpGet("GetAllTask")]
        public IActionResult GetAll()
        {
            var getAllTask = _repository.GetAll();
            return Ok(getAllTask);
        }

        [HttpDelete("HardDelete")]
        public IActionResult HardDelete(Guid guid)
        {
            var task = _repository.GetById(guid);
            _repository.Delete(task.TaskId);
            return Ok(guid);
        }
        
        [HttpPut]
        public IActionResult UpdateDeveloper(Guid taskId, [FromBody] UpdateTaskDto updateTaskDto)
        {
            var developer = _repository.GetById(taskId);
            _repository.Update(taskId, updateTaskDto);
            return Ok(developer);
        }

        
        [HttpPut("SoftDelete")]
        public IActionResult SoftDelete(Guid guid, [FromBody] SoftDeleteDto softDeleteDto)
        {
            var taskProperties = _repository.GetById(guid);
            _repository.SoftDelete(taskProperties.Id, softDeleteDto);
            return Ok(guid);
        }
    }
}