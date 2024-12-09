namespace MembukuAPI.Auths.Dtos;

public class AuthRegisterDto {
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; } = null!;
    public DateTime JoinDate { get; set; }
    public DateTime LastActiveDate { get; set; }
}
