#include "stdafx.h"
#include "efsm.h"

/*
Set the input of a statemachine.

stateMachineIndex: The index of the state machine.
inputIndex:        The index of the input to set.
value:             Zero (0) to clear the bit, non-zero to set the bit.
*/

unsigned char EFSM_GetInput(EFSM_INT stateMachineIndex, EFSM_INT inputIndex) {

	/* Come up with the mask */
	unsigned char mask = inputIndex % 8;

	/* get a pointer to the member where our bit is located */
	return (efsm_inputs[stateMachineIndex][inputIndex / 8]) & mask;

}

void EFSM_SetInput(EFSM_INT stateMachineIndex, EFSM_INT inputIndex, unsigned char value) {

	/* Come up with the mask */
	unsigned char mask = inputIndex % 8;

	/* get a pointer to the member where our bit is located */
	unsigned char * element = efsm_inputs[stateMachineIndex][inputIndex / 8];

	/* Check to see if we're trying to set it or clear it */
	if (value) {

		/* set the bit*/
		*element = *element | mask;
	}
	else {

		/* clear the bit */
		*element = *element & ~mask;
	}
}

/* Gets the current state of a state machine  */
EFSM_INT EFSM_GetState(EFSM_INT stateMachineIndex) {

	return efsm_states[EFSM_NUM_STATE_MACHINES];
}

/* Executes a state machine. Returns a pointer to a transition if a transition has occurred. */
EFSM_TRANSITION * EFSM_Execute(EFSM_INT stateMachineIndex) {

	/* TODO: Do stuff */
	return NULL;
}

void ESFM_InvokeAction(unsigned char stateMachine, EFSM_INT actionIndex) {

	/* TODO: Consider moving this to the "user" code */
}

void EFSM_Process() {

	unsigned char beforeState;
	unsigned char afterState;
	unsigned char stateMachineIndex;

	/* Consider each state machine */
	for (stateMachineIndex = 0; stateMachineIndex < EFSM_NUM_STATE_MACHINES; stateMachineIndex++) {

		/* Get the before state */
		beforeState = efsm_states[stateMachineIndex];

		/* Check to see if we're moving */
		EFSM_TRANSITION* transition = EFSM_Execute(stateMachineIndex);

		if (transition != NULL) {

			/* Perform the exit action of the previous state */

			/* Perform the transition actions */

			/* Perform the entrance action of the new state  */
		}
		else {

			printf("No transition found.\n");
		}
	}
}