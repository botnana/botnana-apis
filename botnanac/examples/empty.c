#include <stdio.h>
#include "botnana.h"


void handle_meaasge (const char * src)
{
    printf("handle_meaasge: %s \n", src);

}


void debug_callback (const char * src)
{
    printf("debug callback: %s \n", src);
}

int main()
{

    struct Botnana * botnana = botnana_connect("192.168.7.2", handle_meaasge);

    botnana_set_debug_callback(botnana, debug_callback);

    //send real time script 'empty  marker empty'
    botnana_motion_evaluate(botnana, "empty  marker empty");

    while (1)
    {
        sleep(1);
    }
    return 0;

}

