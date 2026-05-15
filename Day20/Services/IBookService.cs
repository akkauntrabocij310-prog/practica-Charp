using BookLibrary.Models;
using BookLibrary.ViewModels;

namespace BookLibrary.Services;

/// <summary>
/// Интерфейс сервиса для работы с книгами
/// </summary>
public interface IBookService
{
    /// <summary>Получить список всех книг</summary>
    IEnumerable<Book> GetAll();

    /// <summary>Получить книгу по ID</summary>
    Book? GetById(int id);

    /// <summary>Добавить новую книгу из ViewModel</summary>
    Book Add(BookViewModel viewModel);
}
