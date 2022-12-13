using System;

namespace TaskWepApi1.TaskModel.TaskRequestModels
{
    public class CreateTaskRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Department { get; set; }

        public Guid? DeveloperId { get; set; }

        public int Status { get; set; } 
    }
}