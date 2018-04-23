#ifndef __BOTNANA_H__
#define __BOTNANA_H__

#include <stdint.h>

struct Botnana;

struct Program;

struct Botnana * botnana_connect(const char * address, void (* fn)(const char * str));

void botnana_event_attach (struct Botnana * desc, const char * address, uint32_t count, void (* fn)(const char * str));


struct Program * botnana_new_program(const char * name);


// Json API

// motion_evaluate
void botnana_motion_evaluate(struct Botnana * desc, const char * script);


#endif
