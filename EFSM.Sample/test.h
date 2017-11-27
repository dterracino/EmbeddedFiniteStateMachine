#ifndef TEST_H
#define TEST_H

#include "efsm_core.h"

/******************************		General Material			******************************/
#define VALUE_UNKNOWN													0xff

extern uint8_t InputValues[10];

void SetInput(EFSM_INSTANCE * efsmInstance, uint8_t efsmInputNumber, uint8_t value);
void DisplayEfsmCondtion(EFSM_INSTANCE * efsmInstance);
void DisplayInstanceData(EFSM_INSTANCE * efsmInstance);
void DisplayBufferContentsUint8(uint8_t * buffer, uint8_t numberOfElements, char * msg);

/******************************		Test 0 Material Start		******************************/
/*
Description: A simple light control state machine, taking one input, and turning a virtual 
			 light on and off. 
Number of states: 2
Number of inputs: 1
Initial state: 0

State 0:
	Name: Light Off
	Transition 0:
		Next state: 1
		Conditions for execution: input A is true
		Action on execution: turn the light on
State 1:
	Name: Light On
	Transition 0: 
		Next state: 0
		Conditions for execution: input A is false
		Action on execution: turn the light off
*/

/*GENERAL INFORMATION*/
#define TEST_BIN_0_NUMBER_OF_STATES										2
#define TEST_BIN_0_TOTAL_NUMBER_OF_INPUTS								1
#define TEST_BIN_0_INITIAL_STATE										0

/*TABLE OF CONTENTS FOR STATES*/
#define TEST_BIN_0_STATE_0_EFSM_BIN_INDEX								5
#define TEST_BIN_0_STATE_1_EFSM_BIN_INDEX								16

/*STATE 0 DATA*/
#define TEST_BIN_0_STATE_0_NUMBER_OF_INPUTS								1
#define TEST_BIN_0_STATE_0_NUMBER_OF_TRANSITIONS						1
#define TEST_BIN_0_STATE_0_EFSM_BIN_INDEX_OF_IQFN_DATA					9
#define TEST_BIN_0_STATE_0_EFSM_BIN_INDEX_TRAN_0_DATA					10
#define TEST_BIN_0_STATE_0_IQFN_0_FRARR_INDEX							0	/*Corresponds to input 0.*/
#define TEST_BIN_0_STATE_0_EFSM_BIN_INDEX_TRANSITION_ACTIONS_DATA		14
#define TEST_BIN_0_STATE_0_NEXT_STATE_AFTER_TRANSITION					1
#define TEST_BIN_0_STATE_0_NUMBER_OF_OPCODES							1
#define TEST_BIN_0_STATE_0_BOOLEAN_OPCODE_0_1							0x0080
#define TEST_BIN_0_STATE_0_NUMBER_OF_TRANSITION_ACTIONS					1
#define TEST_BIN_0_STATE_0_ACTION_0_FRARR_INDEX							0

/*STATE 1 DATA*/
#define TEST_BIN_0_STATE_1_NUMBER_OF_INPUTS								1
#define TEST_BIN_0_STATE_1_NUMBER_OF_TRANSITIONS						1
#define TEST_BIN_0_STATE_1_EFSM_BIN_INDEX_OF_IQFN_DATA					20
#define TEST_BIN_0_STATE_1_EFSM_BIN_INDEX_TRAN_0_DATA					21
#define TEST_BIN_0_STATE_1_IQFN_0_FRARR_INDEX							0
#define TEST_BIN_0_STATE_1_EFSM_BIN_INDEX_TRANSITION_ACTIONS_DATA		25
#define TEST_BIN_0_STATE_1_NEXT_STATE_AFTER_TRANSITION					0
#define TEST_BIN_0_STATE_1_NUMBER_OF_OPCODES							2
#define TEST_BIN_0_STATE_1_OPCODE_0_1									0x0380
#define TEST_BIN_0_STATE_1_NUMBER_OF_TRANSITION_ACTIONS					1
#define TEST_BIN_0_STATE_1_ACTION_0_FRARR_INDEX							1

