#ifndef __BOTNANA_H__
#define __BOTNANA_H__

#include <stdint.h>

struct Botnana;

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

//abort program
void botnana_abort_program (struct Botnana * desc);


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

//set motion config
void botnana_set_motion_config(struct Botnana * desc, uint32_t perios_us, uint32_t group_capacity, uint32_t axis_capacity);

void botnana_set_motion_config_period_us(struct Botnana * desc, uint32_t perios_us);

void botnana_set_motion_config_group_capacity(struct Botnana * desc, uint32_t group_capacity);

void botnana_set_motion_config_axis_capacity(struct Botnana * desc, uint32_t axis_capacity);

void botnana_get_motion_config(struct Botnana * desc);


//set group config
void botnana_set_group_config(
    struct Botnana * desc,
    uint32_t position,
    const char * name,
    const char * gtype,
    const char * mapping,
    double vmax,
    double amax,
    double jmax);

//set group config name
void botnana_set_group_config_name(struct Botnana * desc, uint32_t position, const char * name);

//set group config gtype
void botnana_set_group_config_gtype(struct Botnana * desc, uint32_t position, const char * gtype, const char * mapping);

//set group config vmax
void botnana_set_group_config_vmax(struct Botnana * desc, uint32_t position, double vmax);

//set group config amax
void botnana_set_group_config_amax(struct Botnana * desc, uint32_t position, double amax);

//set group config jmax
void botnana_set_group_config_jmax(struct Botnana * desc, uint32_t position, double jmax);




// save configuration
void botnana_save_configure(struct Botnana * desc);

#endif
