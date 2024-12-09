using MembukuAPI.Reviews.ReviewDtos;

namespace MembukuAPI.Reviews;
public interface IReviewService {
    IEnumerable<ReviewDto> GetAllReviews();
    IEnumerable<ReviewRowDto> GetAllReviews(string username);
    ReviewDto GetReviewById(string username, int bookId);
    ReviewDto CreateReview(CreateReviewDto dto);
    ReviewDto UpdateReview(string username, int bookId, UpdateReviewDto dto);
    bool DeleteReview(string username, int bookId);
    ReviewRowDto UpdateReviewRating(string username, int bookId, UpdateReviewRatingDto dto);
}