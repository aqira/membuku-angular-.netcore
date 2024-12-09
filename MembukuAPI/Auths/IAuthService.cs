using MembukuAPI.Auths.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MembukuAPI.Auths;

public interface IAuthService {
    AuthUserDto GetUserByUsername(string username);
    AuthUserDto Register(AuthRegisterDto dto);
    AuthUserDto Login(AuthLoginDto dto);
    IEnumerable<Claim> GetTokenClaims(string tokenString);
}
