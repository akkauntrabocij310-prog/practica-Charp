using BookLibrary.Services;
using BookLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers;

public class BooksController(IBookService bookService) : Controller
{
    // GET /Books
    public async Task<IActionResult> Index()
    {
        var books = await bookService.GetAllAsync();
        return View(books);
    }

    // GET /Books/Details/{id}
    public async Task<IActionResult> Details(int id)
    {
        var book = await bookService.GetByIdAsync(id);
        return book is null ? NotFound() : View(book);
    }

    // GET /Books/Create
    public IActionResult Create() => View(new BookViewModel());

    // POST /Books/Create
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);
        await bookService.AddAsync(vm);
        TempData["Message"] = $"Книга «{vm.Title}» успешно добавлена!";
        return RedirectToAction(nameof(Index));
    }

    // GET /Books/Edit/{id}
    public async Task<IActionResult> Edit(int id)
    {
        var book = await bookService.GetByIdAsync(id);
        if (book is null) return NotFound();
        return View(new BookViewModel
        {
            Id = book.Id, Title = book.Title, Author = book.Author,
            Genre = book.Genre, Year = book.Year, ISBN = book.ISBN
        });
    }

    // POST /Books/Edit/{id}
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, BookViewModel vm)
    {
        if (id != vm.Id) return BadRequest();
        if (!ModelState.IsValid) return View(vm);
        await bookService.UpdateAsync(vm);
        TempData["Message"] = $"Книга «{vm.Title}» обновлена.";
        return RedirectToAction(nameof(Index));
    }

    // GET /Books/Delete/{id}
    public async Task<IActionResult> Delete(int id)
    {
        var book = await bookService.GetByIdAsync(id);
        return book is null ? NotFound() : View(book);
    }

    // POST /Books/Delete/{id}
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var book = await bookService.GetByIdAsync(id);
        var title = book?.Title ?? "Книга";
        await bookService.DeleteAsync(id);
        TempData["Message"] = $"«{title}» удалена из библиотеки.";
        return RedirectToAction(nameof(Index));
    }
}
