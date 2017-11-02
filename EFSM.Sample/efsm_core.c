#include "efsm.h"
#include "stdint.h"
#include "efsm_core.h"
#include "efsm_binary_protocol.h"
#include "debug.c"

//EFSM_BINARY efsmBinary0 = { .data = testBinary0Data,.id = 0 };
void(*Actions0[EFSM_NUM_ACTIONS_FOR_ID_0])();
uint8_t(*InputQueries0[EFSM_NUM_INPUTS_FOR_ID_0])();

EFSM_INSTANCE * efsmInstanceArray[EFSM_NUM_STATE_MACHINES];

EFSM_INSTANCE efsm0;

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

/*
Evaluates every input relevant to the current state, and stores the results. 
*/
void EFSM_EvaluateInputs(EFSM_INSTANCE * efsmInstance)
{
	uint16_t numberOfInputs = 0;
	uint8_t frarrInputIndex = 0;

	numberOfInputs = EFSM_GetNumberIqfnsCurrentState(efsmInstance);

	/*Evaluate each input, and buffer the results.*/
	for (uint16_t i = 0; i < numberOfInputs; i++)
	{
		/*Get the index of the relevant function pointer in the input function referance array (FRARR).*/
		frarrInputIndex = efsmInstance->efsmBinary->data[efsmInstance->baseIndexIqfnData + i];

		/*Run the input query and buffer the result.*/
		efsmInstance->inputBuffer[i] = efsmInstance->InputQueries[frarrInputIndex]();
	}
}

/*
Returns a 1 byte opcode, as read from a buffer whose element size is 2 bytes. Each element of the buffer
contains two opcodes. The opcode with the lesser index is stored in the least significant byte of an 
element, and the opcode with the greater index is stored at the most significant byte of an element. 
*/
uint8_t OpcodeFetch(uint16_t * opcodes, uint16_t opcodeIndex)
{
	if (opcodeIndex % 2)
		return (uint8_t)((opcodes[opcodeIndex / 2] >> 8) & 0xff);

	return (uint8_t)(opcodes[opcodeIndex / 2] & 0xff);
}

/*
Performs boolean operations on a set of input values per a series of single byte opcodes. If the final result
of the operations is true, a one is returned. If the result is false, a zero is returned. 
*/
uint8_t EFSM_PerformLogicOpsOnInputs(uint16_t * opcodes, uint16_t numberOfOpcodes, uint8_t * inputBuffer, EFSM_WORKSPACE_UINT8 workspace)
{
	uint8_t opcode;
	uint16_t opcodeIndex = 0;
	uint16_t workspaceIndex = 0;
	uint8_t operationResult;
	uint8_t operandA = 0;
	uint8_t operandB = 0;

	for (opcodeIndex = 0; opcodeIndex < numberOfOpcodes; opcodeIndex++)
	{
		opcode = OpcodeFetch(opcodes, opcodeIndex);

		if (opcode & EFSM_OPCODE_MASK_PUSH)
		{
			workspace.buffer[workspaceIndex++] = inputBuffer[opcode & EFSM_OPCODE_MASK_INPUT_BUFFER_INDEX];
		}
		else if ((opcode == EFSM_OPCODE_OR) && (workspaceIndex >= EFSM_MIN_WORKSPACE_INDEX_FOR_OR_OPERATION))
		{
			operandA = workspace.buffer[workspaceIndex - EFSM_OR_OPERATION_OPERAND_A_OFFSET];
			operandB = workspace.buffer[workspaceIndex - EFSM_OR_OPERATION_OPERAND_B_OFFSET];
			operationResult = operandA || operandB;
			workspace.buffer[workspaceIndex - EFSM_OR_OPERATION_STORE_OFFSET] = operationResult;
			workspaceIndex--;
		}
		else if ((opcode == EFSM_OPCODE_AND) && (workspaceIndex >= EFSM_MIN_WORKSPACE_INDEX_FOR_AND_OPERATION))
		{
			operandA = workspace.buffer[workspaceIndex - EFSM_AND_OPERATION_OPERAND_A_OFFSET];
			operandB = workspace.buffer[workspaceIndex - EFSM_AND_OPERATION_OPERAND_B_OFFSET];
			operationResult = operandA && operandB;
			workspace.buffer[workspaceIndex - EFSM_AND_OPERATION_STORE_OFFSET] = operationResult;
			workspaceIndex--;
		}
		else if ((opcode == EFSM_OPCODE_NOT) && (workspaceIndex >= EFSM_MIN_WORKSPACE_INDEX_FOR_NOT_OPERATION))
		{
			operationResult = !(workspace.buffer[workspaceIndex - EFSM_NOT_OPERATION_OPERAND_OFFSET]);
			workspace.buffer[workspaceIndex - EFSM_NOT_OPERATION_STORE_OFFSET] = operationResult;
		}
	}

	return workspace.buffer[EFSM_WORKSPACE_INDEX_FOR_RESULT];
}

