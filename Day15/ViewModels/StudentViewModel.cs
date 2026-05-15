using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using StudentDiary.Commands;
using StudentDiary.Models;
using StudentDiary.Services;

namespace StudentDiary.ViewModels
{
    /// <summary>
    /// Главный ViewModel. Содержит список студентов, их оценки
    /// и все команды для работы с дневником.
    /// </summary>
    public class StudentViewModel : BaseViewModel
    {
        private readonly GradeService _gradeService;

        // ─── Студенты ────────────────────────────────────────────────────────
        public ObservableCollection<StudentModel> Students { get; } = new();

        private StudentModel? _selectedStudent;
        public StudentModel? SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                if (SetProperty(ref _selectedStudent, value))
                    _ = LoadGradesAsync();   // асинхронная загрузка оценок при смене студента
            }
        }

        // ─── Оценки ──────────────────────────────────────────────────────────
        public ObservableCollection<GradeModel> Grades { get; } = new();

        private GradeModel? _selectedGrade;
        public GradeModel? SelectedGrade
        {
            get => _selectedGrade;
            set => SetProperty(ref _selectedGrade, value);
        }

        // ─── Форма добавления оценки ─────────────────────────────────────────
        private string _newSubject = string.Empty;
        public string NewSubject
        {
            get => _newSubject;
            set => SetProperty(ref _newSubject, value);
        }

        private int _newGradeValue = 5;
        public int NewGradeValue
        {
            get => _newGradeValue;
            set => SetProperty(ref _newGradeValue, value);
        }

        private string _newComment = string.Empty;
        public string NewComment
        {
            get => _newComment;
            set => SetProperty(ref _newComment, value);
        }

        // ─── Состояние UI ────────────────────────────────────────────────────
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private string _statusMessage = "Готов к работе";
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        private double _averageGrade;
        public double AverageGrade
        {
            get => _averageGrade;
            set => SetProperty(ref _averageGrade, value);
        }

        // ─── Доступные оценки (1..5) для ComboBox ───────────────────────────
        public IReadOnlyList<int> AvailableGrades { get; } = new[] { 1, 2, 3, 4, 5 };

        // ─── Команды ─────────────────────────────────────────────────────────
        public ICommand LoadStudentsCommand { get; }
        public ICommand AddGradeCommand { get; }
        public ICommand RemoveGradeCommand { get; }

        // ─── Конструктор ─────────────────────────────────────────────────────
        public StudentViewModel()
        {
            _gradeService = new GradeService();

            LoadStudentsCommand = new AsyncRelayCommand(_ => LoadStudentsAsync());
            AddGradeCommand     = new AsyncRelayCommand(_ => AddGradeAsync(), _ => CanAddGrade());
            RemoveGradeCommand  = new AsyncRelayCommand(_ => RemoveGradeAsync(), _ => SelectedGrade != null);

            // Автоматически загружаем студентов при создании ViewModel
            _ = LoadStudentsAsync();
        }

        // ─── Загрузка студентов (async) ──────────────────────────────────────
        private async Task LoadStudentsAsync()
        {
            IsLoading = true;
            StatusMessage = "Загрузка списка студентов…";
            Students.Clear();
            Grades.Clear();

            try
            {
                var students = await _gradeService.LoadStudentsAsync();
                foreach (var s in students)
                    Students.Add(s);

                StatusMessage = $"Загружено студентов: {Students.Count}";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Ошибка загрузки: {ex.Message}";
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        // ─── Загрузка оценок выбранного студента ─────────────────────────────
        private async Task LoadGradesAsync()
        {
            Grades.Clear();
            AverageGrade = 0;

            if (SelectedStudent == null) return;

            var grades = await _gradeService.GetGradesAsync(SelectedStudent.Id);
            foreach (var g in grades)
                Grades.Add(g);

            AverageGrade = _gradeService.GetAverage(SelectedStudent.Id);
            StatusMessage = $"Оценок студента {SelectedStudent.FullName}: {Grades.Count}";
        }

        // ─── Добавить оценку ─────────────────────────────────────────────────
        private async Task AddGradeAsync()
        {
            if (SelectedStudent == null) return;

            try
            {
                var grade = await _gradeService.AddGradeAsync(
                    SelectedStudent.Id, NewSubject, NewGradeValue, NewComment);

                Grades.Add(grade);
                AverageGrade = _gradeService.GetAverage(SelectedStudent.Id);
                StatusMessage = $"Оценка добавлена: {grade.GradeDisplay}";

                // Очистить поля
                NewSubject = string.Empty;
                NewComment = string.Empty;
                NewGradeValue = 5;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Ошибка: {ex.Message}";
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool CanAddGrade()
            => SelectedStudent != null && !string.IsNullOrWhiteSpace(NewSubject);

        // ─── Удалить оценку ──────────────────────────────────────────────────
        private async Task RemoveGradeAsync()
        {
            if (SelectedGrade == null) return;

            var confirm = MessageBox.Show(
                $"Удалить оценку «{SelectedGrade.GradeDisplay}»?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirm != MessageBoxResult.Yes) return;

            var removed = await _gradeService.RemoveGradeAsync(SelectedGrade.Id);
            if (removed)
            {
                Grades.Remove(SelectedGrade);
                SelectedGrade = null;
                AverageGrade = SelectedStudent != null
                    ? _gradeService.GetAverage(SelectedStudent.Id) : 0;
                StatusMessage = "Оценка удалена.";
            }
        }
    }
}
