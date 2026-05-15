using System.Windows;
using System.Windows.Controls;
using StudentDiary.ViewModels;

namespace StudentDiary
{
    public partial class MainWindow : Window
    {
        private MainViewModel ViewModel => (MainViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void FilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = FilterBox.Text.ToLower();
            var vm = ViewModel;

            GradesListView.Items.Filter = item =>
            {
                if (item is Models.StudentGrade g)
                    return string.IsNullOrWhiteSpace(text)
                        || g.StudentName.ToLower().Contains(text)
                        || g.Subject.ToLower().Contains(text);
                return true;
            };
        }
    }
}
