using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GradeBook.Commands;
using GradeBook.Models;

namespace GradeBook.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Grade _selectedGrade;
        private static int _nextId = 1;

        public ObservableCollection<Grade> Grades { get; } = new ObservableCollection<Grade>();

        public Grade SelectedGrade
        {
            get => _selectedGrade;
            set
            {
                _selectedGrade = value;
                OnPropertyChanged(nameof(SelectedGrade));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ICommand AddGradeCommand { get; }
        public ICommand EditGradeCommand { get; }
        public ICommand DeleteGradeCommand { get; }
        public ICommand ExitCommand { get; }

        public MainViewModel()
        {
            AddGradeCommand = new AddGradeCommand(grade =>
            {
                grade.Id = _nextId++;
                Grades.Add(grade);
                SelectedGrade = grade;
            });

            EditGradeCommand = new EditGradeCommand(
                () => SelectedGrade,
                updatedGrade =>
                {
                    var existing = Grades.FirstOrDefault(g => g.Id == updatedGrade.Id);
                    if (existing != null)
                    {
                        existing.StudentName = updatedGrade.StudentName;
                        existing.Subject = updatedGrade.Subject;
                        existing.Value = updatedGrade.Value;
                        existing.Date = updatedGrade.Date;
                    }
                });

            DeleteGradeCommand = new DeleteGradeCommand(
                () => SelectedGrade,
                grade =>
                {
                    Grades.Remove(grade);
                    SelectedGrade = null;
                });

            ExitCommand = new RelayCommand(_ => System.Windows.Application.Current.Shutdown());

            // Тестовые данные
            Grades.Add(new Grade { Id = _nextId++, StudentName = "Иванов Иван", Subject = "Математика", Value = 5, Date = System.DateTime.Today });
            Grades.Add(new Grade { Id = _nextId++, StudentName = "Петрова Мария", Subject = "Физика", Value = 4, Date = System.DateTime.Today });
            Grades.Add(new Grade { Id = _nextId++, StudentName = "Сидоров Алексей", Subject = "История", Value = 3, Date = System.DateTime.Today });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
