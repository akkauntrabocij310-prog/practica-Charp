using System;
using System.Collections.Generic;
using TextEditorCommand.Models.Commands;

namespace TextEditorCommand.Models
{
    public class EditorInvoker
    {
        private CommandHistory _history;
        private Queue<ICommand> _commandQueue;

        public EditorInvoker(CommandHistory history)
        {
            _history = history;
            _commandQueue = new Queue<ICommand>();
        }

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _history.Push(command);
        }

        public void QueueCommand(ICommand command)
        {
            _commandQueue.Enqueue(command);
            Console.WriteLine($"  [QUEUE] Command queued: {command.GetDescription()}");
        }

        public void ExecuteQueuedCommands()
        {
            Console.WriteLine($"\n  [QUEUE] Executing {_commandQueue.Count} queued commands...");
            while (_commandQueue.Count > 0)
            {
                ICommand command = _commandQueue.Dequeue();
                ExecuteCommand(command);
            }
        }

        public void ExecuteMacroCommand(List<ICommand> commands, string macroName)
        {
            Console.WriteLine($"\n  [MACRO] Executing macro: {macroName}");
            Console.WriteLine($"  +-----------------------------------------+");

            foreach (var command in commands)
            {
                Console.Write($"  | ");
                command.Execute();
                _history.Push(command);
            }

            Console.WriteLine($"  +-----------------------------------------+");
        }
    }
}