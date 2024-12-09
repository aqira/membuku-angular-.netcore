namespace MembukuAPI.Images.Dtos;

public class ImageDto {
    public Stream ImageStream { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public string ContentType { get; set; } = null!;
}
