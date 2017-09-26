using System;
using System.Linq;
using EFSM.Generator.Model;
using EFSM.OffsetWriter;
using MiscUtil.Conversion;

namespace EFSM.Generator
{
    /// <summary>
    /// Generates the binary info that is used by the embedded runtime.
    /// </summary>
    internal class BinaryGenerator
    {
        public ProjectBinaryGenerationResult Generate(ProjectGenerationModel project)
        {
            EndianBitConverter bitConverter;

            if (project.Model.GenerationOptions.IsLittleEndian)
            {
                bitConverter = EndianBitConverter.Little;
            }
            else
            {
                bitConverter = EndianBitConverter.Big;
            }

            var stateMachineResults = project.StateMachinesGenerationModel
                .Select(s => Generate(s, bitConverter))
                .ToArray();

            return new ProjectBinaryGenerationResult(stateMachineResults);
        }

        private StateMachineBinaryGenerationResult Generate(StateMachineGenerationModel stateMachine, EndianBitConverter bitConverter)
        {
            var inputs = stateMachine
                .Inputs.Select(i => i.Model)
                .ToArray();

            var rootElementList = new ElementList();

            //Number of states
            rootElementList.Add(bitConverter, (ushort) stateMachine.States.Length, $"# of states ({stateMachine.States.Length})");

            //Number of inputs (do we care about this?)
            //rootElementList.Add(bitConverter, (ushort) stateMachine.Inputs.Length, $"# of inputs ({stateMachine.Inputs.Length})");

            //States TOC
            var statesTocEntries = stateMachine.States
                .Select(s => new DelayedResolutionElementUshort(bitConverter, "Address of '" + s.Model.Name  + "' [{0}]"))
                .ToArray();

            //Add them 
            rootElementList.AddRange(statesTocEntries);
            foreach (var stateTocEntry in statesTocEntries)
            {
                rootElementList.Add(stateTocEntry);
            }

            //Add the actual states
            for (var stateIndex = 0; stateIndex < stateMachine.States.Length; stateIndex++)
            {
                var state = stateMachine.States[stateIndex];

                //Add this so the beginning of the state gets resolved
                rootElementList.Add(new MarkerElement(statesTocEntries[stateIndex], $"State '{state.Model.Name}'"));

                //Number of entry actions
                rootElementList.Add(bitConverter, (ushort) state.EntryActions.Length, $"# of entry actions: {state.EntryActions.Length}");

                //Number of exit actions
                rootElementList.Add(bitConverter, (ushort) state.ExitActions.Length, $"# of exit actions: {state.ExitActions.Length}");
               
                //Number of transitions
                rootElementList.Add(bitConverter, (ushort) state.Transitions.Count, $"# of transitions ({state.Transitions.Count}) ");

                //Transition TOC
                var transitionsTocEntries = state.Transitions
                    .Select(t => new DelayedResolutionElementUshort(bitConverter, $"Address of Transition '{t.Model.Name}': {{0}}"))
                    .ToArray();

                rootElementList.AddRange(transitionsTocEntries);

                for (var transitionIndex = 0; transitionIndex < transitionsTocEntries.Length; transitionIndex++)
                {
                    var transition = state.Transitions[transitionIndex];
                    var transitionToc = transitionsTocEntries[transitionIndex];

                    rootElementList.Add(new MarkerElement(transitionToc, $"Transition '{transition.Model.Name}'"));

                    //Add the target state index
                    rootElementList.Add(new SimpleElement(bitConverter.GetBytes((ushort)transition.Target.Index), $"Target state index: {transition.Target.Index}"));

                    var instructionFactory = new InstructionFactory();

                    //Get the instructions
                    Instruction[] instructions = instructionFactory.GetInstructions(transition.Model.Condition, inputs);
                    
                    //Add the number of instructions
                    rootElementList.Add(new SimpleElement(bitConverter.GetBytes((ushort)instructions.Length), $"# of instructions: {instructions.Length}"));

                    //Add the instructions
                    foreach (var instruction in instructions)
                    {
                        rootElementList.Add(new SimpleElement(new byte[] {instruction.ToByte()}, instruction.ToString()));
                    }

                    //Pad the list of instructions
                    if (instructions.Length % 2 != 0)
                    {
                        rootElementList.Add(new SimpleElement(new byte[] { 0 }, "Padding"));
                    }

                    //Add the number of actions
                    rootElementList.Add(new SimpleElement(bitConverter.GetBytes((ushort)transition.Actions.Length), $"# of transition actions: {transition.Actions.Length}"));

                    foreach (var action in transition.Actions)
                    {
                        rootElementList.Add(new SimpleElement(bitConverter.GetBytes((ushort) action.Index), $"Action [{action.Index}]"));
                    }
                }

                //Entry actions
                foreach (var action in state.EntryActions)
                {
                    rootElementList.Add(new SimpleElement(bitConverter.GetBytes((ushort)action.Index), $"Entry Action [{action.Index}]"));
                }

                //Exit actions
                foreach (var action in state.ExitActions)
                {
                    rootElementList.Add(new SimpleElement(bitConverter.GetBytes((ushort)action.Index), $"Exit Action [{action.Index}]"));
                }
            }

            //Resolve
            int size = rootElementList.ResolveCore(0);

            //Create the element write target
            var elementWriteTarget = new ElementWriteTarget(size);

            //Write it
            rootElementList.Write(elementWriteTarget);

            //Get the bytes
            var segments = elementWriteTarget.GetSegments();

            //We're done here
            return new StateMachineBinaryGenerationResult(stateMachine, segments);
        }
    }
}