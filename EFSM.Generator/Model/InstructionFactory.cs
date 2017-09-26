using System;
using System.Collections.Generic;
using System.Linq;
using EFSM.Domain;

namespace EFSM.Generator.Model
{
    public class InstructionFactory
    {
        public Instruction[] GetInstructions(StateMachineCondition condition, StateMachineInput[] inputs)
        {
            List<Instruction> instructions = new List<Instruction>();

            AddInstruction(instructions, condition, inputs);

            var temp = instructions.ToArray();

            temp.Validate(inputs.Length);

            return  temp;
        }

        private void AddInstruction(IList<Instruction> instructions, StateMachineCondition condition, StateMachineInput[] inputs)
        {
            if (condition == null)
                return;

            switch (condition.ConditionType)
            {
                case ConditionType.Input:

                    if (condition.SourceInputId == null)
                        throw new InvalidOperationException($"An input condition was specified without an input id.");

                    var input = inputs.FirstOrDefault(i => i.Id == condition.SourceInputId.Value);

                    if (input == null)
                        throw new InvalidOperationException($"Input with id '{condition.SourceInputId.Value}' could not be found.");

                    int inputIndex = Array.IndexOf(inputs, input);

                    instructions.Add(new Instruction(OpCode.Push, (byte)inputIndex));
                    break;

                case ConditionType.Or:
                    AddCompoundInstruction(instructions, condition, OpCode.Or, inputs);
                    break;

                case ConditionType.And:
                    AddCompoundInstruction(instructions, condition, OpCode.And, inputs);
                    break;

                case ConditionType.Not:
                    instructions.Add(new Instruction(OpCode.Not));
                    break;

                default:
                    throw new NotSupportedException($"ConditionType '{condition.ConditionType}'");
            }
        }

        private void AddCompoundInstruction(IList<Instruction> instructions, StateMachineCondition condition, OpCode opCode, StateMachineInput[] inputs)
        {
            if (condition.Conditions == null)
                throw new InvalidOperationException("A compound logical type had null children");

            for (int conditionIndex = 0; conditionIndex < condition.Conditions.Count; conditionIndex++)
            {
                AddInstruction(instructions, condition.Conditions[conditionIndex], inputs);

                if (conditionIndex != 0)
                {
                    instructions.Add(new Instruction(opCode));
                }
            }

        }
    }
}