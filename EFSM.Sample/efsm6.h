#ifndef EFSM_6_H
#define EFSM_6_H

#include <stdint.h>
#include "efsm_core.h"

/*State machine "efsm6" information.*/

#define EFSM_EFSM6_NUMBER_OF_INPUTS      5
#define EFSM_EFSM6_NUMBER_OF_OUTPUTS      10

extern uint8_t (*efsm6_Inputs[EFSM_EFSM6_NUMBER_OF_INPUTS])(uint8_t indexOnEfsmType);
extern void (*efsm6_OutputActions[EFSM_EFSM6_NUMBER_OF_OUTPUTS])(uint8_t indexOnEfsmType);
extern EFSM_BINARY efsm6Binary;

void EFSM_efsm6_Init();

/*Input function prototypes.*/

uint8_t EFSM_efsm6_InputA(uint8_t indexOnEfsmType);
uint8_t EFSM_efsm6_InputB(uint8_t indexOnEfsmType);
uint8_t EFSM_efsm6_InputC(uint8_t indexOnEfsmType);
uint8_t EFSM_efsm6_InputD(uint8_t indexOnEfsmType);
uint8_t EFSM_efsm6_InputE(uint8_t indexOnEfsmType);

/*Action function prototypes.*/

void EFSM_efsm6_ActionA(uint8_t indexOnEfsmType);
void EFSM_efsm6_ActionB(uint8_t indexOnEfsmType);
void EFSM_efsm6_ActionC(uint8_t indexOnEfsmType);
void EFSM_efsm6_ActionD(uint8_t indexOnEfsmType);
void EFSM_efsm6_ActionE(uint8_t indexOnEfsmType);
void EFSM_efsm6_ActionF(uint8_t indexOnEfsmType);
void EFSM_efsm6_ActionG(uint8_t indexOnEfsmType);
void EFSM_efsm6_ActionH(uint8_t indexOnEfsmType);
void EFSM_efsm6_ActionI(uint8_t indexOnEfsmType);
void EFSM_efsm6_ActionJ(uint8_t indexOnEfsmType);


///*State machine "Additional" information.*/
//
//#define EFSM_ADDITIONAL_NUMBER_OF_INPUTS      2
//#define EFSM_ADDITIONAL_NUMBER_OF_OUTPUTS      4
//
//extern uint8_t (*Additional_Inputs[EFSM_ADDITIONAL_NUMBER_OF_INPUTS])(uint8_t indexOnEfsmType);
//extern void (*Additional_OutputActions[EFSM_ADDITIONAL_NUMBER_OF_OUTPUTS])(uint8_t indexOnEfsmType);
//extern EFSM_BINARY AdditionalBinary;
//
//void EFSM_Additional_Init();
//
///*Input function prototypes.*/
//
//uint8_t EFSM_Additional_IsStartButtonPressed(uint8_t indexOnEfsmType);
//uint8_t EFSM_Additional_IsTimerExpired(uint8_t indexOnEfsmType);
//
///*Action function prototypes.*/
//
//void EFSM_Additional_TurnOnToaster(uint8_t indexOnEfsmType);
//void EFSM_Additional_TurnOffToaster(uint8_t indexOnEfsmType);
//void EFSM_Additional_StartTimer(uint8_t indexOnEfsmType);
//void EFSM_Additional_ResetTimer(uint8_t indexOnEfsmType);



#endif
