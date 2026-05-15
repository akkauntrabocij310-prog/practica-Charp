using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using StudentDiaryFull.Models;

namespace StudentDiaryFull.Services
{
    /// <summary>
    /// Сервис хранения данных в diary.json и users.json.
    /// </summary>
    public static class DataService
    {
        private static readonly string BaseDir =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

        public static readonly string DiaryPath = Path.Combine(BaseDir, "diary.json");
        public static readonly string UsersPath  = Path.Combine(BaseDir, "users.json");

        private static readonly JsonSerializerOptions Opts = new()
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        // ─── Инициализация ───────────────────────────────────────────────────
        public static void Initialize()
        {
            Directory.CreateDirectory(BaseDir);
            if (!File.Exists(DiaryPath)) SeedDiary();
            if (!File.Exists(UsersPath))  SeedUsers();
        }

        // ─── Загрузка / сохранение Diary ────────────────────────────────────
        public static DiaryData LoadDiary()
        {
            var json = File.ReadAllText(DiaryPath, Encoding.UTF8);
            return JsonSerializer.Deserialize<DiaryData>(json, Opts) ?? new DiaryData();
        }

        public static void SaveDiary(DiaryData data)
        {
            var json = JsonSerializer.Serialize(data, Opts);
            File.WriteAllText(DiaryPath, json, Encoding.UTF8);
        }

        // ─── Загрузка / сохранение Users ────────────────────────────────────
        public static UsersData LoadUsers()
        {
            var json = File.ReadAllText(UsersPath, Encoding.UTF8);
            return JsonSerializer.Deserialize<UsersData>(json, Opts) ?? new UsersData();
        }

        public static void SaveUsers(UsersData data)
        {
            var json = JsonSerializer.Serialize(data, Opts);
            File.WriteAllText(UsersPath, json, Encoding.UTF8);
        }

        // ─── Аутентификация ──────────────────────────────────────────────────
        public static UserModel? Authenticate(string login, string password)
        {
            var users = LoadUsers();
            var hash  = Hash(password);
            return users.Users.FirstOrDefault(u =>
                u.Login.Equals(login, StringComparison.OrdinalIgnoreCase) &&
                u.PasswordHash == hash);
        }

