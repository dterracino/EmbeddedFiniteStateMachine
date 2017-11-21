#include "efsm6.h"
#include <stdint.h>
#include "efsm_core.h"

uint8_t (*efsm6_Inputs[EFSM_EFSM6_NUMBER_OF_INPUTS])();
void (*efsm6_OutputActions[EFSM_EFSM6_NUMBER_OF_OUTPUTS])();

/*Input query functions.*/

uint8_t EFSM_efsm6_InputA()
{
    printf("InputA");
    return 0;
}

uint8_t EFSM_efsm6_InputB()
{
    printf("InputB");
    return 0;
}

uint8_t EFSM_efsm6_InputC()
{
    printf("InputC");
    return 0;
}

uint8_t EFSM_efsm6_InputD()
{
    printf("InputD");
    return 0;
}

uint8_t EFSM_efsm6_InputE()
{
    printf("InputE");
    return 0;
}

/*Output action functions.*/

void EFSM_efsm6_ActionA()
{
    printf("Executing ActionA");
}

void EFSM_efsm6_ActionB()
{
    printf("Executing ActionB");
}

void EFSM_efsm6_ActionC()
{
    printf("Executing ActionC");
}

void EFSM_efsm6_ActionD()
{
    printf("Executing ActionD");
}

void EFSM_efsm6_ActionE()
{
    printf("Executing ActionE");
}

void EFSM_efsm6_ActionF()
{
    printf("Executing ActionF");
}

void EFSM_efsm6_ActionG()
{
    printf("Executing ActionG");
}

void EFSM_efsm6_ActionH()
{
    printf("Executing ActionH");
}

void EFSM_efsm6_ActionI()
{
    printf("Executing ActionI");
}

void EFSM_efsm6_ActionJ()
{
    printf("Executing ActionJ");
}


