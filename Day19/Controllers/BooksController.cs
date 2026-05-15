using Microsoft.AspNetCore.Mvc;
using BookLibrary.Models;

namespace BookLibrary.Controllers
{
    /// <summary>
    /// Контроллер для управления книгами библиотеки.
    /// Маршруты:
    ///   GET  /Books          → Index  — список всех книг
    ///   GET  /Books/Details/{id} → Details — подробная карточка книги
    ///   GET  /Books/Create   → Create (GET) — форма добавления
    ///   POST /Books/Create   → Create (POST) — сохранение новой книги
    ///   POST /Books/Delete/{id} → Delete — удаление книги
    /// </summary>
    public class BooksController : Controller
    {
        // Внедрение зависимости через DI — репозиторий зарегистрирован как Singleton
        private readonly BookRepository _repo;

        public BooksController(BookRepository repo)
        {
            _repo = repo;
        }

        // ─────────────────────────────────────────────────────────
        // GET /Books  →  список всех книг
        // ─────────────────────────────────────────────────────────
        [HttpGet]
        public IActionResult Index()
        {
            var books = _repo.GetAll();
            return View(books);
        }

        // ─────────────────────────────────────────────────────────
        // GET /Books/Details/{id}  →  карточка книги
        // ─────────────────────────────────────────────────────────
        [HttpGet]
        [Route("Books/Details/{id:int}")]
        public IActionResult Details(int id)
        {
            var book = _repo.GetById(id);

            if (book == null)
                return NotFound(); // 404 — книга не найдена

            return View(book);
        }

        // ─────────────────────────────────────────────────────────
        // GET /Books/Create  →  форма добавления книги
        // ─────────────────────────────────────────────────────────
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Book { Year = DateTime.Now.Year });
        }

        // ─────────────────────────────────────────────────────────
        // POST /Books/Create  →  сохранение новой книги
        // [ValidateAntiForgeryToken] защищает от CSRF-атак
        // ─────────────────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                // Форма заполнена с ошибками — возвращаем обратно с сообщениями
                return View(book);
            }

            int newId = _repo.Add(book);

            // После успешного сохранения — редирект на Details новой книги
            TempData["SuccessMessage"] = $"Книга «{book.Title}» успешно добавлена!";
            return RedirectToAction(nameof(Details), new { id = newId });
        }

        // ─────────────────────────────────────────────────────────
        // POST /Books/Delete/{id}  →  удаление книги
        // ─────────────────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Books/Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var book = _repo.GetById(id);
            if (book == null) return NotFound();

            _repo.Delete(id);
            TempData["SuccessMessage"] = $"Книга «{book.Title}» удалена из библиотеки.";
            return RedirectToAction(nameof(Index));
        }
    }
}
