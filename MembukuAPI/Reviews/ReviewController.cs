using MembukuAPI.Reviews.ReviewDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MembukuAPI.Reviews;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase {
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService) {
        _reviewService = reviewService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ReviewDto>> GetReviews() {
        var reviews = _reviewService.GetAllReviews();
        return Ok(reviews);
    }

    [HttpGet("{username}")]
    public ActionResult<IEnumerable<ReviewDto>> GetReviewsByUsername(string username) {
        var reviews = _reviewService.GetAllReviews(username);
        return Ok(reviews);
    }

    [HttpGet("{username}/{bookId}")]
    public ActionResult<ReviewDto> GetReview(string username, int bookId) {
        var review = _reviewService.GetReviewById(username, bookId);
        if (review == null) {
            return NotFound();
        }
        return Ok(review);
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    public ActionResult<ReviewDto> CreateReview([FromBody] CreateReviewDto dto) {
        var review = _reviewService.CreateReview(dto);
        return CreatedAtAction(nameof(GetReview), new { username = review.Username, bookId = review.BookId }, review);
    }

    [Authorize(Roles = "User")]
    [HttpPut("{username}/{bookId}")]
    public ActionResult<ReviewDto> UpdateReview(string username, int bookId, [FromBody] UpdateReviewDto dto) {
        var updatedReview = _reviewService.UpdateReview(username, bookId, dto);
        if (updatedReview == null) {
            return NotFound();
        }
        return Ok(updatedReview);
    }

    [Authorize(Roles = "User")]
    [HttpPatch("{username}/{bookId}/rating")]
    public ActionResult<ReviewRowDto> UpdateReviewRating(string username, int bookId, [FromBody] UpdateReviewRatingDto dto){
        var updateDto = _reviewService.UpdateReviewRating(username, bookId, dto);
        if (updateDto == null) {
            return NotFound();
        }
        return Ok(updateDto);
    }

    [Authorize(Roles = "User")]
    [HttpDelete("{username}/{bookId}")]
    public ActionResult DeleteReview(string username, int bookId) {
        var deleted = _reviewService.DeleteReview(username, bookId);
        if (!deleted) {
            return NotFound();
        }
        return NoContent();
    }
}