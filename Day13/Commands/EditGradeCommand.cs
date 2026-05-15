using System;
using System.Windows.Input;
using GradeBook.Models;
using GradeBook.Views;

namespace GradeBook.Commands
{
    public class EditGradeCommand : ICommand
    {
        private readonly Func<Grade> _getSelected;
        private readonly Action<Grade> _onEdit;

        public EditGradeCommand(Func<Grade> getSelected, Action<Grade> onEdit)
        {
            _getSelected = getSelected;
            _onEdit = onEdit;
        }

        public bool CanExecute(object parameter) => _getSelected?.Invoke() != null;

        public void Execute(object parameter)
        {
            var selected = _getSelected?.Invoke();
            if (selected == null) return;

            var dialog = new GradeDialog("Редактировать оценку", selected.Clone());
            if (dialog.ShowDialog() == true)
            {
                _onEdit?.Invoke(dialog.Grade);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
