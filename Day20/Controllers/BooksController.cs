using BookLibrary.Services;
using BookLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers;

/// <summary>
/// Контроллер для управления книгами
/// </summary>
public class BooksController : Controller
{
    private readonly IBookService _bookService;

    // Внедрение зависимости через конструктор (DI)
    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    // GET: /Books
    public IActionResult Index()
    {
        var books = _bookService.GetAll();
        return View(books);
    }

    // GET: /Books/Details/{id}
    [HttpGet("Books/Details/{id:int}")]
    public IActionResult Details(int id)
    {
        var book = _bookService.GetById(id);
        if (book is null)
            return NotFound($"Книга с ID={id} не найдена.");

        return View(book);
    }

    // GET: /Books/Create
    public IActionResult Create()
    {
        return View(new BookViewModel());
    }

    // POST: /Books/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BookViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var book = _bookService.Add(model);

        // Сообщение через TempData (сохраняется на одно перенаправление)
        TempData["SuccessMessage"] = $"Книга «{book.Title}» успешно добавлена!";

        return RedirectToAction(nameof(Index));
    }
}
