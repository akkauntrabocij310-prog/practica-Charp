using TextEditorCommand.Models.Commands;

namespace TextEditorCommand.Models.Commands
{
    public class RedoCommand : ICommand
    {
        private CommandHistory _history;

        public RedoCommand(CommandHistory history)
        {
            _history = history;
        }

        public void Execute()
        {
            Console.WriteLine($"\n  [EXECUTE] REDO");
            _history.Redo();
        }

        public void Undo()
        {
            Console.WriteLine($"  [UNDO] Redo operation = Undo");
            _history.Undo();
        }

        public string GetDescription()
        {
            return "Redo undone action";
        }
    }
}