using System;

namespace StudentDiaryWPF.Models
{
    public class GradeEntry
    {
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public int Grade { get; set; }
        public string Teacher { get; set; }
        public string Comment { get; set; }
    }

    public class ScheduleItem
    {
        public string Subject { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string Room { get; set; }
        public string Teacher { get; set; }
    }

    public class SubjectStatistics
    {
        public string Subject { get; set; }
        public int GradeCount { get; set; }
        public double AverageGrade { get; set; }
        public string GradesList { get; set; }
    }
}