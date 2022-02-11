namespace WeLoveBooks.DataAccess.Models;

public class Book : IPhotoRelation
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Description { get; set; }
    public string? PhotoId { get; set; }
    public virtual Photo? Photo { get; set; }
    public Guid AuthorId { get; set; }
    public virtual Author Author { get; set; }
    public ICollection<Review> Reviews { get; set; }
}