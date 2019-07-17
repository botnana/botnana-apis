#include <stdio.h>
#include <stdlib.h>
//#include <unistd.h>
#include "botnana.h"

// 處理主站傳回的資料
void on_message_cb (void* ptr, const char * src)
{
    unsigned int * count = (unsigned int *) ptr;
    *count += 1;
    printf("handle_meaasge (%u): %s \n", *count, src);
}

// 處理 WebSocket on_open
void on_ws_open_cb (void* ptr, const char * src)
{
    int * open = (int *) ptr;
    *open = 2;
    printf("%s\n", src);
}

// 處理 WebSocket 連線異常
void on_ws_error_cb (void* ptr, const char * src)
{
    int * open = (int *) ptr;
    *open = 0;
    printf("WS client error: %s\n", src);
}

// 收到 version 時的處理機制
void handle_version (void* ptr, const char * src)
{
    unsigned int * count = (unsigned int *) ptr;
    count[0] = count[0] + 1;
    printf("handle_version (%u): %s \n", *count, src);
}

int main()
{
    unsigned int version_count = 0;
    unsigned int message_count = 0;
    int ws_open = 0;

    struct Botnana * botnana = botnana_new("192.168.7.2");
    botnana_set_on_open_cb(botnana, (void *) & ws_open, on_ws_open_cb);
    botnana_set_on_error_cb(botnana, (void *) & ws_open, on_ws_error_cb);
    botnana_set_on_message_cb(botnana, (void *) & message_count, on_message_cb);
    botnana_set_tag_cb(botnana, "version", 12, (void *) &version_count, handle_version);

    printf("*** Rust library version = %s \n***", rust_library_version());
    printf("*** library version = %s \n***", library_version());

    while (1)
    {
        // get version
        if (ws_open == 2)
        {
            version_get(botnana);
        }
        else if (ws_open == 0)
        {
            ws_open = 1;
            botnana_connect(botnana);
        }
        sleep(1);
    }
    return 0;

}
