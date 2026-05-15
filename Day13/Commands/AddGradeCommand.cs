using System;
using System.Windows.Input;
using GradeBook.Models;
using GradeBook.Views;

namespace GradeBook.Commands
{
    public class AddGradeCommand : ICommand
    {
        private readonly Action<Grade> _onAdd;

        public AddGradeCommand(Action<Grade> onAdd)
        {
            _onAdd = onAdd;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var dialog = new GradeDialog("Добавить оценку");
            if (dialog.ShowDialog() == true)
            {
                _onAdd?.Invoke(dialog.Grade);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
