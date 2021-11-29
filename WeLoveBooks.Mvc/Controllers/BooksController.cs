using Microsoft.AspNetCore.Mvc;

namespace WeLoveBooks.Mvc.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
