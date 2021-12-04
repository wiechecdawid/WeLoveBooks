using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeLoveBooks.DataAccess.Data;
using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.ViewModels;

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

        [Authorize(Policy = "SiteAdmin")]
        [HttpGet("Admin/[action]")]
        public IActionResult Create()
        {
            CreateBookViewModel model = new()
            { Authors = _context.Authors.OrderBy(a => a.LastName).ThenBy(a => a.FirstName).ToList() };

            return View(model);
        }

        [Authorize(Policy = "SiteAdmin")]
        [HttpPost("Admin/[action]")]
        public async Task<IActionResult> Create(CreateBookViewModel model)
        {
            Book book = new()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                Id = Guid.NewGuid(),
                AuthorId = model.Author.Id,
                CreatedDate = model.CreatedDate
            };

            _context.Books.Add(book);

            if (await _context.SaveChangesAsync() > 0)
                TempData["Result"] = "Success";
            else
                TempData["Result"] = "Failed";

            return RedirectToAction("Index", "Home");
        }
    }
}
