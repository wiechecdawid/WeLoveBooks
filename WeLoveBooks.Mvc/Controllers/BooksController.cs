using Microsoft.AspNetCore.Mvc;
using WeLoveBooks.DataAccess.Data;

namespace WeLoveBooks.Mvc.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            return View();
        }
    }
}
