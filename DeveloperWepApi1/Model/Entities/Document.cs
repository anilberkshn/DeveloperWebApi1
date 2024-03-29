using System;
using System.Security.AccessControl;

namespace DeveloperWepApi1.Model.Entities
{
    public class Document
    {
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime DeleteTime { get; set; }
        public bool IsDeleted { get; set; } 
    }
}