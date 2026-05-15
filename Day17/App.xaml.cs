using ElectronicDiary.Data;

namespace ElectronicDiary;

public partial class App : System.Windows.Application
{
    public App()
    {
        using var db = new DiaryDbContext();
        db.Database.EnsureCreated();
    }
}