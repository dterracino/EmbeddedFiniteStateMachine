#include "df_stateMachines.h"
#include <stdint.h>
#include "efsm_core.h"
#include "stdio.h"
#include "stdlib.h"

/*
----------------------------------------------------------------------------------------------------
Notes

The entire contents of this file are generated.
Typically, the user should not modify files which are generated. Reasons for this are:

-To avoid introducing errors.
-Additions are lost every time a file is generated (this can be counterproductive).

An important distinction to make is between a state machine definition, and an actual state machine.
instance.

In the EFSM environment, a state machine definition and a state machine instance are described as follows:

   State Machine Definition
      -A set of binary instructions (an array of 16 bit integers).
      -An array of pointers to the functions required for evaluating inputs.
      -An array of pointers to the functions required for performing actions.
      -Serves as a template for behavior.

   State Machine Instance
      -A variable of type EFSM_INSTANCE which has been initialized to a particular
       state machine definition.

Contents of this File:

-Binary Structure Declarations (variables of type EFSM_BINARY)
-State Machine Instance Declarations (variables of type EFSM_INSTANCE)
-Arrays for Function Pointers
-EFSM State Machine Binary Arrays (arrays of type uint16_t)
-Initialization Function
-EFSM Process Initialization Function (the only function in this file that should be called
 from user code)
/*
----------------------------------------------------------------------------------------------------
Binary Structure Declarations.

Note:
Reference to a set of binary instructions (an array of 16 bit integers) is 
"wrapped" in a corresponding structure of type EFSM_BINARY. In turn, it is the initialized
instance of an EFSM_BINARY variable which is used by the EFSM execution mechanism. The 
typedef for the EFSM_BINARY struct may be found in efsm_core.h.
*/
EFSM_BINARY HVAC_DF_Binary;
EFSM_BINARY Blower_DF_Binary;
EFSM_BINARY Heating_DF_Binary;
EFSM_BINARY Cooling_DF_Binary;
EFSM_BINARY Ovrd_Discharge_DF_Binary;
EFSM_BINARY Freezestat_DF_Binary;
EFSM_BINARY MixingBox_DF_Binary;
/*
----------------------------------------------------------------------------------------------------
State Machine Instances
*/
/*HVAC_DF Instances*/
EFSM_INSTANCE HVAC_DF_Instance_0;

/*Blower_DF Instances*/
EFSM_INSTANCE Blower_DF_Instance_0;

/*Heating_DF Instances*/
EFSM_INSTANCE Heating_DF_Instance_0;

/*Cooling_DF Instances*/
EFSM_INSTANCE Cooling_DF_Instance_0;

/*Ovrd_Discharge_DF Instances*/
EFSM_INSTANCE Ovrd_Discharge_DF_Instance_0;

/*Freezestat_DF Instances*/
EFSM_INSTANCE Freezestat_DF_Instance_0;

/*MixingBox_DF Instances*/
EFSM_INSTANCE MixingBox_DF_Instance_0;

/*
----------------------------------------------------------------------------------------------------
State Machine Instance Array Declaration
*/
EFSM_INSTANCE * efsmInstanceArray[EFSM_TOTAL_NUMBER_OF_STATE_MACHINE_INSTANCES];
/*
----------------------------------------------------------------------------------------------------
Arrays for Function Pointers.

Note:
These arrays are used by the EFSM execution mechanism to access the functions which perform actions
and evaluate inputs as required by the binary instructions for a given state machine definition.
A given state machine definition will have a single array of pointers to input query functions, and
a single array of pointers to action functions. They are initialized by calling the Initialization 
function (generated below), and are collectively referred to as the "function reference arrays" for 
a given state machine definition. 
*/

/*State Machine Definition "HVAC_DF"*/

/*Array for pointers to input query functions.*/
uint8_t (*HVAC_DF_Inputs[EFSM_HVAC_DF_NUMBER_OF_INPUTS])(uint8_t indexOnEfsmType);

/*Array for pointers to action functions.*/
void (*HVAC_DF_OutputActions[EFSM_HVAC_DF_NUMBER_OF_OUTPUTS])(uint8_t indexOnEfsmType);

/*State Machine Definition "Blower_DF"*/

/*Array for pointers to input query functions.*/
uint8_t (*Blower_DF_Inputs[EFSM_BLOWER_DF_NUMBER_OF_INPUTS])(uint8_t indexOnEfsmType);

/*Array for pointers to action functions.*/
void (*Blower_DF_OutputActions[EFSM_BLOWER_DF_NUMBER_OF_OUTPUTS])(uint8_t indexOnEfsmType);

/*State Machine Definition "Heating_DF"*/

/*Array for pointers to input query functions.*/
uint8_t (*Heating_DF_Inputs[EFSM_HEATING_DF_NUMBER_OF_INPUTS])(uint8_t indexOnEfsmType);

/*Array for pointers to action functions.*/
void (*Heating_DF_OutputActions[EFSM_HEATING_DF_NUMBER_OF_OUTPUTS])(uint8_t indexOnEfsmType);

/*State Machine Definition "Cooling_DF"*/

/*Array for pointers to input query functions.*/
uint8_t (*Cooling_DF_Inputs[EFSM_COOLING_DF_NUMBER_OF_INPUTS])(uint8_t indexOnEfsmType);

/*Array for pointers to action functions.*/
void (*Cooling_DF_OutputActions[EFSM_COOLING_DF_NUMBER_OF_OUTPUTS])(uint8_t indexOnEfsmType);

/*State Machine Definition "Ovrd_Discharge_DF"*/

/*Array for pointers to input query functions.*/
uint8_t (*Ovrd_Discharge_DF_Inputs[EFSM_OVRD_DISCHARGE_DF_NUMBER_OF_INPUTS])(uint8_t indexOnEfsmType);

/*Array for pointers to action functions.*/
void (*Ovrd_Discharge_DF_OutputActions[EFSM_OVRD_DISCHARGE_DF_NUMBER_OF_OUTPUTS])(uint8_t indexOnEfsmType);

/*State Machine Definition "Freezestat_DF"*/

/*Array for pointers to input query functions.*/
uint8_t (*Freezestat_DF_Inputs[EFSM_FREEZESTAT_DF_NUMBER_OF_INPUTS])(uint8_t indexOnEfsmType);

/*Array for pointers to action functions.*/
void (*Freezestat_DF_OutputActions[EFSM_FREEZESTAT_DF_NUMBER_OF_OUTPUTS])(uint8_t indexOnEfsmType);

/*State Machine Definition "MixingBox_DF"*/

/*Array for pointers to input query functions.*/
uint8_t (*MixingBox_DF_Inputs[EFSM_MIXINGBOX_DF_NUMBER_OF_INPUTS])(uint8_t indexOnEfsmType);

/*Array for pointers to action functions.*/
void (*MixingBox_DF_OutputActions[EFSM_MIXINGBOX_DF_NUMBER_OF_OUTPUTS])(uint8_t indexOnEfsmType);

/*
----------------------------------------------------------------------------------------------------
EFSM Definition Binary Arrays
*/

/* HVAC_DF Definition*/
uint16_t efsm_HVAC_DF_binaryData[] = {

    /*[0]: Number of states */
    10, 
    
    /*[1]: Total number of inputs. */
    11, 
    
    /*[2]: Initial state identifier. */
    0, 
    
    /*[3]: EFSM binary index of state Initial State */
    13, 
    
    /*[4]: EFSM binary index of state IDLE */
    25, 
    
    /*[5]: EFSM binary index of state BLOWER */
    38, 
    
    /*[6]: EFSM binary index of state STOP_BLOWER */
    71, 
    
    /*[7]: EFSM binary index of state PREHEAT */
    82, 
    
    /*[8]: EFSM binary index of state HEATING */
    106, 
    
    /*[9]: EFSM binary index of state STOP_HEAT */
    125, 
    
    /*[10]: EFSM binary index of state PRECOOL */
    151, 
    
    /*[11]: EFSM binary index of state COOLING */
    173, 
    
    /*[12]: EFSM binary index of state STOP_COOL */
    189, 
    
    /*[13]: State Initial State base index. */
    /*[13]: Number of inputs. */
    1, 
    
    /*[14]: Number of transitions. */
    1, 
    
    /*[15]: EFSM binary index of IQFN data for state Initial State */
    17, 
    
    /*[16]: EFSM binary index of transition InitIdle data. */
    18, 
    
    /*[17]: State Initial State start of IQFN data. */
    /*[17]: IQFN function reference index for prototype IsStartupTimeElapsed */
    10, 
    
    /*[18]: Transition InitIdle data. */
    /*[18]: EFSM binary index of transition InitIdle actions data. */
    22, 
    
    /*[19]: Next state after transition. */
    1, 
    
    /*[20]: Number of opcodes. */
    1, 
    
    /*[21]: Opcodes 0 and 1 */
    138, 
    
    /*[22]: Transition Actions data. */
    /*[22]: Number of transition actions for transition InitIdle */
    2, 
    
    /*[23]: Action HvacInit function reference index. */
    0, 
    
    /*[24]: Action HvacEnterIdle function reference index. */
    8, 
    
    /*[25]: State IDLE base index. */
    /*[25]: Number of inputs. */
    2, 
    
    /*[26]: Number of transitions. */
    1, 
    
    /*[27]: EFSM binary index of IQFN data for state IDLE */
    29, 
    
    /*[28]: EFSM binary index of transition IdleToBlower data. */
    31, 
    
    /*[29]: State IDLE start of IQFN data. */
    /*[29]: IQFN function reference index for prototype IsBlowerRequired */
    0, 
    
    /*[30]: IQFN function reference index for prototype CanBlow */
    1, 
    
    /*[31]: Transition IdleToBlower data. */
    /*[31]: EFSM binary index of transition IdleToBlower actions data. */
    36, 
    
    /*[32]: Next state after transition. */
    2, 
    
    /*[33]: Number of opcodes. */
    3, 
    
    /*[34]: Opcodes 0 and 1 */
    33152, 
    
    /*[35]: Transition Actions data. */
    /*[35]: Opcodes 2 and 3 */
    2, 
    
    /*[36]: Transition Actions data. */
    /*[36]: Number of transition actions for transition IdleToBlower */
    1, 
    
    /*[37]: Action HvacEnterBlower function reference index. */
    1, 
    
    /*[38]: State BLOWER base index. */
    /*[38]: Number of inputs. */
    6, 
    
    /*[39]: Number of transitions. */
    3, 
    
    /*[40]: EFSM binary index of IQFN data for state BLOWER */
    44, 
    
    /*[41]: EFSM binary index of transition BlowerToStopBlower data. */
    50, 
    
    /*[42]: EFSM binary index of transition BlowerToPreheat data. */
    57, 
    
    /*[43]: EFSM binary index of transition BlowerToPrecool data. */
    64, 
    
    /*[44]: State BLOWER start of IQFN data. */
    /*[44]: IQFN function reference index for prototype IsBlowerRequired */
    0, 
    
    /*[45]: IQFN function reference index for prototype CanBlow */
    1, 
    
    /*[46]: IQFN function reference index for prototype IsInHeatRange */
    2, 
    
    /*[47]: IQFN function reference index for prototype CanHeat */
    3, 
    
    /*[48]: IQFN function reference index for prototype IsInCoolRange */
    4, 
    
    /*[49]: IQFN function reference index for prototype CanCool */
    5, 
    
    /*[50]: Transition BlowerToStopBlower data. */
    /*[50]: EFSM binary index of transition BlowerToStopBlower actions data. */
    56, 
    
    /*[51]: Next state after transition. */
    3, 
    
    /*[52]: Number of opcodes. */
    5, 
    
    /*[53]: Opcodes 0 and 1 */
    896, 
    
    /*[54]: Transition Actions data. */
    /*[54]: Opcodes 2 and 3 */
    897, 
    
    /*[55]: Transition Actions data. */
    /*[55]: Opcodes 4 and 5 */
    1, 
    
    /*[56]: Transition Actions data. */
    /*[56]: Number of transition actions for transition BlowerToStopBlower */
    0, 
    
    /*[57]: Transition BlowerToPreheat data. */
    /*[57]: EFSM binary index of transition BlowerToPreheat actions data. */
    62, 
    
    /*[58]: Next state after transition. */
    4, 
    
    /*[59]: Number of opcodes. */
    3, 
    
    /*[60]: Opcodes 0 and 1 */
    33666, 
    
    /*[61]: Transition Actions data. */
    /*[61]: Opcodes 2 and 3 */
    2, 
    
    /*[62]: Transition Actions data. */
    /*[62]: Number of transition actions for transition BlowerToPreheat */
    1, 
    
    /*[63]: Action HvacEnterPreheat function reference index. */
    2, 
    
    /*[64]: Transition BlowerToPrecool data. */
    /*[64]: EFSM binary index of transition BlowerToPrecool actions data. */
    69, 
    
    /*[65]: Next state after transition. */
    7, 
    
    /*[66]: Number of opcodes. */
    3, 
    
    /*[67]: Opcodes 0 and 1 */
    34180, 
    
    /*[68]: Transition Actions data. */
    /*[68]: Opcodes 2 and 3 */
    2, 
    
    /*[69]: Transition Actions data. */
    /*[69]: Number of transition actions for transition BlowerToPrecool */
    1, 
    
    /*[70]: Action HvacEnterPrecool function reference index. */
    5, 
    
    /*[71]: State STOP_BLOWER base index. */
    /*[71]: Number of inputs. */
    1, 
    
    /*[72]: Number of transitions. */
    1, 
    
    /*[73]: EFSM binary index of IQFN data for state STOP_BLOWER */
    75, 
    
    /*[74]: EFSM binary index of transition StopBlowerToIdle data. */
    76, 
    
    /*[75]: State STOP_BLOWER start of IQFN data. */
    /*[75]: IQFN function reference index for prototype IsStartupTimeElapsed */
    10, 
    
    /*[76]: Transition StopBlowerToIdle data. */
    /*[76]: EFSM binary index of transition StopBlowerToIdle actions data. */
    80, 
    
    /*[77]: Next state after transition. */
    1, 
    
    /*[78]: Number of opcodes. */
    1, 
    
    /*[79]: Opcodes 0 and 1 */
    138, 
    
    /*[80]: Transition Actions data. */
    /*[80]: Number of transition actions for transition StopBlowerToIdle */
    1, 
    
    /*[81]: Action HvacEnterIdle function reference index. */
    8, 
    
    /*[82]: State PREHEAT base index. */
    /*[82]: Number of inputs. */
    4, 
    
    /*[83]: Number of transitions. */
    2, 
    
    /*[84]: EFSM binary index of IQFN data for state PREHEAT */
    87, 
    
    /*[85]: EFSM binary index of transition PreheatToBlower data. */
    91, 
    
    /*[86]: EFSM binary index of transition PreheatToHeating data. */
    99, 
    
    /*[87]: State PREHEAT start of IQFN data. */
    /*[87]: IQFN function reference index for prototype IsInHeatRange */
    2, 
    
    /*[88]: IQFN function reference index for prototype CanHeat */
    3, 
    
    /*[89]: IQFN function reference index for prototype IsBlowerOn */
    6, 
    
    /*[90]: IQFN function reference index for prototype IsBlowerInDelayedStart */
    7, 
    
    /*[91]: Transition PreheatToBlower data. */
    /*[91]: EFSM binary index of transition PreheatToBlower actions data. */
    97, 
    
    /*[92]: Next state after transition. */
    2, 
    
    /*[93]: Number of opcodes. */
    5, 
    
    /*[94]: Opcodes 0 and 1 */
    898, 
    
    /*[95]: Transition Actions data. */
    /*[95]: Opcodes 2 and 3 */
    899, 
    
    /*[96]: Transition Actions data. */
    /*[96]: Opcodes 4 and 5 */
    1, 
    
    /*[97]: Transition Actions data. */
    /*[97]: Number of transition actions for transition PreheatToBlower */
    1, 
    
    /*[98]: Action HvacEnterBlower function reference index. */
    1, 
    
    /*[99]: Transition PreheatToHeating data. */
    /*[99]: EFSM binary index of transition PreheatToHeating actions data. */
    104, 
    
    /*[100]: Next state after transition. */
    5, 
    
    /*[101]: Number of opcodes. */
    3, 
    
    /*[102]: Opcodes 0 and 1 */
    34694, 
    
    /*[103]: Transition Actions data. */
    /*[103]: Opcodes 2 and 3 */
    1, 
    
    /*[104]: Transition Actions data. */
    /*[104]: Number of transition actions for transition PreheatToHeating */
    1, 
    
    /*[105]: Action HvacEnterHeating function reference index. */
    3, 
    
    /*[106]: State HEATING base index. */
    /*[106]: Number of inputs. */
    4, 
    
    /*[107]: Number of transitions. */
    1, 
    
    /*[108]: EFSM binary index of IQFN data for state HEATING */
    110, 
    
    /*[109]: EFSM binary index of transition HeatingToStopHeat data. */
    114, 
    
    /*[110]: State HEATING start of IQFN data. */
    /*[110]: IQFN function reference index for prototype IsInHeatRange */
    2, 
    
    /*[111]: IQFN function reference index for prototype CanHeat */
    3, 
    
    /*[112]: IQFN function reference index for prototype IsBlowerOn */
    6, 
    
    /*[113]: IQFN function reference index for prototype IsBlowerInDelayedStart */
    7, 
    
    /*[114]: Transition HeatingToStopHeat data. */
    /*[114]: EFSM binary index of transition HeatingToStopHeat actions data. */
    123, 
    
    /*[115]: Next state after transition. */
    6, 
    
    /*[116]: Number of opcodes. */
    11, 
    
    /*[117]: Opcodes 0 and 1 */
    898, 
    
    /*[118]: Transition Actions data. */
    /*[118]: Opcodes 2 and 3 */
    899, 
    
    /*[119]: Transition Actions data. */
    /*[119]: Opcodes 4 and 5 */
    34305, 
    
    /*[120]: Transition Actions data. */
    /*[120]: Opcodes 6 and 7 */
    34563, 
    
    /*[121]: Transition Actions data. */
    /*[121]: Opcodes 8 and 9 */
    515, 
    
    /*[122]: Transition Actions data. */
    /*[122]: Opcodes 10 and 11 */
    1, 
    
    /*[123]: Transition Actions data. */
    /*[123]: Number of transition actions for transition HeatingToStopHeat */
    1, 
    
    /*[124]: Action HvacEnterStopHeat function reference index. */
    4, 
    
    /*[125]: State STOP_HEAT base index. */
    /*[125]: Number of inputs. */
    5, 
    
    /*[126]: Number of transitions. */
    2, 
    
    /*[127]: EFSM binary index of IQFN data for state STOP_HEAT */
    130, 
    
    /*[128]: EFSM binary index of transition StopHeatToHeating data. */
    135, 
    
    /*[129]: EFSM binary index of transition StopHeatToBlower data. */
    145, 
    
    /*[130]: State STOP_HEAT start of IQFN data. */
    /*[130]: IQFN function reference index for prototype IsInHeatRange */
    2, 
    
    /*[131]: IQFN function reference index for prototype CanHeat */
    3, 
    
    /*[132]: IQFN function reference index for prototype IsHeatOn */
    8, 
    
    /*[133]: IQFN function reference index for prototype IsBlowerOn */
    6, 
    
    /*[134]: IQFN function reference index for prototype IsBlowerInDelayedStart */
    7, 
    
    /*[135]: Transition StopHeatToHeating data. */
    /*[135]: EFSM binary index of transition StopHeatToHeating actions data. */
    143, 
    
    /*[136]: Next state after transition. */
    5, 
    
    /*[137]: Number of opcodes. */
    9, 
    
    /*[138]: Opcodes 0 and 1 */
    33666, 
    
    /*[139]: Transition Actions data. */
    /*[139]: Opcodes 2 and 3 */
    34818, 
    
    /*[140]: Transition Actions data. */
    /*[140]: Opcodes 4 and 5 */
    34306, 
    
    /*[141]: Transition Actions data. */
    /*[141]: Opcodes 6 and 7 */
    391, 
    
    /*[142]: Transition Actions data. */
    /*[142]: Opcodes 8 and 9 */
    2, 
    
    /*[143]: Transition Actions data. */
    /*[143]: Number of transition actions for transition StopHeatToHeating */
    1, 
    
    /*[144]: Action HvacEnterHeating function reference index. */
    3, 
    
    /*[145]: Transition StopHeatToBlower data. */
    /*[145]: EFSM binary index of transition StopHeatToBlower actions data. */
    149, 
    
    /*[146]: Next state after transition. */
    2, 
    
    /*[147]: Number of opcodes. */
    2, 
    
    /*[148]: Opcodes 0 and 1 */
    904, 
    
    /*[149]: Transition Actions data. */
    /*[149]: Number of transition actions for transition StopHeatToBlower */
    1, 
    
    /*[150]: Action HvacEnterBlower function reference index. */
    1, 
    
    /*[151]: State PRECOOL base index. */
    /*[151]: Number of inputs. */
    3, 
    
    /*[152]: Number of transitions. */
    2, 
    
    /*[153]: EFSM binary index of IQFN data for state PRECOOL */
    156, 
    
    /*[154]: EFSM binary index of transition PrecoolToBlower data. */
    159, 
    
    /*[155]: EFSM binary index of transition PrecoolToCooling data. */
    167, 
    
    /*[156]: State PRECOOL start of IQFN data. */
    /*[156]: IQFN function reference index for prototype IsInCoolRange */
    4, 
    
    /*[157]: IQFN function reference index for prototype CanCool */
    5, 
    
    /*[158]: IQFN function reference index for prototype IsBlowerOn */
    6, 
    
    /*[159]: Transition PrecoolToBlower data. */
    /*[159]: EFSM binary index of transition PrecoolToBlower actions data. */
    165, 
    
    /*[160]: Next state after transition. */
    2, 
    
    /*[161]: Number of opcodes. */
    5, 
    
    /*[162]: Opcodes 0 and 1 */
    900, 
    
    /*[163]: Transition Actions data. */
    /*[163]: Opcodes 2 and 3 */
    901, 
    
    /*[164]: Transition Actions data. */
    /*[164]: Opcodes 4 and 5 */
    1, 
    
    /*[165]: Transition Actions data. */
    /*[165]: Number of transition actions for transition PrecoolToBlower */
    1, 
    
    /*[166]: Action HvacEnterBlower function reference index. */
    1, 
    
    /*[167]: Transition PrecoolToCooling data. */
    /*[167]: EFSM binary index of transition PrecoolToCooling actions data. */
    171, 
    
    /*[168]: Next state after transition. */
    8, 
    
    /*[169]: Number of opcodes. */
    1, 
    
    /*[170]: Opcodes 0 and 1 */
    134, 
    
    /*[171]: Transition Actions data. */
    /*[171]: Number of transition actions for transition PrecoolToCooling */
    1, 
    
    /*[172]: Action HvacEnterCooling function reference index. */
    6, 
    
    /*[173]: State COOLING base index. */
    /*[173]: Number of inputs. */
    3, 
    
    /*[174]: Number of transitions. */
    1, 
    
    /*[175]: EFSM binary index of IQFN data for state COOLING */
    177, 
    
    /*[176]: EFSM binary index of transition CoolingToStopCool data. */
    180, 
    
    /*[177]: State COOLING start of IQFN data. */
    /*[177]: IQFN function reference index for prototype IsInCoolRange */
    4, 
    
    /*[178]: IQFN function reference index for prototype CanCool */
    5, 
    
    /*[179]: IQFN function reference index for prototype IsBlowerOn */
    6, 
    
    /*[180]: Transition CoolingToStopCool data. */
    /*[180]: EFSM binary index of transition CoolingToStopCool actions data. */
    187, 
    
    /*[181]: Next state after transition. */
    9, 
    
    /*[182]: Number of opcodes. */
    8, 
    
    /*[183]: Opcodes 0 and 1 */
    900, 
    
    /*[184]: Transition Actions data. */
    /*[184]: Opcodes 2 and 3 */
    901, 
    
    /*[185]: Transition Actions data. */
    /*[185]: Opcodes 4 and 5 */
    34305, 
    
    /*[186]: Transition Actions data. */
    /*[186]: Opcodes 6 and 7 */
    259, 
    
    /*[187]: Transition Actions data. */
    /*[187]: Number of transition actions for transition CoolingToStopCool */
    1, 
    
    /*[188]: Action HvacEnterStopCool function reference index. */
    7, 
    
    /*[189]: State STOP_COOL base index. */
    /*[189]: Number of inputs. */
    4, 
    
    /*[190]: Number of transitions. */
    2, 
    
    /*[191]: EFSM binary index of IQFN data for state STOP_COOL */
    194, 
    
    /*[192]: EFSM binary index of transition StopCoolToCooling data. */
    198, 
    
    /*[193]: EFSM binary index of transition StopCoolToBlower data. */
    207, 
    
    /*[194]: State STOP_COOL start of IQFN data. */
    /*[194]: IQFN function reference index for prototype IsInCoolRange */
    4, 
    
    /*[195]: IQFN function reference index for prototype CanCool */
    5, 
    
    /*[196]: IQFN function reference index for prototype IsCoolOn */
    9, 
    
    /*[197]: IQFN function reference index for prototype IsBlowerOn */
    6, 
    
    /*[198]: Transition StopCoolToCooling data. */
    /*[198]: EFSM binary index of transition StopCoolToCooling actions data. */
    205, 
    
    /*[199]: Next state after transition. */
    8, 
    
    /*[200]: Number of opcodes. */
    7, 
    
    /*[201]: Opcodes 0 and 1 */
    34180, 
    
    /*[202]: Transition Actions data. */
    /*[202]: Opcodes 2 and 3 */
    35074, 
    
    /*[203]: Transition Actions data. */
    /*[203]: Opcodes 4 and 5 */
    34306, 
    
    /*[204]: Transition Actions data. */
    /*[204]: Opcodes 6 and 7 */
    2, 
    
    /*[205]: Transition Actions data. */
    /*[205]: Number of transition actions for transition StopCoolToCooling */
    1, 
    
    /*[206]: Action HvacEnterCooling function reference index. */
    6, 
    
    /*[207]: Transition StopCoolToBlower data. */
    /*[207]: EFSM binary index of transition StopCoolToBlower actions data. */
    211, 
    
    /*[208]: Next state after transition. */
    2, 
    
    /*[209]: Number of opcodes. */
    2, 
    
    /*[210]: Opcodes 0 and 1 */
    905, 
    
    /*[211]: Transition Actions data. */
    /*[211]: Number of transition actions for transition StopCoolToBlower */
    1, 
    
    /*[212]: Action HvacEnterBlower function reference index. */
    1 
    
};

