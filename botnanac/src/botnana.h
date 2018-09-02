#ifndef __BOTNANA_H__
#define __BOTNANA_H__

#ifdef __cplusplus
extern "C" {
#endif

#include <stdint.h>

// server descriptor
struct Botnana;

// connect to motion server
// address        : IP of motion server
// on_ws_error_cb : on Websocket error callback
struct Botnana * botnana_connect(const char * address,
                                 void (* on_ws_error_cb)(const char * str));

// Send raw message
void botnana_send_message(struct Botnana * desc,
                          const char * msg);

// Set tag callback function
// desc  : server descriptor
// tag   : tag
// count : called times, 0 as always
// cb    : handle corresponding valve function
int32_t botnana_set_tag_cb (struct Botnana * desc,
                            const char * tag,
                            uint32_t count,
                            void (* cb)(const char * str));

// Set on_message callback function
// desc: server descriptor
// cb  : on_message callback function
void botnana_set_on_send_cb(struct Botnana * desc,
                            void (* cb)(const char * str));


// Set on_send callback function
// desc: server descriptor
// cb:   on_send callback function
void botnana_set_on_send_cb(struct Botnana * desc,
                            void (* cb)(const char * str));


// abort current background program
// desc: server descriptor
void botnana_abort_program (struct Botnana * desc);

//****** Json API ********/

// motion evaluate
//
// desc:   server descriptor
// script: real time script

void motion_evaluate(struct Botnana * desc,
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
                         uint32_t position,
                         uint32_t channel,
                         const char * param,
                         int32_t value);

// JSON-API: config.motion.set
//
// desc:     server descriptor
// param:    setting item. possible item:
//	        perios_us,
//		group_capacity,
//		axis_capacity,
// value: setting value
// return: 0 表示有將 JSON-API 送出
int32_t config_motion_set(struct Botnana * desc,
                          const char * param,
                          uint32_t value);

// JSON-API: config.motion.get
//
// desc: server descriptor
void config_motion_get(struct Botnana * desc);

// JSON-API: config.group.set for string data type
//
// desc:     server descriptor
// position: group index
// param:    setting item. possible item:
//	        name,
//		gtype,
// value: setting value
// return: 0 表示有將 JSON-API 送出
int32_t config_group_set_string(struct Botnana * desc,
                                uint32_t position,
                                const char * param,
                                const char *  value);

// JSON-API: config.group.set for mapping
//
// desc:     server descriptor
// position: group index
// value:    mapping
// return: 0 表示有將 JSON-API 送出
int32_t config_group_set_mapping(struct Botnana * desc,
                                 uint32_t position,
                                 const char *  value);

// JSON-API: config.group.set for double data type
//
// desc:     server descriptor
// position: group index
// param:    setting item. possible item:
//	        vmax,
//		amax,
//		jmax,
// value: setting value
// return: 0 表示有將 JSON-API 送出
int32_t config_group_set_double(struct Botnana * desc,
                                uint32_t position,
                                const char * param,
                                double value);

// JSON-API: config.group.get
//
// desc: server descriptor
// position: group index
void config_group_get(struct Botnana * desc,
                      uint32_t position);

// JSON-API: config.axis.set for string data type
//
// desc:     server descriptor
// position: axis index
// param:    setting item. possible item:
//	        name,
//		encoder_length_unit,
// value: setting value
// return: 0 表示有將 JSON-API 送出
int32_t config_axis_set_string(struct Botnana * desc,
                               uint32_t position,
                               const char * param,
                               const char *  value);

// JSON-API: config.axis.set for double data type
//
// desc:     server descriptor
// position: axis index
// param:    setting item. possible item:
//		home_offset
//		encoder_ppu,
//		vmax,
//		amax,
// value: setting value
// return: 0 表示有將 JSON-API 送出
int32_t config_axis_set_double(struct Botnana * desc,
                               uint32_t position,
                               const char * param,
                               double value);


// JSON-API: config.axis.set for integer data type
//
// desc:     server descriptor
// position: axis index
// param:    setting item. possible item:
//		encoder_direction
//		slave_position
// 		drive_channel
// value: setting value
// return: 0 表示有將 JSON-API 送出
int32_t config_axis_set_integer(struct Botnana * desc,
                                uint32_t position,
                                const char * param,
                                int32_t value);

// JSON-API: config.axis.get
//
// desc: server descriptor
// position: axis index
void config_axis_get(struct Botnana * desc,
                     uint32_t position);
// save configuration
//
// desc:     server descriptor
void configure_save(struct Botnana * desc);

// program descriptor
struct Program;

// new program
struct Program * program_new (const char * name);

// push real time script to program
void program_line(struct Program * pm, const char * cmd);

// deploy program
void program_deploy(struct Botnana * desc, struct Program * pm);

// run program
//
// desc: server descriptor
// pm:   program descriptor
void program_run(struct Botnana * desc, struct Program * pm);

#ifdef __cplusplus
}
#endif

#endif
