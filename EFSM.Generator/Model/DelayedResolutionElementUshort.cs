using System;
using EFSM.OffsetWriter;
using MiscUtil.Conversion;

namespace EFSM.Generator.Model
{
    public class DelayedResolutionElementUshort : OffsetElement, IDelayedResolutionElement
    {
        private readonly EndianBitConverter _bitConverter;
        private readonly string _commentFormat;
        private ushort? _absoluteOffset;

        public DelayedResolutionElementUshort(EndianBitConverter bitConverter, string commentFormat, IDelayedResolutionElement referencedBy = null) 
            : base(referencedBy)
        {
            if (bitConverter == null) throw new ArgumentNullException(nameof(bitConverter));
            _bitConverter = bitConverter;
            _commentFormat = commentFormat;
        }

        public override int ResolveCore(int absoluteOffset)
        {
            return 2;
        }

        public override void Write(IElementWriteTarget target)
        {
            if (_absoluteOffset == null)
                throw new InvalidOperationException("Delayed resolution has not completed for this element.");

            target.Write(_bitConverter.GetBytes(_absoluteOffset.Value), this);
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