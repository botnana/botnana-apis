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
    // connect to motion server
    struct Botnana * botnana = botnana_connect("192.168.7.2", handle_meaasge);

    // get slave information
    botnana_get_slave(botnana, 1);
    sleep(1);

    //set home method of slave 1 to 35
    botnana_set_slave(botnana, 1, "homing_method", 35);
    sleep(1);

    //set homing speed 1 of slave 1 to 100000
    botnana_set_slave(botnana, 1, "homing_speed_1", 100000);
    sleep(1);

    // set homing speed 2 of slave 1 to 1000
    botnana_set_slave(botnana, 1, "homing_speed_2", 1000);
    sleep(1);

    // set homing acceleration of slave 1 to 2000000
    botnana_set_slave(botnana, 1, "homing_acceleration", 2000000);
    sleep(1);

    // set profile velocity of slave 1 to 200000
    botnana_set_slave(botnana, 1, "profile_velocity", 200000);
    sleep(1);

    // set profile acceleration of slave 1 to 2000000
    botnana_set_slave(botnana, 1, "profile_acceleration", 2000000);
    sleep(1);

    // set profile deceleration of slave 1 to 2000000
    botnana_set_slave(botnana, 1, "profile_deceleration", 2000000);
    sleep(1);

    // get slave diff. information
    botnana_get_slave_diff(botnana, 1);
    sleep(1);

    //set home method of slave 1 to 33
    botnana_set_slave(botnana, 1, "homing_method", 33);
    sleep(1);

    // get slave diff. information
    botnana_get_slave_diff(botnana, 1);
    sleep(1);

    //save configuration
    botnana_save_config(botnana);

    while (1)
    {
        sleep(1);
    }
    return 0;

}
