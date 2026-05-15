using System;

namespace TextEditorCommand.Models.Commands
{
    public class PasteCommand : ICommand
    {
        private TextEditor _editor;
        private int _position;
        private string _insertedText;
        private int _insertedLength;

        public PasteCommand(TextEditor editor, int position = -1)
        {
            _editor = editor;
            _position = position;
            _insertedText = string.Empty;
        }

        public void Execute()
        {
            Console.WriteLine($"\n  [EXECUTE] Command: PASTE");

            _insertedText = _editor.GetClipboard();
            _insertedLength = _insertedText.Length;

            _editor.Paste(_position);
        }

        public void Undo()
        {
            if (_insertedLength > 0)
            {
                Console.WriteLine($"  [UNDO] Paste: deleting \"{_insertedText}\"");
                _editor.DeleteText(_insertedLength, _position == -1 ? _editor.GetLength() - _insertedLength : _position);
            }
        }

        public string GetDescription()
        {
            return $"Paste from clipboard (position: {(_position == -1 ? "end" : _position.ToString())})";
        }
    }
}