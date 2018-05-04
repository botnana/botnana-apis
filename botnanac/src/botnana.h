#ifndef __BOTNANA_H__
#define __BOTNANA_H__

#ifdef __cplusplus
extern "C" {
#endif

#include <stdint.h>

// server descriptor
struct Botnana;

// connect to motion server
// address: IP of motion server
// fn:      handle message function
struct Botnana * botnana_connect(const char * address,
                                 void (* fn)(const char * str));

// attach event
// desc:  server descriptor
// event: message tag
// count: called times, 0 as always
// fn:    handle event function
void botnana_attach_event (struct Botnana * desc,
                           const char * event,
                           uint32_t count,
                           void (* fn)(const char * str));


// enable debug, display sender command
// desc: server descriptor
void botnana_enable_debug(struct Botnana * desc);

// disable debug
// desc: server descriptor
void botnana_disable_debug(struct Botnana * desc);

// empty user program and command
// desc: server descriptor
void botnana_empty(struct Botnana * desc);

// abort current program
// desc: server descriptor
void botnana_abort_program (struct Botnana * desc);


//****** Json API ********/

// motion evaluate
//
// desc:   server descriptor
// script: real time script

void botnana_motion_evaluate(struct Botnana * desc,
                             const char * script);

// get version
//
// desc: server descriptor
void botnana_get_version(struct Botnana * desc);

// get slave information
//
// desc:     server descriptor
// position: slave position, start by 1
void botnana_get_slave(struct Botnana * desc,
                       uint32_t position);

// set slave
//
// desc:     server descriptor
// position: slave position, start by 1
// tag:      setting item. possible item:
//			homing_method,
//			home_offset,
//			homing_speed_1,
//			homing_speed_2,
//			homing_acceleration,
//    			profile_velocity,
//			profile_acceleration,
//			profile_deceleration
// value: setting value
void botnana_set_slave(struct Botnana * desc,
                       uint32_t position,
                       const char * tag,
                       int32_t value);

// get slave diff. information
//
// desc:     server descriptor
// position: slave position, start by 1
void botnana_get_slave_diff(struct Botnana * desc,
                            uint32_t position);


// set motion period_us (need reboot)
//
// desc:     server descriptor
// perios_us: real time period
void botnana_set_motion_config_period_us(struct Botnana * desc,
        uint32_t perios_us);

// set motion group capacity (need reboot)
//
// desc:     	   server descriptor
// group_capacity: group_capacity
void botnana_set_motion_config_group_capacity(struct Botnana * desc,
        uint32_t group_capacity);

// set motion axis capacity (need reboot)
//
// desc:     	   server descriptor
// axis_capacity: axis_capacity
void botnana_set_motion_config_axis_capacity(struct Botnana * desc,
        uint32_t axis_capacity);

// get motion configuration
//
// desc: server descriptor
void botnana_get_motion_config(struct Botnana * desc);

// get motion real time configuration
//
// desc: server descriptor
void botnana_get_motion_info(struct Botnana * desc);

// set group config name
//
// desc:     server descriptor
// position: group index
// name:     group name
void botnana_set_group_config_name(struct Botnana * desc,
                                   uint32_t position,
                                   const char * name);

// set group config gtype
//
// desc:     server descriptor
// position: group index
// gtype:    group type. possible type:
//			1D,
//			2D,
//			3D,
void botnana_set_group_config_gtype(struct Botnana * desc,
                                    uint32_t position,
                                    const char * gtype);

// set mapping of group 1D
//
// desc:     server descriptor
// position: group index
// a1:       axis index
void botnana_set_group_map1d(struct Botnana * desc,
                             uint32_t position,
                             uint32_t a1);

// set mapping of group 2D
//
// desc:     server descriptor
// position: group index
// a1:       axis index
// a2:       axis index
void botnana_set_group_map2d(struct Botnana * desc,
                             uint32_t position,
                             uint32_t a1,
                             uint32_t a2);

// set mapping of group 3D
//
// desc:     server descriptor
// position: group index
// a1:       axis index
// a2:       axis index
// a3:       axis index
void botnana_set_group_map3d(struct Botnana * desc,
                             uint32_t position,
                             uint32_t a1,
                             uint32_t a2,
                             uint32_t a3);

// set group vmax
//
// desc:     server descriptor
// position: group index
// vmax:     vmax
void botnana_set_group_vmax(struct Botnana * desc,
                            uint32_t position,
                            double vmax);

// set group amax
//
// desc:     server descriptor
// position: group index
// amax:     amax
void botnana_set_group_amax(struct Botnana * desc,
                            uint32_t position,
                            double amax);

// set group jmax
//
// desc:     server descriptor
// position: group index
// jmax:     jmax
void botnana_set_group_jmax(struct Botnana * desc,
                            uint32_t position,
                            double jmax);

// get group configuration
//
// desc: server descriptor
// position: group index
void botnana_get_grpcfg(struct Botnana * desc,
                        uint32_t position);

// get real time group configuration
//
// desc: server descriptor
// position: group index
void botnana_get_rt_grpcfg(struct Botnana * desc,
                           uint32_t position);

// get group information
//
// desc: server descriptor
// position: group index
void botnana_get_group_info(struct Botnana * desc,
                            uint32_t position);

// set axis configure name
//
// desc:     server descriptor
// position: axis index
// name:     axis name
void botnana_set_axis_config_name(struct Botnana * desc,
                                  uint32_t position,
                                  const char * name);

// set axis encoder length unit
//
// desc:     server descriptor
// position: axis index
// unit:     unit, possible unit:
//			Meter,
//			meter,
//			m,
//			Revolution,
//			revolution,
//			rev,
//			Pulse,
//			pulse
void botnana_set_axis_encoder_length_unit(struct Botnana * desc,
        uint32_t position,
        const char * unit);

// set axis encoder pulses per unit
//
// desc:     server descriptor
// position: axis index
// ppu:      encoder pulses per unit
void botnana_set_axis_ppu(struct Botnana * desc,
                          uint32_t position,
                          double ppu);

// set axis home offset
//
// desc:     server descriptor
// position: axis index
// offset:   home offset
void botnana_set_axis_home_offset(struct Botnana * desc,
                                  uint32_t position,
                                  double offset);

// set axis encoder direction
//
// desc:      server descriptor
// position:  axis index
// direction: encoder direction, possible direction 1 or -1
void botnana_set_axis_encoder_direction(struct Botnana * desc,
                                        uint32_t position,
                                        int32_t direction);

// get axis configuration
//
// desc:      server descriptor
// position:  axis index
void botnana_get_axiscfg(struct Botnana * desc,
                         uint32_t position);

// get real time axis configuration
//
// desc:     server descriptor
// position: axis index
void botnana_get_rt_axiscfg(struct Botnana * desc,
                            uint32_t position);


// get axis information
//
// desc:     server descriptor
// position: axis index
void botnana_get_axis_info(struct Botnana * desc,
                           uint32_t position);

// save configuration
//
// desc:     server descriptor
void botnana_save_configure(struct Botnana * desc);


#ifdef __cplusplus
}
#endif

#endif
