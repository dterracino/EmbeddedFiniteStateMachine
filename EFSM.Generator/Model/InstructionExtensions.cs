using System;
using System.Collections.Generic;

namespace EFSM.Generator.Model
{
    public static class InstructionExtensions
    {
        public static void Validate(this Instruction[] instructions, int numberOfInputs)
        {
            bool[] inputs = new bool[numberOfInputs];

            Evalutate(instructions, inputs);
        }
        
        public static bool Evalutate(this Instruction[] instructions, bool[] inputs)
        {
            if (instructions.Length == 0)
                return true;
            
            Stack<bool> stack = new Stack<bool>();

            foreach (var instruction in instructions)
            {
                switch (instruction.OpCode)
                {
                    case OpCode.Push:
                    {

                        if (instruction.InputIndex == null)
                            throw new InvalidOperationException("No input index was provided");

                        stack.Push(inputs[instruction.InputIndex.Value]);
                    }
                        break;

                    case OpCode.Or:
                    {

                        bool a = stack.Pop();
                        bool b = stack.Pop();
                        stack.Push(a || b);
                    }
                        break;

                    case OpCode.And:
                    {
                        bool a = stack.Pop();
                        bool b = stack.Pop();
                        stack.Push(a && b);
                    }

                        break;

                    case OpCode.Not:
                    {
                        bool a = stack.Pop();
                        stack.Push(!a);
                    }
                        break;

                    default:
                        throw new NotSupportedException($"ConditionType '{instruction.OpCode}'");
                }
            }

            if (stack.Count != 1)
                throw new InvalidOperationException($"Instructions do not resolve to a final stack size of 1.");

            return stack.Pop();

        }

    }
}