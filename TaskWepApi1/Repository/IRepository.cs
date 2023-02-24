using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskWepApi1.Model.Entities;
using TaskWepApi1.Model.TaskRequestModels;

namespace TaskWepApi1.Repository
{
    public interface IRepository
    {
        public TaskProperties GetById(Guid id);
        public Task<IEnumerable<TaskProperties>> GetAllAsync();
        public Task<Guid> InsertTaskAsync(TaskProperties taskProperties);
        public void Update(Guid taskId, UpdateTaskDto updateTaskDto);
        public Guid Delete(Guid guid);
        public void SoftDelete(Guid guid, SoftDeleteDto softDeleteDto);
    }
}