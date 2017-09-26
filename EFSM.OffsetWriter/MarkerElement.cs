namespace EFSM.OffsetWriter
{
    /// <summary>
    /// Used to mark an address. Does not output any data or consume any space.
    /// </summary>
    public class MarkerElement : OffsetElement
    {
        private readonly string _comment;

        public MarkerElement(IDelayedResolutionElement referencedBy, string comment) 
            : base(referencedBy)
        {
            _comment = comment;
        }

        public override int ResolveCore(int absoluteOffset)
        {
            return 0;
        }

        public override void Write(IElementWriteTarget target)
        {
            target.Write(new byte[0], this);
        }

        public override string GetComment()
        {
            return _comment ?? base.GetComment();
        }
    }
}