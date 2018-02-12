#ifndef _H
#define _H

#include <stdint.h>
#include "efsm_core.h"

/*
----------------------------------------------------------------------------------------------------
General information.
*/

#define EFSM_TOTAL_NUMBER_OF_STATE_MACHINE_INSTANCES       4

void EFSM_GeneratedDiagnostics(EFSM_INSTANCE * efsmInstance);
/*
----------------------------------------------------------------------------------------------------
Configuration parameters and debugging.
*/
#define EFSM_DEBUG_MODE_EMBEDDED             0
#define EFSM_DEBUG_MODE_DESKTOP              1
#define EFSM_DEBUG_MODE_NONE                 2

#define EFSM_CONFIG_PROJECT_AVAILABLE        1
#define EFSM_CONFIG_ENABLE_DEBUGGING         1
#define EFSM_CONFIG_DEBUG_MODE               EFSM_DEBUG_MODE_DESKTOP
/*
----------------------------------------------------------------------------------------------------
State machine "HVAC_DF" information.
*/
#define EFSM_HVAC_DF_NUMBER_OF_INPUTS      11
#define EFSM_HVAC_DF_NUMBER_OF_OUTPUTS      9

extern uint8_t (*HVAC_DF_Inputs[EFSM_HVAC_DF_NUMBER_OF_INPUTS])(uint8_t indexOnEfsmType);
extern void (*HVAC_DF_OutputActions[EFSM_HVAC_DF_NUMBER_OF_OUTPUTS])(uint8_t indexOnEfsmType);
extern EFSM_BINARY HVAC_DF_Binary;


/*Input function prototypes.*/

uint8_t EFSM_HVAC_DF_IsBlowerRequired(uint8_t indexOnEfsmType);
uint8_t EFSM_HVAC_DF_CanBlow(uint8_t indexOnEfsmType);
uint8_t EFSM_HVAC_DF_IsInHeatRange(uint8_t indexOnEfsmType);
uint8_t EFSM_HVAC_DF_CanHeat(uint8_t indexOnEfsmType);
uint8_t EFSM_HVAC_DF_IsInCoolRange(uint8_t indexOnEfsmType);
uint8_t EFSM_HVAC_DF_CanCool(uint8_t indexOnEfsmType);
uint8_t EFSM_HVAC_DF_IsBlowerOn(uint8_t indexOnEfsmType);
uint8_t EFSM_HVAC_DF_IsBlowerInDelayedStart(uint8_t indexOnEfsmType);
uint8_t EFSM_HVAC_DF_IsHeatOn(uint8_t indexOnEfsmType);
uint8_t EFSM_HVAC_DF_IsCoolOn(uint8_t indexOnEfsmType);
uint8_t EFSM_HVAC_DF_IsStartupTimeElapsed(uint8_t indexOnEfsmType);

/*Action function prototypes.*/

void EFSM_HVAC_DF_HvacInit(uint8_t indexOnEfsmType);
void EFSM_HVAC_DF_HvacEnterBlower(uint8_t indexOnEfsmType);
void EFSM_HVAC_DF_HvacEnterPreheat(uint8_t indexOnEfsmType);
void EFSM_HVAC_DF_HvacEnterHeating(uint8_t indexOnEfsmType);
void EFSM_HVAC_DF_HvacEnterStopHeat(uint8_t indexOnEfsmType);
void EFSM_HVAC_DF_HvacEnterPrecool(uint8_t indexOnEfsmType);
void EFSM_HVAC_DF_HvacEnterCooling(uint8_t indexOnEfsmType);
void EFSM_HVAC_DF_HvacEnterStopCool(uint8_t indexOnEfsmType);
void EFSM_HVAC_DF_HvacEnterIdle(uint8_t indexOnEfsmType);


/*
----------------------------------------------------------------------------------------------------
State machine "Blower_DF" information.
*/
#define EFSM_BLOWER_DF_NUMBER_OF_INPUTS      9
#define EFSM_BLOWER_DF_NUMBER_OF_OUTPUTS      9

extern uint8_t (*Blower_DF_Inputs[EFSM_BLOWER_DF_NUMBER_OF_INPUTS])(uint8_t indexOnEfsmType);
extern void (*Blower_DF_OutputActions[EFSM_BLOWER_DF_NUMBER_OF_OUTPUTS])(uint8_t indexOnEfsmType);
extern EFSM_BINARY Blower_DF_Binary;


/*Input function prototypes.*/

uint8_t EFSM_Blower_DF_IsStartupTimeElapsed(uint8_t indexOnEfsmType);
uint8_t EFSM_Blower_DF_IsHvacCallingForBlower(uint8_t indexOnEfsmType);
uint8_t EFSM_Blower_DF_IsDamperEndReceived(uint8_t indexOnEfsmType);
uint8_t EFSM_Blower_DF_IsAirProvingReceived(uint8_t indexOnEfsmType);
uint8_t EFSM_Blower_DF_IsDelayedStartEnabled(uint8_t indexOnEfsmType);
uint8_t EFSM_Blower_DF_IsDelayedStartTimerExpired(uint8_t indexOnEfsmType);
uint8_t EFSM_Blower_DF_IsDelayedStopEnabled(uint8_t indexOnEfsmType);
uint8_t EFSM_Blower_DF_IsDelayedStopTimerExpired(uint8_t indexOnEfsmType);
uint8_t EFSM_Blower_DF_IsRecoverAirTimerExpired(uint8_t indexOnEfsmType);

