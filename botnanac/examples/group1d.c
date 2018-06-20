#include <stdio.h>
#include "botnana.h"
#include "program.h"

void handle_meaasge(const char * src)
{
    printf("handle_meaasge: %s \n", src);

}

void end_of_program(const char * src)
{
    printf("end-of-program: %s \n", src);

}

void handle_acs(const char * src)
{
    printf("ACS: %s ,", src);
}

void handle_pcs(const char * src)
{
    printf("PCS: %s \n", src);
}

void handle_pos(const char * src)
{
    printf("POS: %s ", src);
}

void handle_feedback(const char * src)
{
    printf("FEEDBACK: %s ", src);
}

int main()
{

    struct Botnana * botnana = botnana_connect("192.168.7.2", handle_meaasge);
    struct Program * pm = program_new("test");
    //botnana_enable_debug(botnana);

    // get real time group configure, to check group 1 is 1D
    botnana_get_rt_grpcfg(botnana, 1);

    // catch end-of-program message tag
    botnana_attach_event(botnana, "end-of-program", 0, end_of_program);

    // catch ACS.1 message tag
    botnana_attach_event(botnana, "ACS.1", 0, handle_acs);

    // catch PCS.1 message tag
    botnana_attach_event(botnana, "PCS.1", 0, handle_pcs);

    // catch axis_command_position.1 message tag
    botnana_attach_event(botnana, "axis_command_position.1", 0, handle_pos);

    // catch axis_corrected_position.1 message tag
    botnana_attach_event(botnana,
                         "axis_corrected_position.1",
                         0,
                         handle_feedback);
    sleep(1);

    // disable coordinated motion
    program_push_disable_coordinator(pm);

    // select group 1 as current group
    program_push_select_group(pm, 1);

    // disable current group
    program_push_disable_group(pm);

    // clear path of current group
    program_push_clear_path (pm);

    // reset fault of slave 1
    program_push_reset_fault(pm, 1);

    // change to csp mode of slave 1
    program_push_csp(pm, 1);

    // servo on of slave 1
    program_push_servo_on(pm, 1);

    // wait 2000 ms
    program_push_script(pm, "2000 ms");

    // enable coordinated motion
    program_push_enable_coordinator(pm);

    // start trajectory planner
    program_push_start_trj(pm);

    // select group 1 as current group
    program_push_select_group(pm, 1);

    // enable current group
    program_push_enable_group(pm);

    // set current position as start position (not moving)
    program_push_move1d(pm, 0);

    // set segment feedrate
    program_push_set_feedrate (pm, 0.05);

    // insert line1d segment
    program_push_line1d(pm, 0.5);

    // set segment feedrate
    program_push_set_feedrate (pm, 0.1);

    // insert line1d segment
    program_push_line1d(pm, 1.0);

    // set segment feedrate
    program_push_set_feedrate (pm, 0.2);

    // insert line1d segment
    program_push_line1d(pm, 2.0);

    // set velocity command
    program_push_set_vcmd (pm, 0.5);

    // waiting end of trajectory of group 1
    program_push_wait_group_end(pm, 1);

    // disable coordinated motion
    program_push_disable_coordinator(pm);

    // empty current user program in motion server
    botnana_empty(botnana);
    sleep(1);

    // deploy program to motion server
    program_deploy(botnana, pm);
    sleep(1);
    // execute program
    program_run(botnana, pm);

    while (1)
    {
        // get axis 1  info
        botnana_get_axis_info(botnana, 1);
        // get group 1 info
        botnana_get_group_info(botnana, 1);
        sleep(2);
    }
    return 0;

}

