#include "eval.h"
#include <stdint.h>
#include "efsm_core.h"
#include <stdio.h>

void DisplayInstanceData(EFSM_INSTANCE * efsmInstance)
{
	printf("\nGeneral Information\n\n");
	printf("Associated binary: %d\n", efsmInstance->efsmBinary->id);
	printf("numberOfStates: %d\n", efsmInstance->numberOfStates);
	printf("totalNumberOfInputs: %d\n", efsmInstance->totalNumberOfInputs);

	printf("\nState Specific Information\n\n");

	printf("state: %d\n", efsmInstance->state);
	printf("numberOfInputs: %d\n", efsmInstance->numberOfInputs);
	printf("numberOfTransitions: %d\n", efsmInstance->numberOfTransitions);
	printf("baseIndexCurrentState: %d\n", efsmInstance->baseIndexCurrentState);
	printf("baseIndexStateHeader: %d\n", efsmInstance->baseIndexStateHeader);
	printf("baseIndexIqfnData: %d\n", efsmInstance->baseIndexIqfnData);
}

uint8_t DoesStringIndicateAnInteger(char * str)
{
	uint8_t len = strlen(str);

	for (uint8_t i = 0; i < len; i++)
	{
		if ((str[i] < '0') || (str[i] > '9'))
			return 0;
	}

	return 1;
}

char * GetTrueOrFalseString(uint8_t value)
{
	if (value)
		return "true";

	return "false";
}

void SetInput(EFSM_INSTANCE * efsmInstance, uint8_t efsmInputNumber, uint8_t value)
{
	printf("Setting input %d to %s\n", efsmInputNumber, GetTrueOrFalseString(value));
}

void DisplayEfsmCondtion(EFSM_INSTANCE * efsmInstance)
{
	printf("current state: %d\n", efsmInstance->state);
}

void EvalInterface(EFSM_INSTANCE * efsmInstance)
{
	char iBuf[I_BUF_SIZE];	
	uint8_t exit = 0;	
	char * token;
	uint8_t index = 0;
	uint8_t efsmInputNumber = 0;

	printf("\nEFSM Evaluation.");

	while (!exit)
	{
		printf("\n\nEnter command.\n\n");
		while (!scanf(" %[^\n]s", iBuf));
		token = strtok(iBuf, " ");
	
		if (!strcmp(token, "input"))
		{
			index += (strlen(token) + 1);
			token = strtok(&iBuf[index], ":");

			if (DoesStringIndicateAnInteger(token))
			{
				efsmInputNumber = atoi(token);
				index += (strlen(token) + 1);

				if (iBuf[index] == 'T')
				{
					SetInput(efsmInstance, efsmInputNumber, 1);
					DisplayEfsmCondtion(efsmInstance);
					index = 0;
				}
				else if (iBuf[index] == 'F')
				{
					SetInput(efsmInstance, efsmInputNumber, 0);
					DisplayEfsmCondtion(efsmInstance);
					index = 0;
				}
				else
					printf("expected T or F");
			}
			else
				printf("expected integer. got %s", token);
		}
		else if (!strcmp(iBuf, "condition"))		
			DisplayEfsmCondtion(efsmInstance);		
		else if (!strcmp(iBuf, "instance"))		
			DisplayInstanceData(efsmInstance);					
		else if (!strcmp(iBuf, "exit"))
		{
			printf("Exiting");
			exit = 1;
		}	
		else if (!strcmp(iBuf, "init"))
		{
			printf("Initializing efsm process");			
			EFSM_InitializeProcess();
		}
		else if (!strcmp(iBuf, ">>"))
		{
			printf("Running state machine...\n");
			EFSM_Process();
		}
		
	}	
}