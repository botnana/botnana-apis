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

void debug_callback (const char * src)
{
    printf("debug callback: %s \n", src);
}


int main()
{
    struct Botnana * botnana = botnana_connect("192.168.7.2", handle_meaasge);
    struct Program * pm = program_new("test");

    // set debug callback function
    botnana_set_debug_callback(botnana, debug_callback);

    // reset drive 1 fault and wait
    program_push_reset_fault(pm, 1);

    // drive 1 servo on and wait
    program_push_servo_on(pm, 1);

    // waiting 1000 ms
    program_push_script(pm, "1000 ms");

    // set drive 1 homing method to 33
    program_push_script(pm," 33 1 homing-method!");

    // change drive 1 to HM mode
    program_push_hm(pm, 1);

    // drive 1 start homing and wait
    program_push_go(pm, 1);

    // change drive 1 to  PP mode
    program_push_pp(pm, 1);

    // set drive 1 target position to 25000
    program_push_target_p(pm, 1, 25000);

    // start moving and wait
    program_push_go(pm, 1);

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

