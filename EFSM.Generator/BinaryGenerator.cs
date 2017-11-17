using System;
using System.Linq;
using EFSM.Generator.Model;
using EFSM.OffsetWriter;
using MiscUtil.Conversion;
using System.Collections.Generic;
using EFSM.Domain;

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

            /*stateMachineResults is an array of StateMachineBinaryGenerationResults.*/
            var stateMachineResults = project.StateMachinesGenerationModel
                .Select(s => Generate(s, bitConverter))
                .ToArray();

            return new ProjectBinaryGenerationResult(stateMachineResults);
        }

        private StateMachineBinaryGenerationResult Generate(StateMachineGenerationModel stateMachine, EndianBitConverter bitConverter)
        {
            //var inputs = stateMachine
            //    .Inputs.Select(i => i.Model)
            //    .ToArray();

            var rootElementList = new ElementList();
            var rootElementList2 = new ElementList2();
                        
            rootElementList2.Add(bitConverter, (UInt16)stateMachine.States.Count, "Number of states");
            rootElementList2.Add(bitConverter, (UInt16)stateMachine.Model.Inputs.Length, "Total number of inputs.");
            rootElementList2.Add(bitConverter, (UInt16)0, "Initial state identifier.");                 

            var stateMachineBaseIndices = new List<DelayedResolutionElementUshort2>();

            foreach (var state in stateMachine.States)
            {
                stateMachineBaseIndices.Add(new DelayedResolutionElementUshort2(bitConverter, $"EFSM binary index of state {state.Name}"));                
            }

            foreach (var del in stateMachineBaseIndices)
            {
                rootElementList2.Add(del);
            }            

            for (var stateIndex = 0; stateIndex < stateMachine.States.Count; stateIndex++)
            {
                var state = stateMachine.States[stateIndex];

                rootElementList2.Add(new MarkerElement2(stateMachineBaseIndices[stateIndex], $"State {state.Name} base index."));
                rootElementList2.Add(bitConverter, (UInt16)state.inputList.Count, "Number of inputs.");
                rootElementList2.Add(bitConverter, (UInt16)state.transitionList.Count, "Number of transitions.");

                /*The base index of IQFN data.*/
                var binIndexOfIqfnData = new DelayedResolutionElementUshort2(bitConverter, $"EFSM binary index of IQFN data for state {state.Name}");
                rootElementList2.Add(binIndexOfIqfnData);

                var tocForTransitionData = new List<DelayedResolutionElementUshort2>();

                foreach (var t in state.transitionList)
                {
                    tocForTransitionData.Add(new DelayedResolutionElementUshort2(bitConverter, $"EFSM binary index of transition {t.Name} data."));
                    rootElementList2.Add(tocForTransitionData.Last());
                }

                rootElementList2.Add(new MarkerElement2(binIndexOfIqfnData, $"State {state.Name} start of IQFN data."));

                /*Add the IQFN function reference indices.*/
                foreach (var iqfn in state.inputList)
                {                
                    rootElementList2.Add(bitConverter, (UInt16)iqfn.FunctionReferenceIndex, $"IQFN function reference index for prototype {iqfn.Name}");
                }

                /*Add the transition data.*/
                for (int i = 0; i < state.transitionList.Count; i++)
                {
                    /*Resolve the tocForTransitionData DelayedResolution elements.*/
                    rootElementList2.Add(new MarkerElement2(tocForTransitionData[i], $"Transition {state.transitionList[i].Name} data."));

                    var binIndexForTrnActionsData = new DelayedResolutionElementUshort2(bitConverter, $"EFSM binary index of transition {state.transitionList[i].Name} actions data.");
                    rootElementList2.Add(binIndexForTrnActionsData);

                    rootElementList2.Add(bitConverter, state.transitionList[i].TargetStateIndex, $"Next state after transition.");
                    rootElementList2.Add(bitConverter, (UInt16)state.transitionList[i].Opcodes.Length, $"Number of opcodes.");
                    /*Add the opcodes.*/
                    for (int j = 0; j < state.transitionList[j].Opcodes.Length; j++)
                    {

                    }
                }
            }
            //Number of states
            //rootElementList.Add(bitConverter, (ushort) stateMachine.States.Length, $"# of states ({stateMachine.States.Length})");
            //rootElementList.Add(bitConverter, 0x4356, "An item value");
            //rootElementList2.Add(bitConverter, 0x4356, "Version 2");
            //rootElementList2.Add(bitConverter, (UInt16)stateMachine.States.Length, $"# of states ({stateMachine.States.Length})");
            //rootElementList2.Add(bitConverter, (UInt16)stateMachine.Inputs.Length, $"# of inputs ({stateMachine.Inputs.Length})");
            //rootElementList2.Add(bitConverter, (UInt16)stateMachine.States[0].Index, $"Initial state ({stateMachine.States[0].Index})");

            /*Create an array of DelayedResolutionElementUshort2's based on each state in stateMachine.*/
            //var statesTocEntries = stateMachine.States.Select(s => new DelayedResolutionElementUshort2(bitConverter, "EFSM index of state'" + s.Model.Name + "' [{0}]")).ToArray();
            //rootElementList2.AddRange(statesTocEntries);           

            /*Get */

            //for (var stateIndex = 0; stateIndex < stateMachine.States.Length; stateIndex++)
            //{
            //    var state = stateMachine.States[stateIndex];
                
            //    /*Get the number of transitions for the current state.*/
            //    //var numberOfTransitionsForState = state.Transitions.Count;

            //    ///*Build a list of inputs relevant to the current state.*/
            //    //var stateInputList = new List<EmbeddedInputModel>();

            //    //foreach (var transition in state.Parent.Transitions)
            //    //{
            //    //    /*Get a list of the inputs for that transition.*/
            //    //    var transitionInputList = transition.GetTransitionInputs();

            //    //    foreach (var transitionInput in transitionInputList)
            //    //    {
            //    //        if (!StateMachineTransition.IsInputInList(transitionInput.Id, stateInputList))
            //    //        {
            //    //            stateInputList.Add(new EmbeddedInputModel(transitionInput.Id));
            //    //        }
            //    //    }
            //    //}

            //    /*Get the number of inputs for the state.*/
            //    //var numberOfInputsForState = stateInputList.Count;

               
                
            //    //stateMachine.States[0].Parent.Transitions[0].

            //    ///*Create a marker element which correspondes to the previously created DelayedResolutionElementUshort2 by submitting the respective DelayedResolutionElementUshort2 as an argument for its reference.*/
            //    //rootElementList2.Add(new MarkerElement2(statesTocEntries[stateIndex], $"State '{state.Model.Name}'"));
                

            //    //rootElementList2.Add(bitConverter, (UInt16)numberOfInputsForState, "Number of inputs.");
            //    //rootElementList2.Add(bitConverter, (UInt16)numberOfTransitionsForState, "Number of transitions");
            //}


            /*Pack the number of states.*/
            //rootElementList2.Add(bitConverter, stateMachine.States.Length, $"# of states ({stateMachine.States.Length})");


            //Number of inputs (do we care about this?)
            //rootElementList.Add(bitConverter, (ushort) stateMachine.Inputs.Length, $"# of inputs ({stateMachine.Inputs.Length})");

            //States TOC
            //var statesTocEntries = stateMachine.States
            //    .Select(s => new DelayedResolutionElementUshort(bitConverter, "Address of '" + s.Model.Name  + "' [{0}]"))
            //    .ToArray();

            ////Add them 
            ////rootElementList.AddRange(statesTocEntries);

            //foreach (var stateTocEntry in statesTocEntries)
            //{
            //    rootElementList.Add(stateTocEntry);
            //}

            ////Add the actual states
            //for (var stateIndex = 0; stateIndex < stateMachine.States.Length; stateIndex++)
            //{
            //    var state = stateMachine.States[stateIndex];


            //    //Add this so the beginning of the state gets resolved
            //    rootElementList.Add(new MarkerElement(statesTocEntries[stateIndex], $"State '{state.Model.Name}'"));

            //    //Number of entry actions
            //    rootElementList.Add(bitConverter, (ushort) state.EntryActions.Length, $"# of entry actions: {state.EntryActions.Length}");

            //    //Number of exit actions
            //    rootElementList.Add(bitConverter, (ushort) state.ExitActions.Length, $"# of exit actions: {state.ExitActions.Length}");

            //    //Number of transitions
            //    rootElementList.Add(bitConverter, (ushort) state.Transitions.Count, $"# of transitions ({state.Transitions.Count}) ");

            //    //Transition TOC
            //    var transitionsTocEntries = state.Transitions
            //        .Select(t => new DelayedResolutionElementUshort(bitConverter, $"Address of Transition '{t.Model.Name}': {{0}}"))
            //        .ToArray();

            //    rootElementList.AddRange(transitionsTocEntries);

            //    for (var transitionIndex = 0; transitionIndex < transitionsTocEntries.Length; transitionIndex++)
            //    {
            //        var transition = state.Transitions[transitionIndex];
            //        var transitionToc = transitionsTocEntries[transitionIndex];

            //        rootElementList.Add(new MarkerElement(transitionToc, $"Transition '{transition.Model.Name}'"));

            //        //Add the target state index
            //        rootElementList.Add(new SimpleElement(bitConverter.GetBytes((ushort)transition.Target.Index), $"Target state index: {transition.Target.Index}"));

            //        var instructionFactory = new InstructionFactory();

            //        //Get the instructions
            //        Instruction[] instructions = instructionFactory.GetInstructions(transition.Model.Condition, inputs);

            //        //Add the number of instructions
            //        rootElementList.Add(new SimpleElement(bitConverter.GetBytes((ushort)instructions.Length), $"# of instructions: {instructions.Length}"));

            //        //Add the instructions
            //        foreach (var instruction in instructions)
            //        {
            //            rootElementList.Add(new SimpleElement(new byte[] {instruction.ToByte()}, instruction.ToString()));
            //        }

            //        //Pad the list of instructions
            //        if (instructions.Length % 2 != 0)
            //        {
            //            rootElementList.Add(new SimpleElement(new byte[] { 0 }, "Padding"));
            //        }

            //        //Add the number of actions
            //        rootElementList.Add(new SimpleElement(bitConverter.GetBytes((ushort)transition.Actions.Length), $"# of transition actions: {transition.Actions.Length}"));

            //        foreach (var action in transition.Actions)
            //        {
            //            rootElementList.Add(new SimpleElement(bitConverter.GetBytes((ushort) action.Index), $"Action [{action.Index}]"));
            //        }
            //    }

            //    //Entry actions
            //    foreach (var action in state.EntryActions)
            //    {
            //        rootElementList.Add(new SimpleElement(bitConverter.GetBytes((ushort)action.Index), $"Entry Action [{action.Index}]"));
            //    }

            //    //Exit actions
            //    foreach (var action in state.ExitActions)
            //    {
            //        rootElementList.Add(new SimpleElement(bitConverter.GetBytes((ushort)action.Index), $"Exit Action [{action.Index}]"));
            //    }
            //}

            //Resolve
            //int size = rootElementList.Resolve(0);
            int size2 = rootElementList2.Resolve(0);

            //Create the element write target
            //var elementWriteTarget = new ElementWriteTarget(size);
            var elementWriteTarget2 = new ElementWriteTarget2(size2);

            //Write it
            //rootElementList.Write(elementWriteTarget);
            rootElementList2.Write(elementWriteTarget2);

            //Get the bytes
            //BinarySegment[] segments = elementWriteTarget.GetSegments();
            BinarySegment2[] segments2 = elementWriteTarget2.GetSegments();
            //BinarySegment2[] segments2 = elementWriteTarget2.GetSegments();

            //BinarySegment[] segments = elementWriteTarget2.GetSegments();

            //We're done here
            return new StateMachineBinaryGenerationResult(stateMachine, segments2);
        }
    }
}