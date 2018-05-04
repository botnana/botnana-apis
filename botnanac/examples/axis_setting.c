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
    struct Botnana * botnana = botnana_connect("192.168.7.2", handle_meaasge);
    botnana_enable_debug(botnana);

    // set name of axis 1
    botnana_set_axis_config_name(botnana, 1, "A1");
    // set encoder length unit of axis 1
    botnana_set_axis_encoder_length_unit(botnana, 1, "Revolution");
    // set pulse per unit of axis 1
    botnana_set_axis_ppu(botnana, 1, 2000000);
    // set home offset of axis 1
    botnana_set_axis_home_offset(botnana, 1, 0.5);
    // set encoder direction of axis 1
    botnana_set_axis_encoder_direction(botnana, 1, -1);
    // get configuration of axis 1
    botnana_get_axiscfg(botnana, 1);
    // get real time axis configuration of axis 1
    botnana_get_rt_axiscfg(botnana, 1);
    sleep(1);
    // set name of axis 1
    botnana_set_axis_config_name(botnana, 1, "A1N");
    // set encoder length unit of axis 1
    botnana_set_axis_encoder_length_unit(botnana, 1, "Meter");
    // set pulse per unit of axis 1
    botnana_set_axis_ppu(botnana, 1, 1000000);
    // set encoder direction of axis 1
    botnana_set_axis_encoder_direction(botnana, 1, 1);
    // set home offset of axis 1
    botnana_set_axis_home_offset(botnana, 1, 0.2);
    // get configuration of axis 1
    botnana_get_axiscfg(botnana, 1);
    // get real time axis configuration of axis 1
    botnana_get_rt_axiscfg(botnana, 1);
    sleep(1);
    botnana_save_config(botnana);
    while (1)
    {
        sleep(1);
    }
    return 0;

}


