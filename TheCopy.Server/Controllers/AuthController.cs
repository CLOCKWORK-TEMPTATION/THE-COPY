
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
            var user = await _authService.Register(model);
            var token = _authService.Login(new LoginRequestDto { Email = user.Email, Password = model.Password });
            return Ok(new AuthResponseDto { Token = token });
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequestDto model)
    {
        try
        {
            var token = _authService.Login(model);
            return Ok(new AuthResponseDto { Token = token });
        }
        catch (System.UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
