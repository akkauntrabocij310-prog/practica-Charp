using TextEditorCommand.Models.Commands;

namespace TextEditorCommand.Models.Commands
{
    public class UndoCommand : ICommand
    {
        private CommandHistory _history;

        public UndoCommand(CommandHistory history)
        {
            _history = history;
        }

        public void Execute()
        {
            Console.WriteLine($"\n  [EXECUTE] UNDO");
            _history.Undo();
        }

        public void Undo()
        {
            Console.WriteLine($"  [UNDO] Undo operation = Redo");
            _history.Redo();
        }

        public string GetDescription()
        {
            return "Undo last action";
        }
    }
}