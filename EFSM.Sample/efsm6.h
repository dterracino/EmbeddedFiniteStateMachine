#ifndef EFSM_6_H
#define EFSM_6_H

#include <stdint.h>
#include "efsm_core.h"

/*State machine efsm6 (at index 0) information.*/

#define EFSM_EFSM6_NUMBER_OF_INPUTS      5
#define EFSM_EFSM6_NUMBER_OF_OUTPUTS      10

extern uint8_t (*efsm6_Inputs[EFSM_EFSM6_NUMBER_OF_INPUTS])();
extern void (*efsm6_OutputActions[EFSM_EFSM6_NUMBER_OF_OUTPUTS])();
extern EFSM_BINARY efsm6Binary;

void EFSM_efsm6_Init();

/*Input function prototypes.*/

uint8_t EFSM_efsm6_InputA();
uint8_t EFSM_efsm6_InputB();
uint8_t EFSM_efsm6_InputC();
uint8_t EFSM_efsm6_InputD();
uint8_t EFSM_efsm6_InputE();

/*Action function prototypes.*/

void EFSM_efsm6_ActionA();
void EFSM_efsm6_ActionB();
void EFSM_efsm6_ActionC();
void EFSM_efsm6_ActionD();
void EFSM_efsm6_ActionE();
void EFSM_efsm6_ActionF();
void EFSM_efsm6_ActionG();
void EFSM_efsm6_ActionH();
void EFSM_efsm6_ActionI();
void EFSM_efsm6_ActionJ();

#endif
