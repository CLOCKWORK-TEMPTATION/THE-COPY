
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheCopy.Server.Entities;

public class Project
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Foreign key for User
    public Guid UserId { get; set; }

    // Navigation property for the related User
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;

    // Navigation property for related Scripts
    public virtual ICollection<Script> Scripts { get; set; } = new List<Script>();
}
