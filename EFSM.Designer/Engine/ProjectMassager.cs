using System;
using EFSM.Domain;

namespace EFSM.Designer.Engine
{
    public static class ProjectMassager
    {
        public static void Massage(this StateMachineProject project)
        {
            if (project == null)
                project = new StateMachineProject();

            //options
            if (project.GenerationOptions == null)
            {
                project.GenerationOptions = new GenerationOptions();
            }

            //State machines
            if (project.StateMachines == null)
                project.StateMachines = new StateMachine[] { };

            //State Machine
            foreach (var stateMachine in project.StateMachines)
            {
                if (stateMachine.Transitions == null)
                {
                    stateMachine.Transitions = new StateMachineTransition[] { };
                }
                else
                {
                    //Transitions
                    foreach (var transition in stateMachine.Transitions)
                    {
                        if (transition.TransitionActions == null)
                        {
                            transition.TransitionActions = new Guid[] { };
                        }
                    }
                }

                //Inputs
                if (stateMachine.Inputs == null)
                {
                    stateMachine.Inputs = new StateMachineInput[] { };
                }

                //Outputs
                if (stateMachine.Actions == null)
                {
                    stateMachine.Actions = new StateMachineOutputAction[] { };
                }

                //States
                if (stateMachine.States == null)
                {
                    stateMachine.States = new State[] { };
                }
                else
                {
                    foreach (var state in stateMachine.States)
                    {
                        if (state.EntryActions == null)
                        {
                            state.EntryActions = new Guid[] { };
                        }

                        if (state.ExitActions == null)
                        {
                            state.ExitActions = new Guid[] { };
                        }
                    }
                }
            }
        }
    }
}