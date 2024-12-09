using MembukuAPI.Books.BookDtos;
using MembukuAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace MembukuAPI.Books;

public class BookRepository : IBookRepository {
    private readonly MembukuContext _context;

    public BookRepository(MembukuContext context) {
        _context = context;
    }

    public IEnumerable<Book> GetAll() {
        return _context.Books.Include(b => b.Author).ToList();
    }

    public IEnumerable<Book> GetAll(int pageNumber, int pageSize, string? name, string? authorName) {
        // Normalize empty strings to null
        name = string.IsNullOrWhiteSpace(name) ? null : name;
        authorName = string.IsNullOrWhiteSpace(authorName) ? null : authorName;

        var query = from book in _context.Books.Include(b => b.Author)
                    where
                    (name == null
                    || book.Name.Contains(name))
                    &&
                    (authorName == null
                    || (book.Author != null && book.Author.Name.Contains(authorName)))
                    select book;

        return query.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public IEnumerable<Book> GetAll(string q, SearchType searchType) {
        IQueryable<Book> query = _context.Books.Include(b => b.Author) ;

        switch (searchType) {
            case SearchType.All:
                query = query.Where((b) => b.Name.Contains(q) || b.Author!.Name.Contains(q));
                break;
            case SearchType.Title:
                query = query.Where((b) => b.Name.Contains(q));
                break;
            case SearchType.Author:
                query = query.Where((b) => b.Author!.Name.Contains(q));
                break;
        }

        return query.ToList();
    }

    public int Count(string? name, string? authorName) {
        name = string.IsNullOrWhiteSpace(name) ? null : name;
        authorName = string.IsNullOrWhiteSpace(authorName) ? null : authorName;

        var query = from book in _context.Books.Include(b => b.Author)
                    where
                    (name == null
                    || book.Name.Contains(name))
                    &&
                    (authorName == null
                    || (book.Author != null && book.Author.Name.Contains(authorName)))
                    select book;

        return query.Count();
    }

    public Book GetById(int id) {
        return _context.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == id);
    }

    public Book Add(Book book) {
        _context.Books.Add(book);
        _context.SaveChanges();
        return book;
    }

    public Book Update(Book book) {
        _context.Books.Update(book);
        _context.SaveChanges();
        return book;
    }

    public bool Delete(int id) {
        var book = _context.Books.Find(id);
        if (book == null) return false;

        _context.Books.Remove(book);
        _context.SaveChanges();
        return true;
    }



}