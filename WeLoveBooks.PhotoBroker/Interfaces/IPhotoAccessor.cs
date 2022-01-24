using WeLoveBooks.PhotoBroker.Photos;

namespace WeLoveBooks.PhotoBroker.Interfaces;

public interface IPhotoAccessor
{
    Task<PhotoUploadResult> AddPhoto(IFormFile file);
    Task<string> DeletePhoto(string publicId);
}
