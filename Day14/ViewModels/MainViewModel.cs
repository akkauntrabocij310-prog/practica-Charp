using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using StudentDiary.Commands;
using StudentDiary.Models;
using StudentDiary.Views;

namespace StudentDiary.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<StudentGrade> _grades;
        private StudentGrade _selectedGrade;
        private double _averageGrade;
        private string _filterText;
        private int _nextId = 1;

        // ── Commands ──────────────────────────────────────────────
        public ICommand AddGradeCommand { get; }
        public ICommand EditGradeCommand { get; }
        public ICommand DeleteGradeCommand { get; }
        public ICommand RefreshCommand { get; }

        // ── Collections ───────────────────────────────────────────
        public ObservableCollection<StudentGrade> Grades
        {
            get => _grades;
            set { _grades = value; OnPropertyChanged(nameof(Grades)); }
        }

        // ── Properties ────────────────────────────────────────────
        public StudentGrade SelectedGrade
        {
            get => _selectedGrade;
            set { _selectedGrade = value; OnPropertyChanged(nameof(SelectedGrade)); }
        }

        // OneWay binding — среднее значение
        public double AverageGrade
        {
            get => _averageGrade;
            private set { _averageGrade = value; OnPropertyChanged(nameof(AverageGrade)); }
        }

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));
                ApplyFilter();
            }
        }

        public int TotalGrades => Grades?.Count ?? 0;
        public int StudentCount => Grades?.Select(g => g.StudentName).Distinct().Count() ?? 0;

        // ── Constructor ───────────────────────────────────────────
        public MainViewModel()
        {
            _grades = new ObservableCollection<StudentGrade>();
            _grades.CollectionChanged += (s, e) => RecalculateStats();

            AddGradeCommand = new AddGradeCommand(this);
            EditGradeCommand = new EditGradeCommand(this);
            DeleteGradeCommand = new DeleteGradeCommand(this);
            RefreshCommand = new RelayCommand(_ => RecalculateStats());

            LoadSampleData();
        }

        // ── Public Methods (called by Commands) ───────────────────
        public void OpenAddGradeDialog()
        {
            var vm = new GradeDialogViewModel();
            var dlg = new GradeDialog { DataContext = vm, Owner = Application.Current.MainWindow };
            if (dlg.ShowDialog() == true)
            {
                var grade = new StudentGrade
                {
                    Id = _nextId++,
                    StudentName = vm.StudentName,
                    Subject = vm.Subject,
                    Grade = vm.Grade,
                    Date = vm.Date,
                    Comment = vm.Comment
                };
                Grades.Add(grade);
                RecalculateStats();
            }
        }

        public void OpenEditGradeDialog()
        {
            if (SelectedGrade == null) return;

            var vm = new GradeDialogViewModel
            {
                StudentName = SelectedGrade.StudentName,
                Subject = SelectedGrade.Subject,
                Grade = SelectedGrade.Grade,
                Date = SelectedGrade.Date,
                Comment = SelectedGrade.Comment,
                IsEdit = true
            };

            var dlg = new GradeDialog { DataContext = vm, Owner = Application.Current.MainWindow };
            if (dlg.ShowDialog() == true)
            {
                SelectedGrade.StudentName = vm.StudentName;
                SelectedGrade.Subject = vm.Subject;
                SelectedGrade.Grade = vm.Grade;
                SelectedGrade.Date = vm.Date;
                SelectedGrade.Comment = vm.Comment;
                RecalculateStats();
            }
        }

        public void DeleteSelectedGrade()
        {
            if (SelectedGrade == null) return;

            var result = MessageBox.Show(
                $"Удалить оценку студента «{SelectedGrade.StudentName}»\nПредмет: {SelectedGrade.Subject} — {SelectedGrade.Grade}?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Grades.Remove(SelectedGrade);
                SelectedGrade = null;
                RecalculateStats();
            }
        }

        // ── Private ───────────────────────────────────────────────
        private void RecalculateStats()
        {
            AverageGrade = Grades.Any() ? Math.Round(Grades.Average(g => g.Grade), 2) : 0;
            OnPropertyChanged(nameof(TotalGrades));
            OnPropertyChanged(nameof(StudentCount));
        }

        private void ApplyFilter()
        {
            // Filter is applied via CollectionViewSource in XAML; trigger refresh
            OnPropertyChanged(nameof(Grades));
        }

        private void LoadSampleData()
        {
            var samples = new[]
            {
                new StudentGrade { Id=_nextId++, StudentName="Иванов Алексей",  Subject="Математика",   Grade=4.8, Date="2025-05-10", Comment="Отлично" },
                new StudentGrade { Id=_nextId++, StudentName="Петрова Мария",   Subject="Физика",       Grade=3.5, Date="2025-05-11", Comment="Хорошо" },
                new StudentGrade { Id=_nextId++, StudentName="Сидоров Дмитрий", Subject="Информатика",  Grade=5.0, Date="2025-05-12", Comment="Превосходно" },
                new StudentGrade { Id=_nextId++, StudentName="Козлова Анна",    Subject="История",      Grade=2.8, Date="2025-05-13", Comment="Нужно подтянуть" },
                new StudentGrade { Id=_nextId++, StudentName="Новиков Кирилл",  Subject="Химия",        Grade=4.2, Date="2025-05-14", Comment="Хорошая работа" },
                new StudentGrade { Id=_nextId++, StudentName="Иванов Алексей",  Subject="Физика",       Grade=3.9, Date="2025-05-15", Comment="Неплохо" },
            };
            foreach (var s in samples) Grades.Add(s);
            RecalculateStats();
        }

        // ── INotifyPropertyChanged ─────────────────────────────────
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
