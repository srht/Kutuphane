using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KutuphaneCase.WebApp.Models;
using KutuphaneCase.Core.Entities;
using KutuphaneCase.Service;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using KutuphaneCase.WebApp.Validators;

namespace KutuphaneCase.WebApp.Controllers;

public class BooksController : Controller
{
    private IValidator<BookViewModel> _bookValidator;
    private IValidator<BorrowViewModel> _bookBorrowValidator;
    private readonly ILogger<BooksController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;

    IBooksService _booksService;
    public BooksController(IBooksService booksService, ILogger<BooksController> logger, IWebHostEnvironment webHostEnvironment, IValidator<BookViewModel> bookValidator, IValidator<BorrowViewModel> bookBorrowValidator)
    {
        _booksService = booksService;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
        _bookValidator = bookValidator;
        _bookBorrowValidator = bookBorrowValidator;
    }

    /// <summary>
    /// Tüm kitapları listeler
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Index()
    {
        try
        {
            var books = await _booksService.GetAllBooks();
            var responseBooks = books.Select(i =>
                                    new BookViewModel
                                    {
                                        BookId = i.Id.ToString(),
                                        Title = i.Title,
                                        Authors = i.Author,
                                        PictureURL = i.URL,
                                        ReturnDate = i.ReturnDate.HasValue ? i.ReturnDate.Value.ToShortDateString() : "",
                                        BorrowedBy = i.BorrowedBy
                                    });

            return View(responseBooks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return View(null);
    }

    /// <summary>
    /// Kitap ekleme sayfası
    /// </summary>
    /// <returns></returns>
    public IActionResult Add()
    {
        return View();
    }

    /// <summary>
    /// Kitap ekleme post metodu
    /// </summary>
    /// <param name="bookToCreate"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Add(BookViewModel bookToCreate)
    {
        try
        {
            var result = await _bookValidator.ValidateAsync(bookToCreate);

            if (!result.IsValid)
            {
                result.AddToModelState(ModelState);

                return View(bookToCreate);
            }

            var book = new Book();
            book.Author = bookToCreate.Authors;
            book.Title = bookToCreate.Title;
            if (bookToCreate.PictureFile != null)
            {
                var uniqueFileName = GetUniqueFileName(bookToCreate.PictureFile.FileName);
                var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploads, uniqueFileName);
                bookToCreate.PictureFile.CopyTo(new FileStream(filePath, FileMode.Create));
                book.URL = "/uploads/" + uniqueFileName;
            }
            await _booksService.CreateBook(book);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Ödünç verme post metodu
    /// </summary>
    /// <param name="borrowViewModel"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Borrow(BorrowViewModel borrowViewModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var foundBook = await _booksService.GetBookById(borrowViewModel.BookId);
                foundBook.ReturnDate = DateTime.Parse(borrowViewModel.ReturnDate);
                foundBook.BorrowedBy = borrowViewModel.BorrowedBy;
                await _booksService.UpdateBook(foundBook);
            }
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
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

}
