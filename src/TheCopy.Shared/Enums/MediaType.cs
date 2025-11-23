using System.Text.Json.Serialization;

namespace TheCopy.Shared.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MediaType
{
    Image,
    Video,
    Audio,
    Document,
    ThreeD,
    Other
}
