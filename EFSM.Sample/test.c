#include "test.h"
#include "efsm_core.h"
#include <stdio.h>
#include <stdint.h>
#include "eval.h"

/******************************		General Material			******************************/

/*Input values.*/
uint8_t InputValues[10] = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

void SetInput(EFSM_INSTANCE * efsmInstance, uint8_t efsmInputNumber, uint8_t value)
{
	printf("Setting input %d to %s\n", efsmInputNumber, GetTrueOrFalseString(value));

	switch (efsmInputNumber)
	{
	case 0:
		//test0InputA = value != 0 ? 1 : 0;
		InputValues[efsmInputNumber] = value != 0 ? 1 : 0;
		break;
	case 1: 
		InputValues[efsmInputNumber] = value != 0 ? 1 : 0;
		break;

	case 2:
		InputValues[efsmInputNumber] = value != 0 ? 1 : 0;
		break;
		
	case 3:
		InputValues[efsmInputNumber] = value != 0 ? 1 : 0;
		break;

	case 4:
		InputValues[efsmInputNumber] = value != 0 ? 1 : 0;
		break;

	case 5:
		InputValues[efsmInputNumber] = value != 0 ? 1 : 0;
		break;

	default:
		break;
	}
}

void DisplayEfsmCondtion(EFSM_INSTANCE * efsmInstance)
{
	printf("current state: %d\n", efsmInstance->state);
}

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

void DisplayBufferContentsUint8(uint8_t * buffer, uint8_t numberOfElements, char * msg)
{
	printf("%s: {", msg);	

	for (int i = 0; i < numberOfElements; i++)
	{
		printf(" %d", buffer[i]);

		if (i < (numberOfElements - 1))
			printf(",");
	}

	printf(" }\n");
}

/******************************		Test 0 Material Start		******************************/

uint16_t test0BinaryData[] =
{
	TEST_BIN_0_NUMBER_OF_STATES,
	TEST_BIN_0_TOTAL_NUMBER_OF_INPUTS,
	TEST_BIN_0_INITIAL_STATE,
	TEST_BIN_0_STATE_0_EFSM_BIN_INDEX,
	TEST_BIN_0_STATE_1_EFSM_BIN_INDEX,
	TEST_BIN_0_STATE_0_NUMBER_OF_INPUTS,
	TEST_BIN_0_STATE_0_NUMBER_OF_TRANSITIONS,
	TEST_BIN_0_STATE_0_EFSM_BIN_INDEX_OF_IQFN_DATA,
	TEST_BIN_0_STATE_0_EFSM_BIN_INDEX_TRAN_0_DATA,
	TEST_BIN_0_STATE_0_IQFN_0_FRARR_INDEX,
	TEST_BIN_0_STATE_0_EFSM_BIN_INDEX_TRANSITION_ACTIONS_DATA,
	TEST_BIN_0_STATE_0_NEXT_STATE_AFTER_TRANSITION,
	TEST_BIN_0_STATE_0_NUMBER_OF_OPCODES,
	TEST_BIN_0_STATE_0_BOOLEAN_OPCODE_0_1,
	TEST_BIN_0_STATE_0_NUMBER_OF_TRANSITION_ACTIONS,
	TEST_BIN_0_STATE_0_ACTION_0_FRARR_INDEX,
	TEST_BIN_0_STATE_1_NUMBER_OF_INPUTS,
	TEST_BIN_0_STATE_1_NUMBER_OF_TRANSITIONS,
	TEST_BIN_0_STATE_1_EFSM_BIN_INDEX_OF_IQFN_DATA,
	TEST_BIN_0_STATE_1_EFSM_BIN_INDEX_TRAN_0_DATA,
	TEST_BIN_0_STATE_1_IQFN_0_FRARR_INDEX,
	TEST_BIN_0_STATE_1_EFSM_BIN_INDEX_TRANSITION_ACTIONS_DATA,
	TEST_BIN_0_STATE_1_NEXT_STATE_AFTER_TRANSITION,
	TEST_BIN_0_STATE_1_NUMBER_OF_OPCODES,
	TEST_BIN_0_STATE_1_OPCODE_0_1,
	TEST_BIN_0_STATE_1_NUMBER_OF_TRANSITION_ACTIONS,
	TEST_BIN_0_STATE_1_ACTION_0_FRARR_INDEX
};

EFSM_BINARY test0Binary;

/*Inputs*/
uint8_t test0InputA;

uint8_t Test0IsA()
{
	return (test0InputA != 0);
}

uint8_t(*Test0Inputs[1])();

/*Actions (Outputs)*/
uint8_t test0Light;

void Test0TurnLightOn()
{
	test0Light = 1;
	printf("Turning light on...");
}

void Test0TurnLightOff()
{
	test0Light = 0;
	printf("Turning light off...");
}

void(*Test0Actions[2])();

/*Initializations*/
Test0Init()
{
	test0Binary.id = 4321;
	test0Binary.data = test0BinaryData;
	Test0Inputs[0] = &Test0IsA;
	Test0Actions[0] = &Test0TurnLightOn;
	Test0Actions[1] = &Test0TurnLightOff;
}

/******************************		Test 0 Material End			******************************/

/******************************		Test 1 Material Start		******************************/
uint16_t test1BinaryData[] = 
{
	EFSM_T1_BIN_NUMBER_OF_STATES,
	EFSM_T1_BIN_TOTAL_NUMBER_OF_INPUTS,
	EFSM_T1_BIN_INITIAL_STATE_IDENTIFIER,
	EFSM_T1_BIN_EFSM_BIN_INDEX_STATE_0_DATA,
	EFSM_T1_BIN_EFSM_BIN_INDEX_STATE_1_DATA,
	EFSM_T1_BIN_EFSM_BIN_INDEX_STATE_2_DATA,
	EFSM_T1_BIN_NUMBER_OF_INPUTS, 
	EFSM_T1_BIN_NUMBER_OF_TRANSITIONS,
	EFSM_T1_BIN_BIN_INDEX_OF_IQFN_DATA,
	EFSM_T1_BIN_BIN_INDEX_OF_TRANSITION_0_DATA,
	EFSM_T1_BIN_BIN_INDEX_OF_TRANSITION_1_DATA,
	EFSM_T1_BIN_IQFN_0_FRARR_INDEX,
	EFSM_T1_BIN_IQFN_1_FRARR_INDEX,
	EFSM_T1_BIN_IQFN_2_FRARR_INDEX,
	EFSM_T1_BIN_TRN_0_BIN_INDEX_FOR_TRN_ACTIONS_DATA,
	EFSM_T1_BIN_TRN_0_NEXT_STATE_AFTER_TRANSITION,
	EFSM_T1_BIN_TRN_0_NUMBER_OF_OPCODES
};

EFSM_BINARY test1Binary;

/*Inputs*/

/*Actions*/

/*Initializations*/

/******************************		Test 1 Material End			******************************/