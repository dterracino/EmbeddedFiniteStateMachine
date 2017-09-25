using System;
using System.Linq;
using EFSM.Generator.Model;
using EFSM.OffsetWriter;
using MiscUtil.Conversion;

namespace EFSM.Generator
{
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
            var rootElementList = new ElementList();

            //Number of states
            rootElementList.Add(bitConverter, (ushort) stateMachine.States.Length, $"# of states ({stateMachine.States.Length})");

            //Number of inputs
            rootElementList.Add(bitConverter, (ushort) stateMachine.Inputs.Length, $"# of inputs ({stateMachine.Inputs.Length})");

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
               
                //Number of transitions
                rootElementList.Add(bitConverter, (ushort) state.Transitions.Count, "# of transitions ");

                //Transition TOC
                var transitionsTocEntries = state.Transitions
                    .Select(t => new DelayedResolutionElementUshort(bitConverter, $"Address of Transition '{t.Model.Name}' {{0}}"))
                    .ToArray();

                rootElementList.AddRange(transitionsTocEntries);

                for (var transitionIndex = 0; transitionIndex < transitionsTocEntries.Length; transitionIndex++)
                {
                    var transition = state.Transitions[transitionIndex];
                    var transitionToc = transitionsTocEntries[transitionIndex];

                    rootElementList.Add(new MarkerElement(transitionToc, $"Transition '{transition.Model.Name}'"));

                    //TODO: Add the number of instructions
                    rootElementList.Add(new SimpleElement(bitConverter.GetBytes((ushort)0), "# of opcodes"));

                    //TODO: Add the instructions necessary to execute the logical tree

                    //TODO: Add the target state index
                    rootElementList.Add(new SimpleElement(bitConverter.GetBytes((ushort) 0), "Target state index"));
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