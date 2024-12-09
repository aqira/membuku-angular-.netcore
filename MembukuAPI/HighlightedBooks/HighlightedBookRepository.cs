using MembukuAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace MembukuAPI.HighlightedBooks;

public class HighlightedBookRepository : IHighlightedBookRepository {
    private readonly MembukuContext _context;

    public HighlightedBookRepository(MembukuContext context) {
        _context = context;
    }

    public IEnumerable<HighlightedBook> GetAll() {
        return _context.HighlightedBooks.Include(hb => hb.Book).ThenInclude(b => b.Author).ToList();
    }

    public HighlightedBook GetById(int bookId) {
        return _context.HighlightedBooks.Include(hb => hb.Book).FirstOrDefault(hb => hb.BookId == bookId);
    }

    public HighlightedBook Add(HighlightedBook highlightedBook) {
        _context.HighlightedBooks.Add(highlightedBook);
        _context.SaveChanges();
        return highlightedBook;
    }

    public HighlightedBook Update(HighlightedBook highlightedBook) {
        _context.HighlightedBooks.Update(highlightedBook);
        _context.SaveChanges();
        return highlightedBook;
    }

    public bool Delete(int bookId) {
        var highlightedBook = _context.HighlightedBooks.Find(bookId);
        if (highlightedBook == null) return false;

        _context.HighlightedBooks.Remove(highlightedBook);
        _context.SaveChanges();
        return true;
    }
}
