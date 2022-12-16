using System;

namespace TaskWepApi1.TaskModel.TaskEntities
{
    public class Task
    {
        public Guid TaskId { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public string Department { get; set; }
        
        public Guid? DeveloperId { get; set; }
        
        public int Status { get; set; }
    }
}