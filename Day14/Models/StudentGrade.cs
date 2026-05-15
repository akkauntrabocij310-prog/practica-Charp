using System.ComponentModel;

namespace StudentDiary.Models
{
    public class StudentGrade : INotifyPropertyChanged
    {
        private int _id;
        private string _studentName;
        private string _subject;
        private double _grade;
        private string _date;
        private string _comment;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
