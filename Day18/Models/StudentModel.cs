using System.Collections.ObjectModel;

namespace ElectronicDiary.Models;

public class StudentModel
{
    public string Name { get; set; } = "";
    public ObservableCollection<GradeModel> Grades { get; set; } = new();
}