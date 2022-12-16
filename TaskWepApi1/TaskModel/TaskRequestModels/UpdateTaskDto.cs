using System;

namespace TaskWepApi1.TaskModel.TaskRequestModels
{
    public class UpdateTaskDto
    {
       // public string Title { get; set; }   title değişmez mantığı ile Create kısmından farklı olması için

        public string Description { get; set; }

        public string Department { get; set; }

        public Guid? DeveloperId { get; set; }

        public int Status { get; set; } 
    }
}