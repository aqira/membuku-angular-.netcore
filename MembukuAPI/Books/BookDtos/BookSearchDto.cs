namespace MembukuAPI.Books.BookDtos;

public class BookSearchDto {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double AvgRating { get; set; }
    public int RatingsCount { get; set; }
    public DateTime ReleaseDate { get; set; }
    public BookAuthorDto Author { get; set; } = null!;
}
