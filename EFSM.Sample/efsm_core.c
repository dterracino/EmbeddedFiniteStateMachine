#include "efsm.h"
#include "stdint.h"
#include "efsm_core.h"
#include "efsm_binary_protocol.h"

uint16_t efsmBinary0Raw[EFSM_BINARY_0_SIZE];
EFSM_BINARY efsmBinary0 = { .data = efsmBinary0Raw,.binaryId = 0 };

uint16_t efsmBinary1Raw[EFSM_BINARY_1_SIZE];
EFSM_BINARY efsmBinary1 = { .data = efsmBinary1Raw,.binaryId = 1 };

void(*Actions0[EFSM_NUM_ACTIONS_FOR_ID_0])();
void(*Actions1[EFSM_NUM_ACTIONS_FOR_ID_1])();

uint8_t(*InputQueries0[EFSM_NUM_INPUTS_FOR_ID_0])();
uint8_t(*InputQueries1[EFSM_NUM_INPUTS_FOR_ID_1])();

EFSM_INSTANCE * efsmInstanceArray[EFSM_NUM_STATE_MACHINES];

EFSM_INSTANCE efsm0;
EFSM_INSTANCE efsm1;

EFSM_INPUTS_EVALUATION_RESULT EFSM_EvaluateInputs(EFSM_INSTANCE * efsmInstance)
{
	EFSM_INPUTS_EVALUATION_RESULT res = { .result = 0,.transitionIndex = 0 };

	return res;
}

void ExecuteGenericStateAction(EFSM_INSTANCE * efsmInstance)
{

}

/*
Returns the number of states administered by the EFSM binary. References off of index 0
in the EFSM binary.
*/
uint16_t EFSM_GetNumberOfStates(EFSM_BINARY * efsmBinary)
{
	return efsmBinary->data[EFSM_BIN_INDEX_NUMBER_OF_STATES];
}

/*
Returns the total number of inputs administered by the EFSM binary. References off of
index 0 in the EFSM binary.
*/
uint16_t EFSM_GetTotalNumberOfInputs(EFSM_BINARY * efsmBinary)
{
	return efsmBinary->data[EFSM_BIN_INDEX_TOTAL_NUMBER_OF_INPUTS];
}

/*
Returns the starting state recorded in the EFSM binary. References off of index 0 in
the EFSM binary.
*/
uint16_t EFSM_GetInitialStateIdentifier(EFSM_BINARY * efsmBinary)
{
	return efsmBinary->data[EFSM_BIN_INDEX_INITIAL_STATE_IDENTIFIER];
}

/*
Returns the EFSM binary index where the data corresponding to a particular state begin.
*/
uint16_t EFSM_GetBaseIndexForState(uint16_t stateId, EFSM_BINARY * efsmBinary)
{
	return efsmBinary->data[EFSM_BIN_INDEX_STATE_DATA_TABLE_OF_CONTENTS + stateId];
}

uint16_t EFSM_GetBaseIndexForStateHeader(uint16_t stateId, EFSM_BINARY * efsmBinary)
{
	return efsmBinary->data[EFSM_BIN_INDEX_STATE_DATA_TABLE_OF_CONTENTS + stateId];
}

uint16_t EFSM_GetBaseIndexStateIqfnData(uint16_t stateId, EFSM_BINARY * efsmBinary)
{
	/*Need the index of the base state.*/

	/*Add that to the standard offset for this value and return.*/
	return 0;
}


/*
An instance of the EFSM_INSTANCE struct is what maintains operational/state information
for a single state machine. This function associates a given instance of the EFSM_INSTANCE
struct with an EFSM binary, and the EFSM binarys' corresponding function reference
arrays (for Actions and InputQueries). It also initializes all other members of the struct
instance.
*/
EFSM_InitializeInstance(EFSM_INSTANCE * efsmInstance, EFSM_BINARY * efsmBinary, void(**Actions)(), uint8_t(**InputQueries)())
{
	/*Pair the state machine instance with binary instructions and function reference arrays.*/
	efsmInstance->efsmBinary = efsmBinary;
	efsmInstance->Actions = Actions;
	efsmInstance->InputQueries = InputQueries;

	/*Start reading the SMB and initializing parameters.*/
	efsmInstance->numberOfStates = EFSM_GetNumberOfStates(efsmBinary);
	efsmInstance->totalNumberOfInputs = EFSM_GetTotalNumberOfInputs(efsmBinary);
	efsmInstance->state = EFSM_GetInitialStateIdentifier(efsmBinary);

	efsmInstance->baseIndexCurrentState = EFSM_GetBaseIndexForState(efsmInstance->state, efsmBinary);
	efsmInstance->baseIndexStateHeaderAndToc = EFSM_GetBaseIndexForStateHeader(efsmInstance->state, efsmBinary);
	efsmInstance->baseIndexIqfnData = EFSM_GetBaseIndexStateIqfnData(efsmInstance->state, efsmBinary);
}

void EFSM_InitializeProcess()
{
	/*Load the instance pointer array.*/
	efsmInstanceArray[0] = &efsm0;
	efsmInstanceArray[1] = &efsm1;

	/*Initialize instances.*/
	EFSM_InitializeInstance(&efsm0, &efsmBinary0, Actions0, InputQueries0);
	EFSM_InitializeInstance(&efsm1, &efsmBinary1, Actions1, InputQueries1);
}

void EFSM_Execute(EFSM_INSTANCE * efsmInstance)
{
	EFSM_INPUTS_EVALUATION_RESULT inputsEvaluationResult;
	/*Evaluate inputs.*/
	inputsEvaluationResult = EFSM_EvaluateInputs(efsmInstance);

	/*If a set of input conditions required for a transition have been met...*/
	if (inputsEvaluationResult.result)
	{
		/*Execute transition.*/
	}
	else
	{
		ExecuteGenericStateAction(efsmInstance);
	}
	/*
	If conditions for a transition are met, fire transition. Will need to execute the exit actions
	for the current state, update the current state, and execute the entry actions for the new state.
	*/
}

void EFSM_Process()
{
	unsigned char stateMachineIndex;
	EFSM_INSTANCE * efsmInstance;

	/*Consider each state machine.*/
	for (stateMachineIndex = 0; stateMachineIndex < EFSM_NUM_STATE_MACHINES; stateMachineIndex++)
	{
		/*Get a pointer to the state machine instance data per the stateMachineIndex.*/
		efsmInstance = efsmInstanceArray[stateMachineIndex];

		/*Run the state machine.*/
		EFSM_Execute(efsmInstance);
	}
}