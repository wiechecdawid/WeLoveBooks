using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.Mvc.ViewModels;

public class HomePageViewModel
{
    public IEnumerable<BookViewModel> Books { get; set; }
    public IEnumerable<Author> Authors { get; set; }
    public IEnumerable<ReviewListViewModel> Reviews { get; set; }
}
