using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeLoveBooks.DataAccess.Data;
using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.Services.ObjectToModelConverter;
using WeLoveBooks.Mvc.ViewModels;

namespace WeLoveBooks.Mvc.Controllers;

[AutoValidateAntiforgeryToken]
public class BooksController : Controller
{
    private readonly AppDbContext _context;
    private readonly IObjectToModelConverter<Book, BookViewModel> _converter;

    public BooksController(AppDbContext context, IObjectToModelConverter<Book, BookViewModel> converter)
    {
        (_context, _converter) = (context, converter);
    }

    [HttpGet("Index")]
    public IActionResult Index()
    {
        var model = _context.Books
            .Include(b => b.Author)
            .Select(b => _converter.Convert(b))
            .ToList();

        return View("Index", model);
    }

    [HttpGet("{id}")]
    public IActionResult Details(string id)
    {
        var book = _context.Books.Include(b => b.Author)
            .FirstOrDefault(b => b.Id.ToString() == id);
        var model = _converter.Convert(book);

        return View(model);
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
    [HttpGet("Admin/[controller]/[action]/{id}")]
    public IActionResult Edit(string id)
    {
        var authors = _context.Authors.Select(x =>
            new SelectListItem($"{x.FirstName} {x.LastName}", x.Id.ToString()))
            .ToList();

        var book = _context.Books.Where(b => b.Id.ToString() == id)
            .Include(b => b.Author)
            .FirstOrDefault();

        if (book is null)
            return NotFound();

        CreateBookViewModel model = new()
        {
            Authors = authors,
            Author = book.Author.Id.ToString(),
            Description = book.Description,
            CreatedDate = book.CreatedDate,
            Title = book.Title
        };

        return View(model);
    }

    [Authorize(Policy = "SiteAdmin")]
    [HttpPost("Admin/[controller]/[action]/{id}")]
    public async Task<IActionResult> Edit(string id, CreateBookViewModel model)
    {
        Author author = _context.Authors.Where(a => a.Id.ToString() == model.Author).FirstOrDefault();

        var book = _context.Books.Where(a => a.Id.ToString() == id).FirstOrDefault();

        if (book is not null)
        {
            book.Title = model.Title;
            book.Author = author;
            book.Description = model.Description;
            book.CreatedDate = model.CreatedDate;
        }

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