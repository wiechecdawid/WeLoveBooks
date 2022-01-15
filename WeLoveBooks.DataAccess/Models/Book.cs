namespace WeLoveBooks.DataAccess.Models;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Description { get; set; }
    public Guid AuthorId { get; set; }
    public virtual Author Author { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<BookRate> BookRates { get; set; }
}