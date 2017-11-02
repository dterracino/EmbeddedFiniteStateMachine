
#ifndef EFSM_CORE_H
#define EFSM_CORE_H

#include <stdint.h>
#include "efsm_generated.h"

#define EFSM_BINARY_0_SIZE										10
#define EFSM_BINARY_1_SIZE										10

#define EFSM_NUM_ACTIONS_FOR_ID_0								10
#define EFSM_NUM_ACTIONS_FOR_ID_1								10
#define EFSM_NUM_INPUTS_FOR_ID_0								10
#define EFSM_NUM_INPUTS_FOR_ID_1								10

#define EFSM_INPUT_BUFFER_NUMBER_OF_ELEMENTS					32
#define EFSM_WORKSPACE_NUMBER_OF_ELEMENTS						32

#define EFSM_TRANSITION_NONE									0
#define EFSM_TRANSITION_REQUIRED								1
#define EFSM_TRANSITION_INDEX_DEFAULT							0

/*Defines related to EFSM boolean operations on inputs.*/

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

#define EFSM_WORKSPACE_INDEX_FOR_RESULT						0

typedef struct
{
	uint16_t * data;
	uint16_t id;
}EFSM_BINARY;

typedef struct
{
	uint8_t * buffer;
	uint8_t numberOfElements;
}EFSM_WORKSPACE_UINT8;

typedef struct
{
	/*These parameters should not change after initialization.*/
	EFSM_BINARY * efsmBinary;
	void(**Actions)();
	uint8_t(**InputQueries)();
	uint16_t numberOfStates;
	uint8_t totalNumberOfInputs;

	/*
	These parameters are subject to change during operation, as they are
	dependent on the current state.
	*/
	uint16_t state;
	uint16_t numberOfInputs;
	uint16_t numberOfTransitions;
	uint16_t baseIndexCurrentState;			/*Holds the index in the EFSM binary where the current states' data begins.*/
	uint16_t baseIndexStateHeader;	/*Holds the index in the EFSM binary of the current states' header data.*/
	uint16_t baseIndexIqfnData;				/*Holds the index in the EFSM binary of the current state's Input Query Function (IQFN) data.*/

	uint8_t inputBuffer[EFSM_INPUT_BUFFER_NUMBER_OF_ELEMENTS];
	uint8_t workspace[EFSM_WORKSPACE_NUMBER_OF_ELEMENTS];
}EFSM_INSTANCE;

typedef struct
{
	uint8_t status;
	uint16_t index;
}EFSM_EVAL_TRANSITIONS_RESULT;

extern uint16_t efsmBinary0Raw[EFSM_BINARY_0_SIZE];
extern EFSM_BINARY efsmBinary0;

extern uint16_t efsmBinary1Raw[EFSM_BINARY_1_SIZE];
extern EFSM_BINARY efsmBinary1;

extern void(*Actions0[EFSM_NUM_ACTIONS_FOR_ID_0])();
extern void(*Actions1[EFSM_NUM_ACTIONS_FOR_ID_1])();

extern uint8_t(*InputQueries0[EFSM_NUM_INPUTS_FOR_ID_0])();
extern uint8_t(*InputQueries1[EFSM_NUM_INPUTS_FOR_ID_1])();

extern EFSM_INSTANCE * efsmInstanceArray[EFSM_NUM_STATE_MACHINES];

extern EFSM_INSTANCE efsm0;
extern EFSM_INSTANCE efsm1;

void EFSM_InitializeProcess();
void EFSM_InitializeInstance(EFSM_INSTANCE * efsmInstance, EFSM_BINARY * efsmBinary, void(**Actions)(), uint8_t(**InputQueries)());
void EFSM_Process();

#endif

