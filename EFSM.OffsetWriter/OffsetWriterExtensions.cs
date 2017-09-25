using MiscUtil.Conversion;

namespace EFSM.OffsetWriter
{
    public static class OffsetWriterExtensions
    {
        public static OffsetElement Add(
            this ElementList elementList, 
            EndianBitConverter bitConverter, 
            ushort value,
            string comment,
            IDelayedResolutionElement referencedBy = null)
        {
            byte[] bytes = bitConverter.GetBytes(value);

            SimpleElement element = new SimpleElement(bytes, comment, referencedBy);

            elementList.Add(element);

            return element;
        }
    }
}