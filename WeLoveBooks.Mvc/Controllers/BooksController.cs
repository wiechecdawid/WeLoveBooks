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

    [HttpGet("[controller]/Index")]
    public IActionResult Index()
    {
        var model = _context.Books
            .Include(b => b.Author)
            .Include(b => b.Photo)
            .Select(b => _converter.Convert(b))
            .ToList();

        return View("Index", model);
    }

    [HttpGet("[controller]/{id}")]
    public IActionResult Details(string id)
    {
        var book = _context.Books.Include(b => b.Author)
            .Include(b => b.Photo)
            .Include(b => b.Reviews)
            .ThenInclude(r => r.AppUser)
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
        var author = _context.Authors
            .Where(a => a.Id.ToString() == model.Author)
            .FirstOrDefault();

        if (author is null)
            return BadRequest("The author does not exist");

        Photo? photo = null;

        if(model.Photo is not null)
        {
            photo = new Photo
            {
                Id = model.Photo.Id,
                Type = (PhotoType)model.Photo.Type,
                Url = model.Photo.Url
            };
        }

        Book book = new()
        {
            Title = model.Title,
            Author = author,
            Description = model.Description,
            Id = Guid.NewGuid(),
            AuthorId = author.Id,
            CreatedDate = model.CreatedDate,
            Photo = photo
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
            .Include(b => b.Photo)
            .FirstOrDefault();

        if (book is null)
            return NotFound();

        PhotoViewModel? photo = null;
        if(book.Photo is not null)
        {
            photo = new PhotoViewModel
            {
                Id = book.Photo.Id,
                Url = book.Photo.Url,
                Type = (int)book.Photo.Type
            };
        }

        CreateBookViewModel model = new()
        {
            Authors = authors,
            Author = book.Author.Id.ToString(),
            Description = book.Description,
            CreatedDate = book.CreatedDate,
            Title = book.Title,
            Photo = photo
        };

        return View(model);
    }

    [Authorize(Policy = "SiteAdmin")]
    [HttpPost("Admin/[controller]/[action]/{id}")]
    public async Task<IActionResult> Edit(string id, CreateBookViewModel model)
    {
        Author author = _context.Authors.Where(a => a.Id.ToString() == model.Author).FirstOrDefault()!;

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