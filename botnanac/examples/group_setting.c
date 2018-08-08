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

    // set group name
    botnana_set_group_config_name(botnana, 1, "G1N");

    // set group type to 1D
    botnana_set_group_config_gtype(botnana, 1, "1D");

    // set axis mapping of group
    botnana_set_group_map1d(botnana, 1, 1);

    // set group type to 2D
    botnana_set_group_config_gtype(botnana, 1, "2D");

    // set axis mapping of group
    botnana_set_group_map2d(botnana, 1, 1, 2);

    // set group type to 3D
    botnana_set_group_config_gtype(botnana, 1, "3D");

    // set axis mapping of group
    botnana_set_group_map3d(botnana, 1, 1, 2, 3);


    // set group vmax
    botnana_set_group_vmax(botnana, 1, 0.5);

    // set group amax
    botnana_set_group_amax(botnana, 1, 2.5);

    // set group jmax
    botnana_set_group_jmax(botnana, 1, 80.5);

    // get group configuration
    botnana_get_grpcfg(botnana, 1);

    // get real time group configuration
    botnana_get_rt_grpcfg(botnana, 1);

    sleep(1);

    // set group vmax
    botnana_set_group_vmax(botnana, 1, 0.6);

    // set group amax
    botnana_set_group_amax(botnana, 1, 2.5);

    // set group jmax
    botnana_set_group_jmax(botnana, 1, 70.0);

    // get group configuration
    botnana_get_grpcfg(botnana, 1);

    // get real time group configuration
    botnana_get_rt_grpcfg(botnana, 1);

    sleep(1);

    // save configuration
    botnana_save_config(botnana);
    while (1)
    {
        sleep(1);
    }
    return 0;

}
