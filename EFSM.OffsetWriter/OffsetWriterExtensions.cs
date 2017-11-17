using MiscUtil.Conversion;
using System;

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

        public static OffsetElement2 Add(
           this ElementList2 elementList,
           EndianBitConverter bitConverter,
           ushort value,
           string comment,
           IDelayedResolutionElement referencedBy = null)
        {
            UInt16[] words = { value };

            SimpleElement2 element = new SimpleElement2(words, comment, referencedBy);

            elementList.Add(element);

            return element;
        }
    }
}