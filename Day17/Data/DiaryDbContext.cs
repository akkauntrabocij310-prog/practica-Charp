using ElectronicDiary.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronicDiary.Data;

public class DiaryDbContext : DbContext
{
    public DbSet<GradeModel> Grades => Set<GradeModel>();

    public DiaryDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=diary.db");
    }
}