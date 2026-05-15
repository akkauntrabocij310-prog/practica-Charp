using System;
using System.Windows;
using System.Windows.Input;
using GradeBook.Models;

namespace GradeBook.Commands
{
    public class DeleteGradeCommand : ICommand
    {
        private readonly Func<Grade> _getSelected;
        private readonly Action<Grade> _onDelete;

        public DeleteGradeCommand(Func<Grade> getSelected, Action<Grade> onDelete)
        {
            _getSelected = getSelected;
            _onDelete = onDelete;
        }

        public bool CanExecute(object parameter) => _getSelected?.Invoke() != null;

        public void Execute(object parameter)
        {
            var selected = _getSelected?.Invoke();
            if (selected == null) return;

            var result = MessageBox.Show(
                $"Вы уверены, что хотите удалить оценку студента «{selected.StudentName}» по предмету «{selected.Subject}»?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _onDelete?.Invoke(selected);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
