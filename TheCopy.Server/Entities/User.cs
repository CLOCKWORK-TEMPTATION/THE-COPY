
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheCopy.Server.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property for related Projects
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
