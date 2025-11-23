using System.Text.Json.Serialization;
using TheCopy.Shared.Enums;

namespace TheCopy.Shared.Models;

/// <summary>
/// Represents a media asset (image, video, audio, 3D model, etc.)
/// </summary>
public record MediaAssetDto(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("type")] MediaType Type,
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("thumbnailUrl")] string? ThumbnailUrl,
    [property: JsonPropertyName("fileSize")] long FileSize,
    [property: JsonPropertyName("mimeType")] string MimeType,
    [property: JsonPropertyName("projectId")] Guid? ProjectId,
    [property: JsonPropertyName("uploadedById")] Guid UploadedById,
    [property: JsonPropertyName("uploaderName")] string? UploaderName,
    [property: JsonPropertyName("dimensions")] MediaDimensions? Dimensions,
    [property: JsonPropertyName("duration")] TimeSpan? Duration,
    [property: JsonPropertyName("tags")] List<string> Tags,
    [property: JsonPropertyName("metadata")] Dictionary<string, object>? Metadata,
    [property: JsonPropertyName("createdAt")] DateTime CreatedAt,
    [property: JsonPropertyName("updatedAt")] DateTime UpdatedAt
);

/// <summary>
/// Media dimensions for images and videos
/// </summary>
public record MediaDimensions(
    [property: JsonPropertyName("width")] int Width,
    [property: JsonPropertyName("height")] int Height,
    [property: JsonPropertyName("aspectRatio")] string? AspectRatio
);

/// <summary>
/// Request model for uploading a media asset
/// </summary>
public record UploadMediaRequest(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("projectId")] Guid? ProjectId,
    [property: JsonPropertyName("tags")] List<string>? Tags
);
