﻿using Microsoft.AspNetCore.Mvc;
using WeLoveBooks.Mvc.Services.PhotoBrokerHttpClient;
using WeLoveBooks.Mvc.ViewModels;

namespace WeLoveBooks.Mvc.Controllers;

[AutoValidateAntiforgeryToken]
public class PhotoController : Controller
{
    private readonly IPhotoBrokerHttpClient _httpClient;

    public PhotoController(IPhotoBrokerHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet("[controller]/Index")]
    public IActionResult Index()
    {
        int.TryParse(TempData["Type"]!.ToString(), out int type);
        var id = TempData["Id"]!.ToString();

        var model = new PhotoFormViewModel
        {
            Type = type,
            Id = id
        };

        return PartialView(@"~/Views/Shared/_PhotoForm.cshtml", model);
    }

    [HttpPost("[controller]/Add")]
    public IActionResult Add(PhotoFormViewModel model)
    {
        var response = _httpClient.SendAsync(model);

        if (!response.IsCompletedSuccessfully)
            TempData["Result"] = "Success";
        else
            TempData["Result"] = "Failure";

        return Ok();
    }
}