/* Blower_DF Definition*/
uint16_t efsm_Blower_DF_binaryData[] = {

    /*[0]: Number of states */
    9, 
    
    /*[1]: Total number of inputs. */
    9, 
    
    /*[2]: Initial state identifier. */
    0, 
    
    /*[3]: EFSM binary index of state Initial State */
    12, 
    
    /*[4]: EFSM binary index of state IDLE */
    24, 
    
    /*[5]: EFSM binary index of state PRE-START */
    35, 
    
    /*[6]: EFSM binary index of state DELAYED_START */
    53, 
    
    /*[7]: EFSM binary index of state OPEN_DAMPER */
    72, 
    
    /*[8]: EFSM binary index of state PROVE_AIR */
    91, 
    
    /*[9]: EFSM binary index of state BLOWER_ON */
    118, 
    
    /*[10]: EFSM binary index of state DELAYED_STOP */
    151, 
    
    /*[11]: EFSM binary index of state RECOVER_AIR */
    170, 
    
    /*[12]: State Initial State base index. */
    /*[12]: Number of inputs. */
    1, 
    
    /*[13]: Number of transitions. */
    1, 
    
    /*[14]: EFSM binary index of IQFN data for state Initial State */
    16, 
    
    /*[15]: EFSM binary index of transition InitIdle data. */
    17, 
    
    /*[16]: State Initial State start of IQFN data. */
    /*[16]: IQFN function reference index for prototype IsStartupTimeElapsed */
    0, 
    
    /*[17]: Transition InitIdle data. */
    /*[17]: EFSM binary index of transition InitIdle actions data. */
    21, 
    
    /*[18]: Next state after transition. */
    1, 
    
    /*[19]: Number of opcodes. */
    1, 
    
    /*[20]: Opcodes 0 and 1 */
    128, 
    
    /*[21]: Transition Actions data. */
    /*[21]: Number of transition actions for transition InitIdle */
    2, 
    
    /*[22]: Action BlowerInitIdle function reference index. */
    7, 
    
    /*[23]: Action BlowerEnterIdle function reference index. */
    0, 
    
    /*[24]: State IDLE base index. */
    /*[24]: Number of inputs. */
    1, 
    
    /*[25]: Number of transitions. */
    1, 
    
    /*[26]: EFSM binary index of IQFN data for state IDLE */
    28, 
    
    /*[27]: EFSM binary index of transition IdleToPreStart data. */
    29, 
    
    /*[28]: State IDLE start of IQFN data. */
    /*[28]: IQFN function reference index for prototype IsHvacCallingForBlower */
    1, 
    
    /*[29]: Transition IdleToPreStart data. */
    /*[29]: EFSM binary index of transition IdleToPreStart actions data. */
    33, 
    
    /*[30]: Next state after transition. */
    2, 
    
    /*[31]: Number of opcodes. */
    1, 
    
    /*[32]: Opcodes 0 and 1 */
    129, 
    
    /*[33]: Transition Actions data. */
    /*[33]: Number of transition actions for transition IdleToPreStart */
    1, 
    
    /*[34]: Action BlowerEnterPreStart function reference index. */
    1, 
    
    /*[35]: State PRE-START base index. */
    /*[35]: Number of inputs. */
    1, 
    
    /*[36]: Number of transitions. */
    2, 
    
    /*[37]: EFSM binary index of IQFN data for state PRE-START */
    40, 
    
    /*[38]: EFSM binary index of transition PreStartToDelayedStart data. */
    41, 
    
    /*[39]: EFSM binary index of transition PreStartToOpenDamper data. */
    47, 
    
    /*[40]: State PRE-START start of IQFN data. */
    /*[40]: IQFN function reference index for prototype IsDelayedStartEnabled */
    4, 
    
    /*[41]: Transition PreStartToDelayedStart data. */
    /*[41]: EFSM binary index of transition PreStartToDelayedStart actions data. */
    45, 
    
    /*[42]: Next state after transition. */
    3, 
    
    /*[43]: Number of opcodes. */
    1, 
    
    /*[44]: Opcodes 0 and 1 */
    132, 
    
    /*[45]: Transition Actions data. */
    /*[45]: Number of transition actions for transition PreStartToDelayedStart */
    1, 
    
    /*[46]: Action BlowerEnterDelayedStart function reference index. */
    2, 
    
    /*[47]: Transition PreStartToOpenDamper data. */
    /*[47]: EFSM binary index of transition PreStartToOpenDamper actions data. */
    51, 
    
    /*[48]: Next state after transition. */
    4, 
    
    /*[49]: Number of opcodes. */
    2, 
    
    /*[50]: Opcodes 0 and 1 */
    900, 
    
    /*[51]: Transition Actions data. */
    /*[51]: Number of transition actions for transition PreStartToOpenDamper */
    1, 
    
    /*[52]: Action BlowerEnterOpenDamper function reference index. */
    3, 
    
    /*[53]: State DELAYED_START base index. */
    /*[53]: Number of inputs. */
    2, 
    
    /*[54]: Number of transitions. */
    2, 
    
    /*[55]: EFSM binary index of IQFN data for state DELAYED_START */
    58, 
    
    /*[56]: EFSM binary index of transition DelayedStartToOpenDamper data. */
    60, 
    
    /*[57]: EFSM binary index of transition DelayedStartToIdle data. */
    66, 
    
    /*[58]: State DELAYED_START start of IQFN data. */
    /*[58]: IQFN function reference index for prototype IsDelayedStartTimerExpired */
    5, 
    
    /*[59]: IQFN function reference index for prototype IsHvacCallingForBlower */
    1, 
    
    /*[60]: Transition DelayedStartToOpenDamper data. */
    /*[60]: EFSM binary index of transition DelayedStartToOpenDamper actions data. */
    64, 
    
    /*[61]: Next state after transition. */
    4, 
    
    /*[62]: Number of opcodes. */
    1, 
    
    /*[63]: Opcodes 0 and 1 */
    133, 
    
    /*[64]: Transition Actions data. */
    /*[64]: Number of transition actions for transition DelayedStartToOpenDamper */
    1, 
    
    /*[65]: Action BlowerEnterOpenDamper function reference index. */
    3, 
    
    /*[66]: Transition DelayedStartToIdle data. */
    /*[66]: EFSM binary index of transition DelayedStartToIdle actions data. */
    70, 
    
    /*[67]: Next state after transition. */
    1, 
    
    /*[68]: Number of opcodes. */
    2, 
    
    /*[69]: Opcodes 0 and 1 */
    897, 
    
    /*[70]: Transition Actions data. */
    /*[70]: Number of transition actions for transition DelayedStartToIdle */
    1, 
    
    /*[71]: Action BlowerEnterIdle function reference index. */
    0, 
    
    /*[72]: State OPEN_DAMPER base index. */
    /*[72]: Number of inputs. */
    2, 
    
    /*[73]: Number of transitions. */
    2, 
    
    /*[74]: EFSM binary index of IQFN data for state OPEN_DAMPER */
    77, 
    
    /*[75]: EFSM binary index of transition OpenDamperToProveAir data. */
    79, 
    
    /*[76]: EFSM binary index of transition OpenDamperToIdle data. */
    85, 
    
    /*[77]: State OPEN_DAMPER start of IQFN data. */
    /*[77]: IQFN function reference index for prototype IsDamperEndReceived */
    2, 
    
    /*[78]: IQFN function reference index for prototype IsHvacCallingForBlower */
    1, 
    
    /*[79]: Transition OpenDamperToProveAir data. */
    /*[79]: EFSM binary index of transition OpenDamperToProveAir actions data. */
    83, 
    
    /*[80]: Next state after transition. */
    5, 
    
    /*[81]: Number of opcodes. */
    1, 
    
    /*[82]: Opcodes 0 and 1 */
    130, 
    
    /*[83]: Transition Actions data. */
    /*[83]: Number of transition actions for transition OpenDamperToProveAir */
    1, 
    
    /*[84]: Action BlowerEnterProveAir function reference index. */
    4, 
    
    /*[85]: Transition OpenDamperToIdle data. */
    /*[85]: EFSM binary index of transition OpenDamperToIdle actions data. */
    89, 
    
    /*[86]: Next state after transition. */
    1, 
    
    /*[87]: Number of opcodes. */
    2, 
    
    /*[88]: Opcodes 0 and 1 */
    897, 
    
    /*[89]: Transition Actions data. */
    /*[89]: Number of transition actions for transition OpenDamperToIdle */
    1, 
    
    /*[90]: Action BlowerEnterIdle function reference index. */
    0, 
    
    /*[91]: State PROVE_AIR base index. */
    /*[91]: Number of inputs. */
    3, 
    
    /*[92]: Number of transitions. */
    3, 
    
    /*[93]: EFSM binary index of IQFN data for state PROVE_AIR */
    97, 
    
    /*[94]: EFSM binary index of transition ProveAirToOpenDamper data. */
    100, 
    
    /*[95]: EFSM binary index of transition ProveAirToBlowerOn data. */
    106, 
    
    /*[96]: EFSM binary index of transition ProveAirToIdle data. */
    112, 
    
    /*[97]: State PROVE_AIR start of IQFN data. */
    /*[97]: IQFN function reference index for prototype IsDamperEndReceived */
    2, 
    
    /*[98]: IQFN function reference index for prototype IsAirProvingReceived */
    3, 
    
    /*[99]: IQFN function reference index for prototype IsHvacCallingForBlower */
    1, 
    
    /*[100]: Transition ProveAirToOpenDamper data. */
    /*[100]: EFSM binary index of transition ProveAirToOpenDamper actions data. */
    104, 
    
    /*[101]: Next state after transition. */
    4, 
    
    /*[102]: Number of opcodes. */
    2, 
    
    /*[103]: Opcodes 0 and 1 */
    898, 
    
    /*[104]: Transition Actions data. */
    /*[104]: Number of transition actions for transition ProveAirToOpenDamper */
    1, 
    
    /*[105]: Action BlowerEnterOpenDamper function reference index. */
    3, 
    
    /*[106]: Transition ProveAirToBlowerOn data. */
    /*[106]: EFSM binary index of transition ProveAirToBlowerOn actions data. */
    110, 
    
    /*[107]: Next state after transition. */
    6, 
    
    /*[108]: Number of opcodes. */
    1, 
    
    /*[109]: Opcodes 0 and 1 */
    131, 
    
    /*[110]: Transition Actions data. */
    /*[110]: Number of transition actions for transition ProveAirToBlowerOn */
    1, 
    
    /*[111]: Action BlowerEnterBlowerOn function reference index. */
    5, 
    
    /*[112]: Transition ProveAirToIdle data. */
    /*[112]: EFSM binary index of transition ProveAirToIdle actions data. */
    116, 
    
    /*[113]: Next state after transition. */
    1, 
    
    /*[114]: Number of opcodes. */
    2, 
    
    /*[115]: Opcodes 0 and 1 */
    897, 
    
    /*[116]: Transition Actions data. */
    /*[116]: Number of transition actions for transition ProveAirToIdle */
    1, 
    
    /*[117]: Action BlowerEnterIdle function reference index. */
    0, 
    
    /*[118]: State BLOWER_ON base index. */
    /*[118]: Number of inputs. */
    4, 
    
    /*[119]: Number of transitions. */
    3, 
    
    /*[120]: EFSM binary index of IQFN data for state BLOWER_ON */
    124, 
    
    /*[121]: EFSM binary index of transition BlowerOnToDelayedStop data. */
    128, 
    
    /*[122]: EFSM binary index of transition BlowerOnToIdle data. */
    135, 
    
    /*[123]: EFSM binary index of transition BlowerOnToRecoverAir data. */
    143, 
    
    /*[124]: State BLOWER_ON start of IQFN data. */
    /*[124]: IQFN function reference index for prototype IsHvacCallingForBlower */
    1, 
    
    /*[125]: IQFN function reference index for prototype IsDelayedStopEnabled */
    6, 
    
    /*[126]: IQFN function reference index for prototype IsAirProvingReceived */
    3, 
    
    /*[127]: IQFN function reference index for prototype IsDamperEndReceived */
    2, 
    
    /*[128]: Transition BlowerOnToDelayedStop data. */
    /*[128]: EFSM binary index of transition BlowerOnToDelayedStop actions data. */
    133, 
    
    /*[129]: Next state after transition. */
    7, 
    
    /*[130]: Number of opcodes. */
    4, 
    
    /*[131]: Opcodes 0 and 1 */
    897, 
    
    /*[132]: Transition Actions data. */
    /*[132]: Opcodes 2 and 3 */
    646, 
    
    /*[133]: Transition Actions data. */
    /*[133]: Number of transition actions for transition BlowerOnToDelayedStop */
    1, 
    
    /*[134]: Action BlowerEnterDelayedStop function reference index. */
    6, 
    
    /*[135]: Transition BlowerOnToIdle data. */
    /*[135]: EFSM binary index of transition BlowerOnToIdle actions data. */
    141, 
    
    /*[136]: Next state after transition. */
    1, 
    
    /*[137]: Number of opcodes. */
    5, 
    
    /*[138]: Opcodes 0 and 1 */
    897, 
    
    /*[139]: Transition Actions data. */
    /*[139]: Opcodes 2 and 3 */
    902, 
    
    /*[140]: Transition Actions data. */
    /*[140]: Opcodes 4 and 5 */
    2, 
    
    /*[141]: Transition Actions data. */
    /*[141]: Number of transition actions for transition BlowerOnToIdle */
    1, 
    
    /*[142]: Action BlowerEnterIdle function reference index. */
    0, 
    
    /*[143]: Transition BlowerOnToRecoverAir data. */
    /*[143]: EFSM binary index of transition BlowerOnToRecoverAir actions data. */
    149, 
    
    /*[144]: Next state after transition. */
    8, 
    
    /*[145]: Number of opcodes. */
    5, 
    
    /*[146]: Opcodes 0 and 1 */
    899, 
    
    /*[147]: Transition Actions data. */
    /*[147]: Opcodes 2 and 3 */
    898, 
    
    /*[148]: Transition Actions data. */
    /*[148]: Opcodes 4 and 5 */
    1, 
    
    /*[149]: Transition Actions data. */
    /*[149]: Number of transition actions for transition BlowerOnToRecoverAir */
    1, 
    
    /*[150]: Action BlowerEnterRecoverAir function reference index. */
    8, 
    
    /*[151]: State DELAYED_STOP base index. */
    /*[151]: Number of inputs. */
    2, 
    
    /*[152]: Number of transitions. */
    2, 
    
    /*[153]: EFSM binary index of IQFN data for state DELAYED_STOP */
    156, 
    
    /*[154]: EFSM binary index of transition DelayedStopToIdle data. */
    158, 
    
    /*[155]: EFSM binary index of transition DelayedStopToBlowerOn data. */
    164, 
    
    /*[156]: State DELAYED_STOP start of IQFN data. */
    /*[156]: IQFN function reference index for prototype IsDelayedStopTimerExpired */
    7, 
    
    /*[157]: IQFN function reference index for prototype IsHvacCallingForBlower */
    1, 
    
    /*[158]: Transition DelayedStopToIdle data. */
    /*[158]: EFSM binary index of transition DelayedStopToIdle actions data. */
    162, 
    
    /*[159]: Next state after transition. */
    1, 
    
    /*[160]: Number of opcodes. */
    1, 
    
    /*[161]: Opcodes 0 and 1 */
    135, 
    
    /*[162]: Transition Actions data. */
    /*[162]: Number of transition actions for transition DelayedStopToIdle */
    1, 
    
    /*[163]: Action BlowerEnterIdle function reference index. */
    0, 
    
    /*[164]: Transition DelayedStopToBlowerOn data. */
    /*[164]: EFSM binary index of transition DelayedStopToBlowerOn actions data. */
    168, 
    
    /*[165]: Next state after transition. */
    6, 
    
    /*[166]: Number of opcodes. */
    1, 
    
    /*[167]: Opcodes 0 and 1 */
    129, 
    
    /*[168]: Transition Actions data. */
    /*[168]: Number of transition actions for transition DelayedStopToBlowerOn */
    1, 
    
    /*[169]: Action BlowerEnterBlowerOn function reference index. */
    5, 
    
    /*[170]: State RECOVER_AIR base index. */
    /*[170]: Number of inputs. */
    3, 
    
    /*[171]: Number of transitions. */
    3, 
    
    /*[172]: EFSM binary index of IQFN data for state RECOVER_AIR */
    176, 
    
    /*[173]: EFSM binary index of transition RecoverAirToProveAir data. */
    179, 
    
    /*[174]: EFSM binary index of transition RecoverAirToOpenDamper data. */
    186, 
    
    /*[175]: EFSM binary index of transition RecoverAirToBlowerOn data. */
    193, 
    
    /*[176]: State RECOVER_AIR start of IQFN data. */
    /*[176]: IQFN function reference index for prototype IsRecoverAirTimerExpired */
    8, 
    
    /*[177]: IQFN function reference index for prototype IsAirProvingReceived */
    3, 
    
    /*[178]: IQFN function reference index for prototype IsDamperEndReceived */
    2, 
    
    /*[179]: Transition RecoverAirToProveAir data. */
    /*[179]: EFSM binary index of transition RecoverAirToProveAir actions data. */
    184, 
    
    /*[180]: Next state after transition. */
    5, 
    
    /*[181]: Number of opcodes. */
    4, 
    
    /*[182]: Opcodes 0 and 1 */
    33672, 
    
    /*[183]: Transition Actions data. */
    /*[183]: Opcodes 2 and 3 */
    515, 
    
    /*[184]: Transition Actions data. */
    /*[184]: Number of transition actions for transition RecoverAirToProveAir */
    1, 
    
    /*[185]: Action BlowerEnterProveAir function reference index. */
    4, 
    
    /*[186]: Transition RecoverAirToOpenDamper data. */
    /*[186]: EFSM binary index of transition RecoverAirToOpenDamper actions data. */
    191, 
    
    /*[187]: Next state after transition. */
    4, 
    
    /*[188]: Number of opcodes. */
    4, 
    
    /*[189]: Opcodes 0 and 1 */
    33416, 
    
    /*[190]: Transition Actions data. */
    /*[190]: Opcodes 2 and 3 */
    515, 
    
    /*[191]: Transition Actions data. */
    /*[191]: Number of transition actions for transition RecoverAirToOpenDamper */
    1, 
    
    /*[192]: Action BlowerEnterOpenDamper function reference index. */
    3, 
    
    /*[193]: Transition RecoverAirToBlowerOn data. */
    /*[193]: EFSM binary index of transition RecoverAirToBlowerOn actions data. */
    198, 
    
    /*[194]: Next state after transition. */
    6, 
    
    /*[195]: Number of opcodes. */
    3, 
    
    /*[196]: Opcodes 0 and 1 */
    33666, 
    
    /*[197]: Transition Actions data. */
    /*[197]: Opcodes 2 and 3 */
    2, 
    
    /*[198]: Transition Actions data. */
    /*[198]: Number of transition actions for transition RecoverAirToBlowerOn */
    1, 
    
    /*[199]: Action BlowerEnterBlowerOn function reference index. */
    5 
    
};

