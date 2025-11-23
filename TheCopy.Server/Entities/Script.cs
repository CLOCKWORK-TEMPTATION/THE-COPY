
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheCopy.Server.Entities;

public class Script
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Foreign key for Project
    public Guid ProjectId { get; set; }

    // Navigation property for the related Project
    [ForeignKey("ProjectId")]
    public virtual Project Project { get; set; } = null!;
}
