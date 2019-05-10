#include <stdio.h>
#include <stdlib.h>
#include "botnana.h"

struct Botnana * botnana;

int ws_state = 0;
// 處理 WebSocket on_error 事件
void on_ws_error_cb (const char * src)
{
    ws_state = 0;
}

// 處理 WebSocket on_open 事件
int on_open_once = 1;
void on_ws_open_cb (const char * src)
{
    ws_state = 2;
    on_open_once = 0;
}

int main()
{
    botnana = botnana_new("192.168.7.2");
    botnana_set_on_open_cb(botnana, on_ws_open_cb);
    botnana_set_on_error_cb(botnana, on_ws_error_cb);
    botnana_connect(botnana);
    ws_state = 1;
    int reboot_time = 0;
    while (1)
    {
        sleep(2);
        if (ws_state == 0)
        {
            ws_state = 1;
            botnana_connect(botnana);
            printf("WS try connect!\n");
        }

        if (on_open_once == 0)
        {
            printf("WS Open!\n");
            on_open_once = 1;
            reboot(botnana);
            reboot_time++;
            if (reboot_time > 3)
            {
                poweroff(botnana);
            }
        }

    }
    return 0;
}
