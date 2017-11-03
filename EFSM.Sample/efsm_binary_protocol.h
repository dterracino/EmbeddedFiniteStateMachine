#ifndef EFSM_BINARY_PROTOCOL_H
#define EFSM_BINARY_PROTOCOL_H

/*
The following definitions follow this naming convention:
[EFSM][The kind of index being defined (absolute or offset)][What is referenced by the index]
*/

/*Absolute indices in an EFSM binary, relative to the zeroth element.*/
#define EFSM_BIN_INDEX_NUMBER_OF_STATES												0
#define EFSM_BIN_INDEX_TOTAL_NUMBER_OF_INPUTS										1
#define EFSM_BIN_INDEX_INITIAL_STATE_IDENTIFIER										2
#define EFSM_BIN_INDEX_TABLE_OF_CONTENTS_FOR_STATES									3

/*Offsets relative to STATE HEADER data.*/
#define EFSM_STATE_HEADER_OFFSET_NUMBER_OF_INPUTS									0
#define EFSM_STATE_HEADER_OFFEST_NUMBER_OF_TRANSITIONS								1
#define EFSM_STATE_HEADER_OFFSET_BIN_INDEX_OF_IQFN_DATA								2
#define EFSM_STATE_HEADER_OFFSET_TOC_FOR_TRANSITION_INSTRUCTIONS_FIELDS				3

/*Offsets relative to TRANSITION X INSTRUCTIONS data.*/
#define EFSM_TRN_INSTR_OFFSET_BIN_INDEX_FOR_TRN_ACTIONS_DATA						0
#define EFSM_TRN_INSTR_OFFSET_NEXT_STATE_AFTER_TRANSITION							1
#define EFSM_TRN_INSTR_OFFSET_NUMBER_OF_OPCODES										2
#define EFSM_TRN_INSTR_OFFSET_OPCODES												3 

/*Offsets relative to TRANSITION ACTIONS DATA.*/
#define EFSM_TRN_ACTIONS_DATA_OFFSET_NUMBER_OF_TRANSITION_ACTIONS					0
#define EFSM_TRN_ACTIONS_DATA_OFFSET_TRANSITION_ACTIONS_ARRAY_INDICES				1

#endif 
