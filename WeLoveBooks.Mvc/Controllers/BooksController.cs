using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeLoveBooks.DataAccess.Data;
using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.ViewModels;

namespace WeLoveBooks.Mvc.Controllers
{
    [AutoValidateAntiforgeryToken]
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
        [HttpGet("Admin/[controller]/[action]")]
        public IActionResult Create()
        {
            var authors = _context.Authors.Select(x => 
                new SelectListItem($"{x.FirstName} {x.LastName}", x.Id.ToString()))
                .ToList();

            CreateBookViewModel model = new()
            { Authors = authors };

            return View(model);
        }

        [Authorize(Policy = "SiteAdmin")]
        [HttpPost("Admin/[controller]/[action]")]
        public async Task<IActionResult> Create(CreateBookViewModel model)
        {
            Author author = _context.Authors.Where(a => a.Id.ToString() == model.Author).FirstOrDefault();
            Book book = new()
            {
                Title = model.Title,
                Author = author,
                Description = model.Description,
                Id = Guid.NewGuid(),
                AuthorId = author.Id,
                CreatedDate = model.CreatedDate
            };

            _context.Books.Add(book);

            if (await _context.SaveChangesAsync() > 0)
                TempData["Result"] = "Success";
            else
                TempData["Result"] = "Failed";

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Policy = "SiteAdmin")]
        [Route("Admin/[controller]/[action]/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _context.Books
                .FirstOrDefaultAsync(a => a.Id.ToString() == id);

            if (book is null)
                return BadRequest();

            _context.Books.Remove(book);

            if (await _context.SaveChangesAsync() > 0)
                TempData["Result"] = "Success";
            else
                TempData["Result"] = "Failed";

            return RedirectToAction("Index", "Home");
        }
    }
}
