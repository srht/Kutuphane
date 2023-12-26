using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KutuphaneCase.WebApp.Models;
using KutuphaneCase.Core.Entities;
using KutuphaneCase.Service;

namespace KutuphaneCase.WebApp.Controllers;

public class BooksController : Controller
{
    private readonly ILogger<BooksController> _logger;

    IBooksService _booksService;
    public BooksController(IBooksService booksService, ILogger<BooksController> logger)
    {
        _booksService=booksService;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Book book)
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
