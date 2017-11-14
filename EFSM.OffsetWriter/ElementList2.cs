using System;
using System.Collections.Generic;

namespace EFSM.OffsetWriter
{
    public class ElementList2 : OffsetElement2
    {
        private readonly List<OffsetElement2> _elements = new List<OffsetElement2>();

        public ElementList2(IDelayedResolutionElement referencedBy = null)
            : base(referencedBy)
        {
        }

        public void Add(OffsetElement2 element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            _elements.Add(element);
        }

        public void AddRange(IEnumerable<OffsetElement2> elements)
        {
            foreach (var element in elements)
            {
                Add(element);
            }
        }

        protected override int ResolveCore(int absoluteOffset)
        {
            int words = 0;

            int currentPosition = absoluteOffset;

            foreach (var element in _elements)
            {
                int wordsForElement = element.Resolve(currentPosition);

                words += wordsForElement;
                currentPosition += wordsForElement;
            }

            return words;
        }

        public override void Write(IElementWriteTarget2 target)
        {
            foreach (var element in _elements)
            {
                element.Write(target);
            }
        }
    }
}