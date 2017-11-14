using System;

namespace EFSM.OffsetWriter
{
    public interface IElementWriteTarget
    {
        void Write(byte[] content, OffsetElement source);
    }

    public interface IElementWriteTarget2
    {
        void Write(UInt16[] content, OffsetElement2 source);
    }
}