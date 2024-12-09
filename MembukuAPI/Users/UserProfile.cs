using AutoMapper;
using MembukuAPI.Users.UserDtos;

namespace MembukuAPI.Users;

public class UserProfile : Profile {
    public UserProfile() {
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();
    }
}
