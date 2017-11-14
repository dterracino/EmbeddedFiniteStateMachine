using System;
using System.Collections.Generic;
using EFSM.Domain;

namespace EFSM.Generator.Model
{
    internal class StateGenerationModel
    {
        public StateGenerationModel()
        {

        }

        /*General information about the state.*/     
        public List<InputGenerationModel> inputList;
        public List<TransitionGenerationModel> transitionList;

        public TransitionGenerationModel Transitions;

        /*Inputs relevant to state.*/

        /*Transitions relevant to state.*/

    }    
}