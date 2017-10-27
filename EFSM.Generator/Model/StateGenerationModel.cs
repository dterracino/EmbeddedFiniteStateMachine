using System;
using System.Collections.Generic;
using EFSM.Domain;

namespace EFSM.Generator.Model
{
    internal class StateGenerationModel : IndexedBase<State>
    {
        public StateGenerationModel(
            State model, 
            int index, 
            StateMachine parent, 
            ActionReferenceGenerationModel[] entryActions, 
            ActionReferenceGenerationModel[] exitActions) 
            : base(model, index)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            if (entryActions == null) throw new ArgumentNullException(nameof(entryActions));
            if (exitActions == null) throw new ArgumentNullException(nameof(exitActions));
            Parent = parent;
            EntryActions = entryActions;
            ExitActions = exitActions;
        }

        public StateMachine Parent { get; }

        public ActionReferenceGenerationModel[] EntryActions { get; }

        public ActionReferenceGenerationModel[] ExitActions { get; }

        public override string IndexDefineName => $"EFSM_{Parent.Name.FixDefineName()}_INDEX";

        public List<TransitionGenerationModel> Transitions { get; } = new List<TransitionGenerationModel>();
    }
}