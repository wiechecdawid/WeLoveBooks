using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.PhotoBroker.Dtos;
using WeLoveBooks.PhotoBroker.Models;

namespace WeLoveBooks.PhotoBroker.Interfaces;

public interface IPhotoService
{
    Task<Photo> AddPhoto(AddPhotoDto photoDto, CancellationToken token);
    Task<OperationResult> DeletePhoto(string photoId);
}
