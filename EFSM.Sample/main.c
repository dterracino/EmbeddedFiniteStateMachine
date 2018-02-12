#include "stdio.h"
#include "efsm_core.h"
#include "test.h"
#include "eval.h"
#include <stdlib.h>

int main(int argc, char *argv[])
{
	printf("Starting the EFSM Debug Manager...\n\n");

	strcpy(debugStatusTxFilename, argv[1]);
	strcpy(debugCommandRxFilename, argv[2]);

	while (1)
	{		
		EfsmDebugManager();
	}

	return 0;
}

