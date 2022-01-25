using Microsoft.AspNetCore.Http;
using WeLoveBooks.Infrastructure.PhotoAccessor;

namespace WeLoveBooks.Infrastructure.Interfaces;

public interface IPhotoAccessor
{
    Task<PhotoUploadResult> AddPhoto(IFormFile file);
    Task<string> DeletePhoto(string publicId);
}
