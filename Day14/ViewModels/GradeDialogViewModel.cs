using System;
using System.ComponentModel;
using System.Windows.Input;
using StudentDiary.Commands;

namespace StudentDiary.ViewModels
{
    public class GradeDialogViewModel : INotifyPropertyChanged
    {
        private string _studentName;
        private string _subject;
        private double _grade = 3.0;
        private string _date;
        private string _comment;
        private bool _isEdit;

        public string StudentName
        {
            get => _studentName;
            set { _studentName = value; OnPropertyChanged(nameof(StudentName)); }
        }

        public string Subject
        {
            get => _subject;
            set { _subject = value; OnPropertyChanged(nameof(Subject)); }
        }

        public double Grade
        {
            get => _grade;
            set { _grade = value; OnPropertyChanged(nameof(Grade)); }
        }

        public string Date
        {
            get => _date;
            set { _date = value; OnPropertyChanged(nameof(Date)); }
        }

        public string Comment
        {
            get => _comment;
            set { _comment = value; OnPropertyChanged(nameof(Comment)); }
        }

        public bool IsEdit
        {
            get => _isEdit;
            set { _isEdit = value; OnPropertyChanged(nameof(IsEdit)); OnPropertyChanged(nameof(DialogTitle)); }
        }

        public string DialogTitle => IsEdit ? "Редактировать оценку" : "Добавить оценку";

        public GradeDialogViewModel()
        {
            Date = DateTime.Now.ToString("yyyy-MM-dd");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
