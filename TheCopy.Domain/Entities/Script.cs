
namespace TheCopy.Domain.Entities;

public class Script
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Guid ProjectId { get; set; }
    public virtual Project Project { get; set; } = null!;
}
