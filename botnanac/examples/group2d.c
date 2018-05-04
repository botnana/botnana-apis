#include <stdio.h>
#include "botnana.h"
#include "program.h"


void handle_meaasge (const char * src)
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




int main()
{

    struct Botnana * botnana = botnana_connect("192.168.7.2", handle_meaasge);
    struct Program * pm = program_new("test");
    botnana_enable_debug(botnana);
    botnana_attach_event(botnana, "end-of-program", 0, end_of_program);
    botnana_attach_event(botnana, "ACS.1", 0, handle_acs);
    botnana_attach_event(botnana, "PCS.1", 0, handle_pcs);
    botnana_attach_event(botnana, "axis_command_position.1", 0, handle_pos1);
    botnana_attach_event(botnana, "axis_corrected_position.1", 0, handle_feedback1);
    botnana_attach_event(botnana, "axis_command_position.2", 0, handle_pos2);
    botnana_attach_event(botnana, "axis_corrected_position.2", 0, handle_feedback2);
    botnana_motion_evaluate(botnana, "1 .axis 1 .group");
    sleep(1);
    program_push_disable_coordinator(pm);
    program_push_select_group(pm, 1);
    program_push_disable_group(pm);
    program_push_clear_path (pm);

    program_push_reset_fault(pm, 1);
    program_push_csp(pm, 1);
    program_push_servo_on(pm, 1);
    program_push_script(pm, "500 ms");
    program_push_reset_fault(pm, 2);
    program_push_csp(pm, 2);
    program_push_servo_on(pm, 2);
    program_push_script(pm, "500 ms");

    program_push_enable_coordinator(pm);
    program_push_start_trj(pm);
    program_push_select_group(pm, 1);
    program_push_enable_group(pm);
    program_push_move2d(pm, 0.0, 0.0);
    program_push_set_feedrate(pm, 0.4);
    program_push_line2d(pm, 1.0, 0.0);
    program_push_arc2d(pm, 0.0, 0.0, 0.0, 1.0, 1);
    program_push_arc2d(pm, 0.0, 0.0, 1.0, 0.0, 1);
    program_push_line2d(pm, 1.0, 1.0);
    program_push_line2d(pm, 0.0, 0.0);
    program_push_set_vcmd(pm, 0.2);
    program_push_wait_group_end(pm, 1);
    program_push_disable_coordinator(pm);

    botnana_abort_program(botnana);
    sleep(1);
    botnana_empty(botnana);
    sleep(1);
    program_deploy(botnana,pm);
    sleep(1);
    program_run(botnana, pm);


    while (1)
    {

        botnana_motion_evaluate(botnana, "1 .axis  2 .axis 1 .group");
        sleep(1);
    }
    return 0;

}

