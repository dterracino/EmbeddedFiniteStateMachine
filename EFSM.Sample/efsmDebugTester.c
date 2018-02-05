#include "efsmDebugTester.h"
#include <stdint.h>
#include "efsm_core.h"

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

-Binary Structure Declaration (variable of type EFSM_BINARY)
-State Machine Instance Declaration (variable of type EFSM_INSTANCE)
-Arrays for Function Pointers
-EFSM State Machine Binary Array (an array of type uint16_t)
-Initialization Function
-EFSM Process Initialization Function (the only function in this file that should be called
 from user code)
/*
----------------------------------------------------------------------------------------------------
Binary Structure Declaration.

Note:
Reference to a set of binary instructions (an array of 16 bit integers) is 
"wrapped" in a corresponding structure of type EFSM_BINARY. In turn, it is the initialized
instance of an EFSM_BINARY variable which is used by the EFSM execution mechanism. The 
typedef for the EFSM_BINARY struct may be found in efsm_core.h.
*/
EFSM_BINARY EFSMDebugTester_Binary;
/*
----------------------------------------------------------------------------------------------------
State Machine Instance
*/
/*EFSMDebugTester Instances*/
EFSM_INSTANCE EFSMDebugTester_Instance_0;

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

/*State Machine Definition "EFSMDebugTester"*/

/*Array for pointers to input query functions.*/
uint8_t (*EFSMDebugTester_Inputs[EFSM_EFSMDEBUGTESTER_NUMBER_OF_INPUTS])(uint8_t indexOnEfsmType);

/*Array for pointers to action functions.*/
void (*EFSMDebugTester_OutputActions[EFSM_EFSMDEBUGTESTER_NUMBER_OF_OUTPUTS])(uint8_t indexOnEfsmType);
/*
----------------------------------------------------------------------------------------------------
EFSM Definition Binary Array
*/

/* EFSMDebugTester Definition*/
uint16_t efsm_EFSMDebugTester_binaryData[] = {

    /*[0]: Number of states */
    2, 
    
    /*[1]: Total number of inputs. */
    2, 
    
    /*[2]: Initial state identifier. */
    0, 
    
    /*[3]: EFSM binary index of state Initial State */
    5, 
    
    /*[4]: EFSM binary index of state State 1 */
    16, 
    
    /*[5]: State Initial State base index. */
    /*[5]: Number of inputs. */
    1, 
    
    /*[6]: Number of transitions. */
    1, 
    
    /*[7]: EFSM binary index of IQFN data for state Initial State */
    9, 
    
    /*[8]: EFSM binary index of transition Transition 1 data. */
    10, 
    
    /*[9]: State Initial State start of IQFN data. */
    /*[9]: IQFN function reference index for prototype InputA */
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
    1, 
    
    /*[15]: Action ActionA function reference index. */
    0, 
    
    /*[16]: State State 1 base index. */
    /*[16]: Number of inputs. */
    1, 
    
    /*[17]: Number of transitions. */
    1, 
    
    /*[18]: EFSM binary index of IQFN data for state State 1 */
    20, 
    
    /*[19]: EFSM binary index of transition Transition 2 data. */
    21, 
    
    /*[20]: State State 1 start of IQFN data. */
    /*[20]: IQFN function reference index for prototype InputB */
    1, 
    
    /*[21]: Transition Transition 2 data. */
    /*[21]: EFSM binary index of transition Transition 2 actions data. */
    25, 
    
    /*[22]: Next state after transition. */
    0, 
    
    /*[23]: Number of opcodes. */
    1, 
    
    /*[24]: Opcodes 0 and 1 */
    129, 
    
    /*[25]: Transition Actions data. */
    /*[25]: Number of transition actions for transition Transition 2 */
    1, 
    
    /*[26]: Action ActionB function reference index. */
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
void EFSM_efsmDebugTester_Init()
{
    /*efsmInstanceArray initialization.*/
    
    efsmInstanceArray[0] = &EFSMDebugTester_Instance_0;
    
    /*State Machine Definition "EFSMDebugTester" Initialization*/
    
    /*Associate an array of binary instructions with an EFSM_BINARY structure.*/
    EFSMDebugTester_Binary.data = efsm_EFSMDebugTester_binaryData;
    
    /*Initialize the input functions array.*/
    EFSMDebugTester_Inputs[0] = &EFSM_EFSMDebugTester_InputA;
    EFSMDebugTester_Inputs[1] = &EFSM_EFSMDebugTester_InputB;
    
    /*Initialize the output functions array.*/
    EFSMDebugTester_OutputActions[0] = &EFSM_EFSMDebugTester_ActionA;
    EFSMDebugTester_OutputActions[1] = &EFSM_EFSMDebugTester_ActionB;
    
    /*Instance initializations.*/
    EFSM_InitializeInstance(&EFSMDebugTester_Instance_0, &EFSMDebugTester_Binary, EFSMDebugTester_OutputActions, EFSMDebugTester_Inputs, 0);
}

/*
----------------------------------------------------------------------------------------------------
EFSM Process Initialization.
*/

void EFSM_InitializeProcess()
{
    EFSM_efsmDebugTester_Init();
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

uint32_t Get_EFSMDebugTester_Instance_0_State()
{
    return EFSMDebugTester_Instance_0.state;
}

/*State Machine Input Accessors*/

/*Corresponds to input "InputA" for instance 0 of state machine definition "EFSMDebugTester".*/
uint32_t Get_EFSMDebugTester_0_Input_0()
{
    return EFSMDebugTester_Instance_0.inputBuffer[0];
}

/*Corresponds to input "InputB" for instance 0 of state machine definition "EFSMDebugTester".*/
uint32_t Get_EFSMDebugTester_0_Input_1()
{
    return EFSMDebugTester_Instance_0.inputBuffer[1];
}

