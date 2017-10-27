
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

typedef struct
{
	uint16_t * data;
	uint16_t binaryId;
}EFSM_BINARY;

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
	uint16_t baseIndexCurrentState;			/*Holds the SMB index where the current states' data begins.*/
	uint16_t baseIndexStateHeaderAndToc;	/*Holds the SMB index of the current states' header data.*/
	uint16_t baseIndexIqfnData;				/*Holds the SMB index of the current state's Input Query Function (IQFN) data.*/

}EFSM_INSTANCE;

typedef struct
{
	EFSM_INT result;
	EFSM_INT transitionIndex;
}EFSM_INPUTS_EVALUATION_RESULT;

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

#endif

