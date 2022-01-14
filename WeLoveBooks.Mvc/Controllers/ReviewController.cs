using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeLoveBooks.DataAccess.Data;
using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.ViewModels;

namespace WeLoveBooks.Mvc.Controllers;

[AutoValidateAntiforgeryToken]
public class ReviewController : Controller
{
    private readonly AppDbContext _context;
    private UserManager<AppUser> _userManager;

    public ReviewController(AppDbContext context,
        UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet("[controller]/Index")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("[controller]/[action]/{id}")]
    public IActionResult Details(string id)
    {
        if (!Guid.TryParse(id, out Guid guidId)) return BadRequest("Invalid review ID");

        var review = _context.Reviews
            .Where(r => r.Id == guidId)
            .Include(r => r.AppUser)
            .FirstOrDefault();
        if (review is null) return BadRequest("Could not find review");

        return View(new ReviewPageViewModel
        {
            Id = id,
            BookId = review.BookId.ToString(),
            Title = review.Title,
            Content = review.Content,
            AppUser = review.AppUser
        });
    }

    [Authorize]
    [HttpGet]
    public IActionResult Create()
    {        
        if(!Guid.TryParse(TempData["BookId"]?.ToString(), out Guid guidId) ||
            guidId == default)
        {
            return BadRequest();
        }

        var book = _context.Books.Where(b => b.Id == guidId).FirstOrDefault();

        CreateReviewViewModel model = new()
        {
            Book = book
        };

        return View(model);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateReviewViewModel model)
    {
        if (!Guid.TryParse(TempData["BookId"]?.ToString(), out Guid guidId))
            return BadRequest("Incorrect book id");

        var book = _context.Books.Where(b => b.Id == guidId).FirstOrDefault();
        var user = await _userManager.FindByNameAsync(User.Identity.Name);

        var review = new Review
        {
            Id = Guid.NewGuid(),
            Title = model.Title,
            Content = model.Content,
            CreatedDate = DateTime.Now,
            AppUser = user,
            AppUserId = user.Id,
            BookId = book.Id,
            Book = book
        };

        _context.Reviews.Add(review);

        if (await _context.SaveChangesAsync() > 0)
            TempData["Result"] = "Success";
        else
            TempData["Result"] = "Failed";

        return RedirectToAction("Index", "Home");
    }
}
