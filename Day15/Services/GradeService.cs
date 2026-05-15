using StudentDiary.Models;

namespace StudentDiary.Services
{
    /// <summary>
    /// Сервис для работы с оценками и студентами.
    /// Вся бизнес-логика вынесена сюда, вне ViewModel и View.
    /// </summary>
    public class GradeService
    {
        private readonly List<StudentModel> _students = new();
        private readonly List<GradeModel> _grades = new();
        private int _nextStudentId = 1;
        private int _nextGradeId = 1;

        /// <summary>
        /// Асинхронная загрузка студентов (имитация запроса к БД / API).
        /// </summary>
        public async Task<List<StudentModel>> LoadStudentsAsync()
        {
            // Имитация задержки сети / базы данных
            await Task.Delay(1500);

            _students.Clear();
            _students.AddRange(new[]
            {
                new StudentModel { Id = _nextStudentId++, FirstName = "Иван",    LastName = "Петров",   Group = "CS-301" },
                new StudentModel { Id = _nextStudentId++, FirstName = "Мария",   LastName = "Сидорова", Group = "CS-301" },
                new StudentModel { Id = _nextStudentId++, FirstName = "Алексей", LastName = "Козлов",   Group = "CS-302" },
                new StudentModel { Id = _nextStudentId++, FirstName = "Анна",    LastName = "Новикова", Group = "CS-302" },
                new StudentModel { Id = _nextStudentId++, FirstName = "Дмитрий", LastName = "Морозов",  Group = "CS-303" },
                new StudentModel { Id = _nextStudentId++, FirstName = "Ольга",   LastName = "Волкова",  Group = "CS-303" },
            });

            // Seed оценок
            _grades.Clear();
            _nextGradeId = 1;
            AddGrade(1, "Математика",   5, "Отлично");
            AddGrade(1, "Физика",       4, "Хорошо");
            AddGrade(2, "Математика",   3, "Удовлетворительно");
            AddGrade(2, "Программирование", 5, "Блестящая работа");
            AddGrade(3, "История",      4, "Хорошо");
            AddGrade(4, "Физика",       5, "Молодец");
            AddGrade(5, "Программирование", 2, "Необходимо доработать");

            return new List<StudentModel>(_students);
        }

        /// <summary>
        /// Получить оценки конкретного студента.
        /// </summary>
        public Task<List<GradeModel>> GetGradesAsync(int studentId)
        {
            var result = _grades.Where(g => g.StudentId == studentId).ToList();
            return Task.FromResult(result);
        }

        /// <summary>
        /// Добавить оценку студенту.
        /// </summary>
        public Task<GradeModel> AddGradeAsync(int studentId, string subject, int value, string comment)
        {
            if (value < 1 || value > 5)
                throw new ArgumentOutOfRangeException(nameof(value), "Оценка должна быть от 1 до 5.");
            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException("Предмет не может быть пустым.", nameof(subject));

            var grade = new GradeModel
            {
                Id = _nextGradeId++,
                StudentId = studentId,
                Subject = subject.Trim(),
                Value = value,
                Date = DateTime.Today,
                Comment = comment?.Trim() ?? string.Empty
            };
            _grades.Add(grade);
            return Task.FromResult(grade);
        }

        /// <summary>
        /// Удалить оценку по Id.
        /// </summary>
        public Task<bool> RemoveGradeAsync(int gradeId)
        {
            var grade = _grades.FirstOrDefault(g => g.Id == gradeId);
            if (grade == null) return Task.FromResult(false);
            _grades.Remove(grade);
            return Task.FromResult(true);
        }

        /// <summary>
        /// Средняя оценка студента.
        /// </summary>
        public double GetAverage(int studentId)
        {
            var studentGrades = _grades.Where(g => g.StudentId == studentId).ToList();
            return studentGrades.Count == 0 ? 0 : studentGrades.Average(g => g.Value);
        }

        // ─── вспомогательный метод ───────────────────────────────────────────
        private void AddGrade(int studentId, string subject, int value, string comment)
        {
            _grades.Add(new GradeModel
            {
                Id = _nextGradeId++,
                StudentId = studentId,
                Subject = subject,
                Value = value,
                Date = DateTime.Today.AddDays(-new Random().Next(0, 30)),
                Comment = comment
            });
        }
    }
}
