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
    botnana_enable_debug(botnana);
    botnana_get_rt_grpcfg(botnana, 1);
    botnana_attach_event(botnana, "end-of-program", 0, end_of_program);
    botnana_attach_event(botnana, "ACS.1", 0, handle_acs);
    botnana_attach_event(botnana, "PCS.1", 0, handle_pcs);
    botnana_attach_event(botnana, "axis_command_position.1", 0, handle_pos);
    botnana_attach_event(botnana,
                         "axis_corrected_position.1",
                         0,
                         handle_feedback);
    sleep(1);
    program_push_disable_coordinator(pm);
    program_push_select_group(pm, 1);
    program_push_disable_group(pm);
    program_push_clear_path (pm);
    program_push_reset_fault(pm, 1);
    program_push_csp(pm, 1);
    program_push_servo_on(pm, 1);
    program_push_script(pm, "2000 ms");
    program_push_enable_coordinator(pm);
    program_push_start_trj(pm);
    program_push_select_group(pm, 1);
    program_push_enable_group(pm);
    program_push_move1d(pm, 0);
    program_push_set_feedrate (pm, 0.05);
    program_push_line1d(pm, 0.5);
    program_push_set_feedrate (pm, 0.1);
    program_push_line1d(pm, 1.0);
    program_push_set_feedrate (pm, 0.2);
    program_push_line1d(pm, 2.0);
    program_push_set_vcmd (pm, 0.5);
    program_push_wait_group_end(pm, 1);
    program_push_disable_coordinator(pm);


    botnana_empty(botnana);
    sleep(1);
    program_deploy(botnana, pm);
    sleep(1);
    program_run(botnana, pm);

    while (1)
    {

        botnana_get_axis_info(botnana, 1);
        botnana_get_group_info(botnana, 1);
        sleep(2);
    }
    return 0;

}

