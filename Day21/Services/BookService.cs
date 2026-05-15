using BookLibrary.Data;
using BookLibrary.Models;
using BookLibrary.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Services;

public interface IBookService
{
    Task<List<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task AddAsync(BookViewModel vm);
    Task UpdateAsync(BookViewModel vm);
    Task DeleteAsync(int id);
}

public class BookService(AppDbContext db) : IBookService
{
    public Task<List<Book>> GetAllAsync() => db.Books.OrderBy(b => b.Title).ToListAsync();

    public Task<Book?> GetByIdAsync(int id) => db.Books.FindAsync(id).AsTask();

    public async Task AddAsync(BookViewModel vm)
    {
        db.Books.Add(new Book
        {
            Title = vm.Title, Author = vm.Author,
            Genre = vm.Genre, Year = vm.Year, ISBN = vm.ISBN
        });
        await db.SaveChangesAsync();
    }

    public async Task UpdateAsync(BookViewModel vm)
    {
        var book = await db.Books.FindAsync(vm.Id) ?? throw new KeyNotFoundException();
        book.Title = vm.Title; book.Author = vm.Author;
        book.Genre = vm.Genre; book.Year = vm.Year; book.ISBN = vm.ISBN;
        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var book = await db.Books.FindAsync(id);
        if (book is not null) { db.Books.Remove(book); await db.SaveChangesAsync(); }
    }
}
