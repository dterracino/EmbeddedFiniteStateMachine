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

        public string HeaderFileName
        {
            get
            {
                var temp = Model.GenerationOptions.HeaderFilePath.Split('\\');

                return temp[temp.Length - 1];
            }
        }

        public string CodeFileName
        {
            get
            {
                var temp = Model.GenerationOptions.CodeFilePath.Split('\\');
                return temp[temp.Length - 1];
            }
        }

        public string ProjectName
        {
            get { return CodeFileName.Split('.')[0]; }
        }

        public StateMachineGenerationModel[] StateMachinesGenerationModel { get; }
    }
}