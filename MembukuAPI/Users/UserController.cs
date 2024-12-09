using MembukuAPI.Users.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MembukuAPI.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase {
    private readonly IUserService _userService;

    public UsersController(IUserService userService) {
        _userService = userService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public ActionResult<IEnumerable<UserDto>> GetUsers() {
        var users = _userService.GetAllUsers();
        return Ok(users);
    }

    [Authorize(Roles = "Admin,User")]
    [HttpGet("{username}")]
    public ActionResult<UserDto> GetUser(string username) {
        var user = _userService.GetUserByUsername(username);
        if (user == null) {
            return NotFound();
        }
        return Ok(user);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult<UserDto> CreateUser([FromBody] CreateUserDto dto) {
        var user = _userService.CreateUser(dto);
        return CreatedAtAction(nameof(GetUser), new { username = user.Username }, user);
    }

    [Authorize(Roles = "Admin,User")]
    [HttpPut("{username}")]
    public ActionResult<UserDto> UpdateUser(string username, [FromBody] UpdateUserDto dto) {
        var updatedUser = _userService.UpdateUser(username, dto);
        if (updatedUser == null) {
            return NotFound();
        }
        return Ok(updatedUser);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{username}")]
    public ActionResult DeleteUser(string username) {
        var deleted = _userService.DeleteUser(username);
        if (!deleted) {
            return NotFound();
        }
        return NoContent();
    }
}