using System.Windows;
using StudentDiaryFull.Services;
using StudentDiaryFull.ViewModels;

namespace StudentDiaryFull.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            LoginBox.Focus();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            ErrorLabel.Visibility = Visibility.Collapsed;
            var login    = LoginBox.Text.Trim();
            var password = PwdBox.Password;

            var user = DataService.Authenticate(login, password);
            if (user == null)
            {
                ErrorLabel.Text       = "Неверный логин или пароль.";
                ErrorLabel.Visibility = Visibility.Visible;
                return;
            }

            var main = new MainWindow(user);
            Application.Current.MainWindow = main;
            main.Show();
            Close();
        }
    }
}
