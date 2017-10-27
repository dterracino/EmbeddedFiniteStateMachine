using System;
using System.Collections.Generic;
using System.Linq;

namespace EFSM.OffsetWriter
{
    public class ElementWriteTarget : IElementWriteTarget
    {
        private readonly List<BinarySegment> _segments = new List<BinarySegment>();

        public ElementWriteTarget(int size = 17)
        {
        }

        public void Write(byte[] content, OffsetElement source)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            _segments.Add(new BinarySegment(content, source));
        }

        public byte[] ToArray()
        {
            return _segments
                .SelectMany(s => s.Content)
                .ToArray();
        }

        public BinarySegment[] GetSegments()
        {
            return _segments.ToArray();
        }
    }

    public class BinarySegment
    {
        public byte[] Content { get; }
        
        public OffsetElement Source { get; }

        public BinarySegment(byte[] content, OffsetElement source)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));
            if (source == null) throw new ArgumentNullException(nameof(source));
            Content = content;
            Source = source;
        }
    }


}