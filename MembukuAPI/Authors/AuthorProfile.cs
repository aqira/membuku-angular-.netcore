using AutoMapper;
using MembukuAPI.Authors.Dtos;

namespace MembukuAPI.Authors;

public class AuthorProfile : Profile {
    public AuthorProfile() {
        CreateMap<Author, AuthorDto>();
        CreateMap<CreateAuthorDto, Author>();
        CreateMap<UpdateAuthorDto, Author>();
    }
}
