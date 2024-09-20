using ECommerce.API.Auth.Models;
using Microsoft.AspNetCore.Mvc;
using SharedService.Interfaces;

namespace ECommerce.API.Auth.Controllers;

[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        await authService.RegisterUser(registerRequest.Username, registerRequest.Password);
        return Ok();
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var authenticateUserResponse = await authService.AuthenticateUser(loginRequest.Username, loginRequest.Password);
        if (authenticateUserResponse.IsAuthenticated == false)
        {
            return Unauthorized();
        }

        return Ok(new { authenticateUserResponse.Jwt });
    }
}