/* efsm6 */
uint16_t efsm_efsm6_binaryData[] = {

    /*[0]: Number of states */
    3, 
    
    /*[1]: Total number of inputs. */
    5, 
    
    /*[2]: Initial state identifier. */
    0, 
    
    /*[3]: EFSM binary index of state State0 */
    6, 
    
    /*[4]: EFSM binary index of state State 1 */
    39, 
    
    /*[5]: EFSM binary index of state State 2 */
    59, 
    
    /*[6]: State State0 base index. */
    /*[6]: Number of inputs. */
    4, 
    
    /*[7]: Number of transitions. */
    2, 
    
    /*[8]: EFSM binary index of IQFN data for state State0 */
    11, 
    
    /*[9]: EFSM binary index of transition Transition 0 data. */
    15, 
    
    /*[10]: EFSM binary index of transition Transition 1 data. */
    27, 
    
    /*[11]: State State0 start of IQFN data. */
    /*[11]: IQFN function reference index for prototype InputA */
    0, 
    
    /*[12]: IQFN function reference index for prototype InputB */
    1, 
    
    /*[13]: IQFN function reference index for prototype InputC */
    2, 
    
    /*[14]: IQFN function reference index for prototype InputD */
    3, 
    
    /*[15]: Transition Transition 0 data. */
    /*[15]: EFSM binary index of transition Transition 0 actions data. */
    20, 
    
    /*[16]: Next state after transition. */
    1, 
    
    /*[17]: Number of opcodes. */
    3, 
    
    /*[18]: Opcodes 0 and 1 */
    33152, 
    
    /*[19]: Transition Actions data. */
    /*[19]: Opcodes 2 and 3 */
    2, 
    
    /*[20]: Transition Actions data. */
    /*[20]: Number of transition actions for transition Transition 0 */
    6, 
    
    /*[21]: Action ActionB function reference index. */
    1, 
    
    /*[22]: Action ActionA function reference index. */
    0, 
    
    /*[23]: Action ActionB function reference index. */
    1, 
    
    /*[24]: Action ActionF function reference index. */
    5, 
    
    /*[25]: Action ActionH function reference index. */
    7, 
    
    /*[26]: Action ActionA function reference index. */
    0, 
    
    /*[27]: Transition Transition 1 data. */
    /*[27]: EFSM binary index of transition Transition 1 actions data. */
    33, 
    
    /*[28]: Next state after transition. */
    2, 
    
    /*[29]: Number of opcodes. */
    6, 
    
    /*[30]: Opcodes 0 and 1 */
    896, 
    
    /*[31]: Transition Actions data. */
    /*[31]: Opcodes 2 and 3 */
    33666, 
    
    /*[32]: Transition Actions data. */
    /*[32]: Opcodes 4 and 5 */
    258, 
    
    /*[33]: Transition Actions data. */
    /*[33]: Number of transition actions for transition Transition 1 */
    5, 
    
    /*[34]: Action ActionB function reference index. */
    1, 
    
    /*[35]: Action ActionE function reference index. */
    4, 
    
    /*[36]: Action ActionG function reference index. */
    6, 
    
    /*[37]: Action ActionJ function reference index. */
    9, 
    
    /*[38]: Action ActionB function reference index. */
    1, 
    
    /*[39]: State State 1 base index. */
    /*[39]: Number of inputs. */
    3, 
    
    /*[40]: Number of transitions. */
    1, 
    
    /*[41]: EFSM binary index of IQFN data for state State 1 */
    43, 
    
    /*[42]: EFSM binary index of transition Transition 2 data. */
    46, 
    
    /*[43]: State State 1 start of IQFN data. */
    /*[43]: IQFN function reference index for prototype InputD */
    3, 
    
    /*[44]: IQFN function reference index for prototype InputB */
    1, 
    
    /*[45]: IQFN function reference index for prototype InputE */
    4, 
    
    /*[46]: Transition Transition 2 data. */
    /*[46]: EFSM binary index of transition Transition 2 actions data. */
    52, 
    
    /*[47]: Next state after transition. */
    2, 
    
    /*[48]: Number of opcodes. */
    5, 
    
    /*[49]: Opcodes 0 and 1 */
    33155, 
    
    /*[50]: Transition Actions data. */
    /*[50]: Opcodes 2 and 3 */
    33793, 
    
    /*[51]: Transition Actions data. */
    /*[51]: Opcodes 4 and 5 */
    1, 
    
    /*[52]: Transition Actions data. */
    /*[52]: Number of transition actions for transition Transition 2 */
    6, 
    
    /*[53]: Action ActionD function reference index. */
    3, 
    
    /*[54]: Action ActionB function reference index. */
    1, 
    
    /*[55]: Action ActionF function reference index. */
    5, 
    
    /*[56]: Action ActionI function reference index. */
    8, 
    
    /*[57]: Action ActionJ function reference index. */
    9, 
    
    /*[58]: Action ActionB function reference index. */
    1, 
    
    /*[59]: State State 2 base index. */
    /*[59]: Number of inputs. */
    2, 
    
    /*[60]: Number of transitions. */
    1, 
    
    /*[61]: EFSM binary index of IQFN data for state State 2 */
    63, 
    
    /*[62]: EFSM binary index of transition Transition 3 data. */
    65, 
    
    /*[63]: State State 2 start of IQFN data. */
    /*[63]: IQFN function reference index for prototype InputB */
    1, 
    
    /*[64]: IQFN function reference index for prototype InputE */
    4, 
    
    /*[65]: Transition Transition 3 data. */
    /*[65]: EFSM binary index of transition Transition 3 actions data. */
    70, 
    
    /*[66]: Next state after transition. */
    0, 
    
    /*[67]: Number of opcodes. */
    4, 
    
    /*[68]: Opcodes 0 and 1 */
    33921, 
    
    /*[69]: Transition Actions data. */
    /*[69]: Opcodes 2 and 3 */
    770, 
    
    /*[70]: Transition Actions data. */
    /*[70]: Number of transition actions for transition Transition 3 */
    4, 
    
    /*[71]: Action ActionC function reference index. */
    2, 
    
    /*[72]: Action ActionB function reference index. */
    1, 
    
    /*[73]: Action ActionA function reference index. */
    0, 
    
    /*[74]: Action ActionC function reference index. */
    2 
    
};

EFSM_BINARY efsm6Binary;

void EFSM_efsm6_Init()
{

    /*Associate the raw binary with the binary container.*/
    efsm6Binary.id = 43981;
    efsm6Binary.data = efsm_efsm6_binaryData;
    
    efsm6_Inputs[0] = &EFSM_efsm6_InputA;
    efsm6_Inputs[1] = &EFSM_efsm6_InputB;
    efsm6_Inputs[2] = &EFSM_efsm6_InputC;
    efsm6_Inputs[3] = &EFSM_efsm6_InputD;
    efsm6_Inputs[4] = &EFSM_efsm6_InputE;
    
    efsm6_OutputActions[0] = &EFSM_efsm6_ActionA;
    efsm6_OutputActions[1] = &EFSM_efsm6_ActionB;
    efsm6_OutputActions[2] = &EFSM_efsm6_ActionC;
    efsm6_OutputActions[3] = &EFSM_efsm6_ActionD;
    efsm6_OutputActions[4] = &EFSM_efsm6_ActionE;
    efsm6_OutputActions[5] = &EFSM_efsm6_ActionF;
    efsm6_OutputActions[6] = &EFSM_efsm6_ActionG;
    efsm6_OutputActions[7] = &EFSM_efsm6_ActionH;
    efsm6_OutputActions[8] = &EFSM_efsm6_ActionI;
    efsm6_OutputActions[9] = &EFSM_efsm6_ActionJ;
}

