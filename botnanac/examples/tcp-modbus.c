#include <stdio.h>
#include <stdlib.h>
#include "botnana.h"
void log_cb (void * data, const char * src)
{
    printf("log|%s\n", src);
}

void error_cb (void * data, const char * src)
{
    printf("error|%s\n", src);
}
// 處理 WebSocket on_open
void on_ws_open_cb (void * data, const char * src)
{
    int * open = (int *) data;
    *open = 1;
}

// 處理 WebSocket 連線異常
void on_ws_error_cb (void * data, const char * src)
{
    int * open = (int *) data;
    *open = 0;
    printf("WS client error: %s\n", src);
    exit(1);
}
int main()
{
    FILE *fp;
    fp = fopen("../../motion/examples/hmpppv/drive-pp.fs", "r");
    char buf[1000];

    int ws_open = 0;
    // connect to motion server
    printf("Creating botnana client to 192.168.7.2\n");
    struct Botnana * botnana = botnana_new("192.168.7.2");

    botnana_set_tag_cb(botnana, "log", 0, NULL, log_cb);
    botnana_set_tag_cb(botnana, "error", 0, NULL ,error_cb);
    botnana_set_on_open_cb(botnana, (void *) & ws_open, on_ws_open_cb);
    botnana_set_on_error_cb(botnana, (void *) & ws_open, on_ws_error_cb);

    botnana_connect(botnana);
    while (ws_open == 0)
    {
        sleep(1);
    }
    while (fgets(buf, 1000, fp) != NULL){
        //printf("%s\n", buf);
        script_evaluate(botnana, buf);
    }
    script_evaluate(botnana, "start-task1");
    printf("Connecting to modbus server at 192.168.7.2\n");
    botnana_mb_connect(botnana);
    printf("Modbus server connected\n");
    script_evaluate(botnana, "start-4drive");
    script_evaluate(botnana, "1 1 reset-fault");
    sleep(1);
    botnana_mb_update(botnana);
    script_evaluate(botnana, "1 1 drive-off");
    sleep(1);
    botnana_mb_update(botnana);
    script_evaluate(botnana, "1 1 drive-on");
    sleep(1);
    botnana_mb_update(botnana);
    script_evaluate(botnana, "1 2 reset-fault");
    sleep(1);
    botnana_mb_update(botnana);
    script_evaluate(botnana, "1 2 drive-off");
    sleep(1);
    botnana_mb_update(botnana);
    script_evaluate(botnana, "1 2 drive-on");
    sleep(1);
    botnana_mb_update(botnana);

    botnana_mb_set_u16(botnana, 40045, 2000);
    sleep(1);
    botnana_mb_publish(botnana);
    script_evaluate(botnana, "1 mb-heart-beat!");
    sleep(1);
    botnana_mb_update(botnana);
    script_evaluate(botnana, "2 mb-heart-beat!");
    sleep(3);
    botnana_mb_update(botnana);

    // 等待連線成功
    while (1)
    {
        //script_evaluate(botnana, "1 mb-heart-beat!");
        botnana_mb_update(botnana);
        printf("bit[10001]: %d, i16[30001]: %d, u16[30001]: %d, i32[30002]: %d, u32[30002]: %u\n",
            botnana_mb_bit(botnana, 10001),
            botnana_mb_i16(botnana, 30001),
            botnana_mb_u16(botnana, 30045),
            botnana_mb_i32(botnana, 30002),
            botnana_mb_u32(botnana, 30002));
        sleep(1);
    }

    return 0;

}
