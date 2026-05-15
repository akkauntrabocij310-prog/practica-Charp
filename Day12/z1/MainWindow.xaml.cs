using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using StudentDiaryWPF.Models;

namespace StudentDiaryWPF
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<GradeEntry> allGrades;
        private ObservableCollection<GradeEntry> filteredGrades;

        public MainWindow()
        {
            InitializeComponent();

            allGrades = new ObservableCollection<GradeEntry>();
            filteredGrades = new ObservableCollection<GradeEntry>();
            GradesGrid.ItemsSource = filteredGrades;

            GradeDatePicker.SelectedDate = DateTime.Today;
            StartDatePicker.SelectedDate = DateTime.Today.AddMonths(-1);
            EndDatePicker.SelectedDate = DateTime.Today;

            LoadTestData();
            LoadSchedule();
            LoadSubjects();
            ApplyFilter();
            UpdateStatistics();
        }

        private void LoadTestData()
        {
            allGrades.Add(new GradeEntry
            {
                Subject = "Математика",
                Date = DateTime.Now.AddDays(-5),
                Grade = 5,
                Teacher = "Иванова М.А.",
                Comment = "Отличная работа"
            });
            allGrades.Add(new GradeEntry
            {
                Subject = "Русский язык",
                Date = DateTime.Now.AddDays(-3),
                Grade = 4,
                Teacher = "Петрова С.В.",
                Comment = "Хорошо, но есть ошибки"
            });
            allGrades.Add(new GradeEntry
            {
                Subject = "Физика",
                Date = DateTime.Now.AddDays(-7),
                Grade = 4,
                Teacher = "Сидоров А.К.",
                Comment = ""
            });
            allGrades.Add(new GradeEntry
            {
                Subject = "Информатика",
                Date = DateTime.Now.AddDays(-2),
                Grade = 5,
                Teacher = "Козлов Д.М.",
                Comment = "Отличное понимание темы"
            });
            allGrades.Add(new GradeEntry
            {
                Subject = "Английский язык",
                Date = DateTime.Now.AddDays(-1),
                Grade = 3,
                Teacher = "Смирнова Е.А.",
                Comment = "Требуется больше практики"
            });
            allGrades.Add(new GradeEntry
            {
                Subject = "Математика",
                Date = DateTime.Now.AddDays(-10),
                Grade = 4,
                Teacher = "Иванова М.А.",
                Comment = "Хорошо"
            });
            allGrades.Add(new GradeEntry
            {
                Subject = "Физика",
                Date = DateTime.Now.AddDays(-4),
                Grade = 3,
                Teacher = "Сидоров А.К.",
                Comment = "Нужно повторить материал"
            });
            allGrades.Add(new GradeEntry
            {
                Subject = "История",
                Date = DateTime.Now.AddDays(-6),
                Grade = 4,
                Teacher = "Морозова Е.Н.",
                Comment = ""
            });
            allGrades.Add(new GradeEntry
            {
                Subject = "Литература",
                Date = DateTime.Now.AddDays(-8),
                Grade = 5,
                Teacher = "Петрова С.В.",
                Comment = "Отличное сочинение"
            });
        }

        private void LoadSchedule()
        {
            var schedule = new List<ScheduleItem>
            {
                new ScheduleItem { Subject = "Математика", Day = "Понедельник", Time = "08:30-10:00", Room = "201", Teacher = "Иванова М.А." },
                new ScheduleItem { Subject = "Русский язык", Day = "Понедельник", Time = "10:15-11:45", Room = "105", Teacher = "Петрова С.В." },
                new ScheduleItem { Subject = "Физика", Day = "Понедельник", Time = "12:00-13:30", Room = "308", Teacher = "Сидоров А.К." },
                new ScheduleItem { Subject = "Информатика", Day = "Вторник", Time = "08:30-10:00", Room = "401", Teacher = "Козлов Д.М." },
                new ScheduleItem { Subject = "Английский язык", Day = "Вторник", Time = "10:15-11:45", Room = "210", Teacher = "Смирнова Е.А." },
                new ScheduleItem { Subject = "История", Day = "Вторник", Time = "12:00-13:30", Room = "112", Teacher = "Морозова Е.Н." },
                new ScheduleItem { Subject = "Литература", Day = "Среда", Time = "08:30-10:00", Room = "105", Teacher = "Петрова С.В." },
                new ScheduleItem { Subject = "Математика", Day = "Среда", Time = "10:15-11:45", Room = "201", Teacher = "Иванова М.А." },
                new ScheduleItem { Subject = "Химия", Day = "Среда", Time = "12:00-13:30", Room = "312", Teacher = "Волкова Н.И." },
                new ScheduleItem { Subject = "Биология", Day = "Четверг", Time = "08:30-10:00", Room = "305", Teacher = "Лебедева О.В." },
                new ScheduleItem { Subject = "География", Day = "Четверг", Time = "10:15-11:45", Room = "215", Teacher = "Новиков П.П." },
                new ScheduleItem { Subject = "Физика", Day = "Четверг", Time = "12:00-13:30", Room = "308", Teacher = "Сидоров А.К." },
                new ScheduleItem { Subject = "Английский язык", Day = "Пятница", Time = "08:30-10:00", Room = "210", Teacher = "Смирнова Е.А." },
                new ScheduleItem { Subject = "Информатика", Day = "Пятница", Time = "10:15-11:45", Room = "401", Teacher = "Козлов Д.М." },
                new ScheduleItem { Subject = "История", Day = "Пятница", Time = "12:00-13:30", Room = "112", Teacher = "Морозова Е.Н." }
            };

            ScheduleGrid.ItemsSource = schedule;
        }

        private void LoadSubjects()
        {
            var subjects = allGrades.Select(g => g.Subject).Distinct().ToList();

            SubjectCombo.Items.Clear();
            FilterSubjectCombo.Items.Clear();
            FilterSubjectCombo.Items.Add("Все предметы");

            foreach (var subject in subjects)
            {
                SubjectCombo.Items.Add(subject);
                FilterSubjectCombo.Items.Add(subject);
            }

            if (SubjectCombo.Items.Count > 0)
                SubjectCombo.SelectedIndex = 0;

            if (FilterSubjectCombo.Items.Count > 0)
                FilterSubjectCombo.SelectedIndex = 0;
        }

        private void ApplyFilter()
        {
            filteredGrades.Clear();

            string selectedSubject = FilterSubjectCombo.SelectedItem?.ToString();
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate?.AddDays(1);

            var filtered = allGrades.AsEnumerable();

            if (selectedSubject != null && selectedSubject != "Все предметы")
            {
                filtered = filtered.Where(g => g.Subject == selectedSubject);
            }

            if (startDate.HasValue)
            {
                filtered = filtered.Where(g => g.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                filtered = filtered.Where(g => g.Date < endDate.Value);
            }

            foreach (var grade in filtered.OrderByDescending(g => g.Date))
            {
                filteredGrades.Add(grade);
            }
        }

        private void UpdateStatistics()
        {
            var subjects = allGrades.Select(g => g.Subject).Distinct();
            TotalSubjectsText.Text = subjects.Count().ToString();
            TotalGradesText.Text = allGrades.Count.ToString();

            if (allGrades.Count > 0)
            {
                double average = allGrades.Average(g => g.Grade);
                AverageGradeText.Text = average.ToString("F2");
                ExcellentCountText.Text = allGrades.Count(g => g.Grade == 5).ToString();
            }
            else
            {
                AverageGradeText.Text = "0.00";
                ExcellentCountText.Text = "0";
            }

            var statistics = new List<SubjectStatistics>();
            foreach (var subject in subjects)
            {
                var subjectGrades = allGrades.Where(g => g.Subject == subject).ToList();
                statistics.Add(new SubjectStatistics
                {
                    Subject = subject,
                    GradeCount = subjectGrades.Count,
                    AverageGrade = subjectGrades.Count > 0 ? subjectGrades.Average(g => g.Grade) : 0,
                    GradesList = string.Join(", ", subjectGrades.OrderByDescending(g => g.Date).Select(g => g.Grade))
                });
            }

            StatisticsGrid.ItemsSource = statistics;
        }

        private void AddGradeButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectCombo.SelectedItem == null)
            {
                MessageBox.Show("Выберите предмет", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (GradeCombo.SelectedItem == null)
            {
                MessageBox.Show("Выберите оценку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string subject = SubjectCombo.SelectedItem.ToString();
            int grade = int.Parse(((ComboBoxItem)GradeCombo.SelectedItem).Content.ToString());
            DateTime date = GradeDatePicker.SelectedDate ?? DateTime.Today;
            string comment = CommentBox.Text ?? "";

            var newGrade = new GradeEntry
            {
                Subject = subject,
                Grade = grade,
                Date = date,
                Teacher = GetTeacherForSubject(subject),
                Comment = comment
            };

            allGrades.Add(newGrade);
            ApplyFilter();
            LoadSubjects();
            UpdateStatistics();

            GradeCombo.SelectedIndex = 0;
            CommentBox.Clear();

            MessageBox.Show($"Оценка {grade} по предмету '{subject}' добавлена!",
                "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private string GetTeacherForSubject(string subject)
        {
            var teachers = new Dictionary<string, string>
            {
                { "Математика", "Иванова М.А." },
                { "Русский язык", "Петрова С.В." },
                { "Литература", "Петрова С.В." },
                { "Физика", "Сидоров А.К." },
                { "Информатика", "Козлов Д.М." },
                { "История", "Морозова Е.Н." },
                { "Английский язык", "Смирнова Е.А." },
                { "Химия", "Волкова Н.И." },
                { "Биология", "Лебедева О.В." },
                { "География", "Новиков П.П." }
            };

            return teachers.ContainsKey(subject) ? teachers[subject] : "Не указан";
        }

        private void FilterSubjectCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilter();
        }

        private void FilterDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilter();
        }

        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {
            FilterSubjectCombo.SelectedIndex = 0;
            StartDatePicker.SelectedDate = DateTime.Today.AddMonths(-1);
            EndDatePicker.SelectedDate = DateTime.Today;
            ApplyFilter();
        }
    }
}