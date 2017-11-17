using System;

namespace EFSM.OffsetWriter
{
    public class SimpleElement2 : OffsetElement2
    {
        private readonly string _comment;

        public SimpleElement2(UInt16[] content, string comment, IDelayedResolutionElement referencedBy = null)
            : base(referencedBy)
        {
            _comment = comment;
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public UInt16[] Content { get; }

        protected override int ResolveCore(int absoluteOffset)
        {
            return Content.Length;
        }

        public override void Write(IElementWriteTarget2 target)
        {
            target.Write(Content, this);
        }

        public override string GetComment()
        {
            return _comment ?? base.GetComment();
        }
    }
}