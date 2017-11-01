#ifndef EVAL_H
#define EVAL_H
#include <stdint.h>
#include "efsm_core.h"

#define I_BUF_SIZE		30

uint8_t DoesStringIndicateAnInteger(char * str);
void EvalInterface(EFSM_INSTANCE * efsmInstance);
void DisplayInstanceData(EFSM_INSTANCE * efsmInstance);

#endif
