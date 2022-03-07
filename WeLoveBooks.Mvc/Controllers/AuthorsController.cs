using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeLoveBooks.DataAccess.Data;
using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.Services.ObjectToModelConverter;
using WeLoveBooks.Mvc.ViewModels;

namespace WeLoveBooks.Mvc.Controllers;

[AutoValidateAntiforgeryToken]
public class AuthorsController : Controller
{
    private readonly AppDbContext _context;
    IObjectToModelConverter<Author, AuthorViewModel> _converter;

    public AuthorsController(AppDbContext context, IObjectToModelConverter<Author, AuthorViewModel> converter)
    {
        _context = context;
        _converter = converter;
    }

    [HttpGet("[controller]/Index")]
    public IActionResult Index()
    {
        var model = _context.Authors
            .Include(a => a.Photo)
            .Include(a => a.Books)
            .Select(a => _converter.Convert(a))
            .ToList();

        return View("Index", model);
    }


    [Authorize(Policy = "SiteAdmin")]
    [HttpGet("Admin/[controller]/[action]")]
    public IActionResult Create() => View();

    [Authorize(Policy = "SiteAdmin")]
    [HttpPost("Admin/[controller]/[action]")]
    public async Task<IActionResult> Create(CreateAuthorViewModel model)
    {
        Author author = new()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Bio = model.Bio,
            DateOfBirth = model.DateOfBirth,
            Id = Guid.NewGuid()
        };

        _context.Authors.Add(author);

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
        var author = await _context.Authors
            .FirstOrDefaultAsync(a => a.Id.ToString() == id);

        if (author is null)
            return BadRequest();

        _context.Authors.Remove(author);

        if (await _context.SaveChangesAsync() > 0)
            TempData["Result"] = "Success";
        else
            TempData["Result"] = "Failed";

        return RedirectToAction("Index", "Home");
    }
}
