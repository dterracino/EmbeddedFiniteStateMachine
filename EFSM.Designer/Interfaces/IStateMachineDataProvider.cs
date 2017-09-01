using EFSM.Domain;
using System;

namespace EFSM.Designer.Interfaces
{
    public interface IStateMachineDataProvider
    {
        StateMachine GetStateMachine(Guid stateMachineGuid);
    }
}
