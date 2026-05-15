using System.Windows;
using StudentDiaryFull.Models;
using StudentDiaryFull.ViewModels;

namespace StudentDiaryFull.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(UserModel user)
        {
            InitializeComponent();
            DataContext = new MainViewModel(user);
        }
    }
}
