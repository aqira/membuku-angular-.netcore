namespace MembukuAPI.Books.BookDtos;

public class BookDto {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Cover { get; set; }
    public DateTime ReleaseDate { get; set; }
    public BookAuthorDto? Author { get; set; }
}