using AutoMapper;
using MembukuAPI.HighlightedBooks.HighlightedBookDtos;

namespace MembukuAPI.HighlightedBooks;

public class HighlightedBookProfile : Profile {
    public HighlightedBookProfile() {
        CreateMap<HighlightedBook, HighlightedBookDto>();
        CreateMap<CreateHighlightedBookDto, HighlightedBook>();
        CreateMap<UpdateHighlightedBookDto, HighlightedBook>();
    }
}