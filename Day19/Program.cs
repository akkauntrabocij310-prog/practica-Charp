using BookLibrary.Models;

var builder = WebApplication.CreateBuilder(args);

// ── Сервисы ───────────────────────────────────────────────────────────────
builder.Services.AddControllersWithViews();

// BookRepository регистрируем как Singleton —
// один экземпляр на всё время жизни приложения (in-memory хранилище)
builder.Services.AddSingleton<BookRepository>();

var app = builder.Build();

// ── Middleware ────────────────────────────────────────────────────────────
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// ── Маршруты ─────────────────────────────────────────────────────────────
// Маршрут по умолчанию: /Controller/Action/{id}
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}");

// Явный именованный маршрут для Details: /Books/Details/{id}
app.MapControllerRoute(
    name: "bookDetails",
    pattern: "Books/Details/{id:int}",
    defaults: new { controller = "Books", action = "Details" });

app.Run();
