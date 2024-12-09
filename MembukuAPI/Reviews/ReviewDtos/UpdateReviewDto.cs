namespace MembukuAPI.Reviews.ReviewDtos;

public class UpdateReviewDto {
    public int? Rating { get; set; }
    public string Description { get; set; }
    public string ReadStatus { get; set; }
    public DateTime? ReadDate { get; set; }
}   