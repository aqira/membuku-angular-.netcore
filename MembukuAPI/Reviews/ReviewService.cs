using AutoMapper;
using MembukuAPI.Books;
using MembukuAPI.Reviews.ReviewDtos;

namespace MembukuAPI.Reviews;

public class ReviewService : IReviewService {
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;

    public ReviewService(IReviewRepository reviewRepository, IMapper mapper) {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }

    public IEnumerable<ReviewDto> GetAllReviews() {
        var reviews = _reviewRepository.GetAll();
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }
    public IEnumerable<ReviewRowDto> GetAllReviews(string username) {
        var reviews = _reviewRepository.GetAll(username);

        return reviews.Select(r => new ReviewRowDto {
            BookId = r.BookId,
            Name = r.Book.Name,
            Cover = r.Book.Cover,
            Author = new ReviewAuthorDto { Id = r.Book.Author!.Id, Name = r.Book.Author.Name },
            AvgRating = _reviewRepository.AverageRatingByBookId(r.BookId),
            Rating = r.Rating,
            ReadStatus = r.ReadStatus,
            ReviewNote = r.Description,
            ReadDate = r.ReadDate,
            AddedDate = r.AddedDate
        });
    }

    public ReviewDto GetReviewById(string username, int bookId) {
        var review = _reviewRepository.GetById(username, bookId);
        return _mapper.Map<ReviewDto>(review);
    }

    public ReviewDto CreateReview(CreateReviewDto dto) {
        var existingReview = _reviewRepository.GetById(dto.Username, dto.BookId);
        if (existingReview != null) {
            throw new ArgumentException("Book has already been in the review collection of this User");
        }
        var review = _mapper.Map<Review>(dto);
        review.ReviewDate = DateTime.Now;
        var createdReview = _reviewRepository.Add(review);
        return _mapper.Map<ReviewDto>(createdReview);
    }

    public ReviewDto UpdateReview(string username, int bookId, UpdateReviewDto dto) {
        var review = _reviewRepository.GetById(username, bookId);
        if (review == null) {
            throw new ArgumentNullException("Cannot add a review which has not existed before in the review collection");
        }

        _mapper.Map(dto, review);
        var updatedReview = _reviewRepository.Update(review);

        return _mapper.Map<ReviewDto>(updatedReview);
    }

    public ReviewRowDto UpdateReviewRating(string username, int bookId, UpdateReviewRatingDto dto) {
        var review = _reviewRepository.GetById(username, bookId);
        if (review == null) {
            throw new ArgumentNullException("Cannot add a review which has not existed before in the review collection");
        }

        _mapper.Map(dto, review);
        var updatedReview = _reviewRepository.Update(review);

        return new ReviewRowDto {
            BookId = updatedReview.BookId,
            Name = updatedReview.Book.Name,
            Cover = updatedReview.Book.Cover,
            Author = new ReviewAuthorDto { Id = updatedReview.Book.Author!.Id, Name = updatedReview.Book.Author.Name },
            AvgRating = _reviewRepository.AverageRatingByBookId(updatedReview.BookId),
            Rating = updatedReview.Rating,
            ReadStatus = updatedReview.ReadStatus,
            ReviewNote = updatedReview.Description,
            ReadDate = updatedReview.ReadDate,
            AddedDate = updatedReview.AddedDate
        };
    }

    public bool DeleteReview(string username, int bookId) {
        return _reviewRepository.Delete(username, bookId);
    }
}