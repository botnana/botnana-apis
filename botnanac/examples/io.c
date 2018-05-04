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

    // connect to motion server
    struct Botnana * botnana = botnana_connect("192.168.7.2", handle_meaasge);
    // new program
    struct Program * pm = program_new("test");

    // enabel ain channel 1 of slave 4
    program_push_enable_ain(pm, 4, 1);
    // enabel aout channel 1 of slave 5
    program_push_enable_aout(pm, 5, 1);
    // set aout channel 1 to 1024 of slave 5
    program_push_set_aout(pm, 5, 1, 1024);
    // set dout channel 1 to 1 of slave 2
    program_push_set_dout(pm, 2, 1, 1);
    // wait 1000 ms
    program_push_script(pm, "1000 ms");
    // set aout channel 1 to 0 of slave 5
    program_push_set_aout(pm, 5, 1, 0);
    // set dout channel 1 to 0 of slave 2
    program_push_set_dout(pm, 2, 1, 0);
    // disable aout channel 1 of slave 5
    program_push_disable_aout(pm, 5, 1);
    // disabel ain channel 1 of slave 4
    program_push_disable_ain(pm, 4, 1);

    botnana_abort_program(botnana);
    sleep(2);
    // catch "end-of-program" tag
    botnana_attach_event(botnana, "end-of-program", 1, end_of_program);
    // catch "ain.1.4" tag (slave 4 ain channel 1)
    botnana_attach_event(botnana, "ain.1.4", 0, handle_ain);
    // catch "din.1.3" tag (slave 3 din channel 1)
    botnana_attach_event(botnana, "din.1.3", 0, handle_din);

    botnana_empty(botnana);
    sleep(1);
    program_deploy(botnana,pm);
    sleep(1);
    program_run(botnana, pm);

    while (1)
    {
        sleep(1);
        // get slave 3 diff. information
        botnana_get_slave_diff(botnana, 3);
        // get slave 4 diff. information
        botnana_get_slave_diff(botnana, 4);
    }
    return 0;

}

