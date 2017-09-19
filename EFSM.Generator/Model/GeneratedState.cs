using System;
using System.Collections.Generic;
using EFSM.Domain;

namespace EFSM.Generator.Model
{
    internal class GeneratedState : IndexedBase<State>
    {
        public GeneratedState(
            State model, 
            int index, 
            StateMachine parent, 
            GeneratedActionReference[] entryActions, 
            GeneratedActionReference[] exitActions) 
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

        public GeneratedActionReference[] EntryActions { get; }

        public GeneratedActionReference[] ExitActions { get; }

        public override string IndexDefineName => $"EFSM_{Parent.Name.FixDefineName()}_INDEX";

        public List<GeneratedTransition> Transitions { get; } = new List<GeneratedTransition>();
    }
}