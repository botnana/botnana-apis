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

    // set name of axis 1
    config_axis_set_string(botnana, 1, "name","A1");
    // set encoder length unit of axis 1
    config_axis_set_string(botnana, 1, "encoder_length_unit","Revolution");
    // set pulse per unit of axis 1
    config_axis_set_double(botnana, 1, "encoder_ppu",2000000);
    // set home offset of axis 1
    config_axis_set_double(botnana, 1, "home_offset" ,0.5);
    // set encoder direction of axis 1
    config_axis_set_integer(botnana, 1, "encoder_direction",-1);
    // Set slave position
    config_axis_set_integer(botnana, 1, "slave_position",2);
    // Set drive channel
    config_axis_set_integer(botnana, 1, "drive_channel",3);
    // config.axis.get
    config_axis_get(botnana, 1);
    sleep(1);

    // set name of axis 1
    config_axis_set_string(botnana, 1, "name","A2");
    // set encoder length unit of axis 1
    config_axis_set_string(botnana, 1, "encoder_length_unit","Meter");
    // set pulse per unit of axis 1
    config_axis_set_double(botnana, 1, "encoder_ppu", 1000000);
    // set home offset of axis 1
    config_axis_set_double(botnana, 1, "home_offset", 0.0);
    // set encoder direction of axis 1
    config_axis_set_integer(botnana, 1, "encoder_direction", 1);
    // Set slave position
    config_axis_set_integer(botnana, 1, "slave_position", 1);
    // Set drive channel
    config_axis_set_integer(botnana, 1, "drive_channel", 1);
    sleep(1);
    // save configuration
    config_save(botnana);
    sleep(1);
    return 0;

}


