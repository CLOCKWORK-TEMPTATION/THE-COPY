
using System;

namespace TheCopy.Shared.DataTransferObjects;

public class CreateScriptRequestDto
{
    public string Title { get; set; }
    public Guid ProjectId { get; set; }
    public string Prompt { get; set; }
}
