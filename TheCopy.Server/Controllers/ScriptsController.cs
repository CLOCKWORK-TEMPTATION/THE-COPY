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
                var scriptDto = await _scriptService.CreateScriptAsync(model);
                return Ok(scriptDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ai-generate")]
        public async Task<IActionResult> GenerateScriptWithAI(string prompt, string genre, string tone)
        {
            try
            {
                var scriptDto = await _scriptService.GenerateScriptAsync(prompt, genre, tone);
                return Ok(scriptDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
