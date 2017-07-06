#ifndef GENERATED_H2
#define GENERATED_H2

#include "stdafx.h"

#define EFSM_CONDITION_TYPE_OR  0
#define EFSM_CONDITION_TYPE_AND 1

typedef struct {
	unsigned char conditionType;
} EFSM_CONDITION;

typedef struct {
	unsigned char * actions;
	unsigned char targetState;
	//TODO: Figure out how to run the conditions
} EFSM_TRANSITION;

typedef struct {
	void(*functionPtr)();
} EFSM_ACTION;

typedef struct {
	unsigned char enterActionsLen;
	unsigned char * enterActions;

	unsigned char exitActionsLen;
	unsigned char * exitActions;

	EFSM_TRANSITION transitions[];
} EFSM_STATE;

typedef struct {
	EFSM_ACTION * actions;
	EFSM_STATE * states;
} EFSM_STATE_MACHINE;



#define EFSM_NUM_STATE_MACHINES 2

/* State Machine MAIN */
#define EFSM_SM_MAIN 0
#define EFSM_SM_MAIN_NUM_STATES 2

#define EFMS_SM_MAIN_S_IDLE 0
#define EFMS_SM_MAIN_S_RUN 1


unsigned char EFSM_SM_MAIN_Input_CallForAir();

void EFSM_SM_MAIN_Output_Run();

/* State machine BLOWER */
#define EFSM_SM_BLOWER 1

#define EFSM_SM_BLOWER_S_IDLE 0
#define EFSM_SM_BLOWER_S_WAIT_FOR_DAMPER 1
#define EFSM_SM_BLOWER_S_WAIT_FOR_AIRFLOW 2
#define EFSM_SM_BLOWER_S_BLOWER_RUNNING 3

unsigned char EFSM_SM_BLOWER_Input_CallForBlower();
unsigned char EFSM_SM_BLOWER_Input_DamperEndSwitch();
unsigned char EFSM_SW_BLOWER_Input_Airflow();

void EFSM_SM_BLOWER_DamperOn();
void EFSM_SM_BLOWER_DamperOff();
void EFSM_SM_BLOWER_BlowerOn();
void EFSM_SM_BLOWER_BlowerOff();
void EFSM_SM_BLOWER_BlowerRunningOn();
void EFSM_SM_BLOWER_BlowerRunningOff();

void EFSMProcess();

#endif

