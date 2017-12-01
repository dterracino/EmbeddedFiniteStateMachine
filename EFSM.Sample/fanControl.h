#ifndef FAN_CONTROL_EFSM_H
#define FAN_CONTROL_EFSM_H

#include <stdint.h>
#include "efsm_core.h"

/*State machine "FanController" information.*/

#define EFSM_FANCONTROLLER_NUMBER_OF_INPUTS      2
#define EFSM_FANCONTROLLER_NUMBER_OF_OUTPUTS      4

extern uint8_t (*FanController_Inputs[EFSM_FANCONTROLLER_NUMBER_OF_INPUTS])(uint8_t indexOnEfsmType);
extern void (*FanController_OutputActions[EFSM_FANCONTROLLER_NUMBER_OF_OUTPUTS])(uint8_t indexOnEfsmType);
extern EFSM_BINARY FanControllerBinary;

void EFSM_FanController_Init();

/*Input function prototypes.*/

uint8_t EFSM_FanController_IsTempAboveThreshold(uint8_t indexOnEfsmType);
uint8_t EFSM_FanController_IsTimerExpired(uint8_t indexOnEfsmType);

/*Action function prototypes.*/

EFSM_FanController_StartTimer(uint8_t indexOnEfsmType);
EFSM_FanController_TurnFanOn(uint8_t indexOnEfsmType);
EFSM_FanController_TurnFanOff(uint8_t indexOnEfsmType);
EFSM_FanController_ResetTimer(uint8_t indexOnEfsmType);



#endif
