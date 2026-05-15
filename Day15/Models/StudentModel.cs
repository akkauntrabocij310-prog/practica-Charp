namespace StudentDiary.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public string FullName => $"{LastName} {FirstName}";
    }
}
