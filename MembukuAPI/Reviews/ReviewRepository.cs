using MembukuAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace MembukuAPI.Reviews;

public class ReviewRepository : IReviewRepository {
    private readonly MembukuContext _context;

    public ReviewRepository(MembukuContext context) {
        _context = context;
    }

    public IEnumerable<Review> GetAll() {
        return _context.Reviews
            .Include(r => r.UsernameNavigation)
            .Include(r => r.Book)
            .ToList();
    }

    public IEnumerable<Review> GetAll(string username) {
        return _context.Reviews
            .Where(uc => uc.Username.Equals(username))
            .Include(uc => uc.UsernameNavigation)
            .Include(uc => uc.Book)
            .ThenInclude(b => b.Author)
            .ToList();
    }


    public Review GetById(string username, int bookId) {
        return _context.Reviews
            .Include(uc => uc.UsernameNavigation)
            .Include(uc => uc.Book)
            .ThenInclude(b => b.Author)
            .FirstOrDefault(r => r.Username == username && r.BookId == bookId);
    }

    public Review Add(Review review) {
        _context.Reviews.Add(review);
        _context.SaveChanges();
        return review;
    }

    public Review Update(Review review) {
        _context.Reviews.Update(review);
        _context.SaveChanges();
        return review;
    }

    public bool Delete(string username, int bookId) {
        var review = _context.Reviews.Find(username, bookId);
        if (review == null) return false;

        _context.Reviews.Remove(review);
        _context.SaveChanges();
        return true;
    }

    public double AverageRatingByBookId(int id) {
        var ratings = _context.Reviews
            .Where(r => r.BookId == id)
            .Where(r => r.Rating != null)
            .Select(r => r.Rating!.Value);

        return ratings.Any() ? ratings.Average() : 0.0;
    }

    public int CountRatingByBookId(int id) {
        var ratings = _context.Reviews
            .Where(r => r.BookId == id)
            .Where(r => r.Rating != null)
            .Select(r => r.Rating!.Value);
        return ratings.Count();
    }
}
