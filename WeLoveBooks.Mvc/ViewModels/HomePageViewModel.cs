using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.Mvc.ViewModels;

public class HomePageViewModel
{
    public IEnumerable<Book> Books { get; set; }
    public IEnumerable<Author> Authors { get; set; }
    public IEnumerable<Review> Reviews { get; set; }
}
