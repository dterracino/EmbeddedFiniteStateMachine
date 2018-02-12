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
    54, 
    
    /*[7]: EFSM binary index of state ON_TESTING */
    68, 
    
    /*[8]: EFSM binary index of state STOP_DF */
    82, 
    
    /*[9]: EFSM binary index of state DF_PRE_PGAS */
    93, 
    
    /*[10]: EFSM binary index of state DF_PRE_SPARK */
    115, 
    
    /*[11]: EFSM binary index of state DF_PRE_MGAS */
    137, 
    
    /*[12]: EFSM binary index of state DF_SPARK_OFF */
    159, 
    
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
    1, 
    
    /*[53]: Action HeatEnterStopDf function reference index. */
    4, 
    
    /*[54]: State ON_NORMAL base index. */
    /*[54]: Number of inputs. */
    2, 
    
    /*[55]: Number of transitions. */
    1, 
    
    /*[56]: EFSM binary index of IQFN data for state ON_NORMAL */
    58, 
    
    /*[57]: EFSM binary index of transition OnNormalToStopDf data. */
    60, 
    
    /*[58]: State ON_NORMAL start of IQFN data. */
    /*[58]: IQFN function reference index for prototype IsHvacCallingForHeat */
    1, 
    
    /*[59]: IQFN function reference index for prototype IsBlowerOn */
    2, 
    
    /*[60]: Transition OnNormalToStopDf data. */
    /*[60]: EFSM binary index of transition OnNormalToStopDf actions data. */
    66, 
    
    /*[61]: Next state after transition. */
    5, 
    
    /*[62]: Number of opcodes. */
    5, 
    
    /*[63]: Opcodes 0 and 1 */
    897, 
    
    /*[64]: Transition Actions data. */
    /*[64]: Opcodes 2 and 3 */
    898, 
    
    /*[65]: Transition Actions data. */
    /*[65]: Opcodes 4 and 5 */
    1, 
    
    /*[66]: Transition Actions data. */
    /*[66]: Number of transition actions for transition OnNormalToStopDf */
    1, 
    
    /*[67]: Action HeatEnterStopDf function reference index. */
    4, 
    
    /*[68]: State ON_TESTING base index. */
    /*[68]: Number of inputs. */
    2, 
    
    /*[69]: Number of transitions. */
    1, 
    
    /*[70]: EFSM binary index of IQFN data for state ON_TESTING */
    72, 
    
    /*[71]: EFSM binary index of transition OnTestingToStopDf data. */
    74, 
    
    /*[72]: State ON_TESTING start of IQFN data. */
    /*[72]: IQFN function reference index for prototype IsHvacCallingForHeat */
    1, 
    
    /*[73]: IQFN function reference index for prototype IsBlowerOn */
    2, 
    
    /*[74]: Transition OnTestingToStopDf data. */
    /*[74]: EFSM binary index of transition OnTestingToStopDf actions data. */
    80, 
    
    /*[75]: Next state after transition. */
    5, 
    
    /*[76]: Number of opcodes. */
    5, 
    
    /*[77]: Opcodes 0 and 1 */
    897, 
    
    /*[78]: Transition Actions data. */
    /*[78]: Opcodes 2 and 3 */
    898, 
    
    /*[79]: Transition Actions data. */
    /*[79]: Opcodes 4 and 5 */
    1, 
    
    /*[80]: Transition Actions data. */
    /*[80]: Number of transition actions for transition OnTestingToStopDf */
    1, 
    
    /*[81]: Action HeatEnterStopDf function reference index. */
    4, 
    
    /*[82]: State STOP_DF base index. */
    /*[82]: Number of inputs. */
    1, 
    
    /*[83]: Number of transitions. */
    1, 
    
    /*[84]: EFSM binary index of IQFN data for state STOP_DF */
    86, 
    
    /*[85]: EFSM binary index of transition StopDfToIdle data. */
    87, 
    
    /*[86]: State STOP_DF start of IQFN data. */
    /*[86]: IQFN function reference index for prototype IsStartupTimeElapsed */
    0, 
    
    /*[87]: Transition StopDfToIdle data. */
    /*[87]: EFSM binary index of transition StopDfToIdle actions data. */
    91, 
    
    /*[88]: Next state after transition. */
    1, 
    
    /*[89]: Number of opcodes. */
    1, 
    
    /*[90]: Opcodes 0 and 1 */
    128, 
    
    /*[91]: Transition Actions data. */
    /*[91]: Number of transition actions for transition StopDfToIdle */
    1, 
    
    /*[92]: Action HeatEnterIdle function reference index. */
    0, 
    
    /*[93]: State DF_PRE_PGAS base index. */
    /*[93]: Number of inputs. */
    3, 
    
    /*[94]: Number of transitions. */
    2, 
    
    /*[95]: EFSM binary index of IQFN data for state DF_PRE_PGAS */
    98, 
    
    /*[96]: EFSM binary index of transition DfPrePGasToPreSpark data. */
    101, 
    
    /*[97]: EFSM binary index of transition DfPrePGasToStop data. */
    107, 
    
    /*[98]: State DF_PRE_PGAS start of IQFN data. */
    /*[98]: IQFN function reference index for prototype IsPilotGasInReceived */
    4, 
    
    /*[99]: IQFN function reference index for prototype IsHvacCallingForHeat */
    1, 
    
    /*[100]: IQFN function reference index for prototype IsBlowerOn */
    2, 
    
    /*[101]: Transition DfPrePGasToPreSpark data. */
    /*[101]: EFSM binary index of transition DfPrePGasToPreSpark actions data. */
    105, 
    
    /*[102]: Next state after transition. */
    7, 
    
    /*[103]: Number of opcodes. */
    1, 
    
    /*[104]: Opcodes 0 and 1 */
    132, 
    
    /*[105]: Transition Actions data. */
    /*[105]: Number of transition actions for transition DfPrePGasToPreSpark */
    1, 
    
    /*[106]: Action HeatEnterPreSpark function reference index. */
    8, 
    
    /*[107]: Transition DfPrePGasToStop data. */
    /*[107]: EFSM binary index of transition DfPrePGasToStop actions data. */
    113, 
    
    /*[108]: Next state after transition. */
    5, 
    
    /*[109]: Number of opcodes. */
    5, 
    
    /*[110]: Opcodes 0 and 1 */
    897, 
    
    /*[111]: Transition Actions data. */
    /*[111]: Opcodes 2 and 3 */
    898, 
    
    /*[112]: Transition Actions data. */
    /*[112]: Opcodes 4 and 5 */
    1, 
    
    /*[113]: Transition Actions data. */
    /*[113]: Number of transition actions for transition DfPrePGasToStop */
    1, 
    
    /*[114]: Action HeatEnterStopDf function reference index. */
    4, 
    
    /*[115]: State DF_PRE_SPARK base index. */
    /*[115]: Number of inputs. */
    3, 
    
    /*[116]: Number of transitions. */
    2, 
    
    /*[117]: EFSM binary index of IQFN data for state DF_PRE_SPARK */
    120, 
    
    /*[118]: EFSM binary index of transition DfPreSparkToPreMGas data. */
    123, 
    
    /*[119]: EFSM binary index of transition DfPreSparkToStop data. */
    129, 
    
    /*[120]: State DF_PRE_SPARK start of IQFN data. */
    /*[120]: IQFN function reference index for prototype IsSparkInReceived */
    5, 
    
    /*[121]: IQFN function reference index for prototype IsHvacCallingForHeat */
    1, 
    
    /*[122]: IQFN function reference index for prototype IsBlowerOn */
    2, 
    
    /*[123]: Transition DfPreSparkToPreMGas data. */
    /*[123]: EFSM binary index of transition DfPreSparkToPreMGas actions data. */
    127, 
    
    /*[124]: Next state after transition. */
    8, 
    
    /*[125]: Number of opcodes. */
    1, 
    
    /*[126]: Opcodes 0 and 1 */
    133, 
    
    /*[127]: Transition Actions data. */
    /*[127]: Number of transition actions for transition DfPreSparkToPreMGas */
    1, 
    
    /*[128]: Action HeatEnterPreMGas function reference index. */
    7, 
    
    /*[129]: Transition DfPreSparkToStop data. */
    /*[129]: EFSM binary index of transition DfPreSparkToStop actions data. */
    135, 
    
    /*[130]: Next state after transition. */
    5, 
    
    /*[131]: Number of opcodes. */
    5, 
    
    /*[132]: Opcodes 0 and 1 */
    897, 
    
    /*[133]: Transition Actions data. */
    /*[133]: Opcodes 2 and 3 */
    898, 
    
    /*[134]: Transition Actions data. */
    /*[134]: Opcodes 4 and 5 */
    1, 
    
    /*[135]: Transition Actions data. */
    /*[135]: Number of transition actions for transition DfPreSparkToStop */
    1, 
    
    /*[136]: Action HeatEnterStopDf function reference index. */
    4, 
    
    /*[137]: State DF_PRE_MGAS base index. */
    /*[137]: Number of inputs. */
    3, 
    
    /*[138]: Number of transitions. */
    2, 
    
    /*[139]: EFSM binary index of IQFN data for state DF_PRE_MGAS */
    142, 
    
    /*[140]: EFSM binary index of transition DfPreMGasToSparkOff data. */
    145, 
    
    /*[141]: EFSM binary index of transition DfPreMGasToStop data. */
    151, 
    
    /*[142]: State DF_PRE_MGAS start of IQFN data. */
    /*[142]: IQFN function reference index for prototype IsMainGasInReceived */
    6, 
    
    /*[143]: IQFN function reference index for prototype IsHvacCallingForHeat */
    1, 
    
    /*[144]: IQFN function reference index for prototype IsBlowerOn */
    2, 
    
    /*[145]: Transition DfPreMGasToSparkOff data. */
    /*[145]: EFSM binary index of transition DfPreMGasToSparkOff actions data. */
    149, 
    
    /*[146]: Next state after transition. */
    9, 
    
    /*[147]: Number of opcodes. */
    1, 
    
    /*[148]: Opcodes 0 and 1 */
    134, 
    
    /*[149]: Transition Actions data. */
    /*[149]: Number of transition actions for transition DfPreMGasToSparkOff */
    1, 
    
    /*[150]: Action HeatEnterSparkOff function reference index. */
    6, 
    
    /*[151]: Transition DfPreMGasToStop data. */
    /*[151]: EFSM binary index of transition DfPreMGasToStop actions data. */
    157, 
    
    /*[152]: Next state after transition. */
    5, 
    
    /*[153]: Number of opcodes. */
    5, 
    
    /*[154]: Opcodes 0 and 1 */
    897, 
    
    /*[155]: Transition Actions data. */
    /*[155]: Opcodes 2 and 3 */
    898, 
    
    /*[156]: Transition Actions data. */
    /*[156]: Opcodes 4 and 5 */
    1, 
    
    /*[157]: Transition Actions data. */
    /*[157]: Number of transition actions for transition DfPreMGasToStop */
    1, 
    
    /*[158]: Action HeatEnterStopDf function reference index. */
    4, 
    
    /*[159]: State DF_SPARK_OFF base index. */
    /*[159]: Number of inputs. */
    4, 
    
    /*[160]: Number of transitions. */
    3, 
    
    /*[161]: EFSM binary index of IQFN data for state DF_SPARK_OFF */
    165, 
    
    /*[162]: EFSM binary index of transition DfSparkOffToOnNormal data. */
    169, 
    
    /*[163]: EFSM binary index of transition DfSparkOffToOnTesting data. */
    177, 
    
    /*[164]: EFSM binary index of transition DfSparkOffToStop data. */
    184, 
    
    /*[165]: State DF_SPARK_OFF start of IQFN data. */
    /*[165]: IQFN function reference index for prototype IsSparkInReceived */
    5, 
    
    /*[166]: IQFN function reference index for prototype IsTestingModeRequested */
    3, 
    
    /*[167]: IQFN function reference index for prototype IsHvacCallingForHeat */
    1, 
    
    /*[168]: IQFN function reference index for prototype IsBlowerOn */
    2, 
    
    /*[169]: Transition DfSparkOffToOnNormal data. */
    /*[169]: EFSM binary index of transition DfSparkOffToOnNormal actions data. */
    175, 
    
    /*[170]: Next state after transition. */
    3, 
    
    /*[171]: Number of opcodes. */
    5, 
    
    /*[172]: Opcodes 0 and 1 */
    901, 
    
    /*[173]: Transition Actions data. */
    /*[173]: Opcodes 2 and 3 */
    899, 
    
    /*[174]: Transition Actions data. */
    /*[174]: Opcodes 4 and 5 */
    2, 
    
    /*[175]: Transition Actions data. */
    /*[175]: Number of transition actions for transition DfSparkOffToOnNormal */
    1, 
    
    /*[176]: Action HeatEnterOnNormal function reference index. */
    5, 
    
    /*[177]: Transition DfSparkOffToOnTesting data. */
    /*[177]: EFSM binary index of transition DfSparkOffToOnTesting actions data. */
    182, 
    
    /*[178]: Next state after transition. */
    4, 
    
    /*[179]: Number of opcodes. */
    4, 
    
    /*[180]: Opcodes 0 and 1 */
    901, 
    
    /*[181]: Transition Actions data. */
    /*[181]: Opcodes 2 and 3 */
    643, 
    
    /*[182]: Transition Actions data. */
    /*[182]: Number of transition actions for transition DfSparkOffToOnTesting */
    1, 
    
    /*[183]: Action HeatEnterOnTesting function reference index. */
    3, 
    
    /*[184]: Transition DfSparkOffToStop data. */
    /*[184]: EFSM binary index of transition DfSparkOffToStop actions data. */
    190, 
    
    /*[185]: Next state after transition. */
    5, 
    
    /*[186]: Number of opcodes. */
    5, 
    
    /*[187]: Opcodes 0 and 1 */
    897, 
    
    /*[188]: Transition Actions data. */
    /*[188]: Opcodes 2 and 3 */
    898, 
    
    /*[189]: Transition Actions data. */
    /*[189]: Opcodes 4 and 5 */
    1, 
    
    /*[190]: Transition Actions data. */
    /*[190]: Number of transition actions for transition DfSparkOffToStop */
    1, 
    
    /*[191]: Action HeatEnterStopDf function reference index. */
    4 
    
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
    97, 
    
    /*[8]: EFSM binary index of state COOL_STAGE1 */
    110, 
    
    /*[9]: EFSM binary index of state STOP_COOLING */
    131, 
    
    /*[10]: EFSM binary index of state COOL_STAGE2 */
    145, 
    
    /*[11]: EFSM binary index of state COOL_STAGE3 */
    166, 
    
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
    5, 
    
    /*[73]: Number of transitions. */
    2, 
    
    /*[74]: EFSM binary index of IQFN data for state COOL_EVAP */
    77, 
    
    /*[75]: EFSM binary index of transition EvapToStopCooling data. */
    82, 
    
    /*[76]: EFSM binary index of transition EvapToStage1 data. */
    89, 
    
    /*[77]: State COOL_EVAP start of IQFN data. */
    /*[77]: IQFN function reference index for prototype IsHvacCallingForCooling */
    1, 
    
    /*[78]: IQFN function reference index for prototype IsBlowerOn */
    6, 
    
    /*[79]: IQFN function reference index for prototype IsEvapHeadStartTimerExpired */
    8, 
    
    /*[80]: IQFN function reference index for prototype IsOaTempTooLowForEvap */
    5, 
    
    /*[81]: IQFN function reference index for prototype IsY1MinOffTimeExpired */
    2, 
    
    /*[82]: Transition EvapToStopCooling data. */
    /*[82]: EFSM binary index of transition EvapToStopCooling actions data. */
    88, 
    
    /*[83]: Next state after transition. */
    6, 
    
    /*[84]: Number of opcodes. */
    5, 
    
    /*[85]: Opcodes 0 and 1 */
    897, 
    
    /*[86]: Transition Actions data. */
    /*[86]: Opcodes 2 and 3 */
    902, 
    
    /*[87]: Transition Actions data. */
    /*[87]: Opcodes 4 and 5 */
    1, 
    
    /*[88]: Transition Actions data. */
    /*[88]: Number of transition actions for transition EvapToStopCooling */
    0, 
    
    /*[89]: Transition EvapToStage1 data. */
    /*[89]: EFSM binary index of transition EvapToStage1 actions data. */
    95, 
    
    /*[90]: Next state after transition. */
    5, 
    
    /*[91]: Number of opcodes. */
    5, 
    
    /*[92]: Opcodes 0 and 1 */
    34184, 
    
    /*[93]: Transition Actions data. */
    /*[93]: Opcodes 2 and 3 */
    33281, 
    
    /*[94]: Transition Actions data. */
    /*[94]: Opcodes 4 and 5 */
    2, 
    
    /*[95]: Transition Actions data. */
    /*[95]: Number of transition actions for transition EvapToStage1 */
    1, 
    
    /*[96]: Action CoolingEnterStage1 function reference index. */
    2, 
    
    /*[97]: State ON_TESTING base index. */
    /*[97]: Number of inputs. */
    2, 
    
    /*[98]: Number of transitions. */
    1, 
    
    /*[99]: EFSM binary index of IQFN data for state ON_TESTING */
    101, 
    
    /*[100]: EFSM binary index of transition OnTestingToStopCooling data. */
    103, 
    
    /*[101]: State ON_TESTING start of IQFN data. */
    /*[101]: IQFN function reference index for prototype IsTestingModeRequested */
    7, 
    
    /*[102]: IQFN function reference index for prototype IsBlowerOn */
    6, 
    
    /*[103]: Transition OnTestingToStopCooling data. */
    /*[103]: EFSM binary index of transition OnTestingToStopCooling actions data. */
    109, 
    
    /*[104]: Next state after transition. */
    6, 
    
    /*[105]: Number of opcodes. */
    5, 
    
    /*[106]: Opcodes 0 and 1 */
    903, 
    
    /*[107]: Transition Actions data. */
    /*[107]: Opcodes 2 and 3 */
    902, 
    
    /*[108]: Transition Actions data. */
    /*[108]: Opcodes 4 and 5 */
    1, 
    
    /*[109]: Transition Actions data. */
    /*[109]: Number of transition actions for transition OnTestingToStopCooling */
    0, 
    
    /*[110]: State COOL_STAGE1 base index. */
    /*[110]: Number of inputs. */
    3, 
    
    /*[111]: Number of transitions. */
    2, 
    
    /*[112]: EFSM binary index of IQFN data for state COOL_STAGE1 */
    115, 
    
    /*[113]: EFSM binary index of transition Stage1ToStopCooling data. */
    118, 
    
    /*[114]: EFSM binary index of transition Stage1ToStage2 data. */
    125, 
    
    /*[115]: State COOL_STAGE1 start of IQFN data. */
    /*[115]: IQFN function reference index for prototype IsHvacCallingForCooling */
    1, 
    
    /*[116]: IQFN function reference index for prototype IsBlowerOn */
    6, 
    
    /*[117]: IQFN function reference index for prototype IsStage2Required */
    11, 
    
    /*[118]: Transition Stage1ToStopCooling data. */
    /*[118]: EFSM binary index of transition Stage1ToStopCooling actions data. */
    124, 
    
    /*[119]: Next state after transition. */
    6, 
    
    /*[120]: Number of opcodes. */
    5, 
    
    /*[121]: Opcodes 0 and 1 */
    897, 
    
    /*[122]: Transition Actions data. */
    /*[122]: Opcodes 2 and 3 */
    902, 
    
    /*[123]: Transition Actions data. */
    /*[123]: Opcodes 4 and 5 */
    1, 
    
    /*[124]: Transition Actions data. */
    /*[124]: Number of transition actions for transition Stage1ToStopCooling */
    0, 
    
    /*[125]: Transition Stage1ToStage2 data. */
    /*[125]: EFSM binary index of transition Stage1ToStage2 actions data. */
    129, 
    
    /*[126]: Next state after transition. */
    7, 
    
    /*[127]: Number of opcodes. */
    1, 
    
    /*[128]: Opcodes 0 and 1 */
    139, 
    
    /*[129]: Transition Actions data. */
    /*[129]: Number of transition actions for transition Stage1ToStage2 */
    1, 
    
    /*[130]: Action CoolingEnterStage2 function reference index. */
    3, 
    
    /*[131]: State STOP_COOLING base index. */
    /*[131]: Number of inputs. */
    2, 
    
    /*[132]: Number of transitions. */
    1, 
    
    /*[133]: EFSM binary index of IQFN data for state STOP_COOLING */
    135, 
    
    /*[134]: EFSM binary index of transition StopCoolingToIdle data. */
    137, 
    
    /*[135]: State STOP_COOLING start of IQFN data. */
    /*[135]: IQFN function reference index for prototype IsAllMinOnTimesExpired */
    3, 
    
    /*[136]: IQFN function reference index for prototype IsQuickStopRequired */
    9, 
    
    /*[137]: Transition StopCoolingToIdle data. */
    /*[137]: EFSM binary index of transition StopCoolingToIdle actions data. */
    142, 
    
    /*[138]: Next state after transition. */
    1, 
    
    /*[139]: Number of opcodes. */
    4, 
    
    /*[140]: Opcodes 0 and 1 */
    35203, 
    
    /*[141]: Transition Actions data. */
    /*[141]: Opcodes 2 and 3 */
    259, 
    
    /*[142]: Transition Actions data. */
    /*[142]: Number of transition actions for transition StopCoolingToIdle */
    2, 
    
    /*[143]: Action CoolingExitStop function reference index. */
    6, 
    
    /*[144]: Action CoolingEnterIdle function reference index. */
    0, 
    
    /*[145]: State COOL_STAGE2 base index. */
    /*[145]: Number of inputs. */
    3, 
    
    /*[146]: Number of transitions. */
    2, 
    
    /*[147]: EFSM binary index of IQFN data for state COOL_STAGE2 */
    150, 
    
    /*[148]: EFSM binary index of transition Stage2ToStage3 data. */
    153, 
    
    /*[149]: EFSM binary index of transition Stage2ToStopCooling data. */
    159, 
    
    /*[150]: State COOL_STAGE2 start of IQFN data. */
    /*[150]: IQFN function reference index for prototype IsStage3Required */
    10, 
    
    /*[151]: IQFN function reference index for prototype IsHvacCallingForCooling */
    1, 
    
    /*[152]: IQFN function reference index for prototype IsBlowerOn */
    6, 
    
    /*[153]: Transition Stage2ToStage3 data. */
    /*[153]: EFSM binary index of transition Stage2ToStage3 actions data. */
    157, 
    
    /*[154]: Next state after transition. */
    8, 
    
    /*[155]: Number of opcodes. */
    1, 
    
    /*[156]: Opcodes 0 and 1 */
    138, 
    
    /*[157]: Transition Actions data. */
    /*[157]: Number of transition actions for transition Stage2ToStage3 */
    1, 
    
    /*[158]: Action CoolingEnterStage3 function reference index. */
    4, 
    
    /*[159]: Transition Stage2ToStopCooling data. */
    /*[159]: EFSM binary index of transition Stage2ToStopCooling actions data. */
    165, 
    
    /*[160]: Next state after transition. */
    6, 
    
    /*[161]: Number of opcodes. */
    5, 
    
    /*[162]: Opcodes 0 and 1 */
    897, 
    
    /*[163]: Transition Actions data. */
    /*[163]: Opcodes 2 and 3 */
    902, 
    
    /*[164]: Transition Actions data. */
    /*[164]: Opcodes 4 and 5 */
    1, 
    
    /*[165]: Transition Actions data. */
    /*[165]: Number of transition actions for transition Stage2ToStopCooling */
    0, 
    
    /*[166]: State COOL_STAGE3 base index. */
    /*[166]: Number of inputs. */
    2, 
    
    /*[167]: Number of transitions. */
    1, 
    
    /*[168]: EFSM binary index of IQFN data for state COOL_STAGE3 */
    170, 
    
    /*[169]: EFSM binary index of transition Stage3ToStopCooling data. */
    172, 
    
    /*[170]: State COOL_STAGE3 start of IQFN data. */
    /*[170]: IQFN function reference index for prototype IsHvacCallingForCooling */
    1, 
    
    /*[171]: IQFN function reference index for prototype IsBlowerOn */
    6, 
    
    /*[172]: Transition Stage3ToStopCooling data. */
    /*[172]: EFSM binary index of transition Stage3ToStopCooling actions data. */
    178, 
    
    /*[173]: Next state after transition. */
    6, 
    
    /*[174]: Number of opcodes. */
    5, 
    
    /*[175]: Opcodes 0 and 1 */
    897, 
    
    /*[176]: Transition Actions data. */
    /*[176]: Opcodes 2 and 3 */
    902, 
    
    /*[177]: Transition Actions data. */
    /*[177]: Opcodes 4 and 5 */
    1, 
    
    /*[178]: Transition Actions data. */
    /*[178]: Number of transition actions for transition Stage3ToStopCooling */
    0 
    
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
    Heating_DF_OutputActions[4] = &EFSM_Heating_DF_HeatEnterStopDf;
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
    
    /*Instance initializations.*/
    EFSM_InitializeInstance(&HVAC_DF_Instance_0, &HVAC_DF_Binary, HVAC_DF_OutputActions, HVAC_DF_Inputs, 0);
    EFSM_InitializeInstance(&Blower_DF_Instance_0, &Blower_DF_Binary, Blower_DF_OutputActions, Blower_DF_Inputs, 0);
    EFSM_InitializeInstance(&Heating_DF_Instance_0, &Heating_DF_Binary, Heating_DF_OutputActions, Heating_DF_Inputs, 0);
    EFSM_InitializeInstance(&Cooling_DF_Instance_0, &Cooling_DF_Binary, Cooling_DF_OutputActions, Cooling_DF_Inputs, 0);
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
void EFSM_Heating_DF_HeatEnterStopDf(uint8_t indexOnEfsmType)
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
