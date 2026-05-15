using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ElectronicDiary.Models;
using ElectronicDiary.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace ElectronicDiary.ViewModels;

public partial class DiaryViewModel : ObservableObject
{
    private readonly GradeService _service = new();

    [ObservableProperty]
    private ObservableCollection<GradeModel> grades = new();

    [ObservableProperty]
    private GradeModel? selectedGrade;

    public DiaryViewModel()
    {
        LoadAsync();

        AddGradeCommand = new RelayCommand(async () =>
        {
            var grade = new GradeModel
            {
                StudentName = "Студент",
                Subject = "Математика",
                Grade = 5
            };

            Grades.Add(grade);
            await _service.AddGradeAsync(grade);
        });

        EditGradeCommand = new RelayCommand(async () =>
        {
            if (SelectedGrade == null) return;
            SelectedGrade.Grade = 4;
            await _service.UpdateGradeAsync(SelectedGrade);
            OnPropertyChanged(nameof(Grades));
        });

        DeleteGradeCommand = new RelayCommand(async () =>
        {
            if (SelectedGrade == null) return;

            if (MessageBox.Show("Удалить оценку?", "Подтверждение",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Grades.Remove(SelectedGrade);
                await _service.DeleteGradeAsync(SelectedGrade);
            }
        });
    }

    public RelayCommand AddGradeCommand { get; }
    public RelayCommand EditGradeCommand { get; }
    public RelayCommand DeleteGradeCommand { get; }

    private async void LoadAsync()
    {
        var list = await _service.GetGradesAsync();
        Grades = new ObservableCollection<GradeModel>(list);
    }
}