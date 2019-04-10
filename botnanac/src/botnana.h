#ifndef __BOTNANA_H__
#define __BOTNANA_H__

#ifdef __cplusplus
extern "C" {
#endif

#include <stdint.h>

// server descriptor
struct Botnana;

// New Botnana motion server descriptor
// ip: motion server IP address
struct Botnana * botnana_new(const char * ip);

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
// return : URl of motion server
const char * botnana_set_ip(struct Botnana * desc, const char * ip);

// URL of motion server
// desc : motion server descriptor
//
// return : URl of motion server
const char * botnana_url(struct Botnana * desc);

// Set on_open callback function
// desc: motion server descriptor
// cb  : on_open callback function
void botnana_set_on_open_cb(struct Botnana * desc,
                            void (* cb)(const char * str));


// Set on_error callback function
// desc: motion server descriptor
// cb  : on_error callback function
void botnana_set_on_error_cb(struct Botnana * desc,
                             void (* cb)(const char * str));


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
                            void (* cb)(const char * str));

// Set on_message callback function
// desc: motion server descriptor
// cb  : on_message callback function
void botnana_set_on_message_cb(struct Botnana * desc,
                               void (* cb)(const char * str));


// Set on_send callback function
// desc: server descriptor
// cb:   on_send callback function
void botnana_set_on_send_cb(struct Botnana * desc,
                            void (* cb)(const char * str));


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

// JSON-API: config.slave.set
//
// desc:     server descriptor
// position: slave position, start by 1
// channel:  device channel, start by 1
// param:    setting item. possible item:
//	        homing_method,
//		home_offset,
//		homing_speed_1,
//		homing_speed_2,
//		homing_acceleration,
//    		profile_velocity,
//		profile_acceleration,
//		profile_deceleration
// value: setting value
// return: 0 表示有將 JSON-API 送出
int32_t config_slave_set(struct Botnana * desc,
                         uint32_t alias,
                         uint32_t position,
                         uint32_t channel,
                         const char * param,
                         int32_t value);

// JSON-API: period_us of config.motion.set
// botnana: Botnana motion server descriptor
// value: real time cycle period [us]
void config_motion_set_period_us(struct Botnana * botnana,
                                 uint32_t value);

// JSON-API: group_capacity of config.motion.set
// botnana: Botnana motion server descriptor
// value: group_capacity
void config_motion_set_group_capacity(struct Botnana * botnana,
                                      uint32_t value);

// JSON-API: axis_capacity of config.motion.set
// botnana: Botnana motion server descriptor
// value: axis_capacity
void config_motion_set_axis_capacity(struct Botnana * botnana,
                                     uint32_t value);

// JSON-API: config.motion.get
// botnana: server descriptor
void config_motion_get(struct Botnana * botnana);

// JSON-API: name of config.group.set
// botnana:  Botnana motion server descriptor
// position: group index
// value:    name
// return: 0 表示有將 JSON-API 送出
int32_t config_group_set_name(struct Botnana * botnana,
                              uint32_t position,
                              const char *  value);

// JSON-API: type as 1D of config.group.set
// botnana:  Botnana motion server descriptor
// position: group index
void config_group_set_gtype_as_1d(struct Botnana * botnana,
                                  uint32_t position);

// JSON-API: type as 2D of config.group.set
// botnana:  Botnana motion server descriptor
// position: group index
void config_group_set_gtype_as_2d(struct Botnana * botnana,
                                  uint32_t position);


// JSON-API: type as 3D of config.group.set
// botnana:  Botnana motion server descriptor
// position: group index
void config_group_set_gtype_as_3d(struct Botnana * botnana,
                                  uint32_t position);

// JSON-API: type as SINE of config.group.set
// botnana:  Botnana motion server descriptor
// position: group index
void config_group_set_gtype_as_sine(struct Botnana * botnana,
                                    uint32_t position);

// JSON-API: config.group.set for mapping
// botnana:  Botnana motion server descriptor
// position: group index
// value:    mapping
// return: 0 表示有將 JSON-API 送出
int32_t config_group_set_mapping(struct Botnana * botnana,
                                 uint32_t position,
                                 const char *  value);

// JSON-API: vmax of config.group.set
// botnana:  Botnana motion server descriptor
// position: group index
// value: vmax
void config_group_set_vmax(struct Botnana * botnana,
                           uint32_t position,
                           double  value);

// JSON-API: amax of config.group.set
// botnana:  Botnana motion server descriptor
// position: group index
// value: amax
void config_group_set_amax(struct Botnana * botnana,
                           uint32_t position,
                           double  value);

// JSON-API: jmax of config.group.set
// botnana:  Botnana motion server descriptor
// position: group index
// value: jmax
void config_group_set_jmax(struct Botnana * botnana,
                           uint32_t position,
                           double  value);



