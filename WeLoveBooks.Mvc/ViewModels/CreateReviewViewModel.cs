using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.Mvc.ViewModels;

public class CreateReviewViewModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string BookId { get; set; }
    public Book Book { get; set; }
    public IEnumerable<Book> Books { get; set; }
    public string UserName { get; set; }
    public DateTime CreatedDate { get; set; }
}
