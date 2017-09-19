using System;

namespace EFSM.Generator.Model
{
    public class Indenter : IDisposable
    {
        private readonly TextGenerator _textGenerator;
        private bool _isDisposed;

        public Indenter(TextGenerator textGenerator)
        {
            if (textGenerator == null) throw new ArgumentNullException(nameof(textGenerator));

            _textGenerator = textGenerator;

            textGenerator.Indent();
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                _textGenerator.Outdent();
            }
        }
    }
}