        public static string Hash(string s)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(s));
            return Convert.ToHexString(bytes);
        }

        // ─── Начальные данные Diary ──────────────────────────────────────────
        private static void SeedDiary()
        {
            var rnd = new Random(42);
            var data = new DiaryData();

            // Студенты
            data.Students.AddRange(new[]
            {
                new StudentModel { Id=1, FirstName="Иван",    LastName="Петров",   Group="CS-301" },
                new StudentModel { Id=2, FirstName="Мария",   LastName="Сидорова", Group="CS-301" },
                new StudentModel { Id=3, FirstName="Алексей", LastName="Козлов",   Group="CS-302" },
                new StudentModel { Id=4, FirstName="Анна",    LastName="Новикова", Group="CS-302" },
                new StudentModel { Id=5, FirstName="Дмитрий", LastName="Морозов",  Group="CS-303" },
                new StudentModel { Id=6, FirstName="Ольга",   LastName="Волкова",  Group="CS-303" },
            });

            // Оценки
            int gid = 1;
            void AddGrade(int sid, string subj, int val, string cmt)
                => data.Grades.Add(new GradeModel
                {
                    Id=gid++, StudentId=sid, Subject=subj, Value=val,
                    Date=DateTime.Today.AddDays(-rnd.Next(1,30)), Comment=cmt
                });

            AddGrade(1, "Математика",       5, "Отлично");
            AddGrade(1, "Физика",           4, "Хорошо");
            AddGrade(1, "Программирование", 5, "Блестяще");
            AddGrade(2, "Математика",       3, "Удовлетворительно");
            AddGrade(2, "Программирование", 5, "Молодец");
            AddGrade(3, "История",          4, "Хорошо");
            AddGrade(3, "Физика",           3, "Нужно повторить");
            AddGrade(4, "Физика",           5, "Отлично");
            AddGrade(5, "Программирование", 2, "Нужно доработать");
            AddGrade(6, "Математика",       4, "Хорошо");

            // Домашние задания
            int hid = 1;
            void AddHw(string group, string subj, string desc, int daysAhead)
                => data.Homework.Add(new HomeworkModel
                {
                    Id=hid++, Group=group, Subject=subj, Description=desc,
                    DueDate=DateTime.Today.AddDays(daysAhead), IsCompleted=false
                });

            AddHw("CS-301", "Математика",       "Решить задачи 5.1–5.10 из учебника",     3);
            AddHw("CS-301", "Программирование", "Реализовать связный список на C#",         5);
            AddHw("CS-302", "Физика",           "Написать реферат по теме «Механика»",      7);
            AddHw("CS-302", "История",          "Подготовить доклад о Второй мировой войне",2);
            AddHw("CS-303", "Программирование", "Написать unit-тесты для своего проекта",   4);
            AddHw("CS-303", "Математика",       "Интегралы: задачи 12-20",                  6);

            // Расписание
            int sid2 = 1;
            void AddSched(string group, DayOfWeek day, string subj, string teacher, string room, string ts, string te)
                => data.Schedule.Add(new ScheduleItemModel
                {
                    Id=sid2++, Group=group, DayOfWeek=day, Subject=subj,
                    Teacher=teacher, Room=room, TimeStart=ts, TimeEnd=te
                });

            // CS-301
            AddSched("CS-301", DayOfWeek.Monday,    "Математика",       "Иванов А.А.",  "А-101", "09:00","10:30");
            AddSched("CS-301", DayOfWeek.Monday,    "Физика",           "Смирнов В.В.", "Б-201", "10:45","12:15");
            AddSched("CS-301", DayOfWeek.Wednesday, "Программирование", "Козлов С.С.",  "В-301", "09:00","10:30");
            AddSched("CS-301", DayOfWeek.Friday,    "История",          "Попова Н.Н.",  "А-102", "11:00","12:30");

            // CS-302
            AddSched("CS-302", DayOfWeek.Tuesday,   "Физика",           "Смирнов В.В.", "Б-202", "09:00","10:30");
            AddSched("CS-302", DayOfWeek.Tuesday,   "История",          "Попова Н.Н.",  "А-103", "10:45","12:15");
            AddSched("CS-302", DayOfWeek.Thursday,  "Программирование", "Козлов С.С.",  "В-302", "09:00","10:30");
            AddSched("CS-302", DayOfWeek.Thursday,  "Математика",       "Иванов А.А.",  "А-104", "10:45","12:15");

            // CS-303
            AddSched("CS-303", DayOfWeek.Monday,    "Программирование", "Козлов С.С.",  "В-303", "13:00","14:30");
            AddSched("CS-303", DayOfWeek.Wednesday, "Математика",       "Иванов А.А.",  "А-105", "09:00","10:30");
            AddSched("CS-303", DayOfWeek.Friday,    "Физика",           "Смирнов В.В.", "Б-203", "09:00","10:30");

            SaveDiary(data);
        }

        // ─── Начальные данные Users ──────────────────────────────────────────
        private static void SeedUsers()
        {
            var data = new UsersData();
            data.Users.AddRange(new[]
            {
                new UserModel { Id=1, Login="teacher",  PasswordHash=Hash("teacher123"),
                                FullName="Преподаватель",  Role=UserRole.Teacher },
                new UserModel { Id=2, Login="petrov",   PasswordHash=Hash("1234"),
                                FullName="Иван Петров",    Role=UserRole.Student, StudentId=1 },
                new UserModel { Id=3, Login="sidorova", PasswordHash=Hash("1234"),
                                FullName="Мария Сидорова", Role=UserRole.Student, StudentId=2 },
                new UserModel { Id=4, Login="kozlov",   PasswordHash=Hash("1234"),
                                FullName="Алексей Козлов", Role=UserRole.Student, StudentId=3 },
                new UserModel { Id=5, Login="novikova", PasswordHash=Hash("1234"),
                                FullName="Анна Новикова",  Role=UserRole.Student, StudentId=4 },
                new UserModel { Id=6, Login="morozov",  PasswordHash=Hash("1234"),
                                FullName="Дмитрий Морозов",Role=UserRole.Student, StudentId=5 },
                new UserModel { Id=7, Login="volkova",  PasswordHash=Hash("1234"),
                                FullName="Ольга Волкова",  Role=UserRole.Student, StudentId=6 },
            });
            SaveUsers(data);
        }
    }
}
