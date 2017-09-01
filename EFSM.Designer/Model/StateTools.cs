using EFSM.Designer.Metadata;
using EFSM.Domain;
using System.Collections.Generic;

namespace EFSM.Designer.Model
{
    public class StateTools
    {
        private readonly StateFactory[] _tools;
        private const string CategoryName = "States";

        public StateTools() => _tools = new[]
            {
                new StateFactory(CategoryName, "Initial State", () => new State()
                {
                    StateType = (int)StateType.Initial
                }),

                new StateFactory(CategoryName, "State", () => new State()
                {
                    StateType = (int)StateType.Normal
                }),
            };

        public IEnumerable<StateFactory> Tools
        {
            get { return _tools; }
        }
    }
}
