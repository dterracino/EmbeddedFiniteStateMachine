using System;

namespace EFSM.OffsetWriter
{
    public class SimpleElement : OffsetElement
    {
        private readonly string _comment;

        public SimpleElement(byte[] content, string comment, IDelayedResolutionElement referencedBy = null)
            : base(referencedBy)
        {
            _comment = comment;
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public byte[] Content { get; }

        protected override int ResolveCore(int absoluteOffset)
        {
            return Content.Length;
        }

        public override void Write(IElementWriteTarget target)
        {
            target.Write(Content, this);
        }

        public override string GetComment()
        {
            return _comment ?? base.GetComment();
        }
    }
}