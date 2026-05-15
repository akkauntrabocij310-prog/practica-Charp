using System;
using System.ComponentModel;

namespace GradeBook.Models
{
    public class Grade : INotifyPropertyChanged
    {
        private string _studentName;
        private string _subject;
        private int _value;
        private DateTime _date;

        public int Id { get; set; }

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public Grade Clone() => new Grade
        {
            Id = Id,
            StudentName = StudentName,
            Subject = Subject,
            Value = Value,
            Date = Date
        };
    }
}
