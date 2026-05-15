namespace BookLibrary.Models
{
    /// <summary>
    /// In-memory репозиторий книг — заменяет БД для демонстрации.
    /// В реальном проекте заменяется на EF Core DbContext.
    /// </summary>
    public class BookRepository
    {
        private static readonly List<Book> _books = new()
        {
            new Book { Id = 1, Title = "Мастер и Маргарита",   Author = "Михаил Булгаков",    Year = 1967, Genre = "Роман",          Description = "Философский роман о добре и зле, свете и тьме, написанный в жанре магического реализма." },
            new Book { Id = 2, Title = "Преступление и наказание", Author = "Фёдор Достоевский", Year = 1866, Genre = "Психологический роман", Description = "Роман о моральных и психологических последствиях убийства, совершённого студентом Раскольниковым." },
            new Book { Id = 3, Title = "Война и мир",           Author = "Лев Толстой",        Year = 1869, Genre = "Эпопея",          Description = "Масштабное произведение о жизни русского общества в эпоху наполеоновских войн." },
            new Book { Id = 4, Title = "Сто лет одиночества",   Author = "Габриэль Гарсиа Маркес", Year = 1967, Genre = "Магический реализм", Description = "Хроника семьи Буэндиа на протяжении семи поколений в вымышленном городе Макондо." },
            new Book { Id = 5, Title = "1984",                  Author = "Джордж Оруэлл",      Year = 1949, Genre = "Антиутопия",      Description = "Роман о тоталитарном обществе, где Большой Брат следит за каждым гражданином." },
            new Book { Id = 6, Title = "Гарри Поттер и философский камень", Author = "Джоан Роулинг", Year = 1997, Genre = "Фэнтези", Description = "История мальчика-волшебника, который узнаёт о своём особом предназначении." },
        };

        private static int _nextId = 7;
        private static readonly object _lock = new();

        /// <summary>Получить все книги</summary>
        public IReadOnlyList<Book> GetAll() => _books.AsReadOnly();

        /// <summary>Получить книгу по Id (null если не найдена)</summary>
        public Book? GetById(int id) => _books.FirstOrDefault(b => b.Id == id);

        /// <summary>Добавить новую книгу; возвращает присвоенный Id</summary>
        public int Add(Book book)
        {
            lock (_lock)
            {
                book.Id = _nextId++;
                _books.Add(book);
                return book.Id;
            }
        }

        /// <summary>Удалить книгу по Id</summary>
        public bool Delete(int id)
        {
            var book = GetById(id);
            if (book == null) return false;
            _books.Remove(book);
            return true;
        }
    }
}
