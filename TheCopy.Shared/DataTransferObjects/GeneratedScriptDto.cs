using System;

namespace TheCopy.Shared.DataTransferObjects
{
    public class GeneratedScriptDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }        
        public Guid ProjectId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
