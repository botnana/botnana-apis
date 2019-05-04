#include <stdio.h>
#include <stdlib.h>
#include "botnana.h"

int ws_open = 0;
// 處理 WebSocket on_open
void on_ws_open_cb (const char * src)
{
    ws_open = 1;
}

// 處理 WebSocket 連線異常
void on_ws_error_cb (const char * src)
{
    printf("WS client error: %s\n", src);
    exit(1);
}

// 處理主站傳回的資料
void on_message_cb (const char * src)
{
    printf("on_meaasge: %s \n", src);
}

void on_send_cb (const char * src)
{
    printf("on_send: %s\n", src);
}

int main()
{
    struct Botnana * botnana = botnana_new("192.168.7.2");
    botnana_set_on_open_cb(botnana, on_ws_open_cb);
    botnana_set_on_error_cb(botnana, on_ws_error_cb);
    botnana_set_on_message_cb(botnana, on_message_cb);
    botnana_set_on_send_cb(botnana, on_send_cb);
    botnana_connect(botnana);

    while (ws_open == 0)
    {
        sleep(1);
    }

    // set group name
    config_group_set_name(botnana, 1, "G2XY");
    // set group type to 2D
    config_group_set_type_as_2d(botnana, 1, 3, 2);
    // set group vmax
    config_group_set_vmax(botnana, 1, 0.01);
    // set group amax
    config_group_set_amax(botnana, 1, 2.5);
    // set group jmax
    config_group_set_jmax(botnana, 1, 80.0);
    //config.group.get
    config_group_get(botnana, 1);
    sleep(1);

    // set group name
    config_group_set_name(botnana, 1, "G3XYZ");
    // set group type to 3D
    config_group_set_type_as_3d(botnana, 1, 1, 2, 5);
    //config.group.get
    config_group_get(botnana, 1);
    sleep(1);

    // set group name
    config_group_set_name(botnana, 1, "SINE");
    // set group type to SINE
    config_group_set_type_as_sine(botnana, 1, 2);
    //config.group.get
    config_group_get(botnana, 1);
    sleep(1);

    // set group name
    config_group_set_name(botnana, 1, "G1X");
    // set group type to 1D
    config_group_set_type_as_1d(botnana, 1, 1);
    // set group vmax
    config_group_set_vmax(botnana, 1, 0.05);
    // set group amax
    config_group_set_amax(botnana, 1, 2.0);
    // set group jmax
    config_group_set_jmax(botnana, 1, 40.0);
    // set group mapping

    //config.group.get
    config_group_get(botnana, 1);
    sleep(1);

    //save configuration
    config_save(botnana);
    sleep(1);

    return 0;

}
