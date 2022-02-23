using Microsoft.EntityFrameworkCore;
using WeLoveBooks.DataAccess.Data;
using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Infrastructure.Interfaces;
using WeLoveBooks.PhotoBroker.Dtos;
using WeLoveBooks.PhotoBroker.Interfaces;
using WeLoveBooks.PhotoBroker.Models;
using PhotoType = WeLoveBooks.DataAccess.Models.PhotoType;

namespace WeLoveBooks.PhotoBroker.Services;

public class PhotoService : IPhotoService
{
    private readonly AppDbContext _context;
    private readonly IPhotoAccessor _accessor;

    public PhotoService(AppDbContext context, IPhotoAccessor accessor)
    {
        (_context, _accessor) = (context, accessor);
    }

    public async Task<Photo> AddPhoto(AddPhotoDto photoDto, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        IPhotoRelation relation;
        var type = (PhotoType)(int)photoDto.PhotoType;

        try
        {
            relation = GetRelationByForeignId(photoDto.ForeignKeyId, type);
        }
        catch (InvalidOperationException)
        {
            throw;
        }

        var photoUploadResult = await _accessor.AddPhoto(photoDto.File);

        if (photoUploadResult is null) throw new InvalidOperationException("Could not upload the file!");

        var photo = new Photo
        {
            Id = photoUploadResult.PublicId,
            Url = photoUploadResult.Url,
            Type = type
        };

        relation.Photo = photo;
        relation.PhotoId = photo.Id;

        if (await _context.SaveChangesAsync() > 0)
            return photo;

        throw new InvalidOperationException("Could not save changes against the database");
    }

    public async Task<OperationResult> DeletePhoto(string photoId)
    {
        var result = new OperationResult();

        var photo = _context.Photos
            .Where(p => p.Id == photoId)
            .FirstOrDefault();

        if (photo is null)
            throw new InvalidDataException("Could not find photo by id");

        Type type;
        if (photo.Type == PhotoType.Book)
            type = typeof(Book);
        if (photo.Type == PhotoType.Author)
            type = typeof(Author);
        else
            type = typeof(AppUser);

        var relation = GetRelationByPhotoId(photo.Id, photo.Type);

        relation.PhotoId = null;
        relation.Photo = null;
        _context.Photos.Remove(photo);


        if (await _context.SaveChangesAsync() > 0)
        {
            result.OperationStatus = OperationStatus.Success;
        }
        else
        {
            result.OperationStatus = OperationStatus.Failure;
        }

        return result;
    }

    private IPhotoRelation GetRelationByForeignId(string foreignId, PhotoType type)
    {
        if (!Guid.TryParse(foreignId, out Guid guidId))
            throw new ArgumentException("Incorrect id");
        IPhotoRelation? relation = type switch
        {
            PhotoType.Author => _context.Authors.Where(a => a.Id.ToString() == foreignId).FirstOrDefault(),
            PhotoType.Book => _context.Books.Where(b => b.Id.ToString() == foreignId).FirstOrDefault(),
            PhotoType.User => _context.Users.Where(u => u.Id.ToString() == foreignId).FirstOrDefault(),
            _ => throw new ArgumentException("Unsupported type!"),
        };
        if (relation is null) throw new InvalidOperationException("Could not fint entity!");

        return relation;
    }

    private IPhotoRelation GetRelationByPhotoId(string id, PhotoType type)
    {
        IPhotoRelation? relation = type switch
        {
            PhotoType.Author => _context.Authors
                .Include(a => a.Photo)
                .Where(a => a.Photo.Id == id)
                .FirstOrDefault(),

            PhotoType.Book => _context.Books
                .Include(a => a.Photo)
                .Where(b => b.Photo.Id == id)
                .FirstOrDefault(),

            PhotoType.User => _context.Users
                .Include(a => a.Photo)
                .Where(u => u.Photo.Id == id)
                .FirstOrDefault(),

            _ => throw new ArgumentException("Unsupported type!"),
        };

        return relation;
    }
}
