using MembukuAPI.Data;

namespace MembukuAPI.Authors;

public class AuthorRepository : IAuthorRepository {
    private readonly MembukuContext _context;

    public AuthorRepository(MembukuContext context) {
        _context = context;
    }

    public IEnumerable<Author> GetAll() {
        return _context.Authors.ToList();
    }

    public Author GetById(int id) {
        return _context.Authors.Find(id);
    }

    public Author Add(Author author) {
        _context.Authors.Add(author);
        _context.SaveChanges();
        return author;
    }

    public Author Update(Author author) {
        _context.Authors.Update(author);
        _context.SaveChanges();
        return author;
    }

    public bool Delete(int id) {
        var author = _context.Authors.Find(id);
        if (author == null) return false;

        _context.Authors.Remove(author);
        _context.SaveChanges();
        return true;
    }
}