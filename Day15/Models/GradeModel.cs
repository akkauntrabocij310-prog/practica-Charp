namespace StudentDiary.Models
{
    public class GradeModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public int Value { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; } = string.Empty;

        public string GradeDisplay => $"{Value} — {Subject} ({Date:dd.MM.yyyy})";
    }
}
