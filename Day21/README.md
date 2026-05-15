# BookLibrary — ASP.NET Core MVC (.NET 10)

## Структура проекта

```
BookLibrary/
├── Controllers/
│   └── BooksController.cs       # CRUD: Index, Details, Create, Edit, Delete
├── Data/
│   └── AppDbContext.cs          # EF Core DbContext + DbSet<Book>
├── Migrations/
│   ├── 20240101000000_CreateLibrary.cs   # Миграция CreateLibrary
│   └── AppDbContextModelSnapshot.cs
├── Models/
│   └── Book.cs                  # Id, Title, Author, Genre, Year, ISBN
├── Services/
│   └── BookService.cs           # IBookService + реализация (DI)
├── ViewModels/
│   └── BookViewModel.cs         # Валидация: Required + ISBN RegularExpression
├── Views/
│   ├── Books/
│   │   ├── Index.cshtml         # Таблица книг + кнопка "Добавить"
│   │   ├── Details.cshtml       # /Books/Details/{id}
│   │   ├── Create.cshtml        # Форма добавления
│   │   ├── Edit.cshtml          # Форма редактирования
│   │   ├── Delete.cshtml        # Подтверждение удаления
│   │   └── _BookForm.cshtml     # Частичное представление формы
│   ├── Shared/
│   │   ├── _Layout.cshtml
│   │   └── _ValidationScriptsPartial.cshtml
│   ├── _ViewImports.cshtml
│   └── _ViewStart.cshtml
├── appsettings.json
├── BookLibrary.csproj           # .NET 10, EF Core SQLite
└── Program.cs                   # DI: IBookService, DbContext, auto-migrate
```

## Запуск в Visual Studio 2022/2026

1. **Открыть** `BookLibrary.csproj`
2. **Запустить** F5 — миграция применится автоматически при старте

## Запуск через CLI

```bash
cd BookLibrary
dotnet run
```

Откроется: http://localhost:5000/Books

## Команды EF Core (если нужно пересоздать миграцию)

```bash
dotnet ef migrations add CreateLibrary
dotnet ef database update
```

## Маршруты

| Маршрут                  | Действие                  |
|--------------------------|---------------------------|
| GET  /Books              | Список всех книг          |
| GET  /Books/Details/{id} | Подробнее о книге         |
| GET  /Books/Create       | Форма добавления          |
| POST /Books/Create       | Сохранить новую книгу     |
| GET  /Books/Edit/{id}    | Форма редактирования      |
| POST /Books/Edit/{id}    | Сохранить изменения       |
| GET  /Books/Delete/{id}  | Подтверждение удаления    |
| POST /Books/Delete/{id}  | Удалить книгу             |

## Ключевые фичи

- **DI**: `IBookService` внедрён в `BooksController` через конструктор
- **TempData**: сообщения об успехе отображаются после редиректа
- **Валидация**: `[Required]` + `[RegularExpression]` для ISBN (978-X-XXX-XXXXX-X)
- **SQLite**: база `library.db` создаётся автоматически
- **Seed-данные**: 3 классические книги при первом запуске
