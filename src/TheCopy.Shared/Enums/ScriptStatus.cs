using System.Text.Json.Serialization;

namespace TheCopy.Shared.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ScriptStatus
{
    Draft,
    InReview,
    Approved,
    Final,
    Archived
}
