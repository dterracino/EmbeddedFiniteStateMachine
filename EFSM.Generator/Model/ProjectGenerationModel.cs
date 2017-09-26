using EFSM.Domain;

namespace EFSM.Generator.Model
{
    internal class ProjectGenerationModel : GenerationModelBase<StateMachineProject>
    {
        public ProjectGenerationModel(StateMachineProject model, StateMachineGenerationModel[] stateMachinesGenerationModel) 
            : base(model)
        {
            
            StateMachinesGenerationModel = stateMachinesGenerationModel;
        }


        public StateMachineGenerationModel[] StateMachinesGenerationModel { get; }
    }
}