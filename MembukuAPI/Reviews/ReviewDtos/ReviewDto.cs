namespace MembukuAPI.Reviews.ReviewDtos;

public class ReviewDto {
    public string Username { get; set; } = null!;
    public int BookId { get; set; }
    public string BookName { get; set; } = null!;
    public string ReadStatus { get; set; } = null!;
    public DateTime AddedDate { get; set; }
    public int? Rating { get; set; }
    public string? Description { get; set; }
    public DateTime? ReviewDate { get; set; }
    public DateTime? ReadDate { get; set; }
    public ReviewAuthorDto Author { get; set; } = null!;
}