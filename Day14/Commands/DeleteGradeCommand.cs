using System;
using System.Windows.Input;
using StudentDiary.ViewModels;

namespace StudentDiary.Commands
{
    public class DeleteGradeCommand : ICommand
    {
        private readonly MainViewModel _viewModel;

        public DeleteGradeCommand(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter) => _viewModel.SelectedGrade != null;

        public void Execute(object parameter)
        {
            _viewModel.DeleteSelectedGrade();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
