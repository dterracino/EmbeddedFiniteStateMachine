namespace EFSM.OffsetWriter
{
    public abstract class OffsetElement2
    {
        private readonly IDelayedResolutionElement _referencedBy;

        protected OffsetElement2(IDelayedResolutionElement referencedBy)
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
            int size = ResolveCore(0);

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

        public abstract void Write(IElementWriteTarget2 target);
    }
}