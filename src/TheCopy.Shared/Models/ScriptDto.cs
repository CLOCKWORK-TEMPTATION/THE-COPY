using System.Text.Json.Serialization;
using TheCopy.Shared.Enums;

namespace TheCopy.Shared.Models;

/// <summary>
/// Represents a script/screenplay in the system
/// </summary>
public record ScriptDto(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("content")] string Content,
    [property: JsonPropertyName("description")] string? Description,
    [property: JsonPropertyName("status")] ScriptStatus Status,
    [property: JsonPropertyName("version")] int Version,
    [property: JsonPropertyName("projectId")] Guid? ProjectId,
    [property: JsonPropertyName("authorId")] Guid AuthorId,
    [property: JsonPropertyName("authorName")] string? AuthorName,
    [property: JsonPropertyName("tags")] List<string> Tags,
    [property: JsonPropertyName("metadata")] Dictionary<string, object>? Metadata,
    [property: JsonPropertyName("createdAt")] DateTime CreatedAt,
    [property: JsonPropertyName("updatedAt")] DateTime UpdatedAt,
    [property: JsonPropertyName("publishedAt")] DateTime? PublishedAt
);

/// <summary>
/// Request model for creating a new script
/// </summary>
public record CreateScriptRequest(
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("content")] string Content,
    [property: JsonPropertyName("description")] string? Description,
    [property: JsonPropertyName("projectId")] Guid? ProjectId,
    [property: JsonPropertyName("tags")] List<string>? Tags
);

/// <summary>
/// Request model for updating an existing script
/// </summary>
public record UpdateScriptRequest(
    [property: JsonPropertyName("title")] string? Title,
    [property: JsonPropertyName("content")] string? Content,
    [property: JsonPropertyName("description")] string? Description,
    [property: JsonPropertyName("status")] ScriptStatus? Status,
    [property: JsonPropertyName("tags")] List<string>? Tags
);
