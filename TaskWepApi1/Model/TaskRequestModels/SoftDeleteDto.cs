using System;

namespace TaskWepApi1.Model.TaskRequestModels
{
    public class SoftDeleteDto
    {
        // public string Title { get; set; }   title değişmez mantığı ile Create kısmından farklı olması için
        public DateTime DeletedTime { get; set; }
        public bool IsDeleted { get; set; }
        
        // public Guid? DeveloperId { get; set; } görev silinirse tamamlandı mantığı olur mu
        //
        // public int Status { get; set; }
    }
}