#include <stdio.h>
#include <stdlib.h>
#include "botnana.h"

unsigned int message_count = 0;

// 處理主站傳回的資料
void on_message_cb (const char * src)
{
    message_count = message_count + 1;
    printf("on_meaasge (%u): %s \n", message_count, src);
}

// 處理 WebSocket 連線異常
void on_ws_error_cb (const char * src)
{
    printf("WS client error: %s\n", src);
    exit(1);
}

void on_send_cb (const char * src)
{
    printf("on_send: %s\n", src);
}

int main()
{
    // connect to motion server
    struct Botnana * botnana = botnana_connect("192.168.7.2", on_ws_error_cb);
    botnana_set_on_message_cb(botnana, on_message_cb);
    botnana_set_on_send_cb(botnana, on_send_cb);


    // set group name
    config_group_set_string(botnana, 1, "name", "G2XY");
    // set group type to 1D
    config_group_set_string(botnana, 1, "gtype", "2D");
    // set group vmax
    config_group_set_double(botnana, 1, "vmax", 0.01);
    // set group amax
    config_group_set_double(botnana, 1, "amax", 2.5);
    // set group jmax
    config_group_set_double(botnana, 1, "jmax", 80.0);
    // set group mapping
    config_group_set_mapping(botnana, 1, "3,2");
    //config.group.get
    config_group_get(botnana, 1);
    sleep(1);

    // set group name
    config_group_set_string(botnana, 1, "name", "G3XYZ");
    // set group type to 1D
    config_group_set_string(botnana, 1, "gtype", "3D");
    // set group mapping
    config_group_set_mapping(botnana, 1, "1,2,5");
    //config.group.get
    config_group_get(botnana, 1);
    sleep(1);

    // set group name
    config_group_set_string(botnana, 1, "name", "G1X");
    // set group type to 1D
    config_group_set_string(botnana, 1, "gtype", "1D");
    // set group vmax
    config_group_set_double(botnana, 1, "vmax", 0.05);
    // set group amax
    config_group_set_double(botnana, 1, "amax", 2.0);
    // set group jmax
    config_group_set_double(botnana, 1, "jmax", 40.0);
    // set group mapping
    config_group_set_mapping(botnana, 1, "1");

    //config.group.get
    config_group_get(botnana, 1);
    sleep(1);

    //save configuration
    config_save(botnana);
    sleep(1);

    return 0;

}
