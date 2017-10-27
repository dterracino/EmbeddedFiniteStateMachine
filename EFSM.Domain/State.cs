using System;

namespace EFSM.Domain
{
    public class State
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid[] EntryActions { get; set; }

        public Guid[] ExitActions { get; set; }

        //public StateMachineTransition[] Transitions { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public int StateType { get; set; }

    }
}