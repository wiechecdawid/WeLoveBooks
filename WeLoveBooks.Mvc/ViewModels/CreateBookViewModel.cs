using Microsoft.AspNetCore.Mvc.Rendering;
using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.Mvc.ViewModels;

public class CreateBookViewModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Author { get; set; }
    public IEnumerable<SelectListItem> Authors { get; set; }
}
