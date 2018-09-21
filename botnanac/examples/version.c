#include <stdio.h>
#include <stdlib.h>
#include "botnana.h"

unsigned int version_count = 0;
unsigned int message_count = 0;

// 處理主站傳回的資料
void on_message_cb (const char * src)
{
    message_count = message_count + 1;
    printf("handle_meaasge (%u): %s \n", message_count, src);
}

// 處理 WebSocket 連線異常
void on_ws_error_cb (const char * src)
{
    printf("WS client error: %s\n", src);
    exit(1);
}

// 收到 version 時的處理機制
void handle_version (const char * src)
{
    version_count = version_count + 1;
    printf("handle_version (%u): %s \n", version_count, src);
}

int main()
{
    // connect to motion server
    struct Botnana * botnana = botnana_connect("192.168.7.2", on_ws_error_cb);

    botnana_set_on_message_cb(botnana, on_message_cb);

    // catch "version" of message tag
    botnana_set_tag_cb(botnana, "version", 12, handle_version);

    while (1)
    {
        // get version
        version_get(botnana);
        sleep(1);
    }
    return 0;

}
