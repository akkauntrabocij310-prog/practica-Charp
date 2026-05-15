using TextEditorCommand.Models.Commands;

namespace TextEditorCommand.Models.Commands
{
    public class CopyCommand : ICommand
    {
        private TextEditor _editor;
        private int _startPosition;
        private int _length;

        public CopyCommand(TextEditor editor, int startPosition, int length)
        {
            _editor = editor;
            _startPosition = startPosition;
            _length = length;
        }

        public void Execute()
        {
            Console.WriteLine($"\n  [EXECUTE] Command: COPY");
            _editor.Copy(_startPosition, _length);
        }

        public void Undo()
        {
            Console.WriteLine($"  [UNDO] Copy operation (no action needed)");
        }

        public string GetDescription()
        {
            return $"Copy {_length} chars from position {_startPosition}";
        }
    }
}