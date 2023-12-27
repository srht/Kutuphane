using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KutuphaneCase.WebApp.Models;
using KutuphaneCase.Core.Entities;
using KutuphaneCase.Service;
using Microsoft.Extensions.Hosting.Internal;

namespace KutuphaneCase.WebApp.Controllers;

public class BooksController : Controller
{
    private readonly ILogger<BooksController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;

    IBooksService _booksService;
    public BooksController(IBooksService booksService, ILogger<BooksController> logger, IWebHostEnvironment webHostEnvironment)
    {
        _booksService = booksService;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var books=await _booksService.GetAllBooks();
        var responseBooks = books.Select(i =>
                                new BookViewModel
                                {
                                    BookId=i.Id.ToString(),
                                    Title = i.Title,
                                    Authors = i.Author,
                                    PictureURL = i.URL,
                                    ReturnDate = i.ReturnDate.HasValue ? i.ReturnDate.Value.ToShortDateString() : "",
                                    BorrowedBy=i.BorrowedBy
                                });
        
        return View(responseBooks);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(BookViewModel bookToCreate)
    {
        if (!ModelState.IsValid) {
            var book = new Book();
            book.Author = bookToCreate.Authors;
            book.Title = bookToCreate.Title;
            if (bookToCreate.PictureFile != null)
            {
                var uniqueFileName = GetUniqueFileName(bookToCreate.PictureFile.FileName);
                var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploads, uniqueFileName);
                bookToCreate.PictureFile.CopyTo(new FileStream(filePath, FileMode.Create));
                book.URL = "/uploads/"+uniqueFileName;
            }
            await _booksService.CreateBook(book);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Borrow(BorrowViewModel borrowViewModel)
    {
        if (ModelState.IsValid)
        {
            var foundBook = await _booksService.GetBookById(borrowViewModel.BookId);
            foundBook.ReturnDate = DateTime.Parse(borrowViewModel.ReturnDate);
            foundBook.BorrowedBy=borrowViewModel.BorrowedBy;
            await _booksService.UpdateBook(foundBook);
        }
        return RedirectToAction(nameof(Index));
    }

    private string GetUniqueFileName(string fileName)
    {
        fileName = Path.GetFileName(fileName);
        return Path.GetFileNameWithoutExtension(fileName)
                  + "_"
                  + Guid.NewGuid().ToString().Substring(0, 4)
                  + Path.GetExtension(fileName);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
