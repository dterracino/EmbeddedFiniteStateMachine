#ifndef _H
#define _H

#include <stdint.h>
#include "efsm_core.h"

/*
----------------------------------------------------------------------------------------------------
General information.
*/

#define EFSM_TOTAL_NUMBER_OF_STATE_MACHINE_INSTANCES       1

void EFSM_GeneratedDiagnostics(EFSM_INSTANCE * efsmInstance);
/*
----------------------------------------------------------------------------------------------------
Configuration parameters and debugging.
*/
#define EFSM_DEBUG_MODE_EMBEDDED             0
#define EFSM_DEBUG_MODE_DESKTOP              1

#define EFSM_CONFIG_PROJECT_AVAILABLE        1
#define EFSM_CONFIG_ENABLE_DEBUGGING         1
#define EFSM_CONFIG_DEBUG_MODE               EFSM_DEBUG_MODE_DESKTOP
/*
----------------------------------------------------------------------------------------------------
State machine "EFSMDebugTester" information.
*/
#define EFSM_EFSMDEBUGTESTER_NUMBER_OF_INPUTS      2
#define EFSM_EFSMDEBUGTESTER_NUMBER_OF_OUTPUTS      2

extern uint8_t (*EFSMDebugTester_Inputs[EFSM_EFSMDEBUGTESTER_NUMBER_OF_INPUTS])(uint8_t indexOnEfsmType);
extern void (*EFSMDebugTester_OutputActions[EFSM_EFSMDEBUGTESTER_NUMBER_OF_OUTPUTS])(uint8_t indexOnEfsmType);
extern EFSM_BINARY EFSMDebugTester_Binary;


/*Input function prototypes.*/

uint8_t EFSM_EFSMDebugTester_InputA(uint8_t indexOnEfsmType);
uint8_t EFSM_EFSMDebugTester_InputB(uint8_t indexOnEfsmType);

/*Action function prototypes.*/

void EFSM_EFSMDebugTester_ActionA(uint8_t indexOnEfsmType);
void EFSM_EFSMDebugTester_ActionB(uint8_t indexOnEfsmType);


/*
----------------------------------------------------------------------------------------------------
General (applies to all state machine definitions).
*/

/*State machine definitions initialization prototype.*/
void EFSM_efsmDebugTester_Init();
void EFSM_InitializeProcess();

/*
----------------------------------------------------------------------------------------------------
Diagnostics.
*/

#define EFSM_GENERATED_DIAGNOSTICS

/*State Machine State Accessor Prototypes*/

uint32_t Get_EFSMDebugTester_Instance_0_State();

/*State Machine Input Accessor Prototypes*/

uint32_t Get_EFSMDebugTester_0_Input_0();
uint32_t Get_EFSMDebugTester_0_Input_1();
#endif
