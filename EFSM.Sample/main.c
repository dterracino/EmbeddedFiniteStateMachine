#include "efsm_generated.h"
#include "stdio.h"
#include "efsm_core.h"
#include "debug.h"
#include "eval.h"
#include <stdlib.h>



int main()
{
	char inputBuffer[50];
	char * token;
	uint8_t len = 0;
	uint8_t index = 0;
	
	//Test0Init();
	//EFSM_InitializeInstance(&efsm0, &test0Binary, Test0Actions, Test0Inputs);
	//DisplayInstanceData(&efsm0);

	EvalInterface(&efsm0);
	return 0;
}

