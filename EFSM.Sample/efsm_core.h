#ifndef EFSM_CORE_H
#define EFSM_CORE_H

#include <stdint.h>

#define EFSM_INPUT_BUFFER_NUMBER_OF_ELEMENTS                                            32
#define EFSM_WORKSPACE_NUMBER_OF_ELEMENTS												32

#define EFSM_TRANSITION_NONE															0
#define EFSM_TRANSITION_REQUIRED														1
#define EFSM_TRANSITION_INDEX_DEFAULT													0

/*Defines related to EFSM boolean operations on inputs.*/

#define EFSM_OPCODE_MASK_PUSH															0x80
#define EFSM_OPCODE_MASK_INPUT_BUFFER_INDEX                                             0x7f
#define EFSM_OPCODE_OR																	0x01
#define EFSM_OPCODE_AND																	0x02
#define EFSM_OPCODE_NOT																	0x03

#define EFSM_MIN_WORKSPACE_INDEX_FOR_OR_OPERATION                                       2
#define EFSM_MIN_WORKSPACE_INDEX_FOR_AND_OPERATION                                      2
#define EFSM_MIN_WORKSPACE_INDEX_FOR_NOT_OPERATION                                      1

#define EFSM_OR_OPERATION_STORE_OFFSET                                                  2
#define EFSM_OR_OPERATION_OPERAND_A_OFFSET                                              1
#define EFSM_OR_OPERATION_OPERAND_B_OFFSET                                              2

#define EFSM_AND_OPERATION_STORE_OFFSET                                                 2
#define EFSM_AND_OPERATION_OPERAND_A_OFFSET                                             1
#define EFSM_AND_OPERATION_OPERAND_B_OFFSET                                             2

#define EFSM_NOT_OPERATION_STORE_OFFSET                                                 1
#define EFSM_NOT_OPERATION_OPERAND_OFFSET                                               1

#define EFSM_WORKSPACE_INDEX_FOR_RESULT                                                 0

#define EFSM_PREVIOUS_STATE_UNDEFINED													0xffff


/*
------------------------------------------------------------------------------------------
Definitions characterizing the arrangement of information in the debug buffer.
*/
#define EFSM_DEBUG_PROTOCOL_INDEX_POSTING_STATUS										0

#define EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_ABSOLUTE_INSTANCE_INDEX						1
#define EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_NUMBER_OF_INPUTS							2
#define EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START								3

#define EFSM_DEBUG_PROTOCOL_INDEX_RESTART_CMD_ABSOLUTE_INSTANCE_INDEX					1 

#define EFSM_DEBUG_PROTOCOL_INDEX_RESP_DIAG_ABSOLUTE_INSTANCE_INDEX						1
#define EFSM_DEBUG_PROTOCOL_INDEX_RESP_DIAG_CURRENT_STATE								2
#define EFSM_DEBUG_PROTOCOL_INDEX_RESP_DIAG_PREVIOUS_STATE								3
#define EFSM_DEBUG_PROTOCOL_INDEX_RESP_DIAG_TOTAL_NUMBER_OF_INPUTS						4
#define EFSM_DEBUG_PROTOCOL_INDEX_RESP_DIAG_INPUTS_START								5

/*EFSM Debug Posting Statuses*/
#define EFSM_DEBUG_PROTOCOL_POSTING_STATUS_MASTER_COMMAND_CYCLE							1
#define EFSM_DEBUG_PROTOCOL_POSTING_STATUS_MASTER_COMMAND_RESTART						2
#define EFSM_DEBUG_PROTOCOL_POSTING_STATUS_SLAVE_RESPONSE_DIAGNOSTICS					3
#define EFSM_DEBUG_PROTOCOL_POSTING_STATUS_NULL											0

/*Related to EFSM debugging and diagnostics.*/
#define EFSM_DEBUG_BUFFER_SIZE															10
#define EFSM_DEBUG_INTERFACE_FILE_NAME													debugFileName

/*Defines related to EFSM diagnostics.*/
#define EFSM_MB_DIAGNOSTICS_NUM_REGISTERS_PER_INSTANCE                                  1
#define EFSM_DIAGNOSTICS_INPUT_READINGS_BUFFER_SIZE                                     32
#define EFSM_SOFT_INPUTS_BUFFER_NUMBER_OF_ELEMENTS                                      32

#define EFSM_DIAGNOSTICS_BUFFER_NUMBER_OF_ELEMENTS										1000

#define EFSM_DIAGNOSTICS_BUFFER_CTRL_CHAR_CONTINUE										1
#define EFSM_DIAGNOSTICS_BUFFER_CTRL_CHAR_STOP											0

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
	void(**Actions)(uint8_t i);
	uint8_t(**InputQueries)(uint8_t i);
	uint8_t indexOnEfsmType;
	uint16_t numberOfStates;
	uint8_t totalNumberOfInputs;

	/*
	These parameters are subject to change during operation, as they are
	dependent on the current state.
	*/
	uint16_t previousState;
	uint16_t state;
	uint16_t numberOfInputs;
	uint16_t numberOfTransitions;
	uint16_t baseIndexCurrentState;			/*Holds the index in the EFSM binary where the current states' data begins.*/
	uint16_t baseIndexStateHeader;			/*Holds the index in the EFSM binary of the current states' header data.*/
	uint16_t baseIndexIqfnData;                     /*Holds the index in the EFSM binary of the current state's Input Query Function (IQFN) data.*/

	uint8_t inputBuffer[EFSM_INPUT_BUFFER_NUMBER_OF_ELEMENTS];
	uint8_t workspace[EFSM_WORKSPACE_NUMBER_OF_ELEMENTS];
}EFSM_INSTANCE;

typedef struct
{
	uint8_t status;
	uint16_t index;
}EFSM_EVAL_TRANSITIONS_RESULT;

typedef struct
{
	uint8_t debugBuffer[EFSM_DEBUG_BUFFER_SIZE];
	uint8_t debugBufferFrameSize;							/*The number of bytes currently relevant in .debugBuffer*/
	uint8_t postingStatus;
	uint8_t instanceAbsoluteIndex;
	uint8_t numberOfInputs;
}EFSM_DEBUG_CONTROL;

extern EFSM_DEBUG_CONTROL efsmDebugControl;

extern EFSM_INSTANCE * efsmInstanceArray[];

extern char debugFileName[];

//void EFSM_InitializeProcess();
void EFSM_InitializeInstance(EFSM_INSTANCE * efsmInstance, EFSM_BINARY * efsmBinary, void(**Actions)(uint8_t i), uint8_t(**InputQueries)(uint8_t i), uint8_t indexOnEfsmType);
void EfsmDebugManager();
void EFSM_Process();

#endif

