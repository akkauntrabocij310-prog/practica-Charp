using System;
using System.Text;

namespace TextEditorCommand.Models
{
    public class TextEditor
    {
        private StringBuilder _content;
        private string _clipboard;

        public TextEditor()
        {
            _content = new StringBuilder();
            _clipboard = string.Empty;
        }

        public void InsertText(string text, int position = -1)
        {
            if (position == -1)
                position = _content.Length;

            if (position >= 0 && position <= _content.Length)
            {
                _content.Insert(position, text);
                Console.WriteLine($"  [INSERT] Text: \"{text}\" at position {position}");
            }
        }

        public string DeleteText(int length, int position = -1)
        {
            if (position == -1)
                position = Math.Max(0, _content.Length - length);

            if (position >= 0 && position < _content.Length)
            {
                int actualLength = Math.Min(length, _content.Length - position);
                string deletedText = _content.ToString(position, actualLength);
                _content.Remove(position, actualLength);
                Console.WriteLine($"  [DELETE] Text: \"{deletedText}\"");
                return deletedText;
            }

            return string.Empty;
        }

        public void Copy(int startPosition, int length)
        {
            if (startPosition >= 0 && startPosition < _content.Length)
            {
                int actualLength = Math.Min(length, _content.Length - startPosition);
                _clipboard = _content.ToString(startPosition, actualLength);
                Console.WriteLine($"  [COPY] Copied to clipboard: \"{_clipboard}\"");
            }
        }

        public void Paste(int position = -1)
        {
            if (!string.IsNullOrEmpty(_clipboard))
            {
                InsertText(_clipboard, position);
                Console.WriteLine($"  [PASTE] From clipboard: \"{_clipboard}\"");
            }
            else
            {
                Console.WriteLine("  [WARNING] Clipboard is empty!");
            }
        }

        public string GetContent()
        {
            return _content.ToString();
        }

        public void DisplayContent()
        {
            Console.WriteLine($"\n  [CONTENT] Current text: \"{_content}\"");
            if (_content.Length == 0)
                Console.WriteLine("  [CONTENT] Text is empty");
        }

        public int GetLength()
        {
            return _content.Length;
        }

        public void Clear()
        {
            _content.Clear();
            Console.WriteLine("  [CLEAR] Text cleared");
        }

        public string GetClipboard()
        {
            return _clipboard;
        }
    }
}