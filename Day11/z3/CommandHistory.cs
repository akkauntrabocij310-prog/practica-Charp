using System;
using System.Collections.Generic;
using TextEditorCommand.Models.Commands;

namespace TextEditorCommand.Models
{
    public class CommandHistory
    {
        private Stack<ICommand> _undoStack;
        private Stack<ICommand> _redoStack;

        public CommandHistory()
        {
            _undoStack = new Stack<ICommand>();
            _redoStack = new Stack<ICommand>();
        }

        public void Push(ICommand command)
        {
            _undoStack.Push(command);
            _redoStack.Clear();
            Console.WriteLine($"  [HISTORY] Command added. Total in history: {_undoStack.Count}");
        }

        public void Undo()
        {
            if (_undoStack.Count > 0)
            {
                ICommand command = _undoStack.Pop();
                command.Undo();
                _redoStack.Push(command);
                Console.WriteLine($"  [UNDO] Undone command: {command.GetDescription()}");
            }
            else
            {
                Console.WriteLine("  [WARNING] Nothing to undo!");
            }
        }

        public void Redo()
        {
            if (_redoStack.Count > 0)
            {
                ICommand command = _redoStack.Pop();
                command.Execute();
                _undoStack.Push(command);
                Console.WriteLine($"  [REDO] Redone command: {command.GetDescription()}");
            }
            else
            {
                Console.WriteLine("  [WARNING] Nothing to redo!");
            }
        }

        public void ShowHistory()
        {
            Console.WriteLine($"\n  [HISTORY] Command History:");
            Console.WriteLine($"  +-----------------------------------------+");

            var commands = _undoStack.ToArray();
            for (int i = commands.Length - 1; i >= 0; i--)
            {
                Console.WriteLine($"  | {commands.Length - i,2}. {commands[i].GetDescription(),-39} |");
            }

            if (commands.Length == 0)
                Console.WriteLine($"  | {"No commands in history",-39} |");

            Console.WriteLine($"  +-----------------------------------------+");
            Console.WriteLine($"  [STATS] Available for Undo: {_undoStack.Count}, for Redo: {_redoStack.Count}");
        }
    }
}