using System;
using System.Collections.Generic;
using EFSM.Domain;
using EFSM.Generator.Model;
using Xunit;

namespace EFSM.Generator.Tests
{
    public class InstructionTests
    {
        private readonly InstructionFactory _instructionFactory = new InstructionFactory();

        [Fact]
        public void OneInput()
        {
            Guid inputId = new Guid("A916E57F-A5C7-4426-AC9F-44396E4427E6");

            StateMachineInput[] inputs = {
                new StateMachineInput
                {
                    Id = inputId
                },
            };

            var rootCondition = new StateMachineCondition()
            {
                ConditionType = ConditionType.Input,
                SourceInputId = inputId
            };

            var instructions = _instructionFactory.GetInstructions(rootCondition, inputs);

            Assert.Equal(1, instructions.Length);

            Assert.Equal(OpCode.Push, instructions[0].OpCode);
            Assert.NotNull(instructions[0].InputIndex);
            Assert.Equal(0, instructions[0].InputIndex.Value);
        }

        [Fact]
        public void SimpleAnd()
        {
            Guid inputIdA = new Guid("A916E57F-A5C7-4426-AC9F-44396E4427E6");
            Guid inputIdB = new Guid("9DFD98B0-FE1F-4A3E-B021-43617877A486");

            StateMachineInput[] inputs = {
                new StateMachineInput
                {
                    Id = inputIdA
                },
                new StateMachineInput
                {
                    Id = inputIdB
                },
            };

            //Condition (A & B)
            var rootCondition = new StateMachineCondition()
            {
                ConditionType = ConditionType.And,
                Conditions = new List<StateMachineCondition>(2)
                {
                    new StateMachineCondition()
                    {
                        ConditionType = ConditionType.Input,
                        SourceInputId = inputIdA,
                    },
                    new StateMachineCondition()
                    {
                        ConditionType = ConditionType.Input,
                        SourceInputId = inputIdB,
                    }
                }
            };

            //Should be: Push 0, Push 1, And

            //Generate the instructions
            var instructions = _instructionFactory.GetInstructions(rootCondition, inputs);

            Assert.Equal(3, instructions.Length);

            //0
            Assert.Equal(OpCode.Push, instructions[0].OpCode);
            Assert.NotNull(instructions[0].InputIndex);
            Assert.Equal((byte)0, instructions[0].InputIndex.Value);

            //1
            Assert.Equal(OpCode.Push, instructions[1].OpCode);
            Assert.NotNull(instructions[1].InputIndex);
            Assert.Equal((byte)1, instructions[1].InputIndex.Value);

            //2
            Assert.Equal(OpCode.And, instructions[2].OpCode);
            Assert.Null(instructions[2].InputIndex);
        }

        [Fact]
        public void CompoundAnd()
        {
            Guid inputIdA = new Guid("A916E57F-A5C7-4426-AC9F-44396E4427E6");
            Guid inputIdB = new Guid("9DFD98B0-FE1F-4A3E-B021-43617877A486");
            Guid inputIdC = new Guid("1444A244-A5B9-4209-8137-CDBC8B543205");

            StateMachineInput[] inputs = {
                new StateMachineInput
                {
                    Id = inputIdA
                },
                new StateMachineInput
                {
                    Id = inputIdB
                },
                new StateMachineInput
                {
                    Id = inputIdC
                },
            };

            //Condition (A & B)
            var rootCondition = new StateMachineCondition()
            {
                ConditionType = ConditionType.And,
                Conditions = new List<StateMachineCondition>(2)
                {
                    new StateMachineCondition()
                    {
                        ConditionType = ConditionType.Input,
                        SourceInputId = inputIdA,
                    },
                    new StateMachineCondition()
                    {
                        ConditionType = ConditionType.Input,
                        SourceInputId = inputIdB,
                    },
                    new StateMachineCondition()
                    {
                        ConditionType = ConditionType.Input,
                        SourceInputId = inputIdC,
                    }
                }
            };

            //Should be: Push 0, Push 1, And, Push 2, And

            //Generate the instructions
            var instructions = _instructionFactory.GetInstructions(rootCondition, inputs);

            Assert.Equal(5, instructions.Length);

            //0
            Assert.Equal(OpCode.Push, instructions[0].OpCode);
            Assert.NotNull(instructions[0].InputIndex);
            Assert.Equal((byte)0, instructions[0].InputIndex.Value);

            //1
            Assert.Equal(OpCode.Push, instructions[1].OpCode);
            Assert.NotNull(instructions[1].InputIndex);
            Assert.Equal((byte)1, instructions[1].InputIndex.Value);

            //2
            Assert.Equal(OpCode.And, instructions[2].OpCode);
            Assert.Null(instructions[2].InputIndex);

            //3
            Assert.Equal(OpCode.Push, instructions[3].OpCode);
            Assert.NotNull(instructions[3].InputIndex);
            Assert.Equal((byte)2, instructions[3].InputIndex.Value);

            //4
            Assert.Equal(OpCode.And, instructions[4].OpCode);
            Assert.Null(instructions[4].InputIndex);
        }
    }
}