/* Heating_DF Definition*/
uint16_t efsm_Heating_DF_binaryData[] = {

    /*[0]: Number of states */
    10, 
    
    /*[1]: Total number of inputs. */
    7, 
    
    /*[2]: Initial state identifier. */
    0, 
    
    /*[3]: EFSM binary index of state Initial State */
    13, 
    
    /*[4]: EFSM binary index of state IDLE */
    24, 
    
    /*[5]: EFSM binary index of state DF_PRE_AIR */
    35, 
    
    /*[6]: EFSM binary index of state ON_NORMAL */
    53, 
    
    /*[7]: EFSM binary index of state ON_TESTING */
    74, 
    
    /*[8]: EFSM binary index of state STOP_DF */
    95, 
    
    /*[9]: EFSM binary index of state DF_PRE_PGAS */
    107, 
    
    /*[10]: EFSM binary index of state DF_PRE_SPARK */
    128, 
    
    /*[11]: EFSM binary index of state DF_PRE_MGAS */
    149, 
    
    /*[12]: EFSM binary index of state DF_SPARK_OFF */
    170, 
    
    /*[13]: State Initial State base index. */
    /*[13]: Number of inputs. */
    1, 
    
    /*[14]: Number of transitions. */
    1, 
    
    /*[15]: EFSM binary index of IQFN data for state Initial State */
    17, 
    
    /*[16]: EFSM binary index of transition InitIdle data. */
    18, 
    
    /*[17]: State Initial State start of IQFN data. */
    /*[17]: IQFN function reference index for prototype IsStartupTimeElapsed */
    0, 
    
    /*[18]: Transition InitIdle data. */
    /*[18]: EFSM binary index of transition InitIdle actions data. */
    22, 
    
    /*[19]: Next state after transition. */
    1, 
    
    /*[20]: Number of opcodes. */
    1, 
    
    /*[21]: Opcodes 0 and 1 */
    128, 
    
    /*[22]: Transition Actions data. */
    /*[22]: Number of transition actions for transition InitIdle */
    1, 
    
    /*[23]: Action HeatEnterIdle function reference index. */
    0, 
    
    /*[24]: State IDLE base index. */
    /*[24]: Number of inputs. */
    1, 
    
    /*[25]: Number of transitions. */
    1, 
    
    /*[26]: EFSM binary index of IQFN data for state IDLE */
    28, 
    
    /*[27]: EFSM binary index of transition IdleToStartDf data. */
    29, 
    
    /*[28]: State IDLE start of IQFN data. */
    /*[28]: IQFN function reference index for prototype IsHvacCallingForHeat */
    1, 
    
    /*[29]: Transition IdleToStartDf data. */
    /*[29]: EFSM binary index of transition IdleToStartDf actions data. */
    33, 
    
    /*[30]: Next state after transition. */
    2, 
    
    /*[31]: Number of opcodes. */
    1, 
    
    /*[32]: Opcodes 0 and 1 */
    129, 
    
    /*[33]: Transition Actions data. */
    /*[33]: Number of transition actions for transition IdleToStartDf */
    1, 
    
    /*[34]: Action HeatEnterPreAir function reference index. */
    1, 
    
    /*[35]: State DF_PRE_AIR base index. */
    /*[35]: Number of inputs. */
    2, 
    
    /*[36]: Number of transitions. */
    2, 
    
    /*[37]: EFSM binary index of IQFN data for state DF_PRE_AIR */
    40, 
    
    /*[38]: EFSM binary index of transition DfPreAirToPrePGas data. */
    42, 
    
    /*[39]: EFSM binary index of transition DfPreAirToStop data. */
    48, 
    
    /*[40]: State DF_PRE_AIR start of IQFN data. */
    /*[40]: IQFN function reference index for prototype IsBlowerOn */
    2, 
    
    /*[41]: IQFN function reference index for prototype IsHvacCallingForHeat */
    1, 
    
    /*[42]: Transition DfPreAirToPrePGas data. */
    /*[42]: EFSM binary index of transition DfPreAirToPrePGas actions data. */
    46, 
    
    /*[43]: Next state after transition. */
    6, 
    
    /*[44]: Number of opcodes. */
    1, 
    
    /*[45]: Opcodes 0 and 1 */
    130, 
    
    /*[46]: Transition Actions data. */
    /*[46]: Number of transition actions for transition DfPreAirToPrePGas */
    1, 
    
    /*[47]: Action HeatEnterPrePGas function reference index. */
    2, 
    
    /*[48]: Transition DfPreAirToStop data. */
    /*[48]: EFSM binary index of transition DfPreAirToStop actions data. */
    52, 
    
    /*[49]: Next state after transition. */
    5, 
    
    /*[50]: Number of opcodes. */
    2, 
    
    /*[51]: Opcodes 0 and 1 */
    897, 
    
    /*[52]: Transition Actions data. */
    /*[52]: Number of transition actions for transition DfPreAirToStop */
    0, 
    
    /*[53]: State ON_NORMAL base index. */
    /*[53]: Number of inputs. */
    3, 
    
    /*[54]: Number of transitions. */
    2, 
    
    /*[55]: EFSM binary index of IQFN data for state ON_NORMAL */
    58, 
    
    /*[56]: EFSM binary index of transition OnNormalToStopDf data. */
    61, 
    
    /*[57]: EFSM binary index of transition OnNormalToOnTesting data. */
    68, 
    
    /*[58]: State ON_NORMAL start of IQFN data. */
    /*[58]: IQFN function reference index for prototype IsHvacCallingForHeat */
    1, 
    
    /*[59]: IQFN function reference index for prototype IsBlowerOn */
    2, 
    
    /*[60]: IQFN function reference index for prototype IsTestingModeRequested */
    3, 
    
    /*[61]: Transition OnNormalToStopDf data. */
    /*[61]: EFSM binary index of transition OnNormalToStopDf actions data. */
    67, 
    
    /*[62]: Next state after transition. */
    5, 
    
    /*[63]: Number of opcodes. */
    5, 
    
    /*[64]: Opcodes 0 and 1 */
    897, 
    
    /*[65]: Transition Actions data. */
    /*[65]: Opcodes 2 and 3 */
    898, 
    
    /*[66]: Transition Actions data. */
    /*[66]: Opcodes 4 and 5 */
    1, 
    
    /*[67]: Transition Actions data. */
    /*[67]: Number of transition actions for transition OnNormalToStopDf */
    0, 
    
    /*[68]: Transition OnNormalToOnTesting data. */
    /*[68]: EFSM binary index of transition OnNormalToOnTesting actions data. */
    72, 
    
    /*[69]: Next state after transition. */
    4, 
    
    /*[70]: Number of opcodes. */
    1, 
    
    /*[71]: Opcodes 0 and 1 */
    131, 
    
    /*[72]: Transition Actions data. */
    /*[72]: Number of transition actions for transition OnNormalToOnTesting */
    1, 
    
    /*[73]: Action HeatEnterOnTesting function reference index. */
    3, 
    
    /*[74]: State ON_TESTING base index. */
    /*[74]: Number of inputs. */
    3, 
    
    /*[75]: Number of transitions. */
    2, 
    
    /*[76]: EFSM binary index of IQFN data for state ON_TESTING */
    79, 
    
    /*[77]: EFSM binary index of transition OnTestingToStopDf data. */
    82, 
    
    /*[78]: EFSM binary index of transition OnTestingToOnNormal data. */
    89, 
    
    /*[79]: State ON_TESTING start of IQFN data. */
    /*[79]: IQFN function reference index for prototype IsHvacCallingForHeat */
    1, 
    
    /*[80]: IQFN function reference index for prototype IsBlowerOn */
    2, 
    
    /*[81]: IQFN function reference index for prototype IsTestingModeRequested */
    3, 
    
    /*[82]: Transition OnTestingToStopDf data. */
    /*[82]: EFSM binary index of transition OnTestingToStopDf actions data. */
    88, 
    
    /*[83]: Next state after transition. */
    5, 
    
    /*[84]: Number of opcodes. */
    5, 
    
    /*[85]: Opcodes 0 and 1 */
    897, 
    
    /*[86]: Transition Actions data. */
    /*[86]: Opcodes 2 and 3 */
    898, 
    
    /*[87]: Transition Actions data. */
    /*[87]: Opcodes 4 and 5 */
    1, 
    
    /*[88]: Transition Actions data. */
    /*[88]: Number of transition actions for transition OnTestingToStopDf */
    0, 
    
    /*[89]: Transition OnTestingToOnNormal data. */
    /*[89]: EFSM binary index of transition OnTestingToOnNormal actions data. */
    93, 
    
    /*[90]: Next state after transition. */
    3, 
    
    /*[91]: Number of opcodes. */
    2, 
    
    /*[92]: Opcodes 0 and 1 */
    899, 
    
    /*[93]: Transition Actions data. */
    /*[93]: Number of transition actions for transition OnTestingToOnNormal */
    1, 
    
    /*[94]: Action HeatEnterOnNormal function reference index. */
    5, 
    
    /*[95]: State STOP_DF base index. */
    /*[95]: Number of inputs. */
    1, 
    
    /*[96]: Number of transitions. */
    1, 
    
    /*[97]: EFSM binary index of IQFN data for state STOP_DF */
    99, 
    
    /*[98]: EFSM binary index of transition StopDfToIdle data. */
    100, 
    
    /*[99]: State STOP_DF start of IQFN data. */
    /*[99]: IQFN function reference index for prototype IsStartupTimeElapsed */
    0, 
    
    /*[100]: Transition StopDfToIdle data. */
    /*[100]: EFSM binary index of transition StopDfToIdle actions data. */
    104, 
    
    /*[101]: Next state after transition. */
    1, 
    
    /*[102]: Number of opcodes. */
    1, 
    
    /*[103]: Opcodes 0 and 1 */
    128, 
    
    /*[104]: Transition Actions data. */
    /*[104]: Number of transition actions for transition StopDfToIdle */
    2, 
    
    /*[105]: Action HeatExitStopDf function reference index. */
    4, 
    
    /*[106]: Action HeatEnterIdle function reference index. */
    0, 
    
    /*[107]: State DF_PRE_PGAS base index. */
    /*[107]: Number of inputs. */
    3, 
    
    /*[108]: Number of transitions. */
    2, 
    
    /*[109]: EFSM binary index of IQFN data for state DF_PRE_PGAS */
    112, 
    
    /*[110]: EFSM binary index of transition DfPrePGasToPreSpark data. */
    115, 
    
    /*[111]: EFSM binary index of transition DfPrePGasToStop data. */
    121, 
    
    /*[112]: State DF_PRE_PGAS start of IQFN data. */
    /*[112]: IQFN function reference index for prototype IsPilotGasInReceived */
    4, 
    
    /*[113]: IQFN function reference index for prototype IsHvacCallingForHeat */
    1, 
    
    /*[114]: IQFN function reference index for prototype IsBlowerOn */
    2, 
    
    /*[115]: Transition DfPrePGasToPreSpark data. */
    /*[115]: EFSM binary index of transition DfPrePGasToPreSpark actions data. */
    119, 
    
    /*[116]: Next state after transition. */
    7, 
    
    /*[117]: Number of opcodes. */
    1, 
    
    /*[118]: Opcodes 0 and 1 */
    132, 
    
    /*[119]: Transition Actions data. */
    /*[119]: Number of transition actions for transition DfPrePGasToPreSpark */
    1, 
    
    /*[120]: Action HeatEnterPreSpark function reference index. */
    8, 
    
    /*[121]: Transition DfPrePGasToStop data. */
    /*[121]: EFSM binary index of transition DfPrePGasToStop actions data. */
    127, 
    
    /*[122]: Next state after transition. */
    5, 
    
    /*[123]: Number of opcodes. */
    5, 
    
    /*[124]: Opcodes 0 and 1 */
    897, 
    
    /*[125]: Transition Actions data. */
    /*[125]: Opcodes 2 and 3 */
    898, 
    
    /*[126]: Transition Actions data. */
    /*[126]: Opcodes 4 and 5 */
    1, 
    
    /*[127]: Transition Actions data. */
    /*[127]: Number of transition actions for transition DfPrePGasToStop */
    0, 
    
    /*[128]: State DF_PRE_SPARK base index. */
    /*[128]: Number of inputs. */
    3, 
    
    /*[129]: Number of transitions. */
    2, 
    
    /*[130]: EFSM binary index of IQFN data for state DF_PRE_SPARK */
    133, 
    
    /*[131]: EFSM binary index of transition DfPreSparkToPreMGas data. */
    136, 
    
    /*[132]: EFSM binary index of transition DfPreSparkToStop data. */
    142, 
    
    /*[133]: State DF_PRE_SPARK start of IQFN data. */
    /*[133]: IQFN function reference index for prototype IsSparkInReceived */
    5, 
    
    /*[134]: IQFN function reference index for prototype IsHvacCallingForHeat */
    1, 
    
    /*[135]: IQFN function reference index for prototype IsBlowerOn */
    2, 
    
    /*[136]: Transition DfPreSparkToPreMGas data. */
    /*[136]: EFSM binary index of transition DfPreSparkToPreMGas actions data. */
    140, 
    
    /*[137]: Next state after transition. */
    8, 
    
    /*[138]: Number of opcodes. */
    1, 
    
    /*[139]: Opcodes 0 and 1 */
    133, 
    
    /*[140]: Transition Actions data. */
    /*[140]: Number of transition actions for transition DfPreSparkToPreMGas */
    1, 
    
    /*[141]: Action HeatEnterPreMGas function reference index. */
    7, 
    
    /*[142]: Transition DfPreSparkToStop data. */
    /*[142]: EFSM binary index of transition DfPreSparkToStop actions data. */
    148, 
    
    /*[143]: Next state after transition. */
    5, 
    
    /*[144]: Number of opcodes. */
    5, 
    
    /*[145]: Opcodes 0 and 1 */
    897, 
    
    /*[146]: Transition Actions data. */
    /*[146]: Opcodes 2 and 3 */
    898, 
    
    /*[147]: Transition Actions data. */
    /*[147]: Opcodes 4 and 5 */
    1, 
    
    /*[148]: Transition Actions data. */
    /*[148]: Number of transition actions for transition DfPreSparkToStop */
    0, 
    
    /*[149]: State DF_PRE_MGAS base index. */
    /*[149]: Number of inputs. */
    3, 
    
    /*[150]: Number of transitions. */
    2, 
    
    /*[151]: EFSM binary index of IQFN data for state DF_PRE_MGAS */
    154, 
    
    /*[152]: EFSM binary index of transition DfPreMGasToSparkOff data. */
    157, 
    
    /*[153]: EFSM binary index of transition DfPreMGasToStop data. */
    163, 
    
    /*[154]: State DF_PRE_MGAS start of IQFN data. */
    /*[154]: IQFN function reference index for prototype IsMainGasInReceived */
    6, 
    
    /*[155]: IQFN function reference index for prototype IsHvacCallingForHeat */
    1, 
    
    /*[156]: IQFN function reference index for prototype IsBlowerOn */
    2, 
    
    /*[157]: Transition DfPreMGasToSparkOff data. */
    /*[157]: EFSM binary index of transition DfPreMGasToSparkOff actions data. */
    161, 
    
    /*[158]: Next state after transition. */
    9, 
    
    /*[159]: Number of opcodes. */
    1, 
    
    /*[160]: Opcodes 0 and 1 */
    134, 
    
    /*[161]: Transition Actions data. */
    /*[161]: Number of transition actions for transition DfPreMGasToSparkOff */
    1, 
    
    /*[162]: Action HeatEnterSparkOff function reference index. */
    6, 
    
    /*[163]: Transition DfPreMGasToStop data. */
    /*[163]: EFSM binary index of transition DfPreMGasToStop actions data. */
    169, 
    
    /*[164]: Next state after transition. */
    5, 
    
    /*[165]: Number of opcodes. */
    5, 
    
    /*[166]: Opcodes 0 and 1 */
    897, 
    
    /*[167]: Transition Actions data. */
    /*[167]: Opcodes 2 and 3 */
    898, 
    
    /*[168]: Transition Actions data. */
    /*[168]: Opcodes 4 and 5 */
    1, 
    
    /*[169]: Transition Actions data. */
    /*[169]: Number of transition actions for transition DfPreMGasToStop */
    0, 
    
    /*[170]: State DF_SPARK_OFF base index. */
    /*[170]: Number of inputs. */
    4, 
    
    /*[171]: Number of transitions. */
    3, 
    
    /*[172]: EFSM binary index of IQFN data for state DF_SPARK_OFF */
    176, 
    
    /*[173]: EFSM binary index of transition DfSparkOffToOnNormal data. */
    180, 
    
    /*[174]: EFSM binary index of transition DfSparkOffToOnTesting data. */
    188, 
    
    /*[175]: EFSM binary index of transition DfSparkOffToStop data. */
    195, 
    
    /*[176]: State DF_SPARK_OFF start of IQFN data. */
    /*[176]: IQFN function reference index for prototype IsSparkInReceived */
    5, 
    
    /*[177]: IQFN function reference index for prototype IsTestingModeRequested */
    3, 
    
    /*[178]: IQFN function reference index for prototype IsHvacCallingForHeat */
    1, 
    
    /*[179]: IQFN function reference index for prototype IsBlowerOn */
    2, 
    
    /*[180]: Transition DfSparkOffToOnNormal data. */
    /*[180]: EFSM binary index of transition DfSparkOffToOnNormal actions data. */
    186, 
    
    /*[181]: Next state after transition. */
    3, 
    
    /*[182]: Number of opcodes. */
    5, 
    
    /*[183]: Opcodes 0 and 1 */
    901, 
    
    /*[184]: Transition Actions data. */
    /*[184]: Opcodes 2 and 3 */
    899, 
    
    /*[185]: Transition Actions data. */
    /*[185]: Opcodes 4 and 5 */
    2, 
    
    /*[186]: Transition Actions data. */
    /*[186]: Number of transition actions for transition DfSparkOffToOnNormal */
    1, 
    
    /*[187]: Action HeatEnterOnNormal function reference index. */
    5, 
    
    /*[188]: Transition DfSparkOffToOnTesting data. */
    /*[188]: EFSM binary index of transition DfSparkOffToOnTesting actions data. */
    193, 
    
    /*[189]: Next state after transition. */
    4, 
    
    /*[190]: Number of opcodes. */
    4, 
    
    /*[191]: Opcodes 0 and 1 */
    901, 
    
    /*[192]: Transition Actions data. */
    /*[192]: Opcodes 2 and 3 */
    643, 
    
    /*[193]: Transition Actions data. */
    /*[193]: Number of transition actions for transition DfSparkOffToOnTesting */
    1, 
    
    /*[194]: Action HeatEnterOnTesting function reference index. */
    3, 
    
    /*[195]: Transition DfSparkOffToStop data. */
    /*[195]: EFSM binary index of transition DfSparkOffToStop actions data. */
    201, 
    
    /*[196]: Next state after transition. */
    5, 
    
    /*[197]: Number of opcodes. */
    5, 
    
    /*[198]: Opcodes 0 and 1 */
    897, 
    
    /*[199]: Transition Actions data. */
    /*[199]: Opcodes 2 and 3 */
    898, 
    
    /*[200]: Transition Actions data. */
    /*[200]: Opcodes 4 and 5 */
    1, 
    
    /*[201]: Transition Actions data. */
    /*[201]: Number of transition actions for transition DfSparkOffToStop */
    0 
    
};

