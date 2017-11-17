using System;

namespace EFSM.OffsetWriter
{
    /// <summary>
    /// Used to mark an address. Does not output any data or consume any space.
    /// </summary>
    public class MarkerElement2 : OffsetElement2
    {
        private readonly string _comment;

        public MarkerElement2(IDelayedResolutionElement referencedBy, string comment)
            : base(referencedBy)
        {
            _comment = comment;
        }

        protected override int ResolveCore(int absoluteOffset)
        {
            return 0;
        }

        public override void Write(IElementWriteTarget2 target)
        {
            target.Write(new UInt16[0], this);
        }

        public override string GetComment()
        {
            return _comment ?? base.GetComment();
        }
    }
}