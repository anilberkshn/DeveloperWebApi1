using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TaskWepApi1.Model.Entities;
using TaskWepApi1.Model.ErrorModels;
using TaskWepApi1.Model.TaskRequestModels;
using TaskWepApi1.Repository;

namespace TaskWepApi1.Services
{
    public class TaskService: ITaskService
    {
        private readonly IRepository _repository;

        public TaskService(IRepository repository)
        {
            _repository = repository;
        }


        public TaskProperties GetById(Guid id)
        {
            var task = _repository.GetById(id);
            
            if (task == null)
            {
                throw new TaskException(HttpStatusCode.NotFound,"developer bulunamadı.");
            }
            if (task.IsDeleted)
            {
                throw new TaskException(HttpStatusCode.NotFound, "developer bulunamadı.");
            }
            
            return task;
        }

        public Task<IEnumerable<TaskProperties>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Guid> InsertTaskAsync(TaskProperties taskProperties)
        {
            return _repository.InsertTaskAsync(taskProperties);
        }

        public void Update(Guid taskId, UpdateTaskDto updateTaskDto)
        {
            _repository.Update(taskId,updateTaskDto);
        }

        public Guid Delete(Guid guid)
        {
            return _repository.Delete(guid);
        }

        public void SoftDelete(Guid guid, SoftDeleteDto softDeleteDto)
        {
            _repository.SoftDelete(guid,softDeleteDto);
        }
    }
}