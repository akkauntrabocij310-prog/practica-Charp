using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using GradeBook.Commands;
using GradeBook.Models;

namespace GradeBook.Views
{
    public partial class GradeDialog : Window
    {
        public Grade Grade => (GradeDialogViewModel)DataContext != null
            ? ((GradeDialogViewModel)DataContext).ToGrade()
            : null;

        public GradeDialog(string title, Grade existing = null)
        {
            InitializeComponent();
            DataContext = new GradeDialogViewModel(title, existing, () =>
            {
                DialogResult = true;
                Close();
            });
        }
    }

    public class GradeDialogViewModel : INotifyPropertyChanged
    {
        private readonly Grade _original;
        private string _studentName;
        private string _subject;
        private int _value = 5;
        private DateTime _date = DateTime.Today;

        public string Title { get; }

        public string StudentName
        {
            get => _studentName;
            set { _studentName = value; OnPropertyChanged(nameof(StudentName)); OkCommand.RaiseCanExecuteChanged(); }
        }

        public string Subject
        {
            get => _subject;
            set { _subject = value; OnPropertyChanged(nameof(Subject)); OkCommand.RaiseCanExecuteChanged(); }
        }

        public int Value
        {
            get => _value;
            set { _value = value; OnPropertyChanged(nameof(Value)); }
        }

        public DateTime Date
        {
            get => _date;
            set { _date = value; OnPropertyChanged(nameof(Date)); }
        }

        public RelayCommand OkCommand { get; }

        public GradeDialogViewModel(string title, Grade existing, Action onOk)
        {
            Title = title;
            _original = existing;

            if (existing != null)
            {
                StudentName = existing.StudentName;
                Subject = existing.Subject;
                Value = existing.Value;
                Date = existing.Date;
            }
            else
            {
                Date = DateTime.Today;
            }

            OkCommand = new RelayCommand(
                _ => onOk?.Invoke(),
                _ => !string.IsNullOrWhiteSpace(StudentName) && !string.IsNullOrWhiteSpace(Subject));
        }

        public Grade ToGrade() => new Grade
        {
            Id = _original?.Id ?? 0,
            StudentName = StudentName?.Trim(),
            Subject = Subject?.Trim(),
            Value = Value,
            Date = Date
        };

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
