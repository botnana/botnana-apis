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


void handle_ain(const char * src)
{
    printf("ain: %s \n", src);

}

void handle_din(const char * src)
{
    printf("din: %s \n", src);

}


int main()
{

    struct Botnana * botnana = botnana_connect("192.168.7.2", handle_meaasge);
    struct Program * pm = program_new("test");
    //botnana_enable_debug(botnana);

    program_push_enable_ain(pm, 4, 1);
    program_push_enable_aout(pm, 5, 1);
    program_push_set_aout(pm, 5, 1, 1024);
    program_push_set_dout(pm, 2, 1, 1);
    program_push_line(pm, "5000 ms");
    program_push_set_aout(pm, 5, 1, 0);
    program_push_set_dout(pm, 2, 1, 0);
    program_push_disable_aout(pm, 5, 1);
    program_push_disable_ain(pm, 4, 1);

    botnana_abort_program(botnana);
    sleep(2);
    botnana_attach_event(botnana, "end-of-program", 1, end_of_program);
    botnana_attach_event(botnana, "ain.1.4", 0, handle_ain);
    botnana_attach_event(botnana, "din.1.3", 0, handle_din);
    botnana_empty(botnana);
    sleep(1);
    program_deploy(botnana,pm);
    sleep(1);
    program_run(botnana, pm);
    botnana_get_slave(botnana, 3);
    botnana_get_slave(botnana, 4);

    while (1)
    {
        sleep(1);
        botnana_get_slave_diff(botnana, 3);
        botnana_get_slave_diff(botnana, 4);
    }
    return 0;

}

