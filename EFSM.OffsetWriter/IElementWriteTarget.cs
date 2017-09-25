using System;

namespace EFSM.OffsetWriter
{
    public interface IElementWriteTarget
    {
        void Write(byte[] content, OffsetElement source);
    }
}