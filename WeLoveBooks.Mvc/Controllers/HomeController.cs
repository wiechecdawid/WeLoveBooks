using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WeLoveBooks.DataAccess.Data;
using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.Services.ObjectToModelConverter;
using WeLoveBooks.Mvc.Services.ReviewService;
using WeLoveBooks.Mvc.ViewModels;

namespace WeLoveBooks.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IObjectToModelConverter<Book, BookViewModel> _converter;
        private readonly IReviewService _reviewService;

        public HomeController(AppDbContext context,
            IReviewService reviewService,
            IObjectToModelConverter<Book, BookViewModel> converter)
        {
            (_context, _converter, _reviewService) =
                (context, converter, reviewService);
        }

        public IActionResult Index()
        {
            HomePageViewModel model = new()
            {
                Books = _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Photo)
                    .OrderByDescending(b => b.CreatedDate)
                    .Select(b => _converter.Convert(b))
                    .Take(5)
                    .ToList(),

                Authors = _context.Authors
                .Include(a => a.Photo)    
                .OrderByDescending(a => a.LastName)
                    .Take(5)
                    .ToList(),

                Reviews = _reviewService.GetLatestReviews()
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