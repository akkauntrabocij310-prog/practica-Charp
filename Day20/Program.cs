using BookLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

// ─── Регистрация сервисов ───────────────────────────────────────────────────
builder.Services.AddControllersWithViews();

// DI: регистрируем IBookService → BookService как Singleton
// (данные живут всё время работы приложения, т.к. хранятся in-memory)
builder.Services.AddSingleton<IBookService, BookService>();

// ─── Построение приложения ──────────────────────────────────────────────────
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Маршруты MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}");

app.Run();
