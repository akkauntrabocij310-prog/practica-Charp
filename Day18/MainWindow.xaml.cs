using ElectronicDiary.ViewModels;

namespace ElectronicDiary;

public partial class MainWindow : System.Windows.Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new DiaryViewModel();
    }
}