#include <stdio.h>
#include <stdlib.h>
#include "botnana.h"

// 處理主站傳回的資料
void on_message_cb (void * data, const char * src)
{
    printf("on_message:  %s\n", src);
}

// 處理 WebSocket on_open
void on_ws_open_cb (void * data, const char * src)
{
    int * open = (int *) data;
    *open = 2;
}

// 處理 WebSocket 連線異常
void on_ws_error_cb (void * data, const char * src)
{
    int * open = (int *) data;
    *open = 0;
    printf("WS client error: %s\n", src);
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

    botnana_set_on_open_cb(botnana, (void *)& ws_open, on_ws_open_cb);
    botnana_set_on_error_cb(botnana, (void *)& ws_open, on_ws_error_cb);
    botnana_set_on_message_cb(botnana, NULL, on_message_cb);
    botnana_set_on_send_cb(botnana, NULL, on_send_cb);

    // 等待連線成功
    while (ws_open != 2)
    {
        // 如果連線失敗，重新嘗試連線
        if (ws_open == 0)
        {
            botnana_connect(botnana);
            ws_open = 1;
        }
        sleep(2);
    }
    // send `words` of real time script
    script_evaluate(botnana, "words");

    // 新增指令 test1, test2
    script_evaluate(botnana, ": test1 3 ;");
    script_evaluate(botnana, ": test2 4 ;");
    script_evaluate(botnana, "words");
    sleep(1);

    // 將test1, test2 從字典中移除
    script_evaluate(botnana, "-work marker -work");
    script_evaluate(botnana, "words");
    sleep(1);

    return 0;

}
