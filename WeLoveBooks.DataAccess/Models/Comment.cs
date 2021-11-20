namespace WeLoveBooks.DataAccess.Models;

public class Comment
{
    public Guid Id { get; set; }
    public Guid ReviewId { get; set; }
    public virtual Review Review { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public DateTime CreatedDate { get; set; }

}
