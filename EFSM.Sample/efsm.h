#ifndef EFSM_H
#define EFSM_H


#include "efsm_generated.h"
#include "efsm_options.h"

/* The condition types */
typedef enum {

	EFSM_CONDITION_TYPE_IS_TRUE  = 0,
	EFSM_CONDITION_TYPE_IS_FALSE = 1,
	EFSM_CONDITION_TYPE_OR		 = 2,
    EFSM_CONDITION_TYPE_AND      = 3,

} EFSM_CONDITION_TYPE;


/* An transition condition */
typedef struct {

	/* The type of condition */
	EFSM_CONDITION_TYPE conditionType;

} EFSM_CONDITION;

typedef struct {

	/* The number of actions */
	EFSM_INT actionsLen;

	/* The actions to perform when moving through this transition */
	EFSM_INT * actions;

	/* The target state */
	EFSM_INT targetState;

	/* The condition(s) under which to follow this transition */
	EFSM_CONDITION condition;

	//TODO: Figure out how to run the conditions
} EFSM_TRANSITION;

typedef struct {

	EFSM_INT transitionsLen;

	EFSM_TRANSITION transitions[];

} EFSM_TRANSITIONS;

typedef struct {

	/* The output function */
	void(*functionPtr)();

} EFSM_OUTPUT_ACTION;

/* Output actions */
typedef struct {

	EFSM_INT len;

	EFSM_OUTPUT_ACTION * actions;

} EFSM_OUTPUT_ACTIONS;

/* A state machine input */
typedef struct {

	/* The input function */
	unsigned char(*inputFunctionPtr)();

} EFSM_STATE_MACHINE_INPUT;

/* A state */
typedef struct {

	/* The actions to perform when entering this state */
	EFSM_OUTPUT_ACTIONS enterActions;

	/* The actions to perform when exiting this state */
	EFSM_OUTPUT_ACTIONS exitActions;

	/* The number of transitions */
	EFSM_INT transitionsLen;
	EFSM_TRANSITION * transitions;

} EFSM_STATE;

typedef struct {

	EFSM_INT statesLen;
	EFSM_STATE * states;

} EFSM_STATES;

/* A state machine */
typedef struct {

	/* The actions available to this state machine */
	//EFSM_OUTPUT_ACTIONS actions;

	/* The states avaialble to this state machine */
	EFSM_STATES  states;

	/* The inputs available to this state machjine */
	//EFSM_STATE_MACHINE_INPUTS  inputs;

} EFSM_STATE_MACHINE;

/* A "project" of state machines. */
typedef struct {

	/* The number of state machines */
	EFSM_INT stateMachinesLen;

	/* The state machines*/
	EFSM_STATE_MACHINE * startMachines;

} EFSM_STATE_MACHINES;

extern unsigned char * efsm_inputs[];
extern EFSM_INT efsm_states[];
extern EFSM_STATE_MACHINE efsm_stateMachines[];

#endif
