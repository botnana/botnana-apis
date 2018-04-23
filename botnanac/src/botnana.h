#ifndef __BOTNANA_H__
#define __BOTNANA_H__

#include <stdint.h>

struct Botnana;

struct Program;

// connect
struct Botnana * botnana_connect(const char * address, void (* fn)(const char * str));

// attach event
void botnana_attach_event (struct Botnana * desc, const char * address, uint32_t count, void (* fn)(const char * str));


//enable debug
void botnana_enable_debug(struct Botnana * desc);

// disable debug
void botnana_disable_debug(struct Botnana * desc);

// empty
void botnana_empty(struct Botnana * desc);


// Json API

// motion_evaluate
void botnana_motion_evaluate(struct Botnana * desc, const char * script);

// get version
void botnana_get_version(struct Botnana * desc);

// get slave
void botnana_get_slave(struct Botnana * desc, uint32_t position);

// set slave
void botnana_set_slave(struct Botnana * desc, uint32_t position, const char * tag, int32_t value);

// get slave diff.
void botnana_get_slave_diff(struct Botnana * desc, uint32_t position);

// set slave config
void botnana_set_slave_config(struct Botnana * desc, uint32_t position, const char * tag, int32_t value);

// save configuration
void botnana_save_configure(struct Botnana * desc);


// Program

// new program
struct Program * botnana_new_program (const char * name);

// push program line
void botnana_push_program_line(struct Program * pm, const char * cmd);

// deploy program
void botnana_deploy_program(struct Botnana * desc, struct Program * pm);

// run program
void botnana_run_program(struct Botnana * desc, struct Program * pm);


//abort program
void botnana_abort_program (struct Botnana * desc);



#endif
