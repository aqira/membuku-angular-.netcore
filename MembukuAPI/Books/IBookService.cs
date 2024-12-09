using MembukuAPI.Books.BookDtos;
using MembukuAPI.Images.Dtos;
using MembukuAPI.Shared.Dtos;

namespace MembukuAPI.Books;

public interface IBookService {
    IEnumerable<BookDto> GetAllBooks();
    PageResponse<BookDto> GetAllBooks(int pageNumber, int pageSize, string? name, string? authorName);
    IEnumerable<BookSearchDto> GetAllBooksSearch(string q, SearchType searchType);
    BookDto GetBookById(int id);
    BookDto CreateBook(CreateBookDto dto);
    BookDto UpdateBook(UpdateBookDto dto);
    bool DeleteBook(int id);
    ImageDto GetCover(string cover);
    BookDto UpdateBookCover(int id, IFormFile file);
}
