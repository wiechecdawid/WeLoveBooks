namespace WeLoveBooks.DataAccess.Models;

public class Review
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid BookId { get; set; }
    public virtual Book Book { get; set; }
    public string AppUserId { get; set; }
    public virtual AppUser AppUser { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public DateTime CreatedDate { get; set; }
}
