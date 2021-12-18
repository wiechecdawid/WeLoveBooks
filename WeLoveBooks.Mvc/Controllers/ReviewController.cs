using Microsoft.AspNetCore.Mvc;
using WeLoveBooks.DataAccess.Data;

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
    public IActionResult Create()
    {
        return View();
    }
}