/* Cooling_DF Definition*/
uint16_t efsm_Cooling_DF_binaryData[] = {

    /*[0]: Number of states */
    9, 
    
    /*[1]: Total number of inputs. */
    12, 
    
    /*[2]: Initial state identifier. */
    0, 
    
    /*[3]: EFSM binary index of state Initial State */
    12, 
    
    /*[4]: EFSM binary index of state IDLE */
    24, 
    
    /*[5]: EFSM binary index of state START_COOLING */
    35, 
    
    /*[6]: EFSM binary index of state COOL_EVAP */
    72, 
    
    /*[7]: EFSM binary index of state ON_TESTING */
    99, 
    
    /*[8]: EFSM binary index of state COOL_STAGE1 */
    112, 
    
    /*[9]: EFSM binary index of state STOP_COOLING */
    135, 
    
    /*[10]: EFSM binary index of state COOL_STAGE2 */
    149, 
    
    /*[11]: EFSM binary index of state COOL_STAGE3 */
    172, 
    
    /*[12]: State Initial State base index. */
    /*[12]: Number of inputs. */
    1, 
    
    /*[13]: Number of transitions. */
    1, 
    
    /*[14]: EFSM binary index of IQFN data for state Initial State */
    16, 
    
    /*[15]: EFSM binary index of transition InitIdle data. */
    17, 
    
    /*[16]: State Initial State start of IQFN data. */
    /*[16]: IQFN function reference index for prototype IsStartupTimeElapsed */
    0, 
    
    /*[17]: Transition InitIdle data. */
    /*[17]: EFSM binary index of transition InitIdle actions data. */
    21, 
    
    /*[18]: Next state after transition. */
    1, 
    
    /*[19]: Number of opcodes. */
    1, 
    
    /*[20]: Opcodes 0 and 1 */
    128, 
    
    /*[21]: Transition Actions data. */
    /*[21]: Number of transition actions for transition InitIdle */
    2, 
    
    /*[22]: Action CoolingInitIdle function reference index. */
    8, 
    
    /*[23]: Action CoolingEnterIdle function reference index. */
    0, 
    
    /*[24]: State IDLE base index. */
    /*[24]: Number of inputs. */
    1, 
    
    /*[25]: Number of transitions. */
    1, 
    
    /*[26]: EFSM binary index of IQFN data for state IDLE */
    28, 
    
    /*[27]: EFSM binary index of transition IdleToStartCooling data. */
    29, 
    
    /*[28]: State IDLE start of IQFN data. */
    /*[28]: IQFN function reference index for prototype IsHvacCallingForCooling */
    1, 
    
    /*[29]: Transition IdleToStartCooling data. */
    /*[29]: EFSM binary index of transition IdleToStartCooling actions data. */
    33, 
    
    /*[30]: Next state after transition. */
    2, 
    
    /*[31]: Number of opcodes. */
    1, 
    
    /*[32]: Opcodes 0 and 1 */
    129, 
    
    /*[33]: Transition Actions data. */
    /*[33]: Number of transition actions for transition IdleToStartCooling */
    1, 
    
    /*[34]: Action CoolingEnterStart function reference index. */
    7, 
    
    /*[35]: State START_COOLING base index. */
    /*[35]: Number of inputs. */
    4, 
    
    /*[36]: Number of transitions. */
    3, 
    
    /*[37]: EFSM binary index of IQFN data for state START_COOLING */
    41, 
    
    /*[38]: EFSM binary index of transition StartCoolingToEvap data. */
    45, 
    
    /*[39]: EFSM binary index of transition StartCoolingToOnTesting data. */
    55, 
    
    /*[40]: EFSM binary index of transition StartCoolingToStage1 data. */
    62, 
    
    /*[41]: State START_COOLING start of IQFN data. */
    /*[41]: IQFN function reference index for prototype IsBlowerOn */
    6, 
    
    /*[42]: IQFN function reference index for prototype IsEvapEnabled */
    4, 
    
    /*[43]: IQFN function reference index for prototype IsOaTempTooLowForEvap */
    5, 
    
    /*[44]: IQFN function reference index for prototype IsTestingModeRequested */
    7, 
    
    /*[45]: Transition StartCoolingToEvap data. */
    /*[45]: EFSM binary index of transition StartCoolingToEvap actions data. */
    53, 
    
    /*[46]: Next state after transition. */
    3, 
    
    /*[47]: Number of opcodes. */
    9, 
    
    /*[48]: Opcodes 0 and 1 */
    33926, 
    
    /*[49]: Transition Actions data. */
    /*[49]: Opcodes 2 and 3 */
    34050, 
    
    /*[50]: Transition Actions data. */
    /*[50]: Opcodes 4 and 5 */
    515, 
    
    /*[51]: Transition Actions data. */
    /*[51]: Opcodes 6 and 7 */
    903, 
    
    /*[52]: Transition Actions data. */
    /*[52]: Opcodes 8 and 9 */
    2, 
    
    /*[53]: Transition Actions data. */
    /*[53]: Number of transition actions for transition StartCoolingToEvap */
    1, 
    
    /*[54]: Action CoolingEnterEvap function reference index. */
    1, 
    
    /*[55]: Transition StartCoolingToOnTesting data. */
    /*[55]: EFSM binary index of transition StartCoolingToOnTesting actions data. */
    60, 
    
    /*[56]: Next state after transition. */
    4, 
    
    /*[57]: Number of opcodes. */
    3, 
    
    /*[58]: Opcodes 0 and 1 */
    34694, 
    
    /*[59]: Transition Actions data. */
    /*[59]: Opcodes 2 and 3 */
    2, 
    
    /*[60]: Transition Actions data. */
    /*[60]: Number of transition actions for transition StartCoolingToOnTesting */
    1, 
    
    /*[61]: Action CoolingEnterTest function reference index. */
    5, 
    
    /*[62]: Transition StartCoolingToStage1 data. */
    /*[62]: EFSM binary index of transition StartCoolingToStage1 actions data. */
    70, 
    
    /*[63]: Next state after transition. */
    5, 
    
    /*[64]: Number of opcodes. */
    9, 
    
    /*[65]: Opcodes 0 and 1 */
    34694, 
    
    /*[66]: Transition Actions data. */
    /*[66]: Opcodes 2 and 3 */
    515, 
    
    /*[67]: Transition Actions data. */
    /*[67]: Opcodes 4 and 5 */
    900, 
    
    /*[68]: Transition Actions data. */
    /*[68]: Opcodes 6 and 7 */
    389, 
    
    /*[69]: Transition Actions data. */
    /*[69]: Opcodes 8 and 9 */
    2, 
    
    /*[70]: Transition Actions data. */
    /*[70]: Number of transition actions for transition StartCoolingToStage1 */
    1, 
    
    /*[71]: Action CoolingEnterStage1 function reference index. */
    2, 
    
    /*[72]: State COOL_EVAP base index. */
    /*[72]: Number of inputs. */
    6, 
    
    /*[73]: Number of transitions. */
    2, 
    
    /*[74]: EFSM binary index of IQFN data for state COOL_EVAP */
    77, 
    
    /*[75]: EFSM binary index of transition EvapToStopCooling data. */
    83, 
    
    /*[76]: EFSM binary index of transition EvapToStage1 data. */
    91, 
    
    /*[77]: State COOL_EVAP start of IQFN data. */
    /*[77]: IQFN function reference index for prototype IsHvacCallingForCooling */
    1, 
    
    /*[78]: IQFN function reference index for prototype IsBlowerOn */
    6, 
    
    /*[79]: IQFN function reference index for prototype IsTestingModeRequested */
    7, 
    
    /*[80]: IQFN function reference index for prototype IsEvapHeadStartTimerExpired */
    8, 
    
    /*[81]: IQFN function reference index for prototype IsOaTempTooLowForEvap */
    5, 
    
    /*[82]: IQFN function reference index for prototype IsY1MinOffTimeExpired */
    2, 
    
    /*[83]: Transition EvapToStopCooling data. */
    /*[83]: EFSM binary index of transition EvapToStopCooling actions data. */
    90, 
    
    /*[84]: Next state after transition. */
    6, 
    
    /*[85]: Number of opcodes. */
    7, 
    
    /*[86]: Opcodes 0 and 1 */
    897, 
    
    /*[87]: Transition Actions data. */
    /*[87]: Opcodes 2 and 3 */
    902, 
    
    /*[88]: Transition Actions data. */
    /*[88]: Opcodes 4 and 5 */
    34561, 
    
    /*[89]: Transition Actions data. */
    /*[89]: Opcodes 6 and 7 */
    1, 
    
    /*[90]: Transition Actions data. */
    /*[90]: Number of transition actions for transition EvapToStopCooling */
    0, 
    
    /*[91]: Transition EvapToStage1 data. */
    /*[91]: EFSM binary index of transition EvapToStage1 actions data. */
    97, 
    
    /*[92]: Next state after transition. */
    5, 
    
    /*[93]: Number of opcodes. */
    5, 
    
    /*[94]: Opcodes 0 and 1 */
    34184, 
    
    /*[95]: Transition Actions data. */
    /*[95]: Opcodes 2 and 3 */
    33281, 
    
    /*[96]: Transition Actions data. */
    /*[96]: Opcodes 4 and 5 */
    2, 
    
    /*[97]: Transition Actions data. */
    /*[97]: Number of transition actions for transition EvapToStage1 */
    1, 
    
    /*[98]: Action CoolingEnterStage1 function reference index. */
    2, 
    
    /*[99]: State ON_TESTING base index. */
    /*[99]: Number of inputs. */
    2, 
    
    /*[100]: Number of transitions. */
    1, 
    
    /*[101]: EFSM binary index of IQFN data for state ON_TESTING */
    103, 
    
    /*[102]: EFSM binary index of transition OnTestingToStopCooling data. */
    105, 
    
    /*[103]: State ON_TESTING start of IQFN data. */
    /*[103]: IQFN function reference index for prototype IsTestingModeRequested */
    7, 
    
    /*[104]: IQFN function reference index for prototype IsBlowerOn */
    6, 
    
    /*[105]: Transition OnTestingToStopCooling data. */
    /*[105]: EFSM binary index of transition OnTestingToStopCooling actions data. */
    111, 
    
    /*[106]: Next state after transition. */
    6, 
    
    /*[107]: Number of opcodes. */
    5, 
    
    /*[108]: Opcodes 0 and 1 */
    903, 
    
    /*[109]: Transition Actions data. */
    /*[109]: Opcodes 2 and 3 */
    902, 
    
    /*[110]: Transition Actions data. */
    /*[110]: Opcodes 4 and 5 */
    1, 
    
    /*[111]: Transition Actions data. */
    /*[111]: Number of transition actions for transition OnTestingToStopCooling */
    0, 
    
    /*[112]: State COOL_STAGE1 base index. */
    /*[112]: Number of inputs. */
    4, 
    
    /*[113]: Number of transitions. */
    2, 
    
    /*[114]: EFSM binary index of IQFN data for state COOL_STAGE1 */
    117, 
    
    /*[115]: EFSM binary index of transition Stage1ToStopCooling data. */
    121, 
    
    /*[116]: EFSM binary index of transition Stage1ToStage2 data. */
    129, 
    
    /*[117]: State COOL_STAGE1 start of IQFN data. */
    /*[117]: IQFN function reference index for prototype IsHvacCallingForCooling */
    1, 
    
    /*[118]: IQFN function reference index for prototype IsBlowerOn */
    6, 
    
    /*[119]: IQFN function reference index for prototype IsTestingModeRequested */
    7, 
    
    /*[120]: IQFN function reference index for prototype IsStage2Required */
    11, 
    
    /*[121]: Transition Stage1ToStopCooling data. */
    /*[121]: EFSM binary index of transition Stage1ToStopCooling actions data. */
    128, 
    
    /*[122]: Next state after transition. */
    6, 
    
    /*[123]: Number of opcodes. */
    7, 
    
    /*[124]: Opcodes 0 and 1 */
    897, 
    
    /*[125]: Transition Actions data. */
    /*[125]: Opcodes 2 and 3 */
    902, 
    
    /*[126]: Transition Actions data. */
    /*[126]: Opcodes 4 and 5 */
    34561, 
    
    /*[127]: Transition Actions data. */
    /*[127]: Opcodes 6 and 7 */
    1, 
    
    /*[128]: Transition Actions data. */
    /*[128]: Number of transition actions for transition Stage1ToStopCooling */
    0, 
    
    /*[129]: Transition Stage1ToStage2 data. */
    /*[129]: EFSM binary index of transition Stage1ToStage2 actions data. */
    133, 
    
    /*[130]: Next state after transition. */
    7, 
    
    /*[131]: Number of opcodes. */
    1, 
    
    /*[132]: Opcodes 0 and 1 */
    139, 
    
    /*[133]: Transition Actions data. */
    /*[133]: Number of transition actions for transition Stage1ToStage2 */
    1, 
    
    /*[134]: Action CoolingEnterStage2 function reference index. */
    3, 
    
    /*[135]: State STOP_COOLING base index. */
    /*[135]: Number of inputs. */
    2, 
    
    /*[136]: Number of transitions. */
    1, 
    
    /*[137]: EFSM binary index of IQFN data for state STOP_COOLING */
    139, 
    
    /*[138]: EFSM binary index of transition StopCoolingToIdle data. */
    141, 
    
    /*[139]: State STOP_COOLING start of IQFN data. */
    /*[139]: IQFN function reference index for prototype IsAllMinOnTimesExpired */
    3, 
    
    /*[140]: IQFN function reference index for prototype IsQuickStopRequired */
    9, 
    
    /*[141]: Transition StopCoolingToIdle data. */
    /*[141]: EFSM binary index of transition StopCoolingToIdle actions data. */
    146, 
    
    /*[142]: Next state after transition. */
    1, 
    
    /*[143]: Number of opcodes. */
    4, 
    
    /*[144]: Opcodes 0 and 1 */
    35203, 
    
    /*[145]: Transition Actions data. */
    /*[145]: Opcodes 2 and 3 */
    259, 
    
    /*[146]: Transition Actions data. */
    /*[146]: Number of transition actions for transition StopCoolingToIdle */
    2, 
    
    /*[147]: Action CoolingExitStop function reference index. */
    6, 
    
    /*[148]: Action CoolingEnterIdle function reference index. */
    0, 
    
    /*[149]: State COOL_STAGE2 base index. */
    /*[149]: Number of inputs. */
    4, 
    
    /*[150]: Number of transitions. */
    2, 
    
    /*[151]: EFSM binary index of IQFN data for state COOL_STAGE2 */
    154, 
    
    /*[152]: EFSM binary index of transition Stage2ToStage3 data. */
    158, 
    
    /*[153]: EFSM binary index of transition Stage2ToStopCooling data. */
    164, 
    
    /*[154]: State COOL_STAGE2 start of IQFN data. */
    /*[154]: IQFN function reference index for prototype IsStage3Required */
    10, 
    
    /*[155]: IQFN function reference index for prototype IsHvacCallingForCooling */
    1, 
    
    /*[156]: IQFN function reference index for prototype IsBlowerOn */
    6, 
    
    /*[157]: IQFN function reference index for prototype IsTestingModeRequested */
    7, 
    
    /*[158]: Transition Stage2ToStage3 data. */
    /*[158]: EFSM binary index of transition Stage2ToStage3 actions data. */
    162, 
    
    /*[159]: Next state after transition. */
    8, 
    
    /*[160]: Number of opcodes. */
    1, 
    
    /*[161]: Opcodes 0 and 1 */
    138, 
    
    /*[162]: Transition Actions data. */
    /*[162]: Number of transition actions for transition Stage2ToStage3 */
    1, 
    
    /*[163]: Action CoolingEnterStage3 function reference index. */
    4, 
    
    /*[164]: Transition Stage2ToStopCooling data. */
    /*[164]: EFSM binary index of transition Stage2ToStopCooling actions data. */
    171, 
    
    /*[165]: Next state after transition. */
    6, 
    
    /*[166]: Number of opcodes. */
    7, 
    
    /*[167]: Opcodes 0 and 1 */
    897, 
    
    /*[168]: Transition Actions data. */
    /*[168]: Opcodes 2 and 3 */
    902, 
    
    /*[169]: Transition Actions data. */
    /*[169]: Opcodes 4 and 5 */
    34561, 
    
    /*[170]: Transition Actions data. */
    /*[170]: Opcodes 6 and 7 */
    1, 
    
    /*[171]: Transition Actions data. */
    /*[171]: Number of transition actions for transition Stage2ToStopCooling */
    0, 
    
    /*[172]: State COOL_STAGE3 base index. */
    /*[172]: Number of inputs. */
    3, 
    
    /*[173]: Number of transitions. */
    1, 
    
    /*[174]: EFSM binary index of IQFN data for state COOL_STAGE3 */
    176, 
    
    /*[175]: EFSM binary index of transition Stage3ToStopCooling data. */
    179, 
    
    /*[176]: State COOL_STAGE3 start of IQFN data. */
    /*[176]: IQFN function reference index for prototype IsHvacCallingForCooling */
    1, 
    
    /*[177]: IQFN function reference index for prototype IsBlowerOn */
    6, 
    
    /*[178]: IQFN function reference index for prototype IsTestingModeRequested */
    7, 
    
    /*[179]: Transition Stage3ToStopCooling data. */
    /*[179]: EFSM binary index of transition Stage3ToStopCooling actions data. */
    186, 
    
    /*[180]: Next state after transition. */
    6, 
    
    /*[181]: Number of opcodes. */
    7, 
    
    /*[182]: Opcodes 0 and 1 */
    897, 
    
    /*[183]: Transition Actions data. */
    /*[183]: Opcodes 2 and 3 */
    902, 
    
    /*[184]: Transition Actions data. */
    /*[184]: Opcodes 4 and 5 */
    34561, 
    
    /*[185]: Transition Actions data. */
    /*[185]: Opcodes 6 and 7 */
    1, 
    
    /*[186]: Transition Actions data. */
    /*[186]: Number of transition actions for transition Stage3ToStopCooling */
    0 
    
};

