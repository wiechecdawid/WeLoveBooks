using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WeLoveBooks.DataAccess.Data;
using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.Services.ObjectToModelConverter;
using WeLoveBooks.Mvc.ViewModels;

namespace WeLoveBooks.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IObjectToModelConverter<Book, BookViewModel> _converter;

        public HomeController(AppDbContext context,
            IObjectToModelConverter<Book, BookViewModel> converter)
        {
            (_context, _converter) = (context, converter);
        }

        public IActionResult Index()
        {
            HomePageViewModel model = new()
            {
                Books = _context.Books
                    .Include(b => b.Author)
                    .OrderByDescending(b => b.CreatedDate)
                    .Select(b => _converter.Convert(b))
                    .Take(5)
                    .ToList(),
                Authors = _context.Authors.OrderByDescending(a => a.LastName).Take(5).ToList(),
                Reviews = _context.Reviews.OrderByDescending(r => r.CreatedDate).Take(5).ToList()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}