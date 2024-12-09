using AutoMapper;
using MembukuAPI.Authors;
using MembukuAPI.Books.BookDtos;

namespace MembukuAPI.Books;

public class BookProfile: Profile {
    public BookProfile() {
        CreateMap<Author, BookAuthorDto>();
        CreateMap<Book, BookDto>();
        CreateMap<CreateBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();
    }
}
