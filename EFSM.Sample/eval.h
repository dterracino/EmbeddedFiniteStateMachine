#ifndef EVAL_H
#define EVAL_H
#include <stdint.h>
#include "efsm_core.h"

#define I_BUF_SIZE		30

uint8_t DoesStringIndicateAnInteger(char * str);
char * GetTrueOrFalseString(uint8_t value);
void EvalInterface(EFSM_INSTANCE * efsmInstance);

#endif
