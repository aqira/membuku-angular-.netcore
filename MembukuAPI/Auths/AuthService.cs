using AutoMapper;
using Isopoh.Cryptography.Argon2;
using MembukuAPI.Auths.Dtos;
using MembukuAPI.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MembukuAPI.Auths;

public class AuthService : IAuthService {
    private IUserRepository _userRepository;
    private IMapper _mapper;
    private IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration) {
        this._userRepository = userRepository;
        _mapper = mapper;
        _configuration = configuration;
    }

    public AuthUserDto GetUserByUsername(string username) {
        var user = _userRepository.GetByUsername(username);
        return new AuthUserDto {
            Username = user.Username,
            Role = user.Role,
            Token = CreateToken(user)
        };
    }

    public AuthUserDto Login(AuthLoginDto dto) {
        var user = _userRepository.GetByUsername(dto.Username);
        if (user == null) {
            throw new ArgumentNullException("Username tidak dapat ditemukan");
        }
        if (!Argon2.Verify(user.Password, dto.Password)) {
            throw new ArgumentException("Username atau Password salah");
        }
        return new AuthUserDto {
            Username = user.Username,
            Role = user.Role,
            Token = CreateToken(user)
        };
    }

    public AuthUserDto Register(AuthRegisterDto dto) {
        if (_userRepository.GetByUsername(dto.Username) != null) {
            throw new ArgumentException("Username dengan nama yang ingin ditambahkan sudah ada yang ambil");
        }
        dto.Password = Argon2.Hash(dto.Password);

        var newUser = new User {
            BirthDate = dto.BirthDate,
            Email = dto.Email,
            FirstName = dto.FirstName,
            Gender = dto.Gender,
            JoinDate = dto.JoinDate,
            LastName = dto.LastName,
            Password = dto.Password,
            Username = dto.Username,
            Role = "User"
        };

        var user = _userRepository.Add(newUser);

        return new AuthUserDto {
            Username = user.Username,
            Role = user.Role,
            Token = CreateToken(user)
        };

    }

    public IEnumerable<Claim> GetTokenClaims(string tokenString) {
        var handler = new JwtSecurityTokenHandler();
        return handler.ReadJwtToken(tokenString).Claims;
    }

    private string CreateToken(User user) {
        List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value)
            );

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
