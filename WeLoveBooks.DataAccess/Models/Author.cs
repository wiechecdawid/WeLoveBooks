namespace WeLoveBooks.DataAccess.Models;

public class Author : IPhotoRelation
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Bio { get; set; }
    public string? PhotoId { get; set; }
    public virtual Photo? Photo { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<Book> Books { get; set; }
}
