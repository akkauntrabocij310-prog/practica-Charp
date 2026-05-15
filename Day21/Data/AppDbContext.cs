using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Мастер и Маргарита", Author = "Булгаков М.А.", Genre = "Роман", Year = 1967, ISBN = "978-5-389-00001-7" },
            new Book { Id = 2, Title = "Преступление и наказание", Author = "Достоевский Ф.М.", Genre = "Роман", Year = 1866, ISBN = "978-5-389-00002-4" },
            new Book { Id = 3, Title = "Война и мир", Author = "Толстой Л.Н.", Genre = "Роман-эпопея", Year = 1869, ISBN = "978-5-389-00003-1" }
        );
    }
}
