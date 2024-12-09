namespace MembukuAPI.Books.BookDtos;

public class CreateBookDto {
    public string Name { get; set; }
    public IFormFile Cover { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int AuthorId { get; set; }
}
