using Microsoft.AspNetCore.Mvc;
using MovieTicket.Dtos;
using MovieTicket.Interface.Auth;

namespace MovieTicket.controllers;


[ApiController]
[Route("api/[controller]")]
public class Authcontroller : ControllerBase
{
    private readonly IAuthService _authService;

    public Authcontroller(IAuthService authService)
    {
        _authService=authService;
    }

    [HttpPost("register-user")]
    public async Task<IActionResult> RegisterUserAsync(RegisterUserDto dto)
    {
        var user=await _authService.RegisterUserAsync(dto);

        return Ok(user);
    }

    [HttpPost("login-user")]
    public async Task<IActionResult> LoginUserAsync(LoginUserDto dto)
    {
        var user=await _authService.LoginUserAsync(dto);

        return Ok(user);
    }

    [HttpPatch("update-user")]
    public async Task<IActionResult> UpdateUserAsync(string email,UpdateUserDto dto)
    {
        var user=await _authService.UpdateUserAsync(email,dto);
        return Ok(user);
    }
}