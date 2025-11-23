using System.Text.Json.Serialization;
using TheCopy.Shared.Enums;

namespace TheCopy.Shared.Models;

/// <summary>
/// Represents a user in the system
/// </summary>
public record UserDto(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("displayName")] string? DisplayName,
    [property: JsonPropertyName("role")] UserRole Role,
    [property: JsonPropertyName("avatarUrl")] string? AvatarUrl,
    [property: JsonPropertyName("isActive")] bool IsActive,
    [property: JsonPropertyName("createdAt")] DateTime CreatedAt,
    [property: JsonPropertyName("updatedAt")] DateTime UpdatedAt,
    [property: JsonPropertyName("lastLoginAt")] DateTime? LastLoginAt
);

/// <summary>
/// Request model for creating a new user
/// </summary>
public record CreateUserRequest(
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("password")] string Password,
    [property: JsonPropertyName("displayName")] string? DisplayName,
    [property: JsonPropertyName("role")] UserRole Role = UserRole.User
);

/// <summary>
/// Request model for updating user information
/// </summary>
public record UpdateUserRequest(
    [property: JsonPropertyName("displayName")] string? DisplayName,
    [property: JsonPropertyName("avatarUrl")] string? AvatarUrl,
    [property: JsonPropertyName("isActive")] bool? IsActive
);

/// <summary>
/// Request model for user login
/// </summary>
public record LoginRequest(
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("password")] string Password
);

/// <summary>
/// Response model for successful login
/// </summary>
public record LoginResponse(
    [property: JsonPropertyName("user")] UserDto User,
    [property: JsonPropertyName("token")] string Token,
    [property: JsonPropertyName("refreshToken")] string RefreshToken,
    [property: JsonPropertyName("expiresAt")] DateTime ExpiresAt
);
