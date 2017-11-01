#include "efsm.h"
#include "stdint.h"
#include "efsm_core.h"
#include "efsm_binary_protocol.h"

uint16_t efsmBinary0Raw[EFSM_BINARY_0_SIZE];
EFSM_BINARY efsmBinary0 = { .data = efsmBinary0Raw,.id = 0 };

uint16_t efsmBinary1Raw[EFSM_BINARY_1_SIZE];
EFSM_BINARY efsmBinary1 = { .data = efsmBinary1Raw,.id = 1 };

void(*Actions0[EFSM_NUM_ACTIONS_FOR_ID_0])();
void(*Actions1[EFSM_NUM_ACTIONS_FOR_ID_1])();

uint8_t(*InputQueries0[EFSM_NUM_INPUTS_FOR_ID_0])();
uint8_t(*InputQueries1[EFSM_NUM_INPUTS_FOR_ID_1])();

EFSM_INSTANCE * efsmInstanceArray[EFSM_NUM_STATE_MACHINES];

EFSM_INSTANCE efsm0;
EFSM_INSTANCE efsm1;

void EFSM_EvaluateInputs(EFSM_INSTANCE * efsmInstance)
{
	uint8_t numberOfInputs = EFSM_GetNumberIqfnsCurrentState(efsmInstance);
	uint8_t frarrInputIndex;

	/*Evaluate each input, and buffer the results.*/
	for (uint8_t i = 0; i < numberOfInputs; i++)
	{
		/*Get the index of the relevant function pointer in the input function referance array (FRARR).*/
		frarrInputIndex = efsmInstance->efsmBinary->data[efsmInstance->baseIndexIqfnData + i];

		/*Run the input query and buffer the result.*/
		efsmInstance->inputBuffer[i] = efsmInstance->InputQueries[frarrInputIndex]();
	}
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
Returns the starting state recorded in the EFSM binary. 
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

/*
Returns the EFSM binary index corresponding the start of header data for a given state.
*/
uint16_t EFSM_GetBaseIndexForStateHeader(uint16_t stateId, EFSM_BINARY * efsmBinary)
{
	return efsmBinary->data[EFSM_BIN_INDEX_STATE_DATA_TABLE_OF_CONTENTS + stateId];
}

/*
Returns the EFSM binary index corresponding to the start of input query function (IQFN)
data for a given state. The state itself is not explicity submitted as an argument, but
rather the base index of the header data for the state whose IQFN's need to be accessed. 
*/
uint16_t EFSM_GetBaseIndexIqfnData(uint16_t stateHeaderBaseIndex, EFSM_BINARY * efsmBinary)
{		
	return efsmBinary->data[stateHeaderBaseIndex + EFSM_STATE_HEADER_OFFSET_BIN_INDEX_OF_IQFN_DATA];
}

/*
Returns the number of input query functions (IQFN's) relevant to the current state of the 
indicated EFSM. This function differs from those used during initialization in that it 
accepts only an argument indicating a particular state machine. It is assumed that the 
operational parameters of the state machine instance are up to date, and as such, no further
arguments are required in the call.
*/
uint16_t EFSM_GetNumberIqfnsCurrentState(EFSM_INSTANCE * efsmInstance)
{
	return efsmInstance->efsmBinary->data[efsmInstance->baseIndexStateHeader + EFSM_STATE_HEADER_OFFSET_NUMBER_OF_INPUTS];
}

/*
Returns the base index in the EFSM binary for the instructions relevant to the given transition index.
*/
uint16_t EFSM_GetBaseIndexForTransition(EFSM_INSTANCE * efsmInstance, uint16_t transition)
{
	/*Get the EFSM binary index where the EFSM binary index for the given transition is stored in the state header.*/
	uint16_t localIndex = efsmInstance->baseIndexStateHeader + transition + EFSM_STATE_HEADER_OFFSET_BIN_INDEX_OF_TRANSITION_DATA_BASE_INDICES;
	
	/*Return the base EFSM binary index for the actual transition instructions, as stored in the state header.*/
	return efsmInstance->efsmBinary->data[localIndex];
}

/*
Returns the number of opcodes associated with the boolean operations on the input values for the particular
transition. 
*/
uint16_t EFSM_GetNumberOfOpcodes(EFSM_INSTANCE * efsmInstance, uint16_t baseIndexForTransition)
{
	return efsmInstance->efsmBinary->data[baseIndexForTransition + EFSM_TRN_INSTR_OFFSET_NUMBER_OF_OPCODES];
}

#define EFSM_OPCODE_MASK_PUSH								0x80
#define EFSM_OPCODE_MASK_INPUT_BUFFER_INDEX					0x7f
#define EFSM_OPCODE_OR										0x01
#define EFSM_OPCODE_AND										0x02
#define EFSM_OPCODE_NOT										0x03

#define EFSM_MIN_WORKSPACE_INDEX_FOR_OR_OPERATION			2
#define EFSM_MIN_WORKSPACE_INDEX_FOR_AND_OPERATION			2
#define EFSM_MIN_WORKSPACE_INDEX_FOR_NOT_OPERATION			1

#define EFSM_OR_OPERATION_STORE_OFFSET						2
#define EFSM_OR_OPERATION_OPERAND_A_OFFSET					1
#define EFSM_OR_OPERATION_OPERAND_B_OFFSET					2

#define EFSM_AND_OPERATION_STORE_OFFSET						2
#define EFSM_AND_OPERATION_OPERAND_A_OFFSET					1
#define EFSM_AND_OPERATION_OPERAND_B_OFFSET					2

#define EFSM_NOT_OPERATION_STORE_OFFSET						1
#define EFSM_NOT_OPERATION_OPERAND_OFFSET					1

uint8_t EFSM_PerformLogicOpsOnInputs(uint16_t * opcodes, uint16_t numberOfOpcodes, uint8_t * inputBuffer, EFSM_WORKSPACE_UINT8 workspace)
{	
	uint8_t opcode;
	uint16_t opcodeIndex = 0;
	uint16_t workspaceIndex = 0;
	uint8_t operationResult;

	for (opcodeIndex = 0; opcodeIndex < numberOfOpcodes; opcodeIndex++)
	{
		opcode = OpcodeFetch(opcodeIndex, opcodes);		
		
		if (opcode & EFSM_OPCODE_MASK_PUSH)
		{			
			workspace.buffer[workspaceIndex++] = inputBuffer[opcode & EFSM_OPCODE_MASK_INPUT_BUFFER_INDEX];
		}
		else if ((opcode == EFSM_OPCODE_OR) && (workspaceIndex >= EFSM_MIN_WORKSPACE_INDEX_FOR_OR_OPERATION))
		{
			operationResult = ((workspace.buffer[workspaceIndex - EFSM_OR_OPERATION_OPERAND_A_OFFSET]) || 
				(workspace.buffer[workspaceIndex - EFSM_OR_OPERATION_OPERAND_B_OFFSET]));
			
			workspace.buffer[workspaceIndex - EFSM_OR_OPERATION_STORE_OFFSET] = operationResult;
		}
		else if ((opcode == EFSM_OPCODE_AND) && (workspaceIndex >= EFSM_MIN_WORKSPACE_INDEX_FOR_AND_OPERATION))
		{
			operationResult = ((workspace.buffer[workspaceIndex - EFSM_AND_OPERATION_OPERAND_A_OFFSET]) &&
				(workspace.buffer[workspaceIndex - EFSM_AND_OPERATION_OPERAND_B_OFFSET));

			workspace.buffer[workspaceIndex - EFSM_AND_OPERATION_STORE_OFFSET] = operationResult;
		}
		else if ((opcode == EFSM_OPCODE_NOT) && (workspaceIndex >= EFSM_MIN_WORKSPACE_INDEX_FOR_NOT_OPERATION))
		{
			operationResult = !(workspace.buffer[workspaceIndex - EFSM_NOT_OPERATION_OPERAND_OFFSET]);
			workspace.buffer[workspaceIndex - EFSM_NOT_OPERATION_STORE_OFFSET];
		}		
	}
	
	return operationResult;
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

	/*Start reading the EFSM binary and initializing parameters.*/
	efsmInstance->numberOfStates = EFSM_GetNumberOfStates(efsmBinary);
	efsmInstance->totalNumberOfInputs = EFSM_GetTotalNumberOfInputs(efsmBinary);
	efsmInstance->state = EFSM_GetInitialStateIdentifier(efsmBinary);
	efsmInstance->baseIndexCurrentState = EFSM_GetBaseIndexForState(efsmInstance->state, efsmBinary);
	efsmInstance->baseIndexStateHeader = EFSM_GetBaseIndexForStateHeader(efsmInstance->state, efsmBinary);
	efsmInstance->baseIndexIqfnData = EFSM_GetBaseIndexIqfnData(efsmInstance->baseIndexStateHeader, efsmBinary);
}

void EFSM_InitializeProcess()
{
	/*Load the instance pointer array.*/
	efsmInstanceArray[0] = &efsm0;
	//efsmInstanceArray[1] = &efsm1;

	/*Initialize instances.*/
	EFSM_InitializeInstance(&efsm0, &efsmBinary0, Actions0, InputQueries0);
	//EFSM_InitializeInstance(&efsm1, &efsmBinary1, Actions1, InputQueries1);
}

EFSM_EVAL_TRANSITIONS_RESULT EFSM_EvaluateTransitions(EFSM_INSTANCE * efsmInstance)
{
	uint16_t baseIndexForTransition = 0;
	uint16_t numberOfOpcodes = 0;
	uint16_t * opcodes;
	EFSM_EVAL_TRANSITIONS_RESULT result = { .status = EFSM_TRANSITION_RESULT_NONE,.index = EFSM_TRANSITION_INDEX_DEFAULT };

	/*
	The general purpose of a workspace is to provide a pointer to a block of memory, WITH an associated size. In this
	case, the workspace variable is intended for use in performing operations found in an EFSM binary.
	*/
	EFSM_WORKSPACE_UINT8 workspace = { .buffer = efsmInstance->workspace,.numberOfElements = EFSM_WORKSPACE_NUMBER_OF_ELEMENTS };
		
	for (uint16_t transition = 0; transition < efsmInstance->numberOfTransitions; transition++)
	{
		/*Get the base EFSM binary index for the transition data.*/
		baseIndexForTransition = EFSM_GetBaseIndexForTransition(efsmInstance, transition);		

		/*Get a pointer to the relevant block of opcodes in the EFSM binary.*/
		opcodes = &(efsmInstance->efsmBinary[baseIndexForTransition + EFSM_TRN_INSTR_OFFSET_OPCODES]);

		/*Get the number of opcodes.*/
		numberOfOpcodes = EFSM_GetNumberOfOpcodes(efsmInstance, baseIndexForTransition);		
		
		if (EFSM_PerformLogicOpsOnInputs(opcodes, numberOfOpcodes, efsmInstance->inputBuffer, workspace))
		{
			result.status = EFSM_TRANSITION_RESULT_TRANSITION_REQUIRED;
			result.index = transition;
			return result;
		}
	}

	return result;
}

void EFSM_Execute(EFSM_INSTANCE * efsmInstance)
{	
	/*Runs the input query functions (IQFNS's), as required by the current state, and buffers the results.*/
	EFSM_EvaluateInputs(efsmInstance);

	/*If a set of input conditions required for a transition have been met...*/
	if (EFSM_EvaluateTransitions(efsmInstance).status == EFSM_TRANSITION_RESULT_TRANSITION_REQUIRED)
	{
		/*Perform transition.*/
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