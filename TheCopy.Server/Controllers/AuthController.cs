
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TheCopy.Application.Interfaces;
using TheCopy.Shared.DataTransferObjects;

namespace TheCopy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto model)
    {
        try
        {
            var response = await _authService.RegisterAsync(model);
            return Ok(response);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto model)
    {
        try
        {
            var response = await _authService.LoginAsync(model);
            return Ok(response);
        }
        catch (System.UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
