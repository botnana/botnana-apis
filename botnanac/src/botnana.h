#ifndef __BOTNANA_H__
#define __BOTNANA_H__

#ifdef __cplusplus
extern "C" {
#endif

#include <stdint.h>

// server descriptor
struct Botnana;

// Library Version
//
// return : Library Version
const char * library_version();

// Rust Library Version
//
// return : Library Version
const char * rust_library_version();

// New Botnana motion server descriptor
// ip: motion server IP address
struct Botnana * botnana_new(const char * ip);

// Clone Botnana motion server descriptor
// desc : motion server descriptor
struct Botnana * botnana_clone(struct Botnana * desc);

// connect to motion server
// desc: IP of motion server
void botnana_connect(struct Botnana * desc);

// Disconnect
// desc : motion server descriptor
void botnana_disconnect(struct Botnana * desc);

// Set IP
// desc : motion server descriptor
// ip   : IP of motion server
//
// return : IP of motion server
const char * botnana_set_ip(struct Botnana * desc, const char * ip);

// Set Port
// desc : motion server descriptor
// port   : Port of motion server
//
// return : Port of motion server
uint16_t botnana_set_port(struct Botnana * desc, uint16_t port);

// URL of motion server
// desc : motion server descriptor
//
// return : URl of motion server
const char * botnana_url(struct Botnana * desc);

// Set on_open callback function
// desc: motion server descriptor
// cb  : on_open callback function
void botnana_set_on_open_cb(struct Botnana * desc,void * ptr,
                            void (* cb)(void * ptr, const char * str));


// Set on_error callback function
// desc: motion server descriptor
// cb  : on_error callback function
void botnana_set_on_error_cb(struct Botnana * desc,void * ptr,
                             void (* cb)(void * ptr, const char * str));


// Send raw message
void botnana_send_message(struct Botnana * desc,
                          const char * msg);

// Set tag callback function
// desc  : motion server descriptor
// tag   : tag
// count : called times, 0 as always
// cb    : handle corresponding valve function
int32_t botnana_set_tag_cb (struct Botnana * desc,
                            const char * tag,
                            uint32_t count,
                            void * ptr,
                            void (* cb)(void * ptr, const char * str));


// Set tag callback function
// desc  : motion server descriptor
// tag   : tag
// count : called times, 0 as always
// cb    : handle corresponding valve function
int32_t botnana_set_tagname_cb (struct Botnana * desc,
                                const char * tag,
                                uint32_t count,
                                void * ptr,
                                void (* cb)(void * ptr, uint32_t position, uint32_t channel, const char * str));


// Set on_message callback function
// desc: motion server descriptor
// cb  : on_message callback function
void botnana_set_on_message_cb(struct Botnana * desc,void * ptr,
                               void (* cb)(void * ptr, const char * str));


// Set on_send callback function
// desc: server descriptor
// cb:   on_send callback function
void botnana_set_on_send_cb(struct Botnana * desc,void * ptr,
                            void (* cb)(void * ptr, const char * str));


// Send script to command buffer
//
// desc:   server descriptor
// script: real time script
void send_script_to_buffer(struct Botnana * desc,
                           const char * script);

// Flush scripts buffer
//
// desc:   server descriptor
void flush_scripts_buffer(struct Botnana * desc);

// Set scripts pop count
//
// desc:  server descriptor
// count: command count
void set_scripts_pop_count(struct Botnana * desc,
                           uint32_t count);

// Set poll interval
//
// desc:  server descriptor
// interval: poll interval [ms]
void set_poll_interval_ms(struct Botnana * desc,
                          uint64_t interval);


//****** Json API ********/

// motion evaluate
//
// desc:   server descriptor
// script: real time script

int32_t script_evaluate(struct Botnana * desc,
                        const char * script);

// JSON-API: motion.poll
//
// desc:   server descriptor
void motion_poll(struct Botnana * desc);

// JSON-API: version.get
//
// desc: server descriptor
void version_get(struct Botnana * desc);

// JSON-API: config.slave.get
//
// desc:     server descriptor
// position: slave position, start by 1
// channel:  device channel, start by 1
void config_slave_get(struct Botnana * desc,
                      uint32_t alias,
                      uint32_t position,
                      uint32_t channel);

// Configuration changes are made through the Botnana Control HMI.

// JSON-API: config.motion.get
// botnana: server descriptor
void config_motion_get(struct Botnana * botnana);

// JSON-API: config.group.get
// botnana: server descriptor
// position: group index
void config_group_get(struct Botnana * botnana,
                      uint32_t position);

// JSON-API: config.axis.get
// botnana: Botnana motion server descriptor
// position: axis index
void config_axis_get(struct Botnana * botnana,
                     uint32_t position);

// Poweroff
//
// desc:     server descriptor
void poweroff(struct Botnana * desc);

// Reboot
//
// desc:     server descriptor
void reboot(struct Botnana * desc);

// program descriptor
struct Program;

// new program
struct Program * program_new (const char * name);

// push real time script to program
void program_line(struct Program * pm, const char * cmd);

// clear program
void program_clear(struct Program * pm);

// deploy program
void program_deploy(struct Botnana * desc, struct Program * pm);

// run program
//
// desc: server descriptor
// pm:   program descriptor
void program_run(struct Botnana * desc, struct Program * pm);

// abort current background program
// desc: server descriptor
void botnana_abort_program (struct Botnana * desc);

#ifdef __cplusplus
}
#endif

#endif
