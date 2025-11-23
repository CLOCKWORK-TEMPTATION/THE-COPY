using System.Text.Json.Serialization;

namespace TheCopy.Shared.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRole
{
    Guest,
    User,
    Director,
    Producer,
    Admin
}
