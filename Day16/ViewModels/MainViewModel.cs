using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using StudentDiaryFull.Commands;
using StudentDiaryFull.Models;
using StudentDiaryFull.Services;

namespace StudentDiaryFull.ViewModels
{
    /// <summary>
    /// ViewModel для преподавателя: студенты, оценки, ДЗ, расписание, чат.
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        private DiaryData _data;
        private readonly UserModel _currentUser;

        // ─── Текущий пользователь ────────────────────────────────────────────
        public string CurrentUserName  => _currentUser.FullName;
        public bool   IsTeacher        => _currentUser.Role == UserRole.Teacher;
        public bool   IsStudent        => _currentUser.Role == UserRole.Student;

        // ─── Студенты ────────────────────────────────────────────────────────
        public ObservableCollection<StudentModel> Students { get; } = new();

        private StudentModel? _selectedStudent;
        public StudentModel? SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                if (SetProperty(ref _selectedStudent, value))
                    RefreshGrades();
            }
        }

        // ─── Оценки ──────────────────────────────────────────────────────────
        public ObservableCollection<GradeModel> Grades { get; } = new();

        private GradeModel? _selectedGrade;
        public GradeModel? SelectedGrade
        {
            get => _selectedGrade;
            set => SetProperty(ref _selectedGrade, value);
        }

        private string _newSubject  = string.Empty;
        private int    _newGradeVal = 5;
        private string _newComment  = string.Empty;

        public string NewSubject
        {
            get => _newSubject;
            set => SetProperty(ref _newSubject, value);
        }
        public int NewGradeValue
        {
            get => _newGradeVal;
            set => SetProperty(ref _newGradeVal, value);
        }
        public string NewComment
        {
            get => _newComment;
            set => SetProperty(ref _newComment, value);
        }

        private double _averageGrade;
        public double AverageGrade
        {
            get => _averageGrade;
            set => SetProperty(ref _averageGrade, value);
        }

        public int[] AvailableGrades { get; } = { 1, 2, 3, 4, 5 };

        // ─── Домашние задания ─────────────────────────────────────────────────
        public ObservableCollection<HomeworkModel> Homework { get; } = new();

        private HomeworkModel? _selectedHw;
        public HomeworkModel? SelectedHomework
        {
            get => _selectedHw;
            set => SetProperty(ref _selectedHw, value);
        }

        private string _hwSubject  = string.Empty;
        private string _hwGroup    = string.Empty;
        private string _hwDesc     = string.Empty;
        private DateTime _hwDue    = DateTime.Today.AddDays(7);

        public string HwSubject
        {
            get => _hwSubject;
            set => SetProperty(ref _hwSubject, value);
        }
        public string HwGroup
        {
            get => _hwGroup;
            set => SetProperty(ref _hwGroup, value);
        }
        public string HwDescription
        {
            get => _hwDesc;
            set => SetProperty(ref _hwDesc, value);
        }
        public DateTime HwDueDate
        {
            get => _hwDue;
            set => SetProperty(ref _hwDue, value);
        }

        // ─── Расписание ───────────────────────────────────────────────────────
        public ObservableCollection<ScheduleItemModel> Schedule { get; } = new();

        private string _filterGroup = string.Empty;
        public string FilterGroup
        {
            get => _filterGroup;
            set { if (SetProperty(ref _filterGroup, value)) RefreshSchedule(); }
        }

        // ─── Чат ─────────────────────────────────────────────────────────────
        public ObservableCollection<ChatMessage> ChatMessages { get; } = new();

        private string _chatInput = string.Empty;
        public string ChatInput
        {
            get => _chatInput;
            set => SetProperty(ref _chatInput, value);
        }

        // ─── Уведомления ──────────────────────────────────────────────────────
        private string _notification = string.Empty;
        public string Notification
        {
            get => _notification;
            set => SetProperty(ref _notification, value);
        }

        // ─── Статус ───────────────────────────────────────────────────────────
        private string _status = "Готов к работе";
        public string StatusMessage
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        // ─── Команды ──────────────────────────────────────────────────────────
        public ICommand AddGradeCommand    { get; }
        public ICommand RemoveGradeCommand { get; }
        public ICommand AddHomeworkCommand { get; }
        public ICommand RemoveHwCommand    { get; }
        public ICommand ToggleHwCommand    { get; }
        public ICommand SendChatCommand    { get; }
        public ICommand SaveCommand        { get; }

        // ─── Конструктор ──────────────────────────────────────────────────────
        public MainViewModel(UserModel user)
        {
            _currentUser = user;
            _data = DataService.LoadDiary();

            AddGradeCommand    = new RelayCommand(_ => AddGrade(),    _ => CanAddGrade());
            RemoveGradeCommand = new RelayCommand(_ => RemoveGrade(), _ => SelectedGrade != null && IsTeacher);
            AddHomeworkCommand = new RelayCommand(_ => AddHomework(), _ => CanAddHomework() && IsTeacher);
            RemoveHwCommand    = new RelayCommand(_ => RemoveHw(),    _ => SelectedHomework != null && IsTeacher);
            ToggleHwCommand    = new RelayCommand(_ => ToggleHw(),    _ => SelectedHomework != null);
            SendChatCommand    = new RelayCommand(_ => SendChat(),    _ => !string.IsNullOrWhiteSpace(ChatInput));
            SaveCommand        = new RelayCommand(_ => SaveAll());

            RefreshStudents();
            RefreshHomework();
            RefreshSchedule();

            // Чат через Named Pipes
            PipeService.Instance.Start(user.FullName);
            PipeService.Instance.MessageReceived += OnPipeMessage;

            // Уведомления через MMF
            MmfService.Instance.NotificationReceived += OnMmfNotification;
            MmfService.Instance.StartPolling(user.FullName);

            // Если студент — показываем только свои данные
            if (IsStudent)
            {
                var student = _data.Students.FirstOrDefault(s => s.Id == user.StudentId);
                if (student != null)
                {
                    SelectedStudent = Students.FirstOrDefault(s => s.Id == student.Id);
                    FilterGroup = student.Group;
                    RefreshHomework();
                }
            }
        }

        // ─── Студенты ────────────────────────────────────────────────────────
        private void RefreshStudents()
        {
            Students.Clear();
            foreach (var s in _data.Students) Students.Add(s);
            StatusMessage = $"Студентов: {Students.Count}";
        }

        // ─── Оценки ──────────────────────────────────────────────────────────
        private void RefreshGrades()
        {
            Grades.Clear();
            AverageGrade = 0;
            if (SelectedStudent == null) return;

            var grades = _data.Grades.Where(g => g.StudentId == SelectedStudent.Id);
            foreach (var g in grades) Grades.Add(g);
            AverageGrade = Grades.Count > 0 ? Grades.Average(g => g.Value) : 0;
            StatusMessage = $"Оценок у {SelectedStudent.FullName}: {Grades.Count}";
        }

        private void AddGrade()
        {
            if (SelectedStudent == null) return;
            var id = _data.Grades.Count > 0 ? _data.Grades.Max(g => g.Id) + 1 : 1;
            var grade = new GradeModel
            {
                Id = id, StudentId = SelectedStudent.Id,
                Subject = NewSubject.Trim(), Value = NewGradeValue,
                Date = DateTime.Today, Comment = NewComment.Trim()
            };
            _data.Grades.Add(grade);
            Grades.Add(grade);
            AverageGrade = Grades.Average(g => g.Value);
            NewSubject = string.Empty;
            NewComment = string.Empty;
            NewGradeValue = 5;
            SaveAll();
            StatusMessage = $"Оценка {grade.GradeDisplay} добавлена.";
        }

        private bool CanAddGrade()
            => IsTeacher && SelectedStudent != null && !string.IsNullOrWhiteSpace(NewSubject);

        private void RemoveGrade()
        {
            if (SelectedGrade == null) return;
            var confirm = MessageBox.Show($"Удалить оценку «{SelectedGrade.GradeDisplay}»?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm != MessageBoxResult.Yes) return;

            _data.Grades.Remove(SelectedGrade);
            Grades.Remove(SelectedGrade);
            SelectedGrade = null;
            AverageGrade = Grades.Count > 0 ? Grades.Average(g => g.Value) : 0;
            SaveAll();
            StatusMessage = "Оценка удалена.";
        }

        // ─── Домашние задания ─────────────────────────────────────────────────
        private void RefreshHomework()
        {
            Homework.Clear();
            var query = _data.Homework.AsEnumerable();
            if (IsStudent)
            {
                var student = _data.Students.FirstOrDefault(s => s.Id == _currentUser.StudentId);
                if (student != null)
                    query = query.Where(h => h.Group == student.Group);
            }
            foreach (var h in query.OrderBy(h => h.DueDate)) Homework.Add(h);
        }

        private void AddHomework()
        {
            var id = _data.Homework.Count > 0 ? _data.Homework.Max(h => h.Id) + 1 : 1;
            var hw = new HomeworkModel
            {
                Id = id, Subject = HwSubject.Trim(),
                Group = HwGroup.Trim(), Description = HwDescription.Trim(),
                DueDate = HwDueDate, IsCompleted = false
            };
            _data.Homework.Add(hw);
            Homework.Add(hw);
            HwSubject = HwGroup = HwDescription = string.Empty;
            HwDueDate = DateTime.Today.AddDays(7);
            SaveAll();
            StatusMessage = $"ДЗ по «{hw.Subject}» добавлено.";

            // Уведомление через MMF
            MmfService.Instance.WriteNotification(
                $"Новое ДЗ для {hw.Group}: {hw.Subject} — до {hw.DueDateDisplay}");
        }

        private bool CanAddHomework()
            => !string.IsNullOrWhiteSpace(HwSubject) && !string.IsNullOrWhiteSpace(HwGroup);

        private void RemoveHw()
        {
            if (SelectedHomework == null) return;
            _data.Homework.Remove(SelectedHomework);
            Homework.Remove(SelectedHomework);
            SelectedHomework = null;
            SaveAll();
            StatusMessage = "ДЗ удалено.";
        }

        private void ToggleHw()
        {
            if (SelectedHomework == null) return;
            var hw = _data.Homework.FirstOrDefault(h => h.Id == SelectedHomework.Id);
            if (hw == null) return;
            hw.IsCompleted = !hw.IsCompleted;
            SelectedHomework.IsCompleted = hw.IsCompleted;
            SaveAll();
            // Обновляем список
            var idx = Homework.IndexOf(SelectedHomework);
            if (idx >= 0) { Homework.RemoveAt(idx); Homework.Insert(idx, hw); SelectedHomework = hw; }
            StatusMessage = hw.IsCompleted ? "ДЗ отмечено выполненным." : "ДЗ отмечено невыполненным.";
        }

        // ─── Расписание ───────────────────────────────────────────────────────
        private void RefreshSchedule()
        {
            Schedule.Clear();
            var query = _data.Schedule.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(FilterGroup))
                query = query.Where(s => s.Group == FilterGroup);
            var order = new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                                DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday };
            foreach (var item in query.OrderBy(s => Array.IndexOf(order, s.DayOfWeek))
                                      .ThenBy(s => s.TimeStart))
                Schedule.Add(item);
        }

        // ─── Чат ─────────────────────────────────────────────────────────────
        private void SendChat()
        {
            PipeService.Instance.SendMessage(ChatInput);
            ChatInput = string.Empty;
        }

        private void OnPipeMessage(ChatMessage msg)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                // Избегаем дублирования собственных сообщений (уже добавлены через SendMessage)
                if (!ChatMessages.Contains(msg))
                    ChatMessages.Add(msg);
            });
        }

        // ─── MMF-уведомления ──────────────────────────────────────────────────
        private void OnMmfNotification(string text)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Notification = text;
                StatusMessage = $"📢 {text}";
            });
        }

        // ─── Сохранение ───────────────────────────────────────────────────────
        private void SaveAll()
        {
            DataService.SaveDiary(_data);
        }

        // ─── Группы для фильтра расписания ────────────────────────────────────
        public ObservableCollection<string> Groups { get; } = new()
            { "CS-301", "CS-302", "CS-303" };
    }
}
