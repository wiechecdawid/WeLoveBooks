using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeLoveBooks.DataAccess.Data;
using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.ViewModels;

namespace WeLoveBooks.Mvc.Controllers;

public class ReviewController : Controller
{
    private readonly AppDbContext _context;
    public ReviewController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Create(string bookId)
    {
        if (!Guid.TryParse(bookId, out Guid id))
            return BadRequest("Incorrect book id");

        var book = await _context.Books.FirstOrDefaultAsync(b =>
            b.Id == id);

        if (book is null) return NotFound();

        var model = new CreateReviewViewModel
        {
            Book = book
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateReviewViewModel model)
    {
        var review = new Review
        {
            Id = Guid.NewGuid(),
            BookId = model.Book.Id,
            Book = model.Book,
        };

        _context.Reviews.Add(review);
        
        if(await _context.SaveChangesAsync() > 0)
            return RedirectToAction("Index");

        return BadRequest("Problem saving data");
    }
}
