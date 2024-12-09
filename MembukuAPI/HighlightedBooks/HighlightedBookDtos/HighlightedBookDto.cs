namespace MembukuAPI.HighlightedBooks.HighlightedBookDtos;

public class HighlightedBookDto {
    public int BookId { get; set; }
    public string Name { get; set; } = null!;
    public string AuthorName { get; set; } = null!;
    public string Cover { get; set; } = null!;
    public DateTime ReleaseDate { get; set; }
    public int OrderNumber { get; set; }
}
