#ifndef __PROGRAM_H__
#define __PROGRAM_H__

#ifdef __cplusplus
extern "C" {
#endif

#include "botnana.h"

// program descriptor
struct Program;

// new program
struct Program * program_new (const char * name);

// push real time script to program
void program_push_script(struct Program * pm, const char * cmd);

// deploy program
void program_deploy(struct Botnana * desc, struct Program * pm);

// run program
//
// desc: server descriptor
// pm:   program descriptor
void program_run(struct Botnana * desc, struct Program * pm);

// push `servo on` to program
//
// pm:       program descriptor
// position: slave index
void program_push_servo_on(struct Program * pm,  uint32_t position);


// push `servo off` to program
//
// pm:       program descriptor
// position: slave index
void program_push_servo_off(struct Program * pm,  uint32_t position);

// push `change to hm mode` script to program
//
// pm:       program descriptor
// position: slave index
void program_push_hm (struct Program * pm,  uint32_t position);

// push `change to pp mode` to program
//
// pm:       program descriptor
// position: slave index
void program_push_pp (struct Program * pm,  uint32_t position);

// push `change to csp mode` to program
//
// pm:       program descriptor
// position: slave index
void program_push_csp (struct Program * pm,  uint32_t position);

// push `go` script to program
//
// pm:       program descriptor
// position: slave index
void program_push_go (struct Program * pm,  uint32_t position);

// push `set target position` to program
//
// pm:       program descriptor
// position: slave index
// target:   target position
void program_push_target_p (struct Program * pm,  uint32_t position, int32_t target);

// push `reset-fault` to program
//
// pm:       program descriptor
// position: slave index
void program_push_reset_fault (struct Program * pm,  uint32_t position);

// push `enable ain` to program
//
// pm:       program descriptor
// position: slave index
// channel:  ain channel
void program_push_enabled_ain (struct Program * pm,  uint32_t position, uint32_t channel);

// push `disable ain` to program
//
// pm:       program descriptor
// position: slave index
// channel:  ain channel
void program_push_disable_ain (struct Program * pm,  uint32_t position, uint32_t channel);

// push `enable aout` to program
//
// pm:       program descriptor
// position: slave index
// channel:  aout channel
void program_push_enabled_aout (struct Program * pm,  uint32_t position, uint32_t channel);

// push `disable aout` to program
//
// pm:       program descriptor
// position: slave index
// channel:  aout channel
void program_push_disable_aout (struct Program * pm,  uint32_t position, uint32_t channel);

// push `set aout` to program
//
// pm:       program descriptor
// position: slave index
// channel:  aout channel
// value:    aout value
void program_push_set_aout (struct Program * pm,  uint32_t position, uint32_t channel, int32_t value);

// push `set dout` to program
//
// pm:       program descriptor
// position: slave index
// channel:  dout channel
// value:    dout value ( 0 or 1)
void program_push_set_dout (struct Program * pm,  uint32_t position, uint32_t channel, int32_t value);

// push `enable coordinator` to program
//
// pm:       program descriptor
void program_push_enable_coordinator(struct Program * pm);

// push `disable coordinator` to program
//
// pm:       program descriptor
void program_push_disable_coordinator(struct Program * pm);

// push `start trajectory` to program
//
// pm:  program descriptor
void program_push_start_trj(struct Program * pm);

// push `clear path` to program
//
// pm:  program descriptor
void program_push_clear_path (struct Program * pm);

// push `set feedrate` to program
//
// pm:        program descriptor
// feedrate : feedrate
void program_push_set_feedrate (struct Program * pm,  double feedrate);

// push `set vcmd` to program
//
// pm:   program descriptor
// vcmd: v cmd
void program_push_set_vcmd (struct Program * pm,  double vcmd);

// push `set group` to program
//
// pm:    program descriptor
// group: group index
void program_push_select_group(struct Program * pm,  uint32_t group);

// push `enable current group` to program
//
// pm:    program descriptor
// group: group index
void program_push_enable_group(struct Program * pm);

// push `disable current group` to program
//
// pm:    program descriptor
// group: group index
void program_push_disable_group(struct Program * pm);

// push `wait path end of group` to program
//
// pm:    program descriptor
// group: group index
void program_push_wait_group_end(struct Program * pm,  uint32_t group);

// push `move1d` to program
//
// pm: program descriptor
// x:  position x
void program_push_move1d(struct Program * pm,  double x);

// push `line1d` to program
//
// pm: program descriptor
// x:  target x
void program_push_line1d(struct Program * pm,  double x);

// push `move2d` to program
//
// pm: program descriptor
// x:  position x
// y:  position y
void program_push_move2d(struct Program * pm,  double x, double y);

// push `line2d` to program
//
// pm: program descriptor
// x:  target x
// y:  target y
void program_push_line2d(struct Program * pm,  double x, double y);

// push `arc2d` to program
//
// pm: program descriptor
// cx:  center x
// cy:  center y
// tx:  target x
// ty:  target y
// rev: rev (need > 0 or < 0)
void program_push_arc2d(struct Program * pm,
                        double cx,
                        double cy,
                        double tx,
                        double ty,
                        int32_t rev);

// push `move3d` to program
//
// pm: program descriptor
// x:  position x
// y:  position y
// z:  position z
void program_push_move3d(struct Program * pm,  double x, double y,  double z);

// push `line3d` to program
//
// pm: program descriptor
// x:  target x
// y:  target y
// z:  target z
void program_push_line3d(struct Program * pm,  double x, double y,  double z);

// push `helix3d` to program
//
// pm: program descriptor
// x:  center x
// y:  center y
// tx:  target x
// ty:  target y
// tz:  target z
// rev: rev (need > 0 or < 0)
void program_push_helix3d(struct Program * pm,
                          double cx,
                          double cy,
                          double tx,
                          double ty,
                          double tz,
                          int32_t rev);


#ifdef __cplusplus
}
#endif

#endif