Test0Init();
extern EFSM_BINARY test0Binary;
extern uint8_t test0InputA;
extern uint8_t test0Light;
extern uint8_t(*Test0Inputs[1])();
extern void(*Test0Actions[2])();

/******************************		Test 0 Material End			******************************/

/******************************		Test 1 Material Start		******************************/

/*
Description: A state machine for running a fan forward if the temperature is a above a threshold,
			 and running it in reverse if the temperature is below a threshold.

State 0:
	Name: Fan Off
	Transition 0: 
		Next state: 1
		Conditions for execution: temperature > forward activation threshold, dwell timer is zero
		Action on execution: engage fan in foward rotation.
	Transition 1:
		Next state: 2
		Conditions for execution: temperature < reverse activation threshold, dwell timer is zero
		Action on execution: engage fan in reverse rotation
State 1:
	Name: Engaged Forward
	Transition 0: 
		Next state: 0
		Conditions for execution: temperature < forward activation threshold
		Actions on execution: disengage fan, start dwell timer
State 2: 
	Name: Engaged Reverse
	Transition 0: 
		Next state: 0
		Conditions for execution: temperature > reverse activation threshold
		Actions on execution: disengage fan, start dwell timer

Input Query Function Names:

EFSM_T1_IsTempAboveFwdActivation	(IQFN 0)
EFSM_T1_IsTempBelowRevActivation	(IQFN 1)
EFSM_T1_IsDwellTimerExpired			(IQFN 2)

Action Function Names:

EFSM_T1_EngageFanForward			(ACTION 0)
EFSM_T1_EngageFanReverse			(ACTION 1)
EFSM_T1_StartDwellTimer				(ACTION 2)


*/

/*GENERAL INFORMATION*/
#define EFSM_T1_BIN_NUMBER_OF_STATES													3
#define EFSM_T1_BIN_TOTAL_NUMBER_OF_INPUTS												3
#define EFSM_T1_BIN_INITIAL_STATE_IDENTIFIER											0

/*TABLE OF CONTENTS FOR STATES*/
#define EFSM_T1_BIN_EFSM_BIN_INDEX_STATE_0_DATA											VALUE_UNKNOWN
#define EFSM_T1_BIN_EFSM_BIN_INDEX_STATE_1_DATA											VALUE_UNKNOWN
#define EFSM_T1_BIN_EFSM_BIN_INDEX_STATE_2_DATA											VALUE_UNKNOWN

/*STATE 0 DATA*/
#define EFSM_T1_BIN_NUMBER_OF_INPUTS													3	/*Check dwell timer, temp above, temp below*/
#define EFSM_T1_BIN_NUMBER_OF_TRANSITIONS												2
#define EFSM_T1_BIN_BIN_INDEX_OF_IQFN_DATA												VALUE_UNKNOWN
#define EFSM_T1_BIN_BIN_INDEX_OF_TRANSITION_0_DATA										VALUE_UNKNOWN
#define EFSM_T1_BIN_BIN_INDEX_OF_TRANSITION_1_DATA										VALUE_UNKNOWN
#define EFSM_T1_BIN_IQFN_0_FRARR_INDEX													2	/*EFSM_T1_IsDwellTimerExpired.*/
#define EFSM_T1_BIN_IQFN_1_FRARR_INDEX													0	/*EFSM_T1_IsTempAboveFwdActivation*/
#define EFSM_T1_BIN_IQFN_2_FRARR_INDEX													1	/*EFSM_T1_IsTempBelowRevActivation*/
#define EFSM_T1_BIN_TRN_0_BIN_INDEX_FOR_TRN_ACTIONS_DATA								VALUE_UNKNOWN
#define EFSM_T1_BIN_TRN_0_NEXT_STATE_AFTER_TRANSITION									1
#define EFSM_T1_BIN_TRN_0_NUMBER_OF_OPCODES												VALUE_UNKNOWN
#define EFSM_T1_BIN_TRN_0_OPCODE_ELEMENT_0
/******************************		Test 1 Material End			******************************/
#endif
