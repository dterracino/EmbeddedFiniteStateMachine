#include "fanControlEfsm.h"
#include <stdint.h>
#include "efsm_core.h"
#include "test.h"

uint8_t (*FanController_Inputs[EFSM_FANCONTROLLER_NUMBER_OF_INPUTS])();
void (*FanController_OutputActions[EFSM_FANCONTROLLER_NUMBER_OF_OUTPUTS])();

/*Input query functions.*/

uint8_t EFSM_FanController_IsTempAboveThreshold()
{
    printf("IsTempAboveThreshold");
    return InputValues[0];
}

uint8_t EFSM_FanController_IsTimerExpired()
{
    printf("IsTimerExpired");
    return InputValues[1];
}

/*Output action functions.*/

void EFSM_FanController_StartTimer()
{
    printf("Executing StartTimer");
}

void EFSM_FanController_TurnFanOn()
{
    printf("Executing TurnFanOn");
}

void EFSM_FanController_TurnFanOff()
{
    printf("Executing TurnFanOff");
}

void EFSM_FanController_ResetTimer()
{
    printf("Executing ResetTimer");
}


/* FanController */
uint16_t efsm_FanController_binaryData[] = {

    /*[0]: Number of states */
    2, 
    
    /*[1]: Total number of inputs. */
    2, 
    
    /*[2]: Initial state identifier. */
    0, 
    
    /*[3]: EFSM binary index of state Fan Off */
    5, 
    
    /*[4]: EFSM binary index of state Fan On */
    17, 
    
    /*[5]: State Fan Off base index. */
    /*[5]: Number of inputs. */
    1, 
    
    /*[6]: Number of transitions. */
    1, 
    
    /*[7]: EFSM binary index of IQFN data for state Fan Off */
    9, 
    
    /*[8]: EFSM binary index of transition Transition 1 data. */
    10, 
    
    /*[9]: State Fan Off start of IQFN data. */
    /*[9]: IQFN function reference index for prototype IsTempAboveThreshold */
    0, 
    
    /*[10]: Transition Transition 1 data. */
    /*[10]: EFSM binary index of transition Transition 1 actions data. */
    14, 
    
    /*[11]: Next state after transition. */
    1, 
    
    /*[12]: Number of opcodes. */
    1, 
    
    /*[13]: Opcodes 0 and 1 */
    128, 
    
    /*[14]: Transition Actions data. */
    /*[14]: Number of transition actions for transition Transition 1 */
    2, 
    
    /*[15]: Action TurnFanOn function reference index. */
    1, 
    
    /*[16]: Action StartTimer function reference index. */
    0, 
    
    /*[17]: State Fan On base index. */
    /*[17]: Number of inputs. */
    2, 
    
    /*[18]: Number of transitions. */
    1, 
    
    /*[19]: EFSM binary index of IQFN data for state Fan On */
    21, 
    
    /*[20]: EFSM binary index of transition Transition 2 data. */
    23, 
    
    /*[21]: State Fan On start of IQFN data. */
    /*[21]: IQFN function reference index for prototype IsTempAboveThreshold */
    0, 
    
    /*[22]: IQFN function reference index for prototype IsTimerExpired */
    1, 
    
    /*[23]: Transition Transition 2 data. */
    /*[23]: EFSM binary index of transition Transition 2 actions data. */
    28, 
    
    /*[24]: Next state after transition. */
    0, 
    
    /*[25]: Number of opcodes. */
    4, 
    
    /*[26]: Opcodes 0 and 1 */
    896, 
    
    /*[27]: Transition Actions data. */
    /*[27]: Opcodes 2 and 3 */
    641, 
    
    /*[28]: Transition Actions data. */
    /*[28]: Number of transition actions for transition Transition 2 */
    2, 
    
    /*[29]: Action ResetTimer function reference index. */
    3, 
    
    /*[30]: Action TurnFanOff function reference index. */
    2 
    
};

EFSM_BINARY FanControllerBinary;

void EFSM_FanController_Init()
{

    /*Associate the raw binary with the binary container.*/
    FanControllerBinary.id = 43981;
    FanControllerBinary.data = efsm_FanController_binaryData;
    
    FanController_Inputs[0] = &EFSM_FanController_IsTempAboveThreshold;
    FanController_Inputs[1] = &EFSM_FanController_IsTimerExpired;
    
    FanController_OutputActions[0] = &EFSM_FanController_StartTimer;
    FanController_OutputActions[1] = &EFSM_FanController_TurnFanOn;
    FanController_OutputActions[2] = &EFSM_FanController_TurnFanOff;
    FanController_OutputActions[3] = &EFSM_FanController_ResetTimer;
}

