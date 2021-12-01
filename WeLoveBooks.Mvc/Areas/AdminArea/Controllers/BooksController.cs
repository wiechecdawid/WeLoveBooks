using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeLoveBooks.DataAccess.Data;
using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.Areas.AdminArea.ViewModels;

namespace WeLoveBooks.Mvc.Areas.AdminArea.Controllers;

[Authorize(Policy= "SiteAdmin")]
public class BooksController : Controller
{
    private readonly AppDbContext _context;

    public BooksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Create()
    {
        CreateBookViewModel model = new()
        { Authors = _context.Authors.OrderBy(a => a.LastName).ThenBy(a => a.FirstName).ToList() };

        return View(model);
    }

    [HttpPost]
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

        return RedirectToAction("Index", "Home", new { area = "" });
    }
}
