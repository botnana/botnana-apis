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
    // connect to motion server
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

    config_motion_get(botnana);
    // set period_us of motion config.
    config_motion_set_period_us(botnana, 1000);
    // set group_capacity of motion config.
    config_motion_set_group_capacity(botnana, 3);
    // set axis_capacity of motion config
    config_motion_set_axis_capacity(botnana, 5);
    // config.motio.get
    config_motion_get(botnana);
    sleep(1);

    // set period_us of motion config.
    config_motion_set_period_us(botnana, 2000);
    // set group_capacity of motion config.
    config_motion_set_group_capacity(botnana, 2);
    // set axis_capacity of motion config
    config_motion_set_axis_capacity(botnana, 6);
    // config.motio.get
    config_motion_get(botnana);
    sleep(1);

    //save configuration
    config_save(botnana);
    sleep(1);

    return 0;
}
