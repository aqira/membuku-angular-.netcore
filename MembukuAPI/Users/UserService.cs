using AutoMapper;
using MembukuAPI.Users.UserDtos;

namespace MembukuAPI.Users;

public class UserService : IUserService {
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper) {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public IEnumerable<UserDto> GetAllUsers() {
        var users = _userRepository.GetAll();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public UserDto GetUserByUsername(string username) {
        var user = _userRepository.GetByUsername(username);
        return _mapper.Map<UserDto>(user);
    }

    public UserDto CreateUser(CreateUserDto dto) {
        var user = _mapper.Map<User>(dto);
        user.JoinDate = DateTime.Now;
        var createdUser = _userRepository.Add(user);
        return _mapper.Map<UserDto>(createdUser);
    }

    public UserDto UpdateUser(string username, UpdateUserDto dto) {
        var user = _userRepository.GetByUsername(username);
        if (user == null) return null;

        _mapper.Map(dto, user);
        var updatedUser = _userRepository.Update(user);
        return _mapper.Map<UserDto>(updatedUser);
    }

    public bool DeleteUser(string username) {
        return _userRepository.Delete(username);
    }
}