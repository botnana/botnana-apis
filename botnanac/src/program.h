#ifndef __PROGRAM_H__
#define __PROGRAM_H__

#include <stdint.h>
#include "botnana.h"

struct Program;

// new program
struct Program * program_new (const char * name);

// push line
void program_push_line(struct Program * pm, const char * cmd);

// deploy
void program_deploy(struct Botnana * desc, struct Program * pm);

// run
void program_run(struct Botnana * desc, struct Program * pm);

// hm
void program_push_hm (struct Program * pm,  uint32_t position);

// pp
void program_push_pp (struct Program * pm,  uint32_t position);

// csp
void program_push_csp (struct Program * pm,  uint32_t position);

// go
void program_push_go (struct Program * pm,  uint32_t position);

// target position
void program_push_target_p (struct Program * pm,  uint32_t position, int32_t target);

// reset fault
void program_push_reset_fault (struct Program * pm,  uint32_t position);

#endif
