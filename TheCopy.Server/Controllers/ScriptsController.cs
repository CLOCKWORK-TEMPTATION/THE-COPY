using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TheCopy.Application.Interfaces;
using TheCopy.Shared.DataTransferObjects;

namespace TheCopy.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ScriptsController : ControllerBase
    {
        private readonly IScriptService _scriptService;

        public ScriptsController(IScriptService scriptService)
        {
            _scriptService = scriptService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateScript(CreateScriptRequestDto model)
        {
            try
            {
                var script = await _scriptService.CreateScript(model);
                var scriptDto = new GeneratedScriptDto
                {
                    Id = script.Id,
                    Title = script.Title,
                    Content = script.Content,
                    ProjectId = script.ProjectId,
                    CreatedAt = script.CreatedAt
                };
                return Ok(scriptDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetScriptsByProject(Guid projectId)
        {
            var scripts = await _scriptService.GetScriptsByProject(projectId);
            return Ok(scripts);
        }
    }
}
