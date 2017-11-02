#ifndef DEBUG_H
#define DEBUG_H
#include "efsm_core.h"

/*Test 0*/

#define VALUE_UNKNOWN													0xff

/*Begin general information.*/
#define TEST_BIN_0_NUMBER_OF_STATES										2
#define TEST_BIN_0_TOTAL_NUMBER_OF_INPUTS								1
#define TEST_BIN_0_INITIAL_STATE										0

/*Begin state data table of contents.*/
#define TEST_BIN_0_STATE_0_EFSM_BIN_INDEX								5
#define TEST_BIN_0_STATE_1_EFSM_BIN_INDEX								15

/*Begin state 0 data.*/
#define TEST_BIN_0_STATE_0_NUMBER_OF_INPUTS								1
#define TEST_BIN_0_STATE_0_NUMBER_OF_TRANSITIONS						1
#define TEST_BIN_0_STATE_0_EFSM_BIN_INDEX_OF_IQFN_DATA					9
#define TEST_BIN_0_STATE_0_EFSM_BIN_INDEX_TRAN_0_DATA					10
#define TEST_BIN_0_STATE_0_IQFN_0_FRARR_INDEX							0	/*Corresponds to input 0.*/
#define TEST_BIN_0_STATE_0_EFSM_BIN_INDEX_TRANSITION_ACTIONS_DATA		13
#define TEST_BIN_0_STATE_0_NEXT_STATE_AFTER_TRANSITION					1
#define TEST_BIN_0_STATE_0_NUMBER_OF_OPCODES							1
#define TEST_BIN_0_STATE_0_BOOLEAN_OPCODE_0_1							0x0080
#define TEST_BIN_0_STATE_0_NUMBER_OF_TRANSITION_ACTIONS					1
#define TEST_BIN_0_STATE_0_ACTION_0_FRARR_INDEX							0

/*Begin state 1 data.*/
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
extern uint8_t inputA;
extern uint8_t light;
extern uint8_t(*Test0Inputs[1])();
extern void (*Test0Actions[2])();

#endif
