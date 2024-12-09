namespace MembukuAPI.HighlightedBooks;

public interface IHighlightedBookRepository {
    IEnumerable<HighlightedBook> GetAll();
    HighlightedBook GetById(int bookId);
    HighlightedBook Add(HighlightedBook highlightedBook);
    HighlightedBook Update(HighlightedBook highlightedBook);
    bool Delete(int bookId);
}
