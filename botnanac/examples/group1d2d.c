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

    /*** catch message tag ***/

    botnana_attach_event(botnana, "end-of-program", 0, end_of_program);
    botnana_attach_event(botnana, "ACS.1", 0, handle_acs1);
    botnana_attach_event(botnana, "PCS.1", 0, handle_pcs1);
    botnana_attach_event(botnana, "ACS.2", 0, handle_acs2);
    botnana_attach_event(botnana, "PCS.2", 0, handle_pcs2);
    botnana_attach_event(botnana, "axis_command_position.1", 0, handle_pos1);
    botnana_attach_event(botnana, "axis_corrected_position.1", 0, handle_feedback1);
    botnana_attach_event(botnana, "axis_command_position.2", 0, handle_pos2);
    botnana_attach_event(botnana, "axis_corrected_position.2", 0, handle_feedback2);


    // get real time group configure, to check group 1 is 1D
    botnana_get_rt_grpcfg(botnana, 1);

    // get real time group configure, to check group 2 is 2D
    botnana_get_rt_grpcfg(botnana, 1);


    sleep(1);

    // disable coordinated motion
    program_push_script(pm, "-coordinator");

    // clear path of current group
    program_push_script(pm, "1 group! -group 0path");
    program_push_script(pm, "2 group! -group 0path");


    // slave 1, 2 and 3 servo on
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

    // enable coordinated motion
    program_push_script(pm, "+coordinator");

    // start trajectory planner
    program_push_script(pm, "start");
    program_push_script(pm, "10 ms");

    // select group 1 as current group and enable group
    program_push_script(pm, "1 group! +group");

    // set current position as start position by MCS
    program_push_script(pm, "mcs");

    // set segment feedrate
    program_push_script(pm, "0.4e feedrate!");

    // insert line1d segment
    program_push_script(pm, "1.0e line1d");

    // set velocity command
    program_push_script(pm, "0.05e vcmd!");

    // select group 2 as current group and enable group
    program_push_script(pm, "2 group! +group");

    // set current position as start position by MCS
    program_push_script(pm, "mcs");

    // set segment feedrate
    program_push_script(pm, "0.4e feedrate!");

    // insert line2d segment
    program_push_script(pm, "1.0e 1.0e line2d");

    // set velocity command
    program_push_script(pm, "0.2e vcmd!");

    // waiting end of trajectory of group 1
    program_push_script(pm, "1 until-grp-end");

    // waiting end of trajectory of group 2
    program_push_script(pm, "2 until-grp-end");

    // select group 1 as current group
    program_push_script(pm, "1 group!");

    // insert line2d segment
    program_push_script(pm, "2.0e line1d");

    // waiting end of trajectory of group 1
    program_push_script(pm, "1 until-grp-end");

    // select group 2 as current group
    program_push_script(pm, "2 group!");

    // insert line2d segment
    program_push_script(pm, "2.0e  2.0e line2d");

    // waiting end of trajectory of group 2
    program_push_script(pm, "2 until-grp-end");

    // disable coordinated motion
    program_push_script(pm, "-coordinator");

    sleep(1);

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
        // get axis and group information
        botnana_motion_evaluate(botnana, "1 .axis  2 .axis 1 .group 2 .group");
        sleep(1);
    }
    return 0;

}

