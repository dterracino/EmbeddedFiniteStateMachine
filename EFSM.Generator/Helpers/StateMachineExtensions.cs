using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFSM.Domain;

namespace EFSM.Generator
{
    public static class StateMachineExtensions
    {
        /*Extension methods for objects of class "StateMachine".*/
        public static bool DoesInputExist(this StateMachine stateMachine, Guid id)
        {
            foreach (var input in stateMachine.Inputs)
            {
                if (id == input.Id)
                    return true;
            }

            return false;
        }

        public static StateMachineInput GetInput(this StateMachine stateMachine, Guid id)
        {
            foreach (var input in stateMachine.Inputs)
            {
                if (id == input.Id)
                    return input;
            }

            return null;
        }

        public static int? GetInputIndex(this StateMachine stateMachine, Guid id)
        {
            for (int i = 0; i < stateMachine.Inputs.Length; i++)
            {
                if (stateMachine.Inputs[i].Id == id)
                    return i;
            }

            return null;
        }

        public static State GetState(this StateMachine stateMachine, Guid id)
        {
            foreach (var state in stateMachine.States)
            {
                if (state.Id == id)
                    return state;
            }

            return null;
        }

        public static int? GetActionReferenceIndex(this StateMachine stateMachine, Guid actionId)
        {
            for (int i = 0; i < stateMachine.Actions.Length; i++)
            {
                if (stateMachine.Actions[i].Id == actionId)
                    return i;
            }

            return null;
        }

        public static string GetActionName(this StateMachine stateMachine, Guid actionId)
        {
            foreach (var a in stateMachine.Actions)
            {
                if (a.Id == actionId)
                    return a.Name;
            }

            return null;
        }

        public static UInt16 GetStateIndex(this StateMachine stateMachine, Guid id)
        {
            for (UInt16 i = 0; i < stateMachine.States.Length; i++)
            {
                if (stateMachine.States[i].Id == id)
                {
                    return i;
                }
            }

            return 0;
        }    
    }
}
