namespace EFSM.OffsetWriter
{
    public interface IDelayedResolutionElement
    {
        void DelayedResolution(int resolvedOffset);
    }
}