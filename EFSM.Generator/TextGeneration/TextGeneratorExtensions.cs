using System;
using EFSM.Generator.Model;

namespace EFSM.Generator.TextGeneration
{
    public static class TextGeneratorExtensions
    {
        public static IDisposable Indent(this TextGenerator text)
        {
            return new Indenter(text);
        }
    }
}