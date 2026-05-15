using System.Windows;
using System.Windows.Input;
using StudentDiaryFull.Commands;
using StudentDiaryFull.Models;
using StudentDiaryFull.Services;

namespace StudentDiaryFull.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _login    = string.Empty;
        private string _password = string.Empty;
        private string _error    = string.Empty;

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string Error
        {
            get => _error;
            set => SetProperty(ref _error, value);
        }

        public UserModel? AuthenticatedUser { get; private set; }

        public ICommand LoginCommand { get; }

        public event System.Action? LoginSuccess;

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(_ => DoLogin());
        }

        private void DoLogin()
        {
            Error = string.Empty;
            var user = DataService.Authenticate(Login.Trim(), Password);
            if (user == null)
            {
                Error = "Неверный логин или пароль.";
                return;
            }
            AuthenticatedUser = user;
            LoginSuccess?.Invoke();
        }
    }
}
