namespace MembukuAPI.Reviews.ReviewDtos;

public class CreateReviewDto {
    public string Username { get; set; } = null!;
    public int BookId { get; set; }
    public int? Rating { get; set; }
    public string? Description { get; set; }
    public string ReadStatus { get; set; } = null!;
    public DateTime? ReadDate { get; set; }
}