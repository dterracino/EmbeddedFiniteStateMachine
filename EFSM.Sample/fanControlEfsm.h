#ifndef FAN_CONTROL_EFSM_H
#define FAN_CONTROL_EFSM_H

#include <stdint.h>
#include "efsm_core.h"

/*State machine FanController (at index 0) information.*/

#define EFSM_FANCONTROLLER_NUMBER_OF_INPUTS      2
#define EFSM_FANCONTROLLER_NUMBER_OF_OUTPUTS      4

extern uint8_t (*FanController_Inputs[EFSM_FANCONTROLLER_NUMBER_OF_INPUTS])();
extern void (*FanController_OutputActions[EFSM_FANCONTROLLER_NUMBER_OF_OUTPUTS])();
extern EFSM_BINARY FanControllerBinary;

void EFSM_FanController_Init();

/*Input function prototypes.*/

uint8_t EFSM_FanController_IsTempAboveThreshold();
uint8_t EFSM_FanController_IsTimerExpired();

/*Action function prototypes.*/

void EFSM_FanController_StartTimer();
void EFSM_FanController_TurnFanOn();
void EFSM_FanController_TurnFanOff();
void EFSM_FanController_ResetTimer();

#endif
