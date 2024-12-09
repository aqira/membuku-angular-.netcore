using MembukuAPI.Images.Dtos;

namespace MembukuAPI.Images;

public class ImageService : IImageService {
    public ImageDto GetImageStream(string imagePathLocation, string fileName) {
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), imagePathLocation, fileName);
        if (!File.Exists(fullPath)) {
            return null;
        }
        return new ImageDto {
            FileName = fileName,
            ImageStream = File.OpenRead(fullPath),
            ContentType = GetImageMimeType(fullPath)
        };
    }

    public ImageDto CreateImage(string imagePathLocation, IFormFile file) {
        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), imagePathLocation, fileName);
        using (var stream = new FileStream(fullPath, FileMode.Create)) {
            file.CopyTo(stream);
        }

        return new ImageDto {
            FileName = fileName,
            ImageStream = File.OpenRead(fullPath),
            ContentType = GetImageMimeType(fullPath)
        };
    }

    public bool DeleteImage(string imagePathLocation, string fileName) {
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), imagePathLocation, fileName);
        if (File.Exists(fullPath)) {
            File.Delete(fullPath); // Directly delete without opening the file
            return true;
        }
        return false;
    }

    private string GetImageMimeType(string filePath) {
        var extension = Path.GetExtension(filePath).ToLowerInvariant();
        return extension switch {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            _ => "application/octet-stream",
        };
    }
}