/* Ovrd_Discharge_DF Definition*/
uint16_t efsm_Ovrd_Discharge_DF_binaryData[] = {

    /*[0]: Number of states */
    5, 
    
    /*[1]: Total number of inputs. */
    6, 
    
    /*[2]: Initial state identifier. */
    0, 
    
    /*[3]: EFSM binary index of state DO_IDLE */
    8, 
    
    /*[4]: EFSM binary index of state DO_MIN_OVRD_HEAT */
    37, 
    
    /*[5]: EFSM binary index of state DO_MAX_OVRD_HEAT */
    50, 
    
    /*[6]: EFSM binary index of state Initial State */
    63, 
    
    /*[7]: EFSM binary index of state DO_TEMP_RISE_OVRD_HEAT */
    74, 
    
    /*[8]: State DO_IDLE base index. */
    /*[8]: Number of inputs. */
    3, 
    
    /*[9]: Number of transitions. */
    3, 
    
    /*[10]: EFSM binary index of IQFN data for state DO_IDLE */
    14, 
    
    /*[11]: EFSM binary index of transition IdleToMinOvrd data. */
    17, 
    
    /*[12]: EFSM binary index of transition IdleToMaxOvrd data. */
    24, 
    
    /*[13]: EFSM binary index of transition IdleToTempRiseOvrd data. */
    31, 
    
    /*[14]: State DO_IDLE start of IQFN data. */
    /*[14]: IQFN function reference index for prototype IsStartMinHeatOvrdMet */
    0, 
    
    /*[15]: IQFN function reference index for prototype IsMaxTempRiseExceeded */
    5, 
    
    /*[16]: IQFN function reference index for prototype IsStartMaxHeatOvrdMet */
    1, 
    
    /*[17]: Transition IdleToMinOvrd data. */
    /*[17]: EFSM binary index of transition IdleToMinOvrd actions data. */
    22, 
    
    /*[18]: Next state after transition. */
    1, 
    
    /*[19]: Number of opcodes. */
    4, 
    
    /*[20]: Opcodes 0 and 1 */
    34176, 
    
    /*[21]: Transition Actions data. */
    /*[21]: Opcodes 2 and 3 */
    515, 
    
    /*[22]: Transition Actions data. */
    /*[22]: Number of transition actions for transition IdleToMinOvrd */
    1, 
    
    /*[23]: Action EnterDoMinOvrdHeat function reference index. */
    1, 
    
    /*[24]: Transition IdleToMaxOvrd data. */
    /*[24]: EFSM binary index of transition IdleToMaxOvrd actions data. */
    29, 
    
    /*[25]: Next state after transition. */
    2, 
    
    /*[26]: Number of opcodes. */
    4, 
    
    /*[27]: Opcodes 0 and 1 */
    34177, 
    
    /*[28]: Transition Actions data. */
    /*[28]: Opcodes 2 and 3 */
    515, 
    
    /*[29]: Transition Actions data. */
    /*[29]: Number of transition actions for transition IdleToMaxOvrd */
    1, 
    
    /*[30]: Action EnterDoMaxOvrdHeat function reference index. */
    2, 
    
    /*[31]: Transition IdleToTempRiseOvrd data. */
    /*[31]: EFSM binary index of transition IdleToTempRiseOvrd actions data. */
    35, 
    
    /*[32]: Next state after transition. */
    4, 
    
    /*[33]: Number of opcodes. */
    1, 
    
    /*[34]: Opcodes 0 and 1 */
    133, 
    
    /*[35]: Transition Actions data. */
    /*[35]: Number of transition actions for transition IdleToTempRiseOvrd */
    1, 
    
    /*[36]: Action EnterDoTempRiseOvrdHeat function reference index. */
    3, 
    
    /*[37]: State DO_MIN_OVRD_HEAT base index. */
    /*[37]: Number of inputs. */
    2, 
    
    /*[38]: Number of transitions. */
    1, 
    
    /*[39]: EFSM binary index of IQFN data for state DO_MIN_OVRD_HEAT */
    41, 
    
    /*[40]: EFSM binary index of transition MinOvrdToIdle data. */
    43, 
    
    /*[41]: State DO_MIN_OVRD_HEAT start of IQFN data. */
    /*[41]: IQFN function reference index for prototype IsEndMinHeatOvrdMet */
    2, 
    
    /*[42]: IQFN function reference index for prototype IsMaxTempRiseExceeded */
    5, 
    
    /*[43]: Transition MinOvrdToIdle data. */
    /*[43]: EFSM binary index of transition MinOvrdToIdle actions data. */
    48, 
    
    /*[44]: Next state after transition. */
    0, 
    
    /*[45]: Number of opcodes. */
    3, 
    
    /*[46]: Opcodes 0 and 1 */
    34178, 
    
    /*[47]: Transition Actions data. */
    /*[47]: Opcodes 2 and 3 */
    1, 
    
    /*[48]: Transition Actions data. */
    /*[48]: Number of transition actions for transition MinOvrdToIdle */
    1, 
    
    /*[49]: Action EnterDoIdle function reference index. */
    0, 
    
    /*[50]: State DO_MAX_OVRD_HEAT base index. */
    /*[50]: Number of inputs. */
    2, 
    
    /*[51]: Number of transitions. */
    1, 
    
    /*[52]: EFSM binary index of IQFN data for state DO_MAX_OVRD_HEAT */
    54, 
    
    /*[53]: EFSM binary index of transition MaxOvrdToIdle data. */
    56, 
    
    /*[54]: State DO_MAX_OVRD_HEAT start of IQFN data. */
    /*[54]: IQFN function reference index for prototype IsEndMaxHeatOvrdMet */
    3, 
    
    /*[55]: IQFN function reference index for prototype IsMaxTempRiseExceeded */
    5, 
    
    /*[56]: Transition MaxOvrdToIdle data. */
    /*[56]: EFSM binary index of transition MaxOvrdToIdle actions data. */
    61, 
    
    /*[57]: Next state after transition. */
    0, 
    
    /*[58]: Number of opcodes. */
    3, 
    
    /*[59]: Opcodes 0 and 1 */
    34179, 
    
    /*[60]: Transition Actions data. */
    /*[60]: Opcodes 2 and 3 */
    1, 
    
    /*[61]: Transition Actions data. */
    /*[61]: Number of transition actions for transition MaxOvrdToIdle */
    1, 
    
    /*[62]: Action EnterDoIdle function reference index. */
    0, 
    
    /*[63]: State Initial State base index. */
    /*[63]: Number of inputs. */
    1, 
    
    /*[64]: Number of transitions. */
    1, 
    
    /*[65]: EFSM binary index of IQFN data for state Initial State */
    67, 
    
    /*[66]: EFSM binary index of transition InitToIdle data. */
    68, 
    
    /*[67]: State Initial State start of IQFN data. */
    /*[67]: IQFN function reference index for prototype IsStartupTimeExpired */
    4, 
    
    /*[68]: Transition InitToIdle data. */
    /*[68]: EFSM binary index of transition InitToIdle actions data. */
    72, 
    
    /*[69]: Next state after transition. */
    0, 
    
    /*[70]: Number of opcodes. */
    1, 
    
    /*[71]: Opcodes 0 and 1 */
    132, 
    
    /*[72]: Transition Actions data. */
    /*[72]: Number of transition actions for transition InitToIdle */
    1, 
    
    /*[73]: Action EnterDoIdle function reference index. */
    0, 
    
    /*[74]: State DO_TEMP_RISE_OVRD_HEAT base index. */
    /*[74]: Number of inputs. */
    1, 
    
    /*[75]: Number of transitions. */
    1, 
    
    /*[76]: EFSM binary index of IQFN data for state DO_TEMP_RISE_OVRD_HEAT */
    78, 
    
    /*[77]: EFSM binary index of transition TempRiseOvrdToIdle data. */
    79, 
    
    /*[78]: State DO_TEMP_RISE_OVRD_HEAT start of IQFN data. */
    /*[78]: IQFN function reference index for prototype IsMaxTempRiseExceeded */
    5, 
    
    /*[79]: Transition TempRiseOvrdToIdle data. */
    /*[79]: EFSM binary index of transition TempRiseOvrdToIdle actions data. */
    83, 
    
    /*[80]: Next state after transition. */
    0, 
    
    /*[81]: Number of opcodes. */
    2, 
    
    /*[82]: Opcodes 0 and 1 */
    901, 
    
    /*[83]: Transition Actions data. */
    /*[83]: Number of transition actions for transition TempRiseOvrdToIdle */
    1, 
    
    /*[84]: Action EnterDoIdle function reference index. */
    0 
    
};

