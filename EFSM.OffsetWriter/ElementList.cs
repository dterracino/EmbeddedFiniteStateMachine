using System;
using System.Collections.Generic;

namespace EFSM.OffsetWriter
{
    public class ElementList : OffsetElement
    {
        private readonly List<OffsetElement> _elements = new List<OffsetElement>();

        public ElementList(IDelayedResolutionElement referencedBy = null)
            : base(referencedBy)
        {
        }

        public void Add(OffsetElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            _elements.Add(element);
        }

        public void AddRange(IEnumerable<OffsetElement> elements)
        {
            foreach (var element in elements)
            {
                Add(element);
            }
        }

        protected override int ResolveCore(int absoluteOffset)
        {
            int bytes = 0;

            int currentPosition = absoluteOffset;

            foreach (var element in _elements)
            {
                int bytesForElement = element.Resolve(currentPosition);

                bytes += bytesForElement;
                currentPosition += bytesForElement;
            }

            return bytes;
        }

        public override void Write(IElementWriteTarget target)
        {
            foreach (var element in _elements)
            {
                element.Write(target);
            }
        }
    }
}