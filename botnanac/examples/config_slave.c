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
    printf("on_meaasge: %s\n", src);
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

    //set home method of slave 1 to 35
    config_slave_set_homing_method(botnana, 0, 1, 1, 1);
    //set homing speed 1 of slave 1 to 100000
    config_slave_set_homing_speed_1(botnana, 0, 1, 1, 5000);
    // set homing speed 2 of slave 1 to 1000
    config_slave_set_homing_speed_2(botnana, 0, 1, 1, 2500);
    // set homing acceleration of slave 1 to 2000000
    config_slave_set_homing_acceleration(botnana, 0, 1, 1, 5000);
    // set profile velocity of slave 1 to 200000
    config_slave_set_profile_velocity(botnana, 0, 1, 1, 10000);
    // set profile acceleration of slave 1 to 2000000
    config_slave_set_profile_acceleration(botnana, 0, 1, 1, 25000);
    // set profile deceleration of slave 1 to 2000000
    config_slave_set_profile_deceleration(botnana, 0, 1, 1, 30000);
    // Drive PDO mapping
    config_slave_set_pdo_digital_inputs(botnana, 0, 1, 1, 0);
    config_slave_set_pdo_demand_position(botnana, 0, 1, 1, 1);
    config_slave_set_pdo_demand_velocity(botnana, 0, 1, 1, 1);
    config_slave_set_pdo_demand_torque(botnana, 0, 1, 1, 1);
    config_slave_set_pdo_real_velocity(botnana, 0, 1, 1, 1);
    config_slave_set_pdo_real_torque(botnana, 0, 1, 1, 1);
    // get slave information
    config_slave_get(botnana, 0, 1, 1);
    sleep(1);

    //set home method of slave 1 to 35
    config_slave_set_homing_method(botnana, 0, 1, 1, 35);
    //set homing speed 1 of slave 1 to 100000
    config_slave_set_homing_speed_1(botnana, 0, 1, 1, 10000);
    // set homing speed 2 of slave 1 to 1000
    config_slave_set_homing_speed_2(botnana, 0, 1, 1, 1000);
    // set homing acceleration of slave 1 to 2000000
    config_slave_set_homing_acceleration(botnana, 0, 1, 1, 50000);
    // set profile velocity of slave 1 to 200000
    config_slave_set_profile_velocity(botnana, 0, 1, 1, 50000);
    // set profile acceleration of slave 1 to 2000000
    config_slave_set_profile_acceleration(botnana, 0, 1, 1, 100000);
    // set profile deceleration of slave 1 to 2000000
    config_slave_set_profile_deceleration(botnana, 0, 1, 1, 100000);
    // Drive PDO mapping
    config_slave_set_pdo_digital_inputs(botnana, 0, 1, 1, 1);
    config_slave_set_pdo_demand_position(botnana, 0, 1, 1, 0);
    config_slave_set_pdo_demand_velocity(botnana, 0, 1, 1, 0);
    config_slave_set_pdo_demand_torque(botnana, 0, 1, 1, 0);
    config_slave_set_pdo_real_velocity(botnana, 0, 1, 1, 0);
    config_slave_set_pdo_real_torque(botnana, 0, 1, 1, 0);
    //config_slave_get
    config_slave_get(botnana, 0, 1, 1);
    sleep(1);

    //save configuration
    config_save(botnana);
    sleep(1);

    return 0;

}
