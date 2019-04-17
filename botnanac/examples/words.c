#include <stdio.h>
#include <stdlib.h>
#include "botnana.h"

// 處理主站傳回的資料
void on_message_cb (const char * src)
{
    printf("on_message:  %s\n", src);
}

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
