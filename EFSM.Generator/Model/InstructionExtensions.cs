using System;
using System.Collections.Generic;

namespace EFSM.Generator.Model
{
    public static class InstructionExtensions
    {
        public static void ValidateInstructions(this Instruction[] instructions)
        {
            if (instructions.Length > 0)
            {
                Stack<int> stack = new Stack<int>();

                foreach (var instruction in instructions)
                {
                    switch (instruction.OpCode)
                    {
                        case OpCode.Push:
                            stack.Push(0);
                            break;

                        case OpCode.Or:
                        case OpCode.And:
                            stack.Pop();
                            stack.Pop();
                            stack.Push(0);

                            break;

                        case OpCode.Not:

                            stack.Pop();
                            stack.Push(0);
                            break;

                        default:
                            throw new NotSupportedException($"ConditionType '{instruction.OpCode}'");
                    }
                }

                if (stack.Count != 1)
                    throw new InvalidOperationException($"Instructions do not resolve to a final stack size of 1.");
            }
        }
    }
}