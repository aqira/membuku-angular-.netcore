using MembukuAPI.Reviews;

namespace MembukuAPI.Reviews;

public interface IReviewRepository {
    IEnumerable<Review> GetAll();
    IEnumerable<Review> GetAll(string username);
    Review GetById(string username, int bookId);
    Review Add(Review review);
    Review Update(Review review);
    bool Delete(string username, int bookId);
    double AverageRatingByBookId(int id);
    int CountRatingByBookId(int id);
}