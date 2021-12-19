using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.Mvc.ViewModels;

public class CreateReviewViewModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Book Book { get; set; }
    public DateTime CreatedDate { get; set; }
}
