namespace TextEditorCommand.Models.Commands
{
    public class TypeCommand : ICommand
    {
        private TextEditor _editor;
        private string _text;
        private int _position;

        public TypeCommand(TextEditor editor, string text, int position = -1)
        {
            _editor = editor;
            _text = text;
            _position = position;
        }

        public void Execute()
        {
            Console.WriteLine($"\n  [EXECUTE] Command: TYPE");
            _editor.InsertText(_text, _position);
        }

        public void Undo()
        {
            Console.WriteLine($"  [UNDO] Type: deleting \"{_text}\"");
            int deletePosition = _position == -1 ? _editor.GetLength() - _text.Length : _position;
            _editor.DeleteText(_text.Length, deletePosition);
        }

        public string GetDescription()
        {
            return $"Type \"{_text}\"";
        }
    }
}