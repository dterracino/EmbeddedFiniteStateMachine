using System;
using System.Text;

namespace EFSM.Generator.TextGeneration
{
    public class TextGenerator
    {
        private readonly int _spacesPerIndent;
        private const int DefaultSpacesPerIndent = 4;
        private int _indentLevel;

        private readonly StringBuilder _text = new StringBuilder();

        private bool _isAppending;
        private bool _isAwaitingNewLine;

        public TextGenerator(int spacesPerIndent = DefaultSpacesPerIndent)
        {
            _spacesPerIndent = spacesPerIndent;
        }

        public void AddIndent()
        {
            _indentLevel++;
        }

        public void RemoveIndent()
        {
            if (_indentLevel > 0)
            {
                _indentLevel--;
            }
            else
            {
                throw new InvalidOperationException("Unable to outdent. We are not currently indented.");
            }
        }

        private void PerformIndent()
        {
            if (_isAwaitingNewLine)
            {
                _text.AppendLine();
                _isAwaitingNewLine = false;
            }

            if (!_isAppending && _indentLevel > 0)
            {
                _text.Append(new string(' ', _indentLevel*_spacesPerIndent));
            }
        }

        public void PostPend(string text)
        {
            _text.Append(text);
        }

        public void Append(string text)
        {
            PerformIndent();
            _text.Append(text);
            _isAppending = true;
        }

        public void AppendLine(string text)
        {
            PerformIndent();
            _text.Append(text);
            _isAppending = false;
            _isAwaitingNewLine = true;
        }

        public void AppendLine()
        {
            PerformIndent();
            _isAppending = false;
            _isAwaitingNewLine = true;
        }

        public int IndentLevel => _indentLevel;

        public override string ToString()
        {
            return _text.ToString();
        }
    }
}