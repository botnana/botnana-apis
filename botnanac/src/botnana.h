#ifndef __BOTNANA_H__
#define __BOTNANA_H__

struct Botnana;

struct Program;

void hello_botnana ();

struct Botnana * connect_to_botnana(const char * address, void (* fn)(const char * str));

void attach_function_to_event(struct Botnana * desc, const char * event, unsigned int count, void  (* fn)(const char * str));


struct Program * new_program(const char * name);

#endif
