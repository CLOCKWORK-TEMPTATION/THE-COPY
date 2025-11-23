using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TheCopy.Shared.Models;

namespace TheCopy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ScriptsController : ControllerBase
{
    private readonly ILogger<ScriptsController> _logger;
    private readonly IValidator<CreateScriptRequest> _createValidator;
    private readonly IValidator<UpdateScriptRequest> _updateValidator;

    public ScriptsController(
        ILogger<ScriptsController> logger,
        IValidator<CreateScriptRequest> createValidator,
        IValidator<UpdateScriptRequest> updateValidator)
    {
        _logger = logger;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    /// <summary>
    /// Get all scripts
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<ScriptDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ScriptDto>>> GetScripts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        // TODO: Implement actual database query
        _logger.LogInformation("Getting scripts - Page: {Page}, PageSize: {PageSize}", page, pageSize);

        var scripts = new List<ScriptDto>
        {
            new ScriptDto(
                Id: Guid.NewGuid(),
                Title: "Sample Script",
                Content: "FADE IN:\n\nINT. SAMPLE LOCATION - DAY\n\nA sample script scene...",
                Description: "This is a sample script for demonstration",
                Status: Shared.Enums.ScriptStatus.Draft,
                Version: 1,
                ProjectId: null,
                AuthorId: Guid.NewGuid(),
                AuthorName: "John Doe",
                Tags: new List<string> { "sample", "demo" },
                Metadata: null,
                CreatedAt: DateTime.UtcNow.AddDays(-7),
                UpdatedAt: DateTime.UtcNow.AddDays(-1),
                PublishedAt: null
            )
        };

        return Ok(scripts);
    }

    /// <summary>
    /// Get a script by ID
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ScriptDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScriptDto>> GetScript(Guid id)
    {
        // TODO: Implement actual database query
        _logger.LogInformation("Getting script with ID: {ScriptId}", id);

        // Placeholder
        return NotFound(new { message = $"Script with ID {id} not found" });
    }

    /// <summary>
    /// Create a new script
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ScriptDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ScriptDto>> CreateScript([FromBody] CreateScriptRequest request)
    {
        var validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        // TODO: Implement actual database insert
        _logger.LogInformation("Creating script: {Title}", request.Title);

        var newScript = new ScriptDto(
            Id: Guid.NewGuid(),
            Title: request.Title,
            Content: request.Content,
            Description: request.Description,
            Status: Shared.Enums.ScriptStatus.Draft,
            Version: 1,
            ProjectId: request.ProjectId,
            AuthorId: Guid.NewGuid(), // TODO: Get from authenticated user
            AuthorName: "Current User",
            Tags: request.Tags ?? new List<string>(),
            Metadata: null,
            CreatedAt: DateTime.UtcNow,
            UpdatedAt: DateTime.UtcNow,
            PublishedAt: null
        );

        return CreatedAtAction(nameof(GetScript), new { id = newScript.Id }, newScript);
    }

    /// <summary>
    /// Update an existing script
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ScriptDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScriptDto>> UpdateScript(
        Guid id,
        [FromBody] UpdateScriptRequest request)
    {
        var validationResult = await _updateValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        // TODO: Implement actual database update
        _logger.LogInformation("Updating script with ID: {ScriptId}", id);

        return NotFound(new { message = $"Script with ID {id} not found" });
    }

    /// <summary>
    /// Delete a script
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteScript(Guid id)
    {
        // TODO: Implement actual database delete
        _logger.LogInformation("Deleting script with ID: {ScriptId}", id);

        return NotFound(new { message = $"Script with ID {id} not found" });
    }
}
