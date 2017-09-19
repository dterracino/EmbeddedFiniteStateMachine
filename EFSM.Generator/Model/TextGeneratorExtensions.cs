using System;

namespace EFSM.Generator.Model
{
    public static class TextGeneratorExtensions
    {
        public static IDisposable Indent(this TextGenerator text)
        {
            return new Indenter(text);
        }
    }
}