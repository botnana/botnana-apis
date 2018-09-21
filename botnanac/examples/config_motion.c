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

    // set period_us of motion config.
    config_motion_set(botnana,"period_us" ,1000);
    // set group_capacity of motion config.
    config_motion_set(botnana,"group_capacity",3);
    // set axis_capacity of motion config
    config_motion_set(botnana,"axis_capacity",5);

    // config.motio.get
    config_motion_get(botnana);
    sleep(1);

    // set period_us of motion config.
    config_motion_set(botnana,"period_us" ,2000);
    // set group_capacity of motion config.
    config_motion_set(botnana,"group_capacity",2);
    // set axis_capacity of motion config
    config_motion_set(botnana,"axis_capacity",6);

    // config.motio.get
    config_motion_get(botnana);
    sleep(1);

    //save configuration
    config_save(botnana);
    sleep(1);

    return 0;
}
