#include <stdio.h>
#include <stdlib.h>
#include "botnana.h"

// 處理 WebSocket on_open
void on_ws_open_cb (void * data, const char * src)
{
    int * open = (int *) data;
    *open = 1;
    printf("WS Open: %s\n", src);
}

// 處理 WebSocket 連線異常
void on_ws_error_cb (void * data, const char * src)
{
    int * open = (int *) data;
    *open = 0;
    printf("WS client error: %s\n", src);
    exit(1);
}

// 處理主站傳回的資料
void on_message_cb (void * data, const char * src)
{
    printf("on_meaasge: %s \n", src);
}

void on_send_cb (void * data, const char * src)
{
    printf("on_send: %s\n", src);
}

int main()
{
    int ws_open = 0;
    // connect to motion server
    struct Botnana * botnana = botnana_new("192.168.7.2");
    botnana_set_on_open_cb(botnana, (void *) & ws_open, on_ws_open_cb);
    botnana_set_on_error_cb(botnana, (void *) & ws_open, on_ws_error_cb);
    botnana_set_on_message_cb(botnana, NULL, on_message_cb);
    botnana_set_on_send_cb(botnana, NULL, on_send_cb);
    botnana_connect(botnana);

    // 等待 WS 連線
    while (ws_open == 0)
    {
        sleep(1);
    }

    // set name of axis 1
    config_axis_set_name(botnana, 1, "ATest");
    // 設定 Axis 參數檔中的編碼器參考單位
    config_axis_set_encoder_length_unit_as_revolution(botnana, 1);
    // 設定 Axis 參數檔中的每編碼器參考單位對應的脈波數
    config_axis_set_encoder_ppu(botnana, 1, 2000000.0);
    config_axis_set_ext_encoder_ppu(botnana, 1, 3000000.0);
    // 設定 Axis 參數檔中的位置雙回授濾波器的截止頻率
    config_axis_set_closed_loop_filter(botnana, 1, 15.0);
    config_axis_set_max_position_deviation(botnana, 1, 0.05);
    // 設定 Axis 參數檔中的 home offset
    config_axis_set_home_offset(botnana, 1, 0.5);
    // 設定 Axis 參數檔中的編碼器方向
    config_axis_set_encoder_direction(botnana, 1, -1);
    config_axis_set_ext_encoder_direction(botnana, 1, -1);
    // 設 Axis 的設定檔參數 (vmax, amax)
    config_axis_set_vmax(botnana, 1, 0.55);
    config_axis_set_amax(botnana, 1, 2.5);
    // 設Axis 對應驅動器的設定檔參數 （alias, position, channel）
    config_axis_set_drive_alias(botnana, 1, 12);
    config_axis_set_drive_slave_position(botnana, 1, 2);
    config_axis_set_drive_channel(botnana, 1, 3);
    // 設定外部光學尺的設定檔參數 （alias, position, channel）
    config_axis_set_ext_encoder_alias(botnana, 1, 13);
    config_axis_set_ext_encoder_slave_position(botnana, 1, 3);
    config_axis_set_ext_encoder_channel(botnana, 1, 2);
    // config.axis.get
    config_axis_get(botnana, 1);
    sleep(1);

    // set name of axis 1
    config_axis_set_name(botnana, 1, "A1");
    // 設定 Axis 參數檔中的編碼器參考單位
    config_axis_set_encoder_length_unit_as_meter(botnana, 1);
    // 設定 Axis 參數檔中的每編碼器參考單位對應的脈波數
    config_axis_set_encoder_ppu(botnana, 1, 1000000.0);
    config_axis_set_ext_encoder_ppu(botnana, 1, 1000000.0);
    // 設定 Axis 參數檔中的位置雙回授濾波器的截止頻率
    config_axis_set_closed_loop_filter(botnana, 1, 30.0);
    // 設定 Axis 參數檔中的 home offset
    config_axis_set_home_offset(botnana, 1, 0.0);
    // 設定 Axis 參數檔中的編碼器方向
    config_axis_set_encoder_direction(botnana, 1, 1);
    config_axis_set_ext_encoder_direction(botnana, 1, 1);
    // 設 Axis 的設定檔參數 (vmax, amax)
    config_axis_set_vmax(botnana, 1, 0.5);
    config_axis_set_amax(botnana, 1, 2.0);
    // 設Axis 對應驅動器的設定檔參數 （alias, position, channel）
    config_axis_set_drive_alias(botnana, 1, 0);
    config_axis_set_drive_slave_position(botnana, 1, 0);
    config_axis_set_drive_channel(botnana, 1, 0);
    // 設定外部光學尺的設定檔參數 （alias, position, channel）
    config_axis_set_ext_encoder_alias(botnana, 1, 0);
    config_axis_set_ext_encoder_slave_position(botnana, 1, 0);
    config_axis_set_ext_encoder_channel(botnana, 1, 0);
    // config.axis.get
    config_axis_get(botnana, 1);

    // save configuration
    config_save(botnana);
    sleep(1);
    return 0;
}
