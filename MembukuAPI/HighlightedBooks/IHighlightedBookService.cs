using MembukuAPI.HighlightedBooks.HighlightedBookDtos;

namespace MembukuAPI.HighlightedBooks;

public interface IHighlightedBookService {
    IEnumerable<HighlightedBookDto> GetAllHighlightedBooks();
    HighlightedBookDto GetHighlightedBookById(int bookId);
    HighlightedBookDto CreateHighlightedBook(CreateHighlightedBookDto dto);
    HighlightedBookDto UpdateHighlightedBook(UpdateHighlightedBookDto dto);
    bool DeleteHighlightedBook(int bookId);
}
