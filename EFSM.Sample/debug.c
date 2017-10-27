#include "efsm_core.h"
#include <stdio.h>
#include <stdint.h>

void DisplayInstanceData(EFSM_INSTANCE * efsmInstance)
{
	printf("\nGeneral Information\n\n");
	printf("Associated binary: %d\n", efsmInstance->efsmBinary->id);
	printf("numberOfStates: %d\n", efsmInstance->numberOfStates);
	printf("totalNumberOfInputs: %d\n", efsmInstance->totalNumberOfInputs);
	
	printf("\nState Specific Information\n\n");

	printf("state: %d\n", efsmInstance->state);
	printf("numberOfInputs: %d\n", efsmInstance->numberOfInputs);
	printf("numberOfTransitions: %d\n", efsmInstance->numberOfTransitions);
	printf("baseIndexCurrentState: %d\n", efsmInstance->baseIndexCurrentState);
	printf("baseIndexStateHeader: %d\n", efsmInstance->baseIndexStateHeader);
	printf("baseIndexIqfnData: %d\n", efsmInstance->baseIndexIqfnData);
}

#define TEST_BIN_ELEMENT_UNKNOWN							0xff

#define TEST_BIN_0_NUMBER_OF_STATES							2
#define TEST_BIN_0_TOTAL_NUMBER_OF_INPUTS					1
#define TEST_BIN_0_INITIAL_STATE							1
#define TEST_BIN_0_STATE_0_EFSM_BIN_INDEX					5
#define TEST_BIN_0_STATE_1_EFSM_BIN_INDEX					TEST_BIN_ELEMENT_UNKNOWN
#define TEST_BIN_0_STATE_0_NUMBER_OF_INPUTS					1
#define TEST_BIN_0_STATE_0_NUMBER_OF_TRANSITIONS			1
#define TEST_BIN_0_STATE_0_EFSM_BIN_INDEX_OF_IQFN_DATA		10
#define TEST_BIN_0_STATE_0_FRARR_GENERAL_ACTION				0	/*State id is the same.*/
#define TEST_BIN_0_STATE_0_BIN_INDEX_TRAN_0_DATA			TEST_BIN_ELEMENT_UNKNOWN
#define TEST_BIN_0_STATE_0_IQFN_0_FRARR_INDEX				2	/*Corresponds to input 2.*/

uint16_t testBinary0Data[] =
{
	TEST_BIN_0_NUMBER_OF_STATES,
	TEST_BIN_0_TOTAL_NUMBER_OF_INPUTS,
	TEST_BIN_0_INITIAL_STATE,
	TEST_BIN_0_STATE_0_EFSM_BIN_INDEX,
	TEST_BIN_0_STATE_1_EFSM_BIN_INDEX,
	TEST_BIN_0_STATE_0_NUMBER_OF_INPUTS,
	TEST_BIN_0_STATE_0_NUMBER_OF_TRANSITIONS,
	TEST_BIN_0_STATE_0_EFSM_BIN_INDEX_OF_IQFN_DATA,
	TEST_BIN_0_STATE_0_FRARR_GENERAL_ACTION,
	TEST_BIN_0_STATE_0_BIN_INDEX_TRAN_0_DATA,
	TEST_BIN_0_STATE_0_IQFN_0_FRARR_INDEX
};

EFSM_BINARY testBinary0;

Test0Init()
{
	testBinary0.id = 4321;
	testBinary0.data = testBinary0Data;
}