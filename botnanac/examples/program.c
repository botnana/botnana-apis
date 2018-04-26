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

    struct Botnana * botnana = botnana_connect("192.168.7.2", handle_meaasge);
    struct Program * pm = program_new("test");
    botnana_enable_debug(botnana);

    program_push_reset_fault(pm, 1);
    program_push_servo_on(pm, 1);
    program_push_hm(pm, 1);
    program_push_go(pm, 1);
    program_push_pp(pm, 1);
    program_push_target_p(pm, 1, 25000);
    program_push_go(pm, 1);

    botnana_abort_program(botnana);
    sleep(2);
    botnana_attach_event(botnana, "end-of-program", 1, end_of_program);
    botnana_empty(botnana);
    sleep(1);
    program_deploy(botnana,pm);
    sleep(1);
    program_run(botnana, pm);


    while (1)
    {
        sleep(1);
    }
    return 0;

}