/*Action function prototypes.*/

void EFSM_Blower_DF_BlowerEnterIdle(uint8_t indexOnEfsmType);
void EFSM_Blower_DF_BlowerEnterPreStart(uint8_t indexOnEfsmType);
void EFSM_Blower_DF_BlowerEnterDelayedStart(uint8_t indexOnEfsmType);
void EFSM_Blower_DF_BlowerEnterOpenDamper(uint8_t indexOnEfsmType);
void EFSM_Blower_DF_BlowerEnterProveAir(uint8_t indexOnEfsmType);
void EFSM_Blower_DF_BlowerEnterBlowerOn(uint8_t indexOnEfsmType);
void EFSM_Blower_DF_BlowerEnterDelayedStop(uint8_t indexOnEfsmType);
void EFSM_Blower_DF_BlowerInitIdle(uint8_t indexOnEfsmType);
void EFSM_Blower_DF_BlowerEnterRecoverAir(uint8_t indexOnEfsmType);


/*
----------------------------------------------------------------------------------------------------
State machine "Heating_DF" information.
*/
#define EFSM_HEATING_DF_NUMBER_OF_INPUTS      7
#define EFSM_HEATING_DF_NUMBER_OF_OUTPUTS      9

extern uint8_t (*Heating_DF_Inputs[EFSM_HEATING_DF_NUMBER_OF_INPUTS])(uint8_t indexOnEfsmType);
extern void (*Heating_DF_OutputActions[EFSM_HEATING_DF_NUMBER_OF_OUTPUTS])(uint8_t indexOnEfsmType);
extern EFSM_BINARY Heating_DF_Binary;


/*Input function prototypes.*/

uint8_t EFSM_Heating_DF_IsStartupTimeElapsed(uint8_t indexOnEfsmType);
uint8_t EFSM_Heating_DF_IsHvacCallingForHeat(uint8_t indexOnEfsmType);
uint8_t EFSM_Heating_DF_IsBlowerOn(uint8_t indexOnEfsmType);
uint8_t EFSM_Heating_DF_IsTestingModeRequested(uint8_t indexOnEfsmType);
uint8_t EFSM_Heating_DF_IsPilotGasInReceived(uint8_t indexOnEfsmType);
uint8_t EFSM_Heating_DF_IsSparkInReceived(uint8_t indexOnEfsmType);
uint8_t EFSM_Heating_DF_IsMainGasInReceived(uint8_t indexOnEfsmType);

/*Action function prototypes.*/

void EFSM_Heating_DF_HeatEnterIdle(uint8_t indexOnEfsmType);
void EFSM_Heating_DF_HeatEnterPreAir(uint8_t indexOnEfsmType);
void EFSM_Heating_DF_HeatEnterPrePGas(uint8_t indexOnEfsmType);
void EFSM_Heating_DF_HeatEnterOnTesting(uint8_t indexOnEfsmType);
void EFSM_Heating_DF_HeatEnterStopDf(uint8_t indexOnEfsmType);
void EFSM_Heating_DF_HeatEnterOnNormal(uint8_t indexOnEfsmType);
void EFSM_Heating_DF_HeatEnterSparkOff(uint8_t indexOnEfsmType);
void EFSM_Heating_DF_HeatEnterPreMGas(uint8_t indexOnEfsmType);
void EFSM_Heating_DF_HeatEnterPreSpark(uint8_t indexOnEfsmType);


/*
----------------------------------------------------------------------------------------------------
State machine "Cooling_DF" information.
*/
#define EFSM_COOLING_DF_NUMBER_OF_INPUTS      12
#define EFSM_COOLING_DF_NUMBER_OF_OUTPUTS      9

extern uint8_t (*Cooling_DF_Inputs[EFSM_COOLING_DF_NUMBER_OF_INPUTS])(uint8_t indexOnEfsmType);
extern void (*Cooling_DF_OutputActions[EFSM_COOLING_DF_NUMBER_OF_OUTPUTS])(uint8_t indexOnEfsmType);
extern EFSM_BINARY Cooling_DF_Binary;


/*Input function prototypes.*/