// JSON-API: config.group.get
// botnana: server descriptor
// position: group index
void config_group_get(struct Botnana * botnana,
                      uint32_t position);


// JSON-API: name of config.axis.set
// botnana: Botnana motion server descriptor
// position: axis index
// name: axis name
int config_axis_set_name(struct Botnana * botnana,
                         uint32_t position,
                         const char * name);

// JSON-API: home_offset of config.axis.set
// botnana: Botnana motion server descriptor
// position: axis index
// value : home offset
void config_axis_set_home_offset(struct Botnana * botnana,
                                 uint32_t position,
                                 double value);

// JSON-API: encoder_length_unit as Meter of config.axis.set
// botnana: Botnana motion server descriptor
// position: axis index
void config_axis_set_encoder_length_unit_as_meter(struct Botnana * botnana,
        uint32_t position);

// JSON-API: encoder_length_unit as Revolution of config.axis.set
// botnana: Botnana motion server descriptor
// position: axis index
void config_axis_set_encoder_length_unit_as_revolution(struct Botnana * botnana,
        uint32_t position);

// JSON-API: encoder_length_unit as pulse of config.axis.set
// botnana: Botnana motion server descriptor
// position: axis index
void config_axis_set_encoder_length_unit_as_pulse(struct Botnana * botnana,
        uint32_t position);

// JSON-API: encoder_ppu of config.axis.set
// botnana: Botnana motion server descriptor
// position: axis index
// value : pulses/unit
void config_axis_set_encoder_ppu(struct Botnana * botnana,
                                 uint32_t position,
                                 double value);

// JSON-API: ext_encoder_ppu of config.axis.set
// botnana: Botnana motion server descriptor
// position: axis index
// value : pulses/unit
void config_axis_set_ext_encoder_ppu(struct Botnana * botnana,
                                     uint32_t position,
                                     double value);

// JSON-API: encoder_direction of config.axis.set
// botnana: Botnana motion server descriptor
// position: axis index
// value : 1 for positive, -1 for negative
void config_axis_set_encoder_direction(struct Botnana * botnana,
                                       uint32_t position,
                                       int32_t value);

// JSON-API: ext_encoder_direction of config.axis.set
// botnana: Botnana motion server descriptor
// position: axis index
// value : 1 for positive, -1 for negative
void config_axis_set_ext_encoder_direction(struct Botnana * botnana,
        uint32_t position,
        int32_t value);

// JSON-API: vmax of config.axis.set
// botnana: Botnana motion server descriptor
// position: axis index
// value : vmax
void config_axis_set_vmax(struct Botnana * botnana,
                          uint32_t position,
                          double value);

// JSON-API: amax of config.axis.set
// botnana: Botnana motion server descriptor
// position: axis index
// value : amax
void config_axis_set_amax(struct Botnana * botnana,
                          uint32_t position,
                          double value);


// JSON-API: drive_alias of config.axis.set
// botnana: Botnana motion server descriptor
// position: axis index
// value : drive alias
void config_axis_set_drive_alias(struct Botnana * botnana,
                                 uint32_t position,
                                 int32_t value);

// JSON-API: drive_slave_position of config.axis.set
// botnana: Botnana motion server descriptor
// position: axis index
// value : drive_slave_position
void config_axis_set_drive_slave_position(struct Botnana * botnana,
        uint32_t position,
        int32_t value);


// JSON-API: drive_channel of config.axis.set
// botnana: Botnana motion server descriptor
// position: axis index
// value : drive_channel
void config_axis_set_drive_channel(struct Botnana * botnana,
                                   uint32_t position,
                                   int32_t value);

// JSON-API: ext_encoder_alias of config.axis.set
// botnana: Botnana motion server descriptor
// position: axis index
// value : ext_encoder_alias
void config_axis_set_ext_encoder_alias(struct Botnana * botnana,
                                       uint32_t position,
                                       int32_t value);

// JSON-API: ext_encoder_slave_position of config.axis.set
// botnana: Botnana motion server descriptor
// position: axis index
// value : ext_encoder_slave_position
void config_axis_set_ext_encoder_slave_position(struct Botnana * botnana,
        uint32_t position,
        int32_t value);


// JSON-API: ext_encoder_channel of config.axis.set
// botnana: Botnana motion server descriptor
// position: axis index
// value : ext_encoder_channel
void config_axis_set_ext_encoder_channel(struct Botnana * botnana,
        uint32_t position,
        int32_t value);


// JSON-API: config.axis.get
//
// botnana: Botnana motion server descriptor
// position: axis index
void config_axis_get(struct Botnana * desc,
                     uint32_t position);

// save configuration
//
// desc:     server descriptor
void configure_save(struct Botnana * desc);

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
