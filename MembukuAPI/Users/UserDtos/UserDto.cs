namespace MembukuAPI.Users.UserDtos;

public class UserDto {
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; }
    public DateTime JoinDate { get; set; }
    public DateTime? LastActiveDate { get; set; }
}
