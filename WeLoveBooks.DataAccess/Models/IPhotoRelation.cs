namespace WeLoveBooks.DataAccess.Models;

public interface IPhotoRelation
{
    public string? PhotoId { get; set; }
    public Photo? Photo { get; set; }
}