uint8_t EFSM_Cooling_DF_IsStartupTimeElapsed(uint8_t indexOnEfsmType);
uint8_t EFSM_Cooling_DF_IsHvacCallingForCooling(uint8_t indexOnEfsmType);
uint8_t EFSM_Cooling_DF_IsY1MinOffTimeExpired(uint8_t indexOnEfsmType);
uint8_t EFSM_Cooling_DF_IsAllMinOnTimesExpired(uint8_t indexOnEfsmType);
uint8_t EFSM_Cooling_DF_IsEvapEnabled(uint8_t indexOnEfsmType);
uint8_t EFSM_Cooling_DF_IsOaTempTooLowForEvap(uint8_t indexOnEfsmType);
uint8_t EFSM_Cooling_DF_IsBlowerOn(uint8_t indexOnEfsmType);
uint8_t EFSM_Cooling_DF_IsTestingModeRequested(uint8_t indexOnEfsmType);
uint8_t EFSM_Cooling_DF_IsEvapHeadStartTimerExpired(uint8_t indexOnEfsmType);
uint8_t EFSM_Cooling_DF_IsQuickStopRequired(uint8_t indexOnEfsmType);
uint8_t EFSM_Cooling_DF_IsStage3Required(uint8_t indexOnEfsmType);
uint8_t EFSM_Cooling_DF_IsStage2Required(uint8_t indexOnEfsmType);

/*Action function prototypes.*/

void EFSM_Cooling_DF_CoolingEnterIdle(uint8_t indexOnEfsmType);
void EFSM_Cooling_DF_CoolingEnterEvap(uint8_t indexOnEfsmType);
void EFSM_Cooling_DF_CoolingEnterStage1(uint8_t indexOnEfsmType);
void EFSM_Cooling_DF_CoolingEnterStage2(uint8_t indexOnEfsmType);
void EFSM_Cooling_DF_CoolingEnterStage3(uint8_t indexOnEfsmType);
void EFSM_Cooling_DF_CoolingEnterTest(uint8_t indexOnEfsmType);
void EFSM_Cooling_DF_CoolingExitStop(uint8_t indexOnEfsmType);
void EFSM_Cooling_DF_CoolingEnterStart(uint8_t indexOnEfsmType);
void EFSM_Cooling_DF_CoolingInitIdle(uint8_t indexOnEfsmType);


/*
----------------------------------------------------------------------------------------------------
General (applies to all state machine definitions).
*/

/*State machine definitions initialization prototype.*/
void EFSM_df_stateMachines_Init();
void EFSM_InitializeProcess();

/*
----------------------------------------------------------------------------------------------------
Diagnostics.
*/

#define EFSM_GENERATED_DIAGNOSTICS

/*State Machine State Accessor Prototypes*/

uint32_t Get_HVAC_DF_Instance_0_State();
uint32_t Get_Blower_DF_Instance_0_State();
uint32_t Get_Heating_DF_Instance_0_State();
uint32_t Get_Cooling_DF_Instance_0_State();

/*State Machine Input Accessor Prototypes*/

uint32_t Get_HVAC_DF_0_Input_0();
uint32_t Get_HVAC_DF_0_Input_1();
uint32_t Get_HVAC_DF_0_Input_2();
uint32_t Get_HVAC_DF_0_Input_3();
uint32_t Get_HVAC_DF_0_Input_4();
uint32_t Get_HVAC_DF_0_Input_5();
uint32_t Get_HVAC_DF_0_Input_6();
uint32_t Get_HVAC_DF_0_Input_7();
uint32_t Get_HVAC_DF_0_Input_8();
uint32_t Get_HVAC_DF_0_Input_9();
uint32_t Get_HVAC_DF_0_Input_10();
uint32_t Get_Blower_DF_0_Input_0();
uint32_t Get_Blower_DF_0_Input_1();
uint32_t Get_Blower_DF_0_Input_2();
uint32_t Get_Blower_DF_0_Input_3();
uint32_t Get_Blower_DF_0_Input_4();
uint32_t Get_Blower_DF_0_Input_5();
uint32_t Get_Blower_DF_0_Input_6();
uint32_t Get_Blower_DF_0_Input_7();
uint32_t Get_Blower_DF_0_Input_8();
uint32_t Get_Heating_DF_0_Input_0();
uint32_t Get_Heating_DF_0_Input_1();
uint32_t Get_Heating_DF_0_Input_2();
uint32_t Get_Heating_DF_0_Input_3();
uint32_t Get_Heating_DF_0_Input_4();
uint32_t Get_Heating_DF_0_Input_5();
uint32_t Get_Heating_DF_0_Input_6();
uint32_t Get_Cooling_DF_0_Input_0();
uint32_t Get_Cooling_DF_0_Input_1();
uint32_t Get_Cooling_DF_0_Input_2();
uint32_t Get_Cooling_DF_0_Input_3();
uint32_t Get_Cooling_DF_0_Input_4();
uint32_t Get_Cooling_DF_0_Input_5();
uint32_t Get_Cooling_DF_0_Input_6();
uint32_t Get_Cooling_DF_0_Input_7();
uint32_t Get_Cooling_DF_0_Input_8();
uint32_t Get_Cooling_DF_0_Input_9();
uint32_t Get_Cooling_DF_0_Input_10();
uint32_t Get_Cooling_DF_0_Input_11();
#endif
