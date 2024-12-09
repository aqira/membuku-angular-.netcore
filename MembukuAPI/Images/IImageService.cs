using MembukuAPI.Images.Dtos;

namespace MembukuAPI.Images;

public interface IImageService {
    ImageDto GetImageStream(string imageDirectory, string fileName);
    ImageDto CreateImage(string imageDirectory, IFormFile file);
    bool DeleteImage(string imageDirectory, string fileName);
}