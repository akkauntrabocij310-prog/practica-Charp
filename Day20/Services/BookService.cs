using BookLibrary.Models;
using BookLibrary.ViewModels;

namespace BookLibrary.Services;

/// <summary>
/// In-memory реализация сервиса книг (для демонстрации — без БД)
/// </summary>
public class BookService : IBookService
{
    private readonly List<Book> _books;
    private int _nextId = 4;

    public BookService()
    {
        // Начальные данные
        _books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Мастер и Маргарита",
                Author = "Михаил Булгаков",
                Year = 1967,
                ISBN = "978-5-17-083086-4",
                Genre = "Роман"
            },
            new Book
            {
                Id = 2,
                Title = "Преступление и наказание",
                Author = "Фёдор Достоевский",
                Year = 1866,
                ISBN = "978-5-04-116640-1",
                Genre = "Классика"
            },
            new Book
            {
                Id = 3,
                Title = "1984",
                Author = "Джордж Оруэлл",
                Year = 1949,
                ISBN = "978-5-17-119392-2",
                Genre = "Антиутопия"
            }
        };
    }

    public IEnumerable<Book> GetAll() => _books.AsReadOnly();

    public Book? GetById(int id) =>
        _books.FirstOrDefault(b => b.Id == id);

    public Book Add(BookViewModel viewModel)
    {
        var book = new Book
        {
            Id = _nextId++,
            Title = viewModel.Title,
            Author = viewModel.Author,
            Year = viewModel.Year,
            ISBN = viewModel.ISBN,
            Genre = viewModel.Genre
        };
        _books.Add(book);
        return book;
    }
}
