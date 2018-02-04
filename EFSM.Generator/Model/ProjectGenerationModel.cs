using EFSM.Domain;

namespace EFSM.Generator.Model
{
    public enum DebugMode { Embedded = 0, Desktop = 1};

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

        public bool DiagnosticsEnabled { get { return true; }}

        public byte DebuggingEnabled { get { return 0; } }
        public DebugMode DebugMode { get { return DebugMode.Desktop; } }

        public string DebugModeEmbeddedDefine { get { return "EFSM_DEBUG_MODE_EMBEDDED"; } }
        public string DebugModeDesktopDefine { get { return "EFSM_DEBUG_MODE_DESKTOP"; } }

        public string DebugModeDefine
        {
            get
            {
                if (DebugMode == DebugMode.Embedded)
                    return DebugModeEmbeddedDefine;

                return DebugModeDesktopDefine;
            }
        }
    

        public StateMachineGenerationModel[] StateMachinesGenerationModel { get; }
    }
}