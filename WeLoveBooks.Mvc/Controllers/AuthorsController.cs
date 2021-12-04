using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeLoveBooks.DataAccess.Data;
using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.ViewModels;

namespace WeLoveBooks.Mvc.Controllers;


public class AuthorsController : Controller
{
    private readonly AppDbContext _context;

    public AuthorsController(AppDbContext context)
    {
        _context = context;
    }

    [Authorize(Policy = "SiteAdmin")]
    [HttpGet("Admin/[action]")]
    public IActionResult Create() => View();

    [Authorize(Policy = "SiteAdmin")]
    [HttpPost("Admin/[action]")]
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
}
