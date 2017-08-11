using System;
using System.Collections.Generic;
using EFSM.Domain;

namespace EFSM.Generator.Console
{
    /// <summary>
    /// Generates some simple state machines for testing.
    /// </summary>
    public static class SampleProjectFactory
    {
        public static StateMachineProject CreateSimple()
        {
            Guid state1Id = new Guid("3757ee04-6b20-4f53-8ad9-08d68ad10a16");
            Guid state2Id = new Guid("c5063b61-c6a7-48f0-a895-b604e46d4bcb");

            var state1 = new State()
            {
                Id = state1Id,
                Name = "Start State",
                EntryActions = new Guid[] {}
            };

            var state2 = new State()
            {
                Id = state2Id,
                Name = "End State",
                EntryActions = new Guid[] { },
            };

            var project = new StateMachineProject()
            {
                StateMachines = new StateMachine[]
                {
                    new StateMachine
                    {
                        States = new State[]
                        {
                            state1,
                            state2
                        }
                    }
                }
            };

            return project;
        }
    }
}