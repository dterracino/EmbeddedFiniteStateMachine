
#include "stdafx.h"
#include "generated.h"

unsigned char efsm_states[EFSM_NUM_STATE_MACHINES];

unsigned char efsm_inputs_main[1];
unsigned char efsm_inputs_blower[1];

unsigned char * efsm_inputs[] = {

	efsm_inputs_main,
	efsm_inputs_blower
};

unsigned char EFSM_MAIN(unsigned char state) {

	switch (state) {

		case EFMS_SM_MAIN_S_IDLE:
			break;

		case EFMS_SM_MAIN_S_RUN:
			break;

		default:
			break;
	}
}

/* Peform enter logic */
void EFSM_MAIN_ENTER(unsigned char state) {

}

/* Perform exit logic */
void EFSM_MAIN_EXIT(unsigned char state) {

}

unsigned char EFSM_BLOWER(unsigned char state) {

	switch (state) {
		case EFSM_SM_BLOWER_S_IDLE:
			break;

		case EFSM_SM_BLOWER_S_WAIT_FOR_DAMPER:
			break;

		case EFSM_SM_BLOWER_S_WAIT_FOR_AIRFLOW:
			break;

		case EFSM_SM_BLOWER_S_BLOWER_RUNNING:
			break;

		default:
			break;
	}
}

/* Peform enter logic */
void EFSM_BLOWER_ENTER(unsigned char state) {

}

/* Perform exit logic */
void EFSM_BLOWER_EXIT(unsigned char state) {

}

void EFSM_SetInput(unsigned char stateMachine, unsigned char input, unsigned char value) {

	/* Come up with the mask */
	unsigned char mask = input % 8;

	/* get a pointer to the member where our bit is located */
	unsigned char * element = efsm_inputs[stateMachine][input / 8];

	if (value) {

		/* set the bit*/
		*element = *element | mask;
	}
	else {

		/* clear the bit */
		*element = *element & ~mask;
	}
}

void EFSMProcess() {

	unsigned char beforeState;
	unsigned char afterState;

	/* MAIN */
	beforeState = efsm_states[EFSM_SM_MAIN];
    afterState = EFSM_MAIN(beforeState);

	if (beforeState != afterState) {
		EFSM_MAIN_EXIT(beforeState);

		//TODO: Perform the transition logic here!

		EFSM_MAIN_ENTER(afterState);
	}

	/* BLOWER */

//	EFSM_BLOWER();

}