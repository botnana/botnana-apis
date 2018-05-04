#include <stdio.h>
#include "botnana.h"

unsigned int message_count = 0;

void handle_meaasge (const char * src)
{
    message_count = message_count + 1;
    printf("handle_meaasge (%u): %s \n", message_count, src);
}

int main()
{
    // connect motion server
    struct Botnana * botnana = botnana_connect("192.168.7.2", handle_meaasge);

    // get motion config.
    botnana_get_motioncfg(botnana);

    //get real time motion info.
    botnana_get_rt_motioncfg(botnana);
    sleep(1);

    // set period_us of motion config.
    botnana_set_motion_config_period_us(botnana, 2000);

    // set group_capacity of motion config.
    botnana_set_motion_config_group_capacity(botnana,2);

    // set axis_capacity of motion config
    botnana_set_motion_config_axis_capacity(botnana,6);
    sleep(1);

    // get motion configuration
    botnana_get_motioncfg(botnana);

    //get real time motion info.
    botnana_get_rt_motioncfg(botnana);
    while (1)
    {
        sleep(1);
    }
    return 0;

}
