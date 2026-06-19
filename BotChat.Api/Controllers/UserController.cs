using BotChat.App.UserLogic;
using BotChat.Contracts.User;
using Microsoft.AspNetCore.Mvc;

namespace BotChat.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService users)
    {
        _userService = users;
    }
    
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> Me()
    {
        var user = await _userService.GetUserAsync();
        
        if (user is null)
            return NotFound();
        
        return Ok(user);
    }
}