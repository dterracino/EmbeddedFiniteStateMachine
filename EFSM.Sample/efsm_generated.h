#ifndef GENERATED_H2
#define GENERATED_H2

#include "efsm.h"

#define EFSM_NUM_STATE_MACHINES 1

/* State Machine MAIN */
#define EFSM_SM_MAIN 0
#define EFSM_SM_MAIN_NUM_STATES 2

#define EFMS_SM_MAIN_S_IDLE 0
#define EFMS_SM_MAIN_S_RUN 1

unsigned char EFSM_SM_MAIN_Input_CallForAir();

/* State machine BLOWER */
#define EFSM_SM_BLOWER 1

#define EFSM_SM_BLOWER_S_IDLE 0
#define EFSM_SM_BLOWER_S_WAIT_FOR_DAMPER 1
#define EFSM_SM_BLOWER_S_WAIT_FOR_AIRFLOW 2
#define EFSM_SM_BLOWER_S_BLOWER_RUNNING 3



#endif

