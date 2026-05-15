namespace TextEditorCommand.Models.Commands
{
    public class DeleteCommand : ICommand
    {
        private TextEditor _editor;
        private int _length;
        private int _position;
        private string _deletedText;

        public DeleteCommand(TextEditor editor, int length, int position = -1)
        {
            _editor = editor;
            _length = length;
            _position = position;
            _deletedText = string.Empty;
        }

        public void Execute()
        {
            Console.WriteLine($"\n  [EXECUTE] Command: DELETE");
            _deletedText = _editor.DeleteText(_length, _position);
        }

        public void Undo()
        {
            if (!string.IsNullOrEmpty(_deletedText))
            {
                Console.WriteLine($"  [UNDO] Delete: restoring \"{_deletedText}\"");
                _editor.InsertText(_deletedText, _position);
            }
        }

        public string GetDescription()
        {
            return $"Delete {_length} chars";
        }
    }
}