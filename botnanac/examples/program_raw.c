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

int main()
{
    // connect to motion server
    struct Botnana * botnana = botnana_connect("192.168.7.2", handle_meaasge);

    // new program
    struct Program * pm = program_new("test");

    // reset drive 1 fault
    program_push_script(pm, "1 reset-fault");

    // wait drive 1 to no fault
    program_push_script(pm, "1 until-no-fault");

    // drive 1 servo on
    program_push_script(pm, "1 servo-on");

    // wait drive 1 to servo on
    program_push_script(pm, "1 until-servo-on");

    // wait 1000 ms
    program_push_script(pm, "1000 ms");

    // set drive 1 to HM mode
    program_push_script(pm, "hm 1 op-mode!");

    // set homing method to 33
    program_push_script(pm," 33 1 homing-method!");

    // wait HM request finished
    program_push_script(pm, "until-no-requests");

    // start drive 1 homing
    program_push_script(pm, "1 go");

    // wait drive 1 homing finished
    program_push_script(pm, "1 until-target-reached");

    // set drive to PP mode
    program_push_script(pm, "pp 1 op-mode!");

    //wait PP request finished
    program_push_script(pm, "until-no-requests");

    //set drive 1 target position to 25000 pulse count
    program_push_script(pm, "25000 1 target-p!");

    // start drive 1 moving
    program_push_script(pm, "1 go");

    // wait drive target reached
    program_push_script(pm, "1 until-target-reached");

    // catch "end-of-program" tag
    botnana_attach_event(botnana, "end-of-program", 1, end_of_program);

    // empty user program or user define command
    botnana_empty(botnana);

    sleep(1);

    // deploy program to motion server
    program_deploy(botnana,pm);
    // wait deployed|ok message
    sleep(1);

    // run program
    program_run(botnana, pm);

    while (1)
    {
        sleep(1);
    }
    return 0;

}