/*
An instance of the EFSM_INSTANCE struct is what maintains operational/state information
for a single state machine. This function associates a given instance of the EFSM_INSTANCE
struct with an EFSM binary, and the EFSM binarys' corresponding function reference
arrays (for Actions and InputQueries). It also initializes all other members of the struct
instance.
*/
void EFSM_InitializeInstance(EFSM_INSTANCE * efsmInstance, EFSM_BINARY * efsmBinary, void(**Actions)(), uint8_t(**InputQueries)())
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

/*
Gets things ready for calls to EFSM_Process().
*/
void EFSM_InitializeProcess()
{
	/*Load the instance pointer array.*/
	efsmInstanceArray[0] = &efsm0;	

	Test0Init();

	/*Initialize instances.*/
	EFSM_InitializeInstance(&efsm0, &test0Binary, Test0Actions, Test0Inputs);	
}

EFSM_EVAL_TRANSITIONS_RESULT EFSM_EvaluateTransitions(EFSM_INSTANCE * efsmInstance)
{
	uint16_t baseIndexForTransition = 0;
	uint16_t numberOfOpcodes = 0;
	uint16_t * opcodes;
	EFSM_EVAL_TRANSITIONS_RESULT result = { .status = EFSM_TRANSITION_NONE,.index = EFSM_TRANSITION_INDEX_DEFAULT };

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
		opcodes = &(efsmInstance->efsmBinary->data[baseIndexForTransition + EFSM_TRN_INSTR_OFFSET_OPCODES]);

		/*Get the number of opcodes.*/
		numberOfOpcodes = EFSM_GetNumberOfOpcodes(efsmInstance, baseIndexForTransition);		
		
		if (EFSM_PerformLogicOpsOnInputs(opcodes, numberOfOpcodes, efsmInstance->inputBuffer, workspace))
		{
			result.status = EFSM_TRANSITION_REQUIRED;
			result.index = transition;
			return result;
		}
	}

	return result;
}

uint16_t EFSM_GetBaseIndexForTrnActions(EFSM_INSTANCE * efsmInstance, uint16_t transitionIndex)
{
	uint16_t baseIndexForTransition = 0;
	baseIndexForTransition = EFSM_GetBaseIndexForTransition(efsmInstance, transitionIndex);

	return efsmInstance->efsmBinary->data[baseIndexForTransition + EFSM_TRN_INSTR_OFFSET_BIN_INDEX_FOR_TRN_ACTIONS_DATA];
}

uint16_t EFSM_GetNextState(EFSM_INSTANCE * efsmInstance, uint16_t transitionIndex)
{
	uint16_t baseIndexForTransition = 0; 
	baseIndexForTransition = EFSM_GetBaseIndexForTransition(efsmInstance, transitionIndex);

	return efsmInstance->efsmBinary->data[baseIndexForTransition + EFSM_TRN_INSTR_OFFSET_NEXT_STATE_AFTER_TRANSITION];
}

void EFSM_PeformTransition(EFSM_INSTANCE * efsmInstance, uint16_t transitionIndex)
{
	
	uint16_t baseIndexForTrnActions = 0;
	uint16_t numberOfTransitionActions = 0;
	uint16_t actionsArrayIndex = 0;
	uint16_t * binary = efsmInstance->efsmBinary->data;

	baseIndexForTrnActions = EFSM_GetBaseIndexForTrnActions(efsmInstance, transitionIndex);
	numberOfTransitionActions = binary[baseIndexForTrnActions + EFSM_TRN_ACTIONS_DATA_OFFSET_NUMBER_OF_TRN_ACTIONS];
	
	for (uint16_t actionIndex = 0; actionIndex < numberOfTransitionActions; actionIndex++)
	{
		actionsArrayIndex = binary[baseIndexForTrnActions + EFSM_TRN_ACTIONS_DATA_OFFSET_TRN_ACTIONS_ARR_INDICES + actionIndex];
		efsmInstance->Actions[actionsArrayIndex]();
	}

	/*Initialize the efsmInstance to the data in the next state.*/
	efsmInstance->state = EFSM_GetNextState(efsmInstance, transitionIndex);;
	efsmInstance->baseIndexCurrentState = EFSM_GetBaseIndexForState(efsmInstance->state, efsmInstance->efsmBinary);
	efsmInstance->baseIndexStateHeader = EFSM_GetBaseIndexForStateHeader(efsmInstance->state, efsmInstance->efsmBinary);
	efsmInstance->baseIndexIqfnData = EFSM_GetBaseIndexIqfnData(efsmInstance->baseIndexStateHeader, efsmInstance->efsmBinary);
}

void EFSM_Execute(EFSM_INSTANCE * efsmInstance)
{	
	EFSM_EVAL_TRANSITIONS_RESULT evalTransitionsResult;

	/*Runs the input query functions (IQFNS's), as required by the current state, and buffers the results.*/
	EFSM_EvaluateInputs(efsmInstance);

	evalTransitionsResult = EFSM_EvaluateTransitions(efsmInstance);

	/*If a set of input conditions required for a transition have been met...*/
	if (evalTransitionsResult.status == EFSM_TRANSITION_REQUIRED)
	{
		/*Perform transition.*/
		EFSM_PeformTransition(efsmInstance, evalTransitionsResult.index);
	}
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