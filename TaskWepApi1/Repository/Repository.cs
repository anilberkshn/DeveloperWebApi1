using System;
using System.Collections.Generic;
using MongoDB.Driver;
using TaskWepApi1.Database;
using TaskWepApi1.Database.Interface;
using TaskWepApi1.Model.Entities;
using TaskWepApi1.Model.TaskRequestModels;

namespace TaskWepApi1.Repository
{
    public class Repository : GenericRepository<TaskProperties>, IRepository
    {
        public Repository(IContext context, string collectionName = "Tasks") : base(context, collectionName)
        {
        }

        public TaskProperties GetById(Guid id)
        {
            var taskProperties = FindOne(x => x.Id == id);

            if (taskProperties == null)
            {
                throw new Exception(); // todo: TaskNotFoundException
            }

            if (taskProperties.IsDeleted)
            {
                throw new Exception(); // todo: TaskNotFoundException
            }

            return taskProperties;
        }

        public List<TaskProperties> GetAll()
        {
            return FindAll();
        }

        public Guid Insert(TaskProperties taskProperties)
        {
            return Create(taskProperties);
        }

        public void Update(Guid taskId, UpdateTaskDto updateTaskDto)
        {
            var update = Builders<TaskProperties>.Update
                .Set(x => x.Title, updateTaskDto.Title)
                .Set(x => x.Description, updateTaskDto.Description)
                .Set(x => x.Department, updateTaskDto.Department)
                .Set(x => x.DeveloperId, updateTaskDto.DeveloperId)
                .Set(x => x.Status, updateTaskDto.Status);

            Update(x => x.TaskId == taskId, update);
        }

        public Guid Delete(Guid guid)
        {
            return Delete(x => x.TaskId == guid);
        }

        public void SoftDelete(Guid guid, SoftDeleteDto softDeleteDto)
        {
            var softDelete  = Builders<TaskProperties>.Update
                    .Set(x => x.DeleteTime, softDeleteDto.DeletedTime)
                    .Set(x => x.IsDeleted, softDeleteDto.IsDeleted)
                ;

            SoftDelete(x => x.TaskId == guid, softDelete);
        }
    }
}