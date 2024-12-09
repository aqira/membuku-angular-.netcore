namespace MembukuAPI.Reviews.ReviewDtos;

public class ReviewRowDto {
    public int BookId { get; set; }
    public string Name { get; set; }
    public string? Cover { get; set; }
    public ReviewAuthorDto Author { get; set; } = null!;
    public double AvgRating { get; set; }
    public int? Rating { get; set; }
    public string? ReadStatus { get; set; }
    public string? ReviewNote { get; set; }
    public DateTime? ReadDate { get; set; }
    public DateTime AddedDate { get; set; }
}
