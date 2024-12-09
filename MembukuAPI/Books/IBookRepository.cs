using MembukuAPI.Books.BookDtos;

namespace MembukuAPI.Books;

public interface IBookRepository {
    IEnumerable<Book> GetAll();
    IEnumerable<Book> GetAll(int pageNumber, int pageSize, string? name, string? authorName);
    IEnumerable<Book> GetAll(string q, SearchType searchType);
    int Count(string? name, string? authorName);
    Book GetById(int id);
    Book Add(Book book);
    Book Update(Book book);
    bool Delete(int id);
}