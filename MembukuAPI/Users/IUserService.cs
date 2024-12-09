using MembukuAPI.Users.UserDtos;

namespace MembukuAPI.Users;

public interface IUserService {
    IEnumerable<UserDto> GetAllUsers();
    UserDto GetUserByUsername(string username);
    UserDto CreateUser(CreateUserDto dto);
    UserDto UpdateUser(string username, UpdateUserDto dto);
    bool DeleteUser(string username);
}