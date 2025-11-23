using System.Text.Json.Serialization;
using TheCopy.Shared.Enums;

namespace TheCopy.Shared.Models;

/// <summary>
/// Represents a production project
/// </summary>
public record ProjectDto(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")] string? Description,
    [property: JsonPropertyName("status")] ProjectStatus Status,
    [property: JsonPropertyName("ownerId")] Guid OwnerId,
    [property: JsonPropertyName("ownerName")] string? OwnerName,
    [property: JsonPropertyName("teamMembers")] List<TeamMemberDto> TeamMembers,
    [property: JsonPropertyName("thumbnailUrl")] string? ThumbnailUrl,
    [property: JsonPropertyName("settings")] ProjectSettings? Settings,
    [property: JsonPropertyName("createdAt")] DateTime CreatedAt,
    [property: JsonPropertyName("updatedAt")] DateTime UpdatedAt,
    [property: JsonPropertyName("deadline")] DateTime? Deadline
);

/// <summary>
/// Represents a team member in a project
/// </summary>
public record TeamMemberDto(
    [property: JsonPropertyName("userId")] Guid UserId,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("joinedAt")] DateTime JoinedAt
);

/// <summary>
/// Project-specific settings
/// </summary>
public record ProjectSettings(
    [property: JsonPropertyName("isPublic")] bool IsPublic,
    [property: JsonPropertyName("allowComments")] bool AllowComments,
    [property: JsonPropertyName("requireApproval")] bool RequireApproval,
    [property: JsonPropertyName("customFields")] Dictionary<string, string>? CustomFields
);

/// <summary>
/// Request model for creating a new project
/// </summary>
public record CreateProjectRequest(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")] string? Description,
    [property: JsonPropertyName("deadline")] DateTime? Deadline
);

/// <summary>
/// Request model for updating a project
/// </summary>
public record UpdateProjectRequest(
    [property: JsonPropertyName("name")] string? Name,
    [property: JsonPropertyName("description")] string? Description,
    [property: JsonPropertyName("status")] ProjectStatus? Status,
    [property: JsonPropertyName("thumbnailUrl")] string? ThumbnailUrl,
    [property: JsonPropertyName("deadline")] DateTime? Deadline
);
