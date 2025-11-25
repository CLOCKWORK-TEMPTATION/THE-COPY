namespace TheCopy.Domain.Entities;
using System;

public class Script
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty; // Added this line
    public string Content { get; set; } = string.Empty;
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}