/* Freezestat_DF Definition*/
uint16_t efsm_Freezestat_DF_binaryData[] = {

    /*[0]: Number of states */
    4, 
    
    /*[1]: Total number of inputs. */
    7, 
    
    /*[2]: Initial state identifier. */
    0, 
    
    /*[3]: EFSM binary index of state FRZST_TIMED_MONITOR */
    7, 
    
    /*[4]: EFSM binary index of state FRZST_IDLE */
    36, 
    
    /*[5]: EFSM binary index of state FRZST_FAIL_RESUME */
    49, 
    
    /*[6]: EFSM binary index of state FRZST_FAIL_LOCK */
    60, 
    
    /*[7]: State FRZST_TIMED_MONITOR base index. */
    /*[7]: Number of inputs. */
    3, 
    
    /*[8]: Number of transitions. */
    3, 
    
    /*[9]: EFSM binary index of IQFN data for state FRZST_TIMED_MONITOR */
    13, 
    
    /*[10]: EFSM binary index of transition MonitorToFailResume data. */
    16, 
    
    /*[11]: EFSM binary index of transition MonitorToFailLock data. */
    23, 
    
    /*[12]: EFSM binary index of transition MonitorToIdle data. */
    30, 
    
    /*[13]: State FRZST_TIMED_MONITOR start of IQFN data. */
    /*[13]: IQFN function reference index for prototype IsMonitorTimeExpired */
    5, 
    
    /*[14]: IQFN function reference index for prototype IsResumeCountMaxed */
    3, 
    
    /*[15]: IQFN function reference index for prototype IsDischargeTempRecovered */
    2, 
    
    /*[16]: Transition MonitorToFailResume data. */
    /*[16]: EFSM binary index of transition MonitorToFailResume actions data. */
    21, 
    
    /*[17]: Next state after transition. */
    2, 
    
    /*[18]: Number of opcodes. */
    4, 
    
    /*[19]: Opcodes 0 and 1 */
    33669, 
    
    /*[20]: Transition Actions data. */
    /*[20]: Opcodes 2 and 3 */
    515, 
    
    /*[21]: Transition Actions data. */
    /*[21]: Number of transition actions for transition MonitorToFailResume */
    1, 
    
    /*[22]: Action EnterFailResume function reference index. */
    2, 
    
    /*[23]: Transition MonitorToFailLock data. */
    /*[23]: EFSM binary index of transition MonitorToFailLock actions data. */
    28, 
    
    /*[24]: Next state after transition. */
    3, 
    
    /*[25]: Number of opcodes. */
    3, 
    
    /*[26]: Opcodes 0 and 1 */
    33669, 
    
    /*[27]: Transition Actions data. */
    /*[27]: Opcodes 2 and 3 */
    2, 
    
    /*[28]: Transition Actions data. */
    /*[28]: Number of transition actions for transition MonitorToFailLock */
    1, 
    
    /*[29]: Action EnterFailLock function reference index. */
    3, 
    
    /*[30]: Transition MonitorToIdle data. */
    /*[30]: EFSM binary index of transition MonitorToIdle actions data. */
    34, 
    
    /*[31]: Next state after transition. */
    1, 
    
    /*[32]: Number of opcodes. */
    1, 
    
    /*[33]: Opcodes 0 and 1 */
    130, 
    
    /*[34]: Transition Actions data. */
    /*[34]: Number of transition actions for transition MonitorToIdle */
    1, 
    
    /*[35]: Action EnterIdle function reference index. */
    0, 
    
    /*[36]: State FRZST_IDLE base index. */
    /*[36]: Number of inputs. */
    2, 
    
    /*[37]: Number of transitions. */
    1, 
    
    /*[38]: EFSM binary index of IQFN data for state FRZST_IDLE */
    40, 
    
    /*[39]: EFSM binary index of transition IdleToMonitor data. */
    42, 
    
    /*[40]: State FRZST_IDLE start of IQFN data. */
    /*[40]: IQFN function reference index for prototype IsStartupTimeElapsed */
    0, 
    
    /*[41]: IQFN function reference index for prototype IsDischargeTempLow */
    1, 
    
    /*[42]: Transition IdleToMonitor data. */
    /*[42]: EFSM binary index of transition IdleToMonitor actions data. */
    47, 
    
    /*[43]: Next state after transition. */
    0, 
    
    /*[44]: Number of opcodes. */
    3, 
    
    /*[45]: Opcodes 0 and 1 */
    33152, 
    
    /*[46]: Transition Actions data. */
    /*[46]: Opcodes 2 and 3 */
    2, 
    
    /*[47]: Transition Actions data. */
    /*[47]: Number of transition actions for transition IdleToMonitor */
    1, 
    
    /*[48]: Action EnterMonitor function reference index. */
    1, 
    
    /*[49]: State FRZST_FAIL_RESUME base index. */
    /*[49]: Number of inputs. */
    1, 
    
    /*[50]: Number of transitions. */
    1, 
    
    /*[51]: EFSM binary index of IQFN data for state FRZST_FAIL_RESUME */
    53, 
    
    /*[52]: EFSM binary index of transition FailResumeToIdle data. */
    54, 
    
    /*[53]: State FRZST_FAIL_RESUME start of IQFN data. */
    /*[53]: IQFN function reference index for prototype IsStopHvacFlagSet */
    6, 
    
    /*[54]: Transition FailResumeToIdle data. */
    /*[54]: EFSM binary index of transition FailResumeToIdle actions data. */
    58, 
    
    /*[55]: Next state after transition. */
    1, 
    
    /*[56]: Number of opcodes. */
    1, 
    
    /*[57]: Opcodes 0 and 1 */
    134, 
    
    /*[58]: Transition Actions data. */
    /*[58]: Number of transition actions for transition FailResumeToIdle */
    1, 
    
    /*[59]: Action EnterIdle function reference index. */
    0, 
    
    /*[60]: State FRZST_FAIL_LOCK base index. */
    /*[60]: Number of inputs. */
    1, 
    
    /*[61]: Number of transitions. */
    1, 
    
    /*[62]: EFSM binary index of IQFN data for state FRZST_FAIL_LOCK */
    64, 
    
    /*[63]: EFSM binary index of transition FailLockToIdle data. */
    65, 
    
    /*[64]: State FRZST_FAIL_LOCK start of IQFN data. */
    /*[64]: IQFN function reference index for prototype IsResetExecuted */
    4, 
    
    /*[65]: Transition FailLockToIdle data. */
    /*[65]: EFSM binary index of transition FailLockToIdle actions data. */
    69, 
    
    /*[66]: Next state after transition. */
    1, 
    
    /*[67]: Number of opcodes. */
    1, 
    
    /*[68]: Opcodes 0 and 1 */
    132, 
    
    /*[69]: Transition Actions data. */
    /*[69]: Number of transition actions for transition FailLockToIdle */
    2, 
    
    /*[70]: Action ExitFailLock function reference index. */
    4, 
    
    /*[71]: Action EnterIdle function reference index. */
    0 
    
};

/* MixingBox_DF Definition*/
uint16_t efsm_MixingBox_DF_binaryData[] = {

    /*[0]: Number of states */
    5, 
    
    /*[1]: Total number of inputs. */
    5, 
    
    /*[2]: Initial state identifier. */
    0, 
    
    /*[3]: EFSM binary index of state MBOX_OFF */
    8, 
    
    /*[4]: EFSM binary index of state MBOX_IDLE */
    21, 
    
    /*[5]: EFSM binary index of state MBOX_ON_TEST */
    61, 
    
    /*[6]: EFSM binary index of state MBOX_ON_PURGE */
    75, 
    
    /*[7]: EFSM binary index of state MBOX_ON_NORMAL */
    93, 
    
    /*[8]: State MBOX_OFF base index. */
    /*[8]: Number of inputs. */
    2, 
    
    /*[9]: Number of transitions. */
    1, 
    
    /*[10]: EFSM binary index of IQFN data for state MBOX_OFF */
    12, 
    
    /*[11]: EFSM binary index of transition OffToIdle data. */
    14, 
    
    /*[12]: State MBOX_OFF start of IQFN data. */
    /*[12]: IQFN function reference index for prototype IsMixboxConfigured */
    0, 
    
    /*[13]: IQFN function reference index for prototype IsStartupTimeElapsed */
    4, 
    
    /*[14]: Transition OffToIdle data. */
    /*[14]: EFSM binary index of transition OffToIdle actions data. */
    19, 
    
    /*[15]: Next state after transition. */
    1, 
    
    /*[16]: Number of opcodes. */
    3, 
    
    /*[17]: Opcodes 0 and 1 */
    33920, 
    
    /*[18]: Transition Actions data. */
    /*[18]: Opcodes 2 and 3 */
    2, 
    
    /*[19]: Transition Actions data. */
    /*[19]: Number of transition actions for transition OffToIdle */
    1, 
    
    /*[20]: Action EnterIdle function reference index. */
    1, 
    
    /*[21]: State MBOX_IDLE base index. */
    /*[21]: Number of inputs. */
    4, 
    
    /*[22]: Number of transitions. */
    4, 
    
    /*[23]: EFSM binary index of IQFN data for state MBOX_IDLE */
    28, 
    
    /*[24]: EFSM binary index of transition IdleToOff data. */
    32, 
    
    /*[25]: EFSM binary index of transition IdleToOnTest data. */
    38, 
    
    /*[26]: EFSM binary index of transition IdleToOnPurge data. */
    44, 
    
    /*[27]: EFSM binary index of transition IdleToOnNormal data. */
    52, 
    
    /*[28]: State MBOX_IDLE start of IQFN data. */
    /*[28]: IQFN function reference index for prototype IsMixboxConfigured */
    0, 
    
    /*[29]: IQFN function reference index for prototype IsTestingModeRequested */
    3, 
    
    /*[30]: IQFN function reference index for prototype IsBlowerOn */
    1, 
    
    /*[31]: IQFN function reference index for prototype IsPurgeOn */
    2, 
    
    /*[32]: Transition IdleToOff data. */
    /*[32]: EFSM binary index of transition IdleToOff actions data. */
    36, 
    
    /*[33]: Next state after transition. */
    0, 
    
    /*[34]: Number of opcodes. */
    2, 
    
    /*[35]: Opcodes 0 and 1 */
    896, 
    
    /*[36]: Transition Actions data. */
    /*[36]: Number of transition actions for transition IdleToOff */
    1, 
    
    /*[37]: Action EnterOff function reference index. */
    0, 
    
    /*[38]: Transition IdleToOnTest data. */
    /*[38]: EFSM binary index of transition IdleToOnTest actions data. */
    42, 
    
    /*[39]: Next state after transition. */
    2, 
    
    /*[40]: Number of opcodes. */
    1, 
    
    /*[41]: Opcodes 0 and 1 */
    131, 
    
    /*[42]: Transition Actions data. */
    /*[42]: Number of transition actions for transition IdleToOnTest */
    1, 
    
    /*[43]: Action EnterOnTest function reference index. */
    2, 
    
    /*[44]: Transition IdleToOnPurge data. */
    /*[44]: EFSM binary index of transition IdleToOnPurge actions data. */
    50, 
    
    /*[45]: Next state after transition. */
    3, 
    
    /*[46]: Number of opcodes. */
    6, 
    
    /*[47]: Opcodes 0 and 1 */
    33409, 
    
    /*[48]: Transition Actions data. */
    /*[48]: Opcodes 2 and 3 */
    33538, 
    
    /*[49]: Transition Actions data. */
    /*[49]: Opcodes 4 and 5 */
    515, 
    
    /*[50]: Transition Actions data. */
    /*[50]: Number of transition actions for transition IdleToOnPurge */
    1, 
    
    /*[51]: Action EnterOnPurge function reference index. */
    3, 
    
    /*[52]: Transition IdleToOnNormal data. */
    /*[52]: EFSM binary index of transition IdleToOnNormal actions data. */
    59, 
    
    /*[53]: Next state after transition. */
    4, 
    
    /*[54]: Number of opcodes. */
    7, 
    
    /*[55]: Opcodes 0 and 1 */
    33665, 
    
    /*[56]: Transition Actions data. */
    /*[56]: Opcodes 2 and 3 */
    515, 
    
    /*[57]: Transition Actions data. */
    /*[57]: Opcodes 4 and 5 */
    898, 
    
    /*[58]: Transition Actions data. */
    /*[58]: Opcodes 6 and 7 */
    2, 
    
    /*[59]: Transition Actions data. */
    /*[59]: Number of transition actions for transition IdleToOnNormal */
    1, 
    
    /*[60]: Action EnterOnNormal function reference index. */
    4, 
    
    /*[61]: State MBOX_ON_TEST base index. */
    /*[61]: Number of inputs. */
    2, 
    
    /*[62]: Number of transitions. */
    1, 
    
    /*[63]: EFSM binary index of IQFN data for state MBOX_ON_TEST */
    65, 
    
    /*[64]: EFSM binary index of transition OnTestToIdle data. */
    67, 
    
    /*[65]: State MBOX_ON_TEST start of IQFN data. */
    /*[65]: IQFN function reference index for prototype IsTestingModeRequested */
    3, 
    
    /*[66]: IQFN function reference index for prototype IsMixboxConfigured */
    0, 
    
    /*[67]: Transition OnTestToIdle data. */
    /*[67]: EFSM binary index of transition OnTestToIdle actions data. */
    73, 
    
    /*[68]: Next state after transition. */
    1, 
    
    /*[69]: Number of opcodes. */
    5, 
    
    /*[70]: Opcodes 0 and 1 */
    899, 
    
    /*[71]: Transition Actions data. */
    /*[71]: Opcodes 2 and 3 */
    896, 
    
    /*[72]: Transition Actions data. */
    /*[72]: Opcodes 4 and 5 */
    1, 
    
    /*[73]: Transition Actions data. */
    /*[73]: Number of transition actions for transition OnTestToIdle */
    1, 
    
    /*[74]: Action EnterIdle function reference index. */
    1, 
    
    /*[75]: State MBOX_ON_PURGE base index. */
    /*[75]: Number of inputs. */
    4, 
    
    /*[76]: Number of transitions. */
    1, 
    
    /*[77]: EFSM binary index of IQFN data for state MBOX_ON_PURGE */
    79, 
    
    /*[78]: EFSM binary index of transition OnPurgeToIdle data. */
    83, 
    
    /*[79]: State MBOX_ON_PURGE start of IQFN data. */
    /*[79]: IQFN function reference index for prototype IsBlowerOn */
    1, 
    
    /*[80]: IQFN function reference index for prototype IsPurgeOn */
    2, 
    
    /*[81]: IQFN function reference index for prototype IsMixboxConfigured */
    0, 
    
    /*[82]: IQFN function reference index for prototype IsTestingModeRequested */
    3, 
    
    /*[83]: Transition OnPurgeToIdle data. */
    /*[83]: EFSM binary index of transition OnPurgeToIdle actions data. */
    91, 
    
    /*[84]: Next state after transition. */
    1, 
    
    /*[85]: Number of opcodes. */
    10, 
    
    /*[86]: Opcodes 0 and 1 */
    897, 
    
    /*[87]: Transition Actions data. */
    /*[87]: Opcodes 2 and 3 */
    898, 
    
    /*[88]: Transition Actions data. */
    /*[88]: Opcodes 4 and 5 */
    32769, 
    
    /*[89]: Transition Actions data. */
    /*[89]: Opcodes 6 and 7 */
    259, 
    
    /*[90]: Transition Actions data. */
    /*[90]: Opcodes 8 and 9 */
    387, 
    
    /*[91]: Transition Actions data. */
    /*[91]: Number of transition actions for transition OnPurgeToIdle */
    1, 
    
    /*[92]: Action EnterIdle function reference index. */
    1, 
    
    /*[93]: State MBOX_ON_NORMAL base index. */
    /*[93]: Number of inputs. */
    4, 
    
    /*[94]: Number of transitions. */
    1, 
    
    /*[95]: EFSM binary index of IQFN data for state MBOX_ON_NORMAL */
    97, 
    
    /*[96]: EFSM binary index of transition OnNormalToIdle data. */
    101, 
    
    /*[97]: State MBOX_ON_NORMAL start of IQFN data. */
    /*[97]: IQFN function reference index for prototype IsMixboxConfigured */
    0, 
    
    /*[98]: IQFN function reference index for prototype IsBlowerOn */
    1, 
    
    /*[99]: IQFN function reference index for prototype IsPurgeOn */
    2, 
    
    /*[100]: IQFN function reference index for prototype IsTestingModeRequested */
    3, 
    
    /*[101]: Transition OnNormalToIdle data. */
    /*[101]: EFSM binary index of transition OnNormalToIdle actions data. */
    109, 
    
    /*[102]: Next state after transition. */
    1, 
    
    /*[103]: Number of opcodes. */
    9, 
    
    /*[104]: Opcodes 0 and 1 */
    896, 
    
    /*[105]: Transition Actions data. */
    /*[105]: Opcodes 2 and 3 */
    897, 
    
    /*[106]: Transition Actions data. */
    /*[106]: Opcodes 4 and 5 */
    33281, 
    
    /*[107]: Transition Actions data. */
    /*[107]: Opcodes 6 and 7 */
    33537, 
    
    /*[108]: Transition Actions data. */
    /*[108]: Opcodes 8 and 9 */
    1, 
    
    /*[109]: Transition Actions data. */
    /*[109]: Number of transition actions for transition OnNormalToIdle */
    1, 
    
    /*[110]: Action EnterIdle function reference index. */
    1 
    
};
/*
----------------------------------------------------------------------------------------------------
Initialization Function.

Note:
This function: 
-Initializes the efsmInstanceArray.
-Initializes the EFSM_BINARY variables, as well as the function reference arrays for 
 every state machine definition. 
-Makes calls to EFSM_InitializeInstance() function for the purpose of initializing the 
 state  machine instances themselves.
*/
void EFSM_df_stateMachines_Init()
{
    /*efsmInstanceArray initialization.*/
    
    efsmInstanceArray[0] = &HVAC_DF_Instance_0;
    efsmInstanceArray[1] = &Blower_DF_Instance_0;
    efsmInstanceArray[2] = &Heating_DF_Instance_0;
    efsmInstanceArray[3] = &Cooling_DF_Instance_0;
    efsmInstanceArray[4] = &Ovrd_Discharge_DF_Instance_0;
    efsmInstanceArray[5] = &Freezestat_DF_Instance_0;
    efsmInstanceArray[6] = &MixingBox_DF_Instance_0;
    
    /*State Machine Definition "HVAC_DF" Initialization*/
    
    /*Associate an array of binary instructions with an EFSM_BINARY structure.*/
    HVAC_DF_Binary.data = efsm_HVAC_DF_binaryData;
    
    /*Initialize the input functions array.*/
    HVAC_DF_Inputs[0] = &EFSM_HVAC_DF_IsBlowerRequired;
    HVAC_DF_Inputs[1] = &EFSM_HVAC_DF_CanBlow;
    HVAC_DF_Inputs[2] = &EFSM_HVAC_DF_IsInHeatRange;
    HVAC_DF_Inputs[3] = &EFSM_HVAC_DF_CanHeat;
    HVAC_DF_Inputs[4] = &EFSM_HVAC_DF_IsInCoolRange;
    HVAC_DF_Inputs[5] = &EFSM_HVAC_DF_CanCool;
    HVAC_DF_Inputs[6] = &EFSM_HVAC_DF_IsBlowerOn;
    HVAC_DF_Inputs[7] = &EFSM_HVAC_DF_IsBlowerInDelayedStart;
    HVAC_DF_Inputs[8] = &EFSM_HVAC_DF_IsHeatOn;
    HVAC_DF_Inputs[9] = &EFSM_HVAC_DF_IsCoolOn;
    HVAC_DF_Inputs[10] = &EFSM_HVAC_DF_IsStartupTimeElapsed;
    
    /*Initialize the output functions array.*/
    HVAC_DF_OutputActions[0] = &EFSM_HVAC_DF_HvacInit;
    HVAC_DF_OutputActions[1] = &EFSM_HVAC_DF_HvacEnterBlower;
    HVAC_DF_OutputActions[2] = &EFSM_HVAC_DF_HvacEnterPreheat;
    HVAC_DF_OutputActions[3] = &EFSM_HVAC_DF_HvacEnterHeating;
    HVAC_DF_OutputActions[4] = &EFSM_HVAC_DF_HvacEnterStopHeat;
    HVAC_DF_OutputActions[5] = &EFSM_HVAC_DF_HvacEnterPrecool;
    HVAC_DF_OutputActions[6] = &EFSM_HVAC_DF_HvacEnterCooling;
    HVAC_DF_OutputActions[7] = &EFSM_HVAC_DF_HvacEnterStopCool;
    HVAC_DF_OutputActions[8] = &EFSM_HVAC_DF_HvacEnterIdle;
    
    /*State Machine Definition "Blower_DF" Initialization*/
    
    /*Associate an array of binary instructions with an EFSM_BINARY structure.*/
    Blower_DF_Binary.data = efsm_Blower_DF_binaryData;
    
    /*Initialize the input functions array.*/
    Blower_DF_Inputs[0] = &EFSM_Blower_DF_IsStartupTimeElapsed;
    Blower_DF_Inputs[1] = &EFSM_Blower_DF_IsHvacCallingForBlower;
    Blower_DF_Inputs[2] = &EFSM_Blower_DF_IsDamperEndReceived;
    Blower_DF_Inputs[3] = &EFSM_Blower_DF_IsAirProvingReceived;
    Blower_DF_Inputs[4] = &EFSM_Blower_DF_IsDelayedStartEnabled;
    Blower_DF_Inputs[5] = &EFSM_Blower_DF_IsDelayedStartTimerExpired;
    Blower_DF_Inputs[6] = &EFSM_Blower_DF_IsDelayedStopEnabled;
    Blower_DF_Inputs[7] = &EFSM_Blower_DF_IsDelayedStopTimerExpired;
    Blower_DF_Inputs[8] = &EFSM_Blower_DF_IsRecoverAirTimerExpired;
    
    /*Initialize the output functions array.*/
    Blower_DF_OutputActions[0] = &EFSM_Blower_DF_BlowerEnterIdle;
    Blower_DF_OutputActions[1] = &EFSM_Blower_DF_BlowerEnterPreStart;
    Blower_DF_OutputActions[2] = &EFSM_Blower_DF_BlowerEnterDelayedStart;
    Blower_DF_OutputActions[3] = &EFSM_Blower_DF_BlowerEnterOpenDamper;
    Blower_DF_OutputActions[4] = &EFSM_Blower_DF_BlowerEnterProveAir;
    Blower_DF_OutputActions[5] = &EFSM_Blower_DF_BlowerEnterBlowerOn;
    Blower_DF_OutputActions[6] = &EFSM_Blower_DF_BlowerEnterDelayedStop;
    Blower_DF_OutputActions[7] = &EFSM_Blower_DF_BlowerInitIdle;
    Blower_DF_OutputActions[8] = &EFSM_Blower_DF_BlowerEnterRecoverAir;
    
    /*State Machine Definition "Heating_DF" Initialization*/
    
    /*Associate an array of binary instructions with an EFSM_BINARY structure.*/
    Heating_DF_Binary.data = efsm_Heating_DF_binaryData;
    
    /*Initialize the input functions array.*/
    Heating_DF_Inputs[0] = &EFSM_Heating_DF_IsStartupTimeElapsed;
    Heating_DF_Inputs[1] = &EFSM_Heating_DF_IsHvacCallingForHeat;
    Heating_DF_Inputs[2] = &EFSM_Heating_DF_IsBlowerOn;
    Heating_DF_Inputs[3] = &EFSM_Heating_DF_IsTestingModeRequested;
    Heating_DF_Inputs[4] = &EFSM_Heating_DF_IsPilotGasInReceived;
    Heating_DF_Inputs[5] = &EFSM_Heating_DF_IsSparkInReceived;
    Heating_DF_Inputs[6] = &EFSM_Heating_DF_IsMainGasInReceived;
    
    /*Initialize the output functions array.*/
    Heating_DF_OutputActions[0] = &EFSM_Heating_DF_HeatEnterIdle;
    Heating_DF_OutputActions[1] = &EFSM_Heating_DF_HeatEnterPreAir;
    Heating_DF_OutputActions[2] = &EFSM_Heating_DF_HeatEnterPrePGas;
    Heating_DF_OutputActions[3] = &EFSM_Heating_DF_HeatEnterOnTesting;
    Heating_DF_OutputActions[4] = &EFSM_Heating_DF_HeatExitStopDf;
    Heating_DF_OutputActions[5] = &EFSM_Heating_DF_HeatEnterOnNormal;
    Heating_DF_OutputActions[6] = &EFSM_Heating_DF_HeatEnterSparkOff;
    Heating_DF_OutputActions[7] = &EFSM_Heating_DF_HeatEnterPreMGas;
    Heating_DF_OutputActions[8] = &EFSM_Heating_DF_HeatEnterPreSpark;
    
    /*State Machine Definition "Cooling_DF" Initialization*/
    
    /*Associate an array of binary instructions with an EFSM_BINARY structure.*/
    Cooling_DF_Binary.data = efsm_Cooling_DF_binaryData;
    
    /*Initialize the input functions array.*/
    Cooling_DF_Inputs[0] = &EFSM_Cooling_DF_IsStartupTimeElapsed;
    Cooling_DF_Inputs[1] = &EFSM_Cooling_DF_IsHvacCallingForCooling;
    Cooling_DF_Inputs[2] = &EFSM_Cooling_DF_IsY1MinOffTimeExpired;
    Cooling_DF_Inputs[3] = &EFSM_Cooling_DF_IsAllMinOnTimesExpired;
    Cooling_DF_Inputs[4] = &EFSM_Cooling_DF_IsEvapEnabled;
    Cooling_DF_Inputs[5] = &EFSM_Cooling_DF_IsOaTempTooLowForEvap;
    Cooling_DF_Inputs[6] = &EFSM_Cooling_DF_IsBlowerOn;
    Cooling_DF_Inputs[7] = &EFSM_Cooling_DF_IsTestingModeRequested;
    Cooling_DF_Inputs[8] = &EFSM_Cooling_DF_IsEvapHeadStartTimerExpired;
    Cooling_DF_Inputs[9] = &EFSM_Cooling_DF_IsQuickStopRequired;
    Cooling_DF_Inputs[10] = &EFSM_Cooling_DF_IsStage3Required;
    Cooling_DF_Inputs[11] = &EFSM_Cooling_DF_IsStage2Required;
    
    /*Initialize the output functions array.*/
    Cooling_DF_OutputActions[0] = &EFSM_Cooling_DF_CoolingEnterIdle;
    Cooling_DF_OutputActions[1] = &EFSM_Cooling_DF_CoolingEnterEvap;
    Cooling_DF_OutputActions[2] = &EFSM_Cooling_DF_CoolingEnterStage1;
    Cooling_DF_OutputActions[3] = &EFSM_Cooling_DF_CoolingEnterStage2;
    Cooling_DF_OutputActions[4] = &EFSM_Cooling_DF_CoolingEnterStage3;
    Cooling_DF_OutputActions[5] = &EFSM_Cooling_DF_CoolingEnterTest;
    Cooling_DF_OutputActions[6] = &EFSM_Cooling_DF_CoolingExitStop;
    Cooling_DF_OutputActions[7] = &EFSM_Cooling_DF_CoolingEnterStart;
    Cooling_DF_OutputActions[8] = &EFSM_Cooling_DF_CoolingInitIdle;
    
    /*State Machine Definition "Ovrd_Discharge_DF" Initialization*/
    
    /*Associate an array of binary instructions with an EFSM_BINARY structure.*/
    Ovrd_Discharge_DF_Binary.data = efsm_Ovrd_Discharge_DF_binaryData;
    
    /*Initialize the input functions array.*/
    Ovrd_Discharge_DF_Inputs[0] = &EFSM_Ovrd_Discharge_DF_IsStartMinHeatOvrdMet;
    Ovrd_Discharge_DF_Inputs[1] = &EFSM_Ovrd_Discharge_DF_IsStartMaxHeatOvrdMet;
    Ovrd_Discharge_DF_Inputs[2] = &EFSM_Ovrd_Discharge_DF_IsEndMinHeatOvrdMet;
    Ovrd_Discharge_DF_Inputs[3] = &EFSM_Ovrd_Discharge_DF_IsEndMaxHeatOvrdMet;
    Ovrd_Discharge_DF_Inputs[4] = &EFSM_Ovrd_Discharge_DF_IsStartupTimeExpired;
    Ovrd_Discharge_DF_Inputs[5] = &EFSM_Ovrd_Discharge_DF_IsMaxTempRiseExceeded;
    
    /*Initialize the output functions array.*/
    Ovrd_Discharge_DF_OutputActions[0] = &EFSM_Ovrd_Discharge_DF_EnterDoIdle;
    Ovrd_Discharge_DF_OutputActions[1] = &EFSM_Ovrd_Discharge_DF_EnterDoMinOvrdHeat;
    Ovrd_Discharge_DF_OutputActions[2] = &EFSM_Ovrd_Discharge_DF_EnterDoMaxOvrdHeat;
    Ovrd_Discharge_DF_OutputActions[3] = &EFSM_Ovrd_Discharge_DF_EnterDoTempRiseOvrdHeat;
    
    /*State Machine Definition "Freezestat_DF" Initialization*/
    
    /*Associate an array of binary instructions with an EFSM_BINARY structure.*/
    Freezestat_DF_Binary.data = efsm_Freezestat_DF_binaryData;
    
    /*Initialize the input functions array.*/
    Freezestat_DF_Inputs[0] = &EFSM_Freezestat_DF_IsStartupTimeElapsed;
    Freezestat_DF_Inputs[1] = &EFSM_Freezestat_DF_IsDischargeTempLow;
    Freezestat_DF_Inputs[2] = &EFSM_Freezestat_DF_IsDischargeTempRecovered;
    Freezestat_DF_Inputs[3] = &EFSM_Freezestat_DF_IsResumeCountMaxed;
    Freezestat_DF_Inputs[4] = &EFSM_Freezestat_DF_IsResetExecuted;
    Freezestat_DF_Inputs[5] = &EFSM_Freezestat_DF_IsMonitorTimeExpired;
    Freezestat_DF_Inputs[6] = &EFSM_Freezestat_DF_IsStopHvacFlagSet;
    
    /*Initialize the output functions array.*/
    Freezestat_DF_OutputActions[0] = &EFSM_Freezestat_DF_EnterIdle;
    Freezestat_DF_OutputActions[1] = &EFSM_Freezestat_DF_EnterMonitor;
    Freezestat_DF_OutputActions[2] = &EFSM_Freezestat_DF_EnterFailResume;
    Freezestat_DF_OutputActions[3] = &EFSM_Freezestat_DF_EnterFailLock;
    Freezestat_DF_OutputActions[4] = &EFSM_Freezestat_DF_ExitFailLock;
    
    /*State Machine Definition "MixingBox_DF" Initialization*/
    
    /*Associate an array of binary instructions with an EFSM_BINARY structure.*/
    MixingBox_DF_Binary.data = efsm_MixingBox_DF_binaryData;
    
    /*Initialize the input functions array.*/
    MixingBox_DF_Inputs[0] = &EFSM_MixingBox_DF_IsMixboxConfigured;
    MixingBox_DF_Inputs[1] = &EFSM_MixingBox_DF_IsBlowerOn;
    MixingBox_DF_Inputs[2] = &EFSM_MixingBox_DF_IsPurgeOn;
    MixingBox_DF_Inputs[3] = &EFSM_MixingBox_DF_IsTestingModeRequested;
    MixingBox_DF_Inputs[4] = &EFSM_MixingBox_DF_IsStartupTimeElapsed;
    
    /*Initialize the output functions array.*/
    MixingBox_DF_OutputActions[0] = &EFSM_MixingBox_DF_EnterOff;
    MixingBox_DF_OutputActions[1] = &EFSM_MixingBox_DF_EnterIdle;
    MixingBox_DF_OutputActions[2] = &EFSM_MixingBox_DF_EnterOnTest;
    MixingBox_DF_OutputActions[3] = &EFSM_MixingBox_DF_EnterOnPurge;
    MixingBox_DF_OutputActions[4] = &EFSM_MixingBox_DF_EnterOnNormal;
    
    /*Instance initializations.*/
    EFSM_InitializeInstance(&HVAC_DF_Instance_0, &HVAC_DF_Binary, HVAC_DF_OutputActions, HVAC_DF_Inputs, 0);
    EFSM_InitializeInstance(&Blower_DF_Instance_0, &Blower_DF_Binary, Blower_DF_OutputActions, Blower_DF_Inputs, 0);
    EFSM_InitializeInstance(&Heating_DF_Instance_0, &Heating_DF_Binary, Heating_DF_OutputActions, Heating_DF_Inputs, 0);
    EFSM_InitializeInstance(&Cooling_DF_Instance_0, &Cooling_DF_Binary, Cooling_DF_OutputActions, Cooling_DF_Inputs, 0);
    EFSM_InitializeInstance(&Ovrd_Discharge_DF_Instance_0, &Ovrd_Discharge_DF_Binary, Ovrd_Discharge_DF_OutputActions, Ovrd_Discharge_DF_Inputs, 0);
    EFSM_InitializeInstance(&Freezestat_DF_Instance_0, &Freezestat_DF_Binary, Freezestat_DF_OutputActions, Freezestat_DF_Inputs, 0);
    EFSM_InitializeInstance(&MixingBox_DF_Instance_0, &MixingBox_DF_Binary, MixingBox_DF_OutputActions, MixingBox_DF_Inputs, 0);
}

/*
----------------------------------------------------------------------------------------------------
EFSM Process Initialization.
*/

void EFSM_InitializeProcess()
{
    EFSM_df_stateMachines_Init();
}
/*
----------------------------------------------------------------------------------------------------
Diagnostics
*/
void EFSM_GeneratedDiagnostics(EFSM_INSTANCE * efsmInstance)
{
    for(int i = 0; i < efsmInstance->totalNumberOfInputs; i++)
    {
        efsmInstance->inputBuffer[i] = efsmInstance->InputQueries[i](efsmInstance->indexOnEfsmType);
    }
}

/*State Machine State Accessors*/

uint32_t Get_HVAC_DF_Instance_0_State()
{
    return HVAC_DF_Instance_0.state;
}
uint32_t Get_Blower_DF_Instance_0_State()
{
    return Blower_DF_Instance_0.state;
}
uint32_t Get_Heating_DF_Instance_0_State()
{
    return Heating_DF_Instance_0.state;
}
uint32_t Get_Cooling_DF_Instance_0_State()
{
    return Cooling_DF_Instance_0.state;
}
uint32_t Get_Ovrd_Discharge_DF_Instance_0_State()
{
    return Ovrd_Discharge_DF_Instance_0.state;
}
uint32_t Get_Freezestat_DF_Instance_0_State()
{
    return Freezestat_DF_Instance_0.state;
}
uint32_t Get_MixingBox_DF_Instance_0_State()
{
    return MixingBox_DF_Instance_0.state;
}

/*State Machine Input Accessors*/

/*Corresponds to input "IsBlowerRequired" for instance 0 of state machine definition "HVAC_DF".*/
uint32_t Get_HVAC_DF_0_Input_0()
{
    return HVAC_DF_Instance_0.inputBuffer[0];
}

/*Corresponds to input "CanBlow" for instance 0 of state machine definition "HVAC_DF".*/
uint32_t Get_HVAC_DF_0_Input_1()
{
    return HVAC_DF_Instance_0.inputBuffer[1];
}

/*Corresponds to input "IsInHeatRange" for instance 0 of state machine definition "HVAC_DF".*/
uint32_t Get_HVAC_DF_0_Input_2()
{
    return HVAC_DF_Instance_0.inputBuffer[2];
}

/*Corresponds to input "CanHeat" for instance 0 of state machine definition "HVAC_DF".*/
uint32_t Get_HVAC_DF_0_Input_3()
{
    return HVAC_DF_Instance_0.inputBuffer[3];
}

/*Corresponds to input "IsInCoolRange" for instance 0 of state machine definition "HVAC_DF".*/
uint32_t Get_HVAC_DF_0_Input_4()
{
    return HVAC_DF_Instance_0.inputBuffer[4];
}

/*Corresponds to input "CanCool" for instance 0 of state machine definition "HVAC_DF".*/
uint32_t Get_HVAC_DF_0_Input_5()
{
    return HVAC_DF_Instance_0.inputBuffer[5];
}

/*Corresponds to input "IsBlowerOn" for instance 0 of state machine definition "HVAC_DF".*/
uint32_t Get_HVAC_DF_0_Input_6()
{
    return HVAC_DF_Instance_0.inputBuffer[6];
}

/*Corresponds to input "IsBlowerInDelayedStart" for instance 0 of state machine definition "HVAC_DF".*/
uint32_t Get_HVAC_DF_0_Input_7()
{
    return HVAC_DF_Instance_0.inputBuffer[7];
}

/*Corresponds to input "IsHeatOn" for instance 0 of state machine definition "HVAC_DF".*/
uint32_t Get_HVAC_DF_0_Input_8()
{
    return HVAC_DF_Instance_0.inputBuffer[8];
}

/*Corresponds to input "IsCoolOn" for instance 0 of state machine definition "HVAC_DF".*/
uint32_t Get_HVAC_DF_0_Input_9()
{
    return HVAC_DF_Instance_0.inputBuffer[9];
}

/*Corresponds to input "IsStartupTimeElapsed" for instance 0 of state machine definition "HVAC_DF".*/
uint32_t Get_HVAC_DF_0_Input_10()
{
    return HVAC_DF_Instance_0.inputBuffer[10];
}

/*Corresponds to input "IsStartupTimeElapsed" for instance 0 of state machine definition "Blower_DF".*/
uint32_t Get_Blower_DF_0_Input_0()
{
    return Blower_DF_Instance_0.inputBuffer[0];
}

/*Corresponds to input "IsHvacCallingForBlower" for instance 0 of state machine definition "Blower_DF".*/
uint32_t Get_Blower_DF_0_Input_1()
{
    return Blower_DF_Instance_0.inputBuffer[1];
}

/*Corresponds to input "IsDamperEndReceived" for instance 0 of state machine definition "Blower_DF".*/
uint32_t Get_Blower_DF_0_Input_2()
{
    return Blower_DF_Instance_0.inputBuffer[2];
}

/*Corresponds to input "IsAirProvingReceived" for instance 0 of state machine definition "Blower_DF".*/
uint32_t Get_Blower_DF_0_Input_3()
{
    return Blower_DF_Instance_0.inputBuffer[3];
}

/*Corresponds to input "IsDelayedStartEnabled" for instance 0 of state machine definition "Blower_DF".*/
uint32_t Get_Blower_DF_0_Input_4()
{
    return Blower_DF_Instance_0.inputBuffer[4];
}

/*Corresponds to input "IsDelayedStartTimerExpired" for instance 0 of state machine definition "Blower_DF".*/
uint32_t Get_Blower_DF_0_Input_5()
{
    return Blower_DF_Instance_0.inputBuffer[5];
}

/*Corresponds to input "IsDelayedStopEnabled" for instance 0 of state machine definition "Blower_DF".*/
uint32_t Get_Blower_DF_0_Input_6()
{
    return Blower_DF_Instance_0.inputBuffer[6];
}

/*Corresponds to input "IsDelayedStopTimerExpired" for instance 0 of state machine definition "Blower_DF".*/
uint32_t Get_Blower_DF_0_Input_7()
{
    return Blower_DF_Instance_0.inputBuffer[7];
}

/*Corresponds to input "IsRecoverAirTimerExpired" for instance 0 of state machine definition "Blower_DF".*/
uint32_t Get_Blower_DF_0_Input_8()
{
    return Blower_DF_Instance_0.inputBuffer[8];
}

/*Corresponds to input "IsStartupTimeElapsed" for instance 0 of state machine definition "Heating_DF".*/
uint32_t Get_Heating_DF_0_Input_0()
{
    return Heating_DF_Instance_0.inputBuffer[0];
}

/*Corresponds to input "IsHvacCallingForHeat" for instance 0 of state machine definition "Heating_DF".*/
uint32_t Get_Heating_DF_0_Input_1()
{
    return Heating_DF_Instance_0.inputBuffer[1];
}

/*Corresponds to input "IsBlowerOn" for instance 0 of state machine definition "Heating_DF".*/
uint32_t Get_Heating_DF_0_Input_2()
{
    return Heating_DF_Instance_0.inputBuffer[2];
}

/*Corresponds to input "IsTestingModeRequested" for instance 0 of state machine definition "Heating_DF".*/
uint32_t Get_Heating_DF_0_Input_3()
{
    return Heating_DF_Instance_0.inputBuffer[3];
}

/*Corresponds to input "IsPilotGasInReceived" for instance 0 of state machine definition "Heating_DF".*/
uint32_t Get_Heating_DF_0_Input_4()
{
    return Heating_DF_Instance_0.inputBuffer[4];
}

/*Corresponds to input "IsSparkInReceived" for instance 0 of state machine definition "Heating_DF".*/
uint32_t Get_Heating_DF_0_Input_5()
{
    return Heating_DF_Instance_0.inputBuffer[5];
}

/*Corresponds to input "IsMainGasInReceived" for instance 0 of state machine definition "Heating_DF".*/
uint32_t Get_Heating_DF_0_Input_6()
{
    return Heating_DF_Instance_0.inputBuffer[6];
}

/*Corresponds to input "IsStartupTimeElapsed" for instance 0 of state machine definition "Cooling_DF".*/
uint32_t Get_Cooling_DF_0_Input_0()
{
    return Cooling_DF_Instance_0.inputBuffer[0];
}

/*Corresponds to input "IsHvacCallingForCooling" for instance 0 of state machine definition "Cooling_DF".*/
uint32_t Get_Cooling_DF_0_Input_1()
{
    return Cooling_DF_Instance_0.inputBuffer[1];
}

/*Corresponds to input "IsY1MinOffTimeExpired" for instance 0 of state machine definition "Cooling_DF".*/
uint32_t Get_Cooling_DF_0_Input_2()
{
    return Cooling_DF_Instance_0.inputBuffer[2];
}

/*Corresponds to input "IsAllMinOnTimesExpired" for instance 0 of state machine definition "Cooling_DF".*/
uint32_t Get_Cooling_DF_0_Input_3()
{
    return Cooling_DF_Instance_0.inputBuffer[3];
}

/*Corresponds to input "IsEvapEnabled" for instance 0 of state machine definition "Cooling_DF".*/
uint32_t Get_Cooling_DF_0_Input_4()
{
    return Cooling_DF_Instance_0.inputBuffer[4];
}

/*Corresponds to input "IsOaTempTooLowForEvap" for instance 0 of state machine definition "Cooling_DF".*/
uint32_t Get_Cooling_DF_0_Input_5()
{
    return Cooling_DF_Instance_0.inputBuffer[5];
}

/*Corresponds to input "IsBlowerOn" for instance 0 of state machine definition "Cooling_DF".*/
uint32_t Get_Cooling_DF_0_Input_6()
{
    return Cooling_DF_Instance_0.inputBuffer[6];
}

/*Corresponds to input "IsTestingModeRequested" for instance 0 of state machine definition "Cooling_DF".*/
uint32_t Get_Cooling_DF_0_Input_7()
{
    return Cooling_DF_Instance_0.inputBuffer[7];
}

/*Corresponds to input "IsEvapHeadStartTimerExpired" for instance 0 of state machine definition "Cooling_DF".*/
uint32_t Get_Cooling_DF_0_Input_8()
{
    return Cooling_DF_Instance_0.inputBuffer[8];
}

/*Corresponds to input "IsQuickStopRequired" for instance 0 of state machine definition "Cooling_DF".*/
uint32_t Get_Cooling_DF_0_Input_9()
{
    return Cooling_DF_Instance_0.inputBuffer[9];
}

/*Corresponds to input "IsStage3Required" for instance 0 of state machine definition "Cooling_DF".*/
uint32_t Get_Cooling_DF_0_Input_10()
{
    return Cooling_DF_Instance_0.inputBuffer[10];
}

/*Corresponds to input "IsStage2Required" for instance 0 of state machine definition "Cooling_DF".*/
uint32_t Get_Cooling_DF_0_Input_11()
{
    return Cooling_DF_Instance_0.inputBuffer[11];
}

/*Corresponds to input "IsStartMinHeatOvrdMet" for instance 0 of state machine definition "Ovrd_Discharge_DF".*/
uint32_t Get_Ovrd_Discharge_DF_0_Input_0()
{
    return Ovrd_Discharge_DF_Instance_0.inputBuffer[0];
}

/*Corresponds to input "IsStartMaxHeatOvrdMet" for instance 0 of state machine definition "Ovrd_Discharge_DF".*/
uint32_t Get_Ovrd_Discharge_DF_0_Input_1()
{
    return Ovrd_Discharge_DF_Instance_0.inputBuffer[1];
}

/*Corresponds to input "IsEndMinHeatOvrdMet" for instance 0 of state machine definition "Ovrd_Discharge_DF".*/
uint32_t Get_Ovrd_Discharge_DF_0_Input_2()
{
    return Ovrd_Discharge_DF_Instance_0.inputBuffer[2];
}

/*Corresponds to input "IsEndMaxHeatOvrdMet" for instance 0 of state machine definition "Ovrd_Discharge_DF".*/
uint32_t Get_Ovrd_Discharge_DF_0_Input_3()
{
    return Ovrd_Discharge_DF_Instance_0.inputBuffer[3];
}

/*Corresponds to input "IsStartupTimeExpired" for instance 0 of state machine definition "Ovrd_Discharge_DF".*/
uint32_t Get_Ovrd_Discharge_DF_0_Input_4()
{
    return Ovrd_Discharge_DF_Instance_0.inputBuffer[4];
}

/*Corresponds to input "IsMaxTempRiseExceeded" for instance 0 of state machine definition "Ovrd_Discharge_DF".*/
uint32_t Get_Ovrd_Discharge_DF_0_Input_5()
{
    return Ovrd_Discharge_DF_Instance_0.inputBuffer[5];
}

/*Corresponds to input "IsStartupTimeElapsed" for instance 0 of state machine definition "Freezestat_DF".*/
uint32_t Get_Freezestat_DF_0_Input_0()
{
    return Freezestat_DF_Instance_0.inputBuffer[0];
}

/*Corresponds to input "IsDischargeTempLow" for instance 0 of state machine definition "Freezestat_DF".*/
uint32_t Get_Freezestat_DF_0_Input_1()
{
    return Freezestat_DF_Instance_0.inputBuffer[1];
}

/*Corresponds to input "IsDischargeTempRecovered" for instance 0 of state machine definition "Freezestat_DF".*/
uint32_t Get_Freezestat_DF_0_Input_2()
{
    return Freezestat_DF_Instance_0.inputBuffer[2];
}

/*Corresponds to input "IsResumeCountMaxed" for instance 0 of state machine definition "Freezestat_DF".*/
uint32_t Get_Freezestat_DF_0_Input_3()
{
    return Freezestat_DF_Instance_0.inputBuffer[3];
}

/*Corresponds to input "IsResetExecuted" for instance 0 of state machine definition "Freezestat_DF".*/
uint32_t Get_Freezestat_DF_0_Input_4()
{
    return Freezestat_DF_Instance_0.inputBuffer[4];
}

/*Corresponds to input "IsMonitorTimeExpired" for instance 0 of state machine definition "Freezestat_DF".*/
uint32_t Get_Freezestat_DF_0_Input_5()
{
    return Freezestat_DF_Instance_0.inputBuffer[5];
}

/*Corresponds to input "IsStopHvacFlagSet" for instance 0 of state machine definition "Freezestat_DF".*/
uint32_t Get_Freezestat_DF_0_Input_6()
{
    return Freezestat_DF_Instance_0.inputBuffer[6];
}

/*Corresponds to input "IsMixboxConfigured" for instance 0 of state machine definition "MixingBox_DF".*/
uint32_t Get_MixingBox_DF_0_Input_0()
{
    return MixingBox_DF_Instance_0.inputBuffer[0];
}

/*Corresponds to input "IsBlowerOn" for instance 0 of state machine definition "MixingBox_DF".*/
uint32_t Get_MixingBox_DF_0_Input_1()
{
    return MixingBox_DF_Instance_0.inputBuffer[1];
}

/*Corresponds to input "IsPurgeOn" for instance 0 of state machine definition "MixingBox_DF".*/
uint32_t Get_MixingBox_DF_0_Input_2()
{
    return MixingBox_DF_Instance_0.inputBuffer[2];
}

/*Corresponds to input "IsTestingModeRequested" for instance 0 of state machine definition "MixingBox_DF".*/
uint32_t Get_MixingBox_DF_0_Input_3()
{
    return MixingBox_DF_Instance_0.inputBuffer[3];
}

/*Corresponds to input "IsStartupTimeElapsed" for instance 0 of state machine definition "MixingBox_DF".*/
uint32_t Get_MixingBox_DF_0_Input_4()
{
    return MixingBox_DF_Instance_0.inputBuffer[4];
}

/*
----------------------------------------------------------------------------------------------------
EFSM Debugging
*/
uint8_t EFSM_HVAC_DF_IsBlowerRequired(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 0];
}
uint8_t EFSM_HVAC_DF_CanBlow(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 1];
}
uint8_t EFSM_HVAC_DF_IsInHeatRange(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 2];
}
uint8_t EFSM_HVAC_DF_CanHeat(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 3];
}
uint8_t EFSM_HVAC_DF_IsInCoolRange(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 4];
}
uint8_t EFSM_HVAC_DF_CanCool(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 5];
}
uint8_t EFSM_HVAC_DF_IsBlowerOn(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 6];
}
uint8_t EFSM_HVAC_DF_IsBlowerInDelayedStart(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 7];
}
uint8_t EFSM_HVAC_DF_IsHeatOn(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 8];
}
uint8_t EFSM_HVAC_DF_IsCoolOn(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 9];
}
uint8_t EFSM_HVAC_DF_IsStartupTimeElapsed(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 10];
}

void EFSM_HVAC_DF_HvacInit(uint8_t indexOnEfsmType)
{
}
void EFSM_HVAC_DF_HvacEnterBlower(uint8_t indexOnEfsmType)
{
}
void EFSM_HVAC_DF_HvacEnterPreheat(uint8_t indexOnEfsmType)
{
}
void EFSM_HVAC_DF_HvacEnterHeating(uint8_t indexOnEfsmType)
{
}
void EFSM_HVAC_DF_HvacEnterStopHeat(uint8_t indexOnEfsmType)
{
}
void EFSM_HVAC_DF_HvacEnterPrecool(uint8_t indexOnEfsmType)
{
}
void EFSM_HVAC_DF_HvacEnterCooling(uint8_t indexOnEfsmType)
{
}
void EFSM_HVAC_DF_HvacEnterStopCool(uint8_t indexOnEfsmType)
{
}
void EFSM_HVAC_DF_HvacEnterIdle(uint8_t indexOnEfsmType)
{
}
uint8_t EFSM_Blower_DF_IsStartupTimeElapsed(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 0];
}
uint8_t EFSM_Blower_DF_IsHvacCallingForBlower(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 1];
}
uint8_t EFSM_Blower_DF_IsDamperEndReceived(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 2];
}
uint8_t EFSM_Blower_DF_IsAirProvingReceived(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 3];
}
uint8_t EFSM_Blower_DF_IsDelayedStartEnabled(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 4];
}
uint8_t EFSM_Blower_DF_IsDelayedStartTimerExpired(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 5];
}
uint8_t EFSM_Blower_DF_IsDelayedStopEnabled(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 6];
}
uint8_t EFSM_Blower_DF_IsDelayedStopTimerExpired(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 7];
}
uint8_t EFSM_Blower_DF_IsRecoverAirTimerExpired(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 8];
}

void EFSM_Blower_DF_BlowerEnterIdle(uint8_t indexOnEfsmType)
{
}
void EFSM_Blower_DF_BlowerEnterPreStart(uint8_t indexOnEfsmType)
{
}
void EFSM_Blower_DF_BlowerEnterDelayedStart(uint8_t indexOnEfsmType)
{
}
void EFSM_Blower_DF_BlowerEnterOpenDamper(uint8_t indexOnEfsmType)
{
}
void EFSM_Blower_DF_BlowerEnterProveAir(uint8_t indexOnEfsmType)
{
}
void EFSM_Blower_DF_BlowerEnterBlowerOn(uint8_t indexOnEfsmType)
{
}
void EFSM_Blower_DF_BlowerEnterDelayedStop(uint8_t indexOnEfsmType)
{
}
void EFSM_Blower_DF_BlowerInitIdle(uint8_t indexOnEfsmType)
{
}
void EFSM_Blower_DF_BlowerEnterRecoverAir(uint8_t indexOnEfsmType)
{
}
uint8_t EFSM_Heating_DF_IsStartupTimeElapsed(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 0];
}
uint8_t EFSM_Heating_DF_IsHvacCallingForHeat(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 1];
}
uint8_t EFSM_Heating_DF_IsBlowerOn(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 2];
}
uint8_t EFSM_Heating_DF_IsTestingModeRequested(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 3];
}
uint8_t EFSM_Heating_DF_IsPilotGasInReceived(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 4];
}
uint8_t EFSM_Heating_DF_IsSparkInReceived(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 5];
}
uint8_t EFSM_Heating_DF_IsMainGasInReceived(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 6];
}

void EFSM_Heating_DF_HeatEnterIdle(uint8_t indexOnEfsmType)
{
}
void EFSM_Heating_DF_HeatEnterPreAir(uint8_t indexOnEfsmType)
{
}
void EFSM_Heating_DF_HeatEnterPrePGas(uint8_t indexOnEfsmType)
{
}
void EFSM_Heating_DF_HeatEnterOnTesting(uint8_t indexOnEfsmType)
{
}
void EFSM_Heating_DF_HeatExitStopDf(uint8_t indexOnEfsmType)
{
}
void EFSM_Heating_DF_HeatEnterOnNormal(uint8_t indexOnEfsmType)
{
}
void EFSM_Heating_DF_HeatEnterSparkOff(uint8_t indexOnEfsmType)
{
}
void EFSM_Heating_DF_HeatEnterPreMGas(uint8_t indexOnEfsmType)
{
}
void EFSM_Heating_DF_HeatEnterPreSpark(uint8_t indexOnEfsmType)
{
}
uint8_t EFSM_Cooling_DF_IsStartupTimeElapsed(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 0];
}
uint8_t EFSM_Cooling_DF_IsHvacCallingForCooling(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 1];
}
uint8_t EFSM_Cooling_DF_IsY1MinOffTimeExpired(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 2];
}
uint8_t EFSM_Cooling_DF_IsAllMinOnTimesExpired(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 3];
}
uint8_t EFSM_Cooling_DF_IsEvapEnabled(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 4];
}
uint8_t EFSM_Cooling_DF_IsOaTempTooLowForEvap(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 5];
}
uint8_t EFSM_Cooling_DF_IsBlowerOn(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 6];
}
uint8_t EFSM_Cooling_DF_IsTestingModeRequested(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 7];
}
uint8_t EFSM_Cooling_DF_IsEvapHeadStartTimerExpired(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 8];
}
uint8_t EFSM_Cooling_DF_IsQuickStopRequired(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 9];
}
uint8_t EFSM_Cooling_DF_IsStage3Required(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 10];
}
uint8_t EFSM_Cooling_DF_IsStage2Required(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 11];
}

void EFSM_Cooling_DF_CoolingEnterIdle(uint8_t indexOnEfsmType)
{
}
void EFSM_Cooling_DF_CoolingEnterEvap(uint8_t indexOnEfsmType)
{
}
void EFSM_Cooling_DF_CoolingEnterStage1(uint8_t indexOnEfsmType)
{
}
void EFSM_Cooling_DF_CoolingEnterStage2(uint8_t indexOnEfsmType)
{
}
void EFSM_Cooling_DF_CoolingEnterStage3(uint8_t indexOnEfsmType)
{
}
void EFSM_Cooling_DF_CoolingEnterTest(uint8_t indexOnEfsmType)
{
}
void EFSM_Cooling_DF_CoolingExitStop(uint8_t indexOnEfsmType)
{
}
void EFSM_Cooling_DF_CoolingEnterStart(uint8_t indexOnEfsmType)
{
}
void EFSM_Cooling_DF_CoolingInitIdle(uint8_t indexOnEfsmType)
{
}
uint8_t EFSM_Ovrd_Discharge_DF_IsStartMinHeatOvrdMet(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 0];
}
uint8_t EFSM_Ovrd_Discharge_DF_IsStartMaxHeatOvrdMet(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 1];
}
uint8_t EFSM_Ovrd_Discharge_DF_IsEndMinHeatOvrdMet(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 2];
}
uint8_t EFSM_Ovrd_Discharge_DF_IsEndMaxHeatOvrdMet(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 3];
}
uint8_t EFSM_Ovrd_Discharge_DF_IsStartupTimeExpired(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 4];
}
uint8_t EFSM_Ovrd_Discharge_DF_IsMaxTempRiseExceeded(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 5];
}

void EFSM_Ovrd_Discharge_DF_EnterDoIdle(uint8_t indexOnEfsmType)
{
}
void EFSM_Ovrd_Discharge_DF_EnterDoMinOvrdHeat(uint8_t indexOnEfsmType)
{
}
void EFSM_Ovrd_Discharge_DF_EnterDoMaxOvrdHeat(uint8_t indexOnEfsmType)
{
}
void EFSM_Ovrd_Discharge_DF_EnterDoTempRiseOvrdHeat(uint8_t indexOnEfsmType)
{
}
uint8_t EFSM_Freezestat_DF_IsStartupTimeElapsed(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 0];
}
uint8_t EFSM_Freezestat_DF_IsDischargeTempLow(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 1];
}
uint8_t EFSM_Freezestat_DF_IsDischargeTempRecovered(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 2];
}
uint8_t EFSM_Freezestat_DF_IsResumeCountMaxed(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 3];
}
uint8_t EFSM_Freezestat_DF_IsResetExecuted(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 4];
}
uint8_t EFSM_Freezestat_DF_IsMonitorTimeExpired(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 5];
}
uint8_t EFSM_Freezestat_DF_IsStopHvacFlagSet(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 6];
}

void EFSM_Freezestat_DF_EnterIdle(uint8_t indexOnEfsmType)
{
}
void EFSM_Freezestat_DF_EnterMonitor(uint8_t indexOnEfsmType)
{
}
void EFSM_Freezestat_DF_EnterFailResume(uint8_t indexOnEfsmType)
{
}
void EFSM_Freezestat_DF_EnterFailLock(uint8_t indexOnEfsmType)
{
}
void EFSM_Freezestat_DF_ExitFailLock(uint8_t indexOnEfsmType)
{
}
uint8_t EFSM_MixingBox_DF_IsMixboxConfigured(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 0];
}
uint8_t EFSM_MixingBox_DF_IsBlowerOn(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 1];
}
uint8_t EFSM_MixingBox_DF_IsPurgeOn(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 2];
}
uint8_t EFSM_MixingBox_DF_IsTestingModeRequested(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 3];
}
uint8_t EFSM_MixingBox_DF_IsStartupTimeElapsed(uint8_t indexOnEfsmType)
{
    return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + 4];
}

void EFSM_MixingBox_DF_EnterOff(uint8_t indexOnEfsmType)
{
}
void EFSM_MixingBox_DF_EnterIdle(uint8_t indexOnEfsmType)
{
}
void EFSM_MixingBox_DF_EnterOnTest(uint8_t indexOnEfsmType)
{
}
void EFSM_MixingBox_DF_EnterOnPurge(uint8_t indexOnEfsmType)
{
}
void EFSM_MixingBox_DF_EnterOnNormal(uint8_t indexOnEfsmType)
{
}

int main(int argc, char *argv[])
{
    printf("Starting the EFSM Debug Manager...\n\n");
    strcpy(debugStatusTxFilename, argv[1]);
    strcpy(debugCommandRxFilename, argv[2]);

    while(1)
    {
        EfsmDebugManager();
    }
    
    return 0;
}
