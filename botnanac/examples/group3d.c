#include <stdio.h>
#include "botnana.h"
#include "program.h"


void handle_meaasge (const char * src)
{
    //printf("handle_meaasge: %s \n", src);

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

void handle_pos1(const char * src)
{
    printf("POS1: %s ", src);
}

void handle_feedback1(const char * src)
{
    printf("FEEDBACK1: %s ", src);
}

void handle_pos2(const char * src)
{
    printf("POS2: %s ", src);
}

void handle_feedback2(const char * src)
{
    printf("FEEDBACK2: %s ", src);
}

void handle_pos3(const char * src)
{
    printf("POS3: %s ", src);
}

void handle_feedback3(const char * src)
{
    printf("FEEDBACK3: %s ", src);
}




int main()
{

    struct Botnana * botnana = botnana_connect("192.168.7.2", handle_meaasge);
    struct Program * pm = program_new("test");
    //botnana_enable_debug(botnana);

    /*** catch massage tag  ***/
    botnana_attach_event(botnana, "end-of-program", 0, end_of_program);
    botnana_attach_event(botnana, "ACS.1", 0, handle_acs);
    botnana_attach_event(botnana, "PCS.1", 0, handle_pcs);
    botnana_attach_event(botnana, "axis_command_position.1", 0, handle_pos1);
    botnana_attach_event(botnana, "axis_corrected_position.1", 0, handle_feedback1);
    botnana_attach_event(botnana, "axis_command_position.2", 0, handle_pos2);
    botnana_attach_event(botnana, "axis_corrected_position.2", 0, handle_feedback2);
    botnana_attach_event(botnana, "axis_command_position.3", 0, handle_pos3);
    botnana_attach_event(botnana, "axis_corrected_position.3", 0, handle_feedback3);

    // get real time group configure, to check group 1 is 3D
    botnana_get_rt_grpcfg(botnana, 1);
    sleep(1);

    // disable coordinated motion
    program_push_disable_coordinator(pm);

    // select group 1 as current group
    program_push_select_group(pm, 1);

    // disable current group
    program_push_disable_group(pm);

    // clear path of current group
    program_push_clear_path (pm);


    /*** slave 1, 2 and 3 servo on ***/
    program_push_reset_fault(pm, 1);
    program_push_csp(pm, 1);
    program_push_servo_on(pm, 1);
    program_push_script(pm, "500 ms");
    program_push_reset_fault(pm, 2);
    program_push_csp(pm, 2);
    program_push_servo_on(pm, 2);
    program_push_script(pm, "500 ms");
    program_push_reset_fault(pm, 3);
    program_push_csp(pm, 3);
    program_push_servo_on(pm, 3);
    program_push_script(pm, "500 ms");

    // enable coordinated motion
    program_push_enable_coordinator(pm);

    // start trajectory planner
    program_push_start_trj(pm);

    // select group 1 as current group
    program_push_select_group(pm, 1);

    // enable current group
    program_push_enable_group(pm);

    // set current position as start position (not moving)
    program_push_move3d(pm, 0, 0, 0);

    // set segment feedrate
    program_push_set_feedrate(pm, 0.4);

    // insert line3d segment
    program_push_line3d(pm, 1.0, 1.0, 1.0);

    // insert helix3d segment
    program_push_helix3d(pm, 0.0, 1.0, -1.0, 1.0, -1.0, 1);

    // insert line3d segment
    program_push_line3d(pm, 0.0, 0.0, 0.0);

    // set velocity command
    program_push_set_vcmd(pm, 0.2);

    // waiting end of trajectory of group 1
    program_push_wait_group_end(pm, 1);

    // disable coordinated motion
    program_push_disable_coordinator(pm);

    // empty current user program in motion server
    botnana_empty(botnana);
    sleep(1);

    // deploy program to motion server
    program_deploy(botnana, pm);
    // wait deployed|ok message
    sleep(1);
    // execute program
    program_run(botnana, pm);


    while (1)
    {

        botnana_motion_evaluate(botnana, "1 .axis  2 .axis 3 .axis 1 .group");
        sleep(1);
    }
    return 0;

}

