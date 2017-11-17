using System;
using System.Collections.Generic;
using System.Linq;

namespace EFSM.OffsetWriter
{
    public class ElementWriteTarget2 : IElementWriteTarget2
    {
        private readonly List<BinarySegment2> _segments = new List<BinarySegment2>();

        public ElementWriteTarget2(int size = 17)
        {
        }

        public void Write(UInt16[] content, OffsetElement2 source)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            _segments.Add(new BinarySegment2(content, source));
        }

        public UInt16[] ToArray()
        {
            return _segments
                .SelectMany(s => s.Content)
                .ToArray();
        }

        public BinarySegment2[] GetSegments()
        {
            return _segments.ToArray();
        }
    }

    public class BinarySegment2
    {
        public UInt16[] Content { get; }

        public OffsetElement2 Source { get; }

        public BinarySegment2(UInt16[] content, OffsetElement2 source)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));
            if (source == null) throw new ArgumentNullException(nameof(source));
            Content = content;
            Source = source;
        }

    }
}