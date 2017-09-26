using System;
using System.Collections.Generic;
using EFSM.Domain;
using EFSM.Generator.Model;
using Xunit;

namespace EFSM.Generator.Tests
{
    public class ExecutionTests
    {
        private readonly InstructionFactory _instructionFactory = new InstructionFactory();

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void OneInput(bool value)
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

            bool[] inputValues = new bool[]
            {
                value
            };

            Assert.Equal(value, instructions.Evalutate(inputValues));
        }

        [Theory]
        [InlineData(false, false, ConditionType.Or, false)]
        [InlineData(true, false, ConditionType.Or, true)]
        [InlineData(false, true, ConditionType.Or, true)]
        [InlineData(true, true, ConditionType.Or, true)]

        [InlineData(false, false, ConditionType.And, false)]
        [InlineData(true, false, ConditionType.And, false)]
        [InlineData(false, true, ConditionType.And, false)]
        [InlineData(true, true, ConditionType.And, true)]
        public void SimpleCompound(bool a, bool b, ConditionType conditionType, bool expected)
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
                ConditionType = conditionType,
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

            //Generate the instructions
            var instructions = _instructionFactory.GetInstructions(rootCondition, inputs);

            bool[] inputValues = new bool[]
            {
                a, 
                b
            };   
            
            Assert.Equal(expected, instructions.Evalutate(inputValues));
        }
    }
}