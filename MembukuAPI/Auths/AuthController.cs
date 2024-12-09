using MembukuAPI.Auths.Dtos;
using MembukuAPI.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MembukuAPI.Auths;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase {
    private IAuthService _service;

    public AuthController(IAuthService service) {
        _service = service;
    }

    [Authorize]
    [HttpGet()]
    public IActionResult GetAuthenticatedUser() {
        var authorizationHeader = HttpContext.Request.Headers["Authorization"];
        if (!authorizationHeader.ToString().StartsWith("Bearer")) {
            return NotFound();
        }
        string tokenString = authorizationHeader.ToString().Substring("Bearer ".Length).Trim();
        var claims = _service.GetTokenClaims(tokenString);
        var username = claims.First(claims => claims.Type == ClaimTypes.NameIdentifier).Value;
        var userDto = _service.GetUserByUsername(username);
        return Ok(userDto);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] AuthLoginDto dto) {
        return Ok(_service.Login(dto));
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] AuthRegisterDto dto) {
        return Ok(_service.Register(dto));
    }
}