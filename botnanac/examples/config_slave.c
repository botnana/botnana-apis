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

    //set home method of slave 1 to 35
    config_slave_set(botnana, 1, 1, "homing_method", 1);
    //set homing speed 1 of slave 1 to 100000
    config_slave_set(botnana, 1, 1, "homing_speed_1", 5000);
    // set homing speed 2 of slave 1 to 1000
    config_slave_set(botnana, 1, 1, "homing_speed_2", 2500);
    // set homing acceleration of slave 1 to 2000000
    config_slave_set(botnana, 1, 1, "homing_acceleration", 5000);
    // set profile velocity of slave 1 to 200000
    config_slave_set(botnana, 1,1, "profile_velocity", 10000);
    // set profile acceleration of slave 1 to 2000000
    config_slave_set(botnana, 1,1, "profile_acceleration", 25000);
    // set profile deceleration of slave 1 to 2000000
    config_slave_set(botnana, 1,1, "profile_deceleration", 30000);

    // get slave information
    config_slave_get(botnana, 1, 1);
    sleep(1);

    //set home method of slave 1 to 35
    config_slave_set(botnana, 1, 1, "homing_method", 35);
    //set homing speed 1 of slave 1 to 100000
    config_slave_set(botnana, 1, 1, "homing_speed_1", 10000);
    // set homing speed 2 of slave 1 to 1000
    config_slave_set(botnana, 1,1, "homing_speed_2", 1000);
    // set homing acceleration of slave 1 to 2000000
    config_slave_set(botnana, 1,1, "homing_acceleration", 50000);
    // set profile velocity of slave 1 to 200000
    config_slave_set(botnana, 1,1, "profile_velocity", 50000);
    // set profile acceleration of slave 1 to 2000000
    config_slave_set(botnana, 1,1, "profile_acceleration", 100000);
    // set profile deceleration of slave 1 to 2000000
    config_slave_set(botnana, 1,1, "profile_deceleration", 100000);

    //config_slave_get
    config_slave_get(botnana, 1, 1);
    sleep(1);

    //save configuration
    config_save(botnana);
    sleep(1);

    return 0;

}
