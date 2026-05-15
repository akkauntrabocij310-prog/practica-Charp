using System;
using System.Windows.Input;
using StudentDiary.ViewModels;

namespace StudentDiary.Commands
{
    public class AddGradeCommand : ICommand
    {
        private readonly MainViewModel _viewModel;

        public AddGradeCommand(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            _viewModel.OpenAddGradeDialog();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
