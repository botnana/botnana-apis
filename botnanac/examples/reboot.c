#include <stdio.h>
#include <stdlib.h>
#include "botnana.h"

// 處理 WebSocket on_error 事件
void on_ws_error_cb (void * data, const char * src)
{
    int * state = (int *) data;
    *state = 0;
}

// 處理 WebSocket on_open 事件
void on_ws_open_cb (void * data, const char * src)
{
    int * state = (int *) data;
    *state = 2;
}

int main()
{
    int state = 0;
    struct Botnana * botnana = botnana_new("192.168.7.2");
    botnana_set_on_open_cb(botnana, (void *) & state, on_ws_open_cb);
    botnana_set_on_error_cb(botnana, (void *) & state, on_ws_error_cb);
    int reboot_time = 0;
    while (1)
    {
        sleep(2);
        if (state == 0)
        {
            state = 1;
            botnana_connect(botnana);
            printf("WS try connect!\n");
        }

        if (state == 2)
        {
            state = 3;
            reboot(botnana);
            reboot_time++;
            if (reboot_time > 3)
            {
                poweroff(botnana);
            }
            printf("WS Open!\n");
        }

    }
    return 0;
}
