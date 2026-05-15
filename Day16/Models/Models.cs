using System;
using System.Collections.Generic;

namespace StudentDiaryFull.Models
{
    // ─── Роли пользователей ──────────────────────────────────────────────────
    public enum UserRole { Student, Teacher }

    // ─── Пользователь ────────────────────────────────────────────────────────
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public int? StudentId { get; set; }   // только для роли Student
    }

    // ─── Студент ─────────────────────────────────────────────────────────────
    public class StudentModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public string FullName => $"{LastName} {FirstName}";
    }

    // ─── Оценка ──────────────────────────────────────────────────────────────
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

    // ─── Домашнее задание ────────────────────────────────────────────────────
    public class HomeworkModel
    {
        public int Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public string DueDateDisplay => DueDate.ToString("dd.MM.yyyy");
    }

    // ─── Занятие в расписании ────────────────────────────────────────────────
    public class ScheduleItemModel
    {
        public int Id { get; set; }
        public string Group { get; set; } = string.Empty;
        public DayOfWeek DayOfWeek { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Teacher { get; set; } = string.Empty;
        public string Room { get; set; } = string.Empty;
        public string TimeStart { get; set; } = string.Empty;  // "09:00"
        public string TimeEnd { get; set; } = string.Empty;    // "10:30"
        public string DayName => DayOfWeek switch
        {
            DayOfWeek.Monday    => "Понедельник",
            DayOfWeek.Tuesday   => "Вторник",
            DayOfWeek.Wednesday => "Среда",
            DayOfWeek.Thursday  => "Четверг",
            DayOfWeek.Friday    => "Пятница",
            DayOfWeek.Saturday  => "Суббота",
            _                   => "Воскресенье"
        };
    }

    // ─── Сообщение чата ──────────────────────────────────────────────────────
    public class ChatMessage
    {
        public string Sender { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Display => $"[{Timestamp:HH:mm}] {Sender}: {Text}";
    }

    // ─── Корень JSON-хранилища ───────────────────────────────────────────────
    public class DiaryData
    {
        public List<StudentModel>      Students  { get; set; } = new();
        public List<GradeModel>        Grades    { get; set; } = new();
        public List<HomeworkModel>     Homework  { get; set; } = new();
        public List<ScheduleItemModel> Schedule  { get; set; } = new();
    }

    public class UsersData
    {
        public List<UserModel> Users { get; set; } = new();
    }
}
