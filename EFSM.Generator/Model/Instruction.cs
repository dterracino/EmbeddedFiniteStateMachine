using System;

namespace EFSM.Generator.Model
{
    public class Instruction
    {
        public OpCode OpCode { get; }
        public byte? InputIndex { get; }

        public Instruction(OpCode opCode, byte? inputIndex = null)
        {
            OpCode = opCode;
            InputIndex = inputIndex;
        }

        public byte ToByte()
        {
            if (OpCode == OpCode.Push)
            {
                if (InputIndex == null)
                    throw new InvalidOperationException($"No input index was available for opCode Push");

                return (byte)(0b10000000 | InputIndex.Value);
            }

            return (byte)OpCode;  
        }

        public override string ToString()
        {
            if (OpCode == OpCode.Push)
            {
                return $"Push {InputIndex}";
            }

            return OpCode.ToString();
        }
    }
}