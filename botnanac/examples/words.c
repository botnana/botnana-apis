#include <stdio.h>
#include <stdlib.h>
#include "botnana.h"

// 處理主站傳回的資料
void on_message_cb (const char * src)
{
    printf("on_message:  %s\n", src);
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

    // send `words` of real time script
    motion_evaluate(botnana, "words");

    // 新增指令 test1, test2
    motion_evaluate(botnana, ": test1 3 ;");
    motion_evaluate(botnana, ": test2 4 ;");
    motion_evaluate(botnana, "words");
    sleep(1);

    // 將test1, test2 從字典中移除
    motion_evaluate(botnana, "-work marker -work");
    motion_evaluate(botnana, "words");
    sleep(1);

    return 0;

}
