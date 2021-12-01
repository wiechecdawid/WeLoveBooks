using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.Mvc.Areas.AdminArea.ViewModels;

public class CreateBookViewModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public Author Author { get; set; }
    public IEnumerable<Author> Authors { get; set; }
}
