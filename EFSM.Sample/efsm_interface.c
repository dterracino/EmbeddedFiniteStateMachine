#include "efsm_core.h"
#include "test.h"

/*Include the generated header file from the EFSM.Designer project here.*/
#include "efsm6.h"

/*Declare the state machine instances here.*/
EFSM_INSTANCE efsm6;

/*
Gets things ready for calls to EFSM_Process().
*/
void EFSM_InitializeProcess()
{
	/*Load the instance pointer array.*/
	/*efsmInstanceArray[0] = &fanControlEfsm;
	EFSM_FanController_Init();
	EFSM_InitializeInstance(&fanControlEfsm, &FanControllerBinary, FanController_OutputActions, FanController_Inputs, 0);*/

	efsmInstanceArray[0] = &efsm6;
	EFSM_efsm6_Init();
	EFSM_InitializeInstance(&efsm6, &efsm6Binary, efsm6_OutputActions, efsm6_Inputs, 0);
}

/*Define the functions for satisfying the EFSM prototypes here.*/
/*Inputs*/
uint8_t EFSM_efsm6_InputA(uint8_t indexOnEfsmType) { printf("inputA\n"); return InputValues[0]; }
uint8_t EFSM_efsm6_InputB(uint8_t indexOnEfsmType) { printf("inputB\n"); return InputValues[1]; }
uint8_t EFSM_efsm6_InputC(uint8_t indexOnEfsmType) { printf("inputC\n"); return InputValues[2]; }
uint8_t EFSM_efsm6_InputD(uint8_t indexOnEfsmType) { printf("inputD\n"); return InputValues[3]; }
uint8_t EFSM_efsm6_InputE(uint8_t indexOnEfsmType) { printf("inputE\n"); return InputValues[4]; }

/*Actions*/

void EFSM_efsm6_ActionA(uint8_t indexOnEfsmType) { printf("actionA\n"); }
void EFSM_efsm6_ActionB(uint8_t indexOnEfsmType) { printf("actionB\n"); }
void EFSM_efsm6_ActionC(uint8_t indexOnEfsmType) { printf("actionC\n"); }
void EFSM_efsm6_ActionD(uint8_t indexOnEfsmType) { printf("actionD\n"); }
void EFSM_efsm6_ActionE(uint8_t indexOnEfsmType) { printf("actionE\n"); }
void EFSM_efsm6_ActionF(uint8_t indexOnEfsmType) { printf("actionF\n"); }
void EFSM_efsm6_ActionG(uint8_t indexOnEfsmType) { printf("actionG\n"); }
void EFSM_efsm6_ActionH(uint8_t indexOnEfsmType) { printf("actionH\n"); }
void EFSM_efsm6_ActionI(uint8_t indexOnEfsmType) { printf("actionI\n"); }
void EFSM_efsm6_ActionJ(uint8_t indexOnEfsmType) { printf("actionJ\n"); }