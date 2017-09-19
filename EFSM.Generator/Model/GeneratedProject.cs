using EFSM.Domain;

namespace EFSM.Generator.Model
{
    internal class GeneratedProject : GeneratedBase<StateMachineProject>
    {
        public GeneratedProject(StateMachineProject model, GeneratedStateMachine[] stateMachines) 
            : base(model)
        {
            
            StateMachines = stateMachines;
        }


        public GeneratedStateMachine[] StateMachines { get; }
    }
}