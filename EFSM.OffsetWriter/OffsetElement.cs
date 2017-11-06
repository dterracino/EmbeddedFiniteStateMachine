namespace EFSM.OffsetWriter
{
    public abstract class OffsetElement
    {
        private readonly IDelayedResolutionElement _referencedBy;

        protected OffsetElement(IDelayedResolutionElement referencedBy)
        {
            _referencedBy = referencedBy;
        }

        /// <summary>
        /// Returns the number of bytes consumed by the resolution operation
        /// </summary>
        /// <param name="absoluteOffset"></param>
        /// <returns></returns>
        public int Resolve(int absoluteOffset)
        {
            int size = ResolveCore(absoluteOffset);

            _referencedBy?.DelayedResolution(absoluteOffset);

            return size;
        }

        public virtual string GetComment()
        {
            return GetType().Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="absoluteOffset"></param>
        /// <returns></returns>
        protected abstract int ResolveCore(int absoluteOffset);

        public abstract void Write(IElementWriteTarget target);
    }
}