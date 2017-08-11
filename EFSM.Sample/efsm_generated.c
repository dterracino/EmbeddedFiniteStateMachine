
#include "stdafx.h"
#include "efsm_generated.h"

EFSM_STATE efsm_s_0_0 = {
	0,
	NULL,

	0,
	NULL,

	0,
	NULL,
};

EFSM_STATE efsm_s_0_1 = {
	0,
	NULL,

	0,
	NULL,

	0,
	NULL
};

EFSM_STATES efsm_states_blalb = {
	.statesLen = 2,
	.states = 
	{
		{
			.enterActions = {
				.len = 0,
				.actions = NULL
			}
		},
		{
			.enterActions = {
				.len = 0,
				.actions = NULL
}
		}
	}
};

//EFSM_STATES efsm_sms_0 = {
//	2,
//	{
//		efsm_s_0_0,
//		efsm_s_0_1
//	}
//}
//
//EFSM_STATE_MACHINE efsm_sm_0 = {
//	
//	//States
//	{
//		1,
//		{
//			efsm_s_0_0 ,
//			efsm_s_0_1
//		}
//	}
//};

EFSM_STATE_MACHINE efsm_sm_1;

EFSM_INT efsm_states[EFSM_NUM_STATE_MACHINES];

EFSM_STATE_MACHINE efsm_stateMachines[EFSM_NUM_STATE_MACHINES];

unsigned char efsm_inputs_MAIN[1];
unsigned char efsm_inputs_BLOWER[1];

unsigned char * efsm_inputs[] = {

	efsm_inputs_MAIN,
	efsm_inputs_BLOWER
};
