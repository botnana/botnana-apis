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

void handle_acs1(const char * src)
{
    printf("ACS1: %s ,", src);
}

void handle_pcs1(const char * src)
{
    printf("PCS1: %s ", src);
}

void handle_acs2(const char * src)
{
    printf("ACS2: %s ,", src);
}

void handle_pcs2(const char * src)
{
    printf("PCS2: %s \n", src);
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
    //botnana_enable_debug(botnana);
    botnana_attach_event(botnana, "end-of-program", 0, end_of_program);
    botnana_attach_event(botnana, "ACS.1", 0, handle_acs1);
    botnana_attach_event(botnana, "PCS.1", 0, handle_pcs1);
    botnana_attach_event(botnana, "ACS.2", 0, handle_acs2);
    botnana_attach_event(botnana, "PCS.2", 0, handle_pcs2);
    botnana_attach_event(botnana, "axis_command_position.1", 0, handle_pos1);
    botnana_attach_event(botnana, "axis_corrected_position.1", 0, handle_feedback1);
    botnana_attach_event(botnana, "axis_command_position.2", 0, handle_pos2);
    botnana_attach_event(botnana, "axis_corrected_position.2", 0, handle_feedback2);
    botnana_motion_evaluate(botnana, "1 .axis 1 .group");
    sleep(1);
    program_push_script(pm, "-coordinator");
    program_push_script(pm, "1 group! -group 0path");
    program_push_script(pm, "2 group! -group 0path");
    program_push_script(pm, "1 reset-fault");
    program_push_script(pm, "1 until-no-fault");
    program_push_script(pm, "2 reset-fault");
    program_push_script(pm, "2 until-no-fault");
    program_push_script(pm, "8 1  op-mode!");
    program_push_script(pm, "8 2  op-mode!");
    program_push_script(pm, "8 3  op-mode!");
    program_push_script(pm, "until-no-requests");
    program_push_script(pm, "1 servo-on");
    program_push_script(pm, "1 until-servo-on");
    program_push_script(pm, "2 servo-on");
    program_push_script(pm, "2 until-servo-on");
    program_push_script(pm, "3 servo-on");
    program_push_script(pm, "3 until-servo-on");
    program_push_script(pm, "2000 ms");
    program_push_script(pm, "+coordinator");
    program_push_script(pm, "start");
    program_push_script(pm, "2000 ms");
    program_push_script(pm, "1 group! +group");
    program_push_script(pm, "mcs");
    program_push_script(pm, "0.4e feedrate!");
    program_push_script(pm, "1.0e line1d");
    program_push_script(pm, "0.05e vcmd!");
    program_push_script(pm, "2 group! +group");
    program_push_script(pm, "mcs");
    program_push_script(pm, "0.4e feedrate!");
    program_push_script(pm, "1.0e 1.0e line2d");
    program_push_script(pm, "0.2e vcmd!");
    program_push_script(pm, "1 until-grp-end");
    program_push_script(pm, "2 until-grp-end");


    program_push_script(pm, "1 group!");
    program_push_script(pm, "2.0e line1d");
    program_push_script(pm, "1 until-grp-end");
    program_push_script(pm, "2 group!");
    program_push_script(pm, "2.0e  2.0e line2d");
    program_push_script(pm, "2 until-grp-end");


    program_push_script(pm, "-coordinator");

    botnana_abort_program(botnana);
    sleep(1);
    botnana_empty(botnana);
    sleep(1);
    program_deploy(botnana,pm);
    sleep(1);
    program_run(botnana, pm);


    while (1)
    {

        botnana_motion_evaluate(botnana, "1 .axis  2 .axis 1 .group 2 .group");
        sleep(1);
    }
    return 0;

}

