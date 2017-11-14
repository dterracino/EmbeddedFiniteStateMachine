using System;
using EFSM.OffsetWriter;
using MiscUtil.Conversion;

namespace EFSM.Generator.Model
{
    public class DelayedResolutionElementUshort2 : OffsetElement2, IDelayedResolutionElement
    {
        private readonly EndianBitConverter _bitConverter;
        private readonly string _commentFormat;
        private ushort? _absoluteOffset;

        public DelayedResolutionElementUshort2(EndianBitConverter bitConverter, string commentFormat, IDelayedResolutionElement referencedBy = null)
            : base(referencedBy)
        {
            if (bitConverter == null) throw new ArgumentNullException(nameof(bitConverter));
            _bitConverter = bitConverter;
            _commentFormat = commentFormat;
        }

        protected override int ResolveCore(int absoluteOffset)
        {
            return 1;
        }

        public override void Write(IElementWriteTarget2 target)
        {
            if (_absoluteOffset == null)
                throw new InvalidOperationException("Delayed resolution has not completed for this element.");

            UInt16[] value = new UInt16[] { _absoluteOffset.Value };

            target.Write(value, this);
        }

        public void DelayedResolution(int resolvedOffset)
        {
            _absoluteOffset = (ushort)resolvedOffset;
        }

        public override string GetComment()
        {
            if (_commentFormat == null)
                return base.GetComment();

            return string.Format(_commentFormat, _absoluteOffset);
        }
    }
}