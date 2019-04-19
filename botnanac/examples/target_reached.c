#include <stdio.h>
#include <stdlib.h>
#include <pthread.h>
#include <unistd.h>
#include <string.h>
#include <time.h>
#include "botnana.h"

struct Botnana * botnana;
struct Program * pm;
int trigger = 0;
FILE * fp;

// 處理主站傳回的資料
void on_message_cb (const char * src)
{
    if (strstr(src,"test-recorder") == 0)
    {
        printf("%s", src);
    }
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

void recorder_cb  (const char * src)
{
    if (trigger == 1)
    {
        fprintf(fp,"%s\n",src);
    }
}

void end_cb  (const char * src)
{
    if (trigger == 1)
    {
        fclose(fp);
        trigger == 0;
    }
}

// 等待輸入命令
void* user_input(void* data)
{
    char *cmd;
    char *format = "%m%d%I%M%S";
    char buffer[1000];
    char timebuf[30];
    time_t clock;
    struct tm *tm;
    while(1)
    {
        cmd = fgets(buffer, 1000, stdin);
        if (strlen(buffer) > 0)
        {
            if (strstr(buffer,"trigger") != 0)
            {
                time(&clock);
                tm =gmtime(&clock);
                strftime(timebuf, sizeof(timebuf), format, tm);
                fp = fopen(strcat(timebuf,".txt"),"w+");
                fprintf(fp,"target position, real position, target reached, status word(hex)\n");
                trigger = 1;
                program_run(botnana, pm);
            }
            else
            {
                buffer[strlen(buffer)-1] = 0;
                script_evaluate(botnana, buffer);
            }
        }
    }

    // 離開子執行緒
    pthread_exit(NULL);
}


int main()
{
    pthread_t user_t;

    // connect to motion server
    botnana = botnana_new("192.168.7.2");
    botnana_set_on_open_cb(botnana, on_ws_open_cb);
    botnana_set_on_error_cb(botnana, on_ws_error_cb);
    botnana_set_on_message_cb(botnana, on_message_cb);
    //botnana_set_on_send_cb(botnana, on_send_cb);
    botnana_set_tag_cb(botnana, "test-recorder", 0, recorder_cb);
    botnana_set_tag_cb(botnana, "end-of-program", 0, end_cb);

    // new program
    pm = program_new("target-reached");
    program_line(pm, "1  1 reset-fault");
    program_line(pm, "pp 1  1 op-mode!");
    program_line(pm, "1000 1  1 profile-v!");
    program_line(pm, "1  1 until-no-requests");
    program_line(pm, "1  1 drive-on 1 1 until-drive-on");
    program_line(pm, "200 ms");
    program_line(pm, "1 1 target-p@ 20000 + 1 1 target-p!");
    program_line(pm, "1 1 go");
    program_line(pm, "0 begin 1 + dup 1200 <= while ");
    program_line(pm, ".\" test-recorder|\" 1 1 target-p@ . 44 emit 1 1 real-p@ . 44 emit 1 1 target-reached? . 44 emit 1 1 drive-sw@ h. cr pause");
    program_line(pm, "repeat");
    program_line(pm, "200 ms");

    botnana_connect(botnana);
    while (ws_open == 0)
    {
        sleep(1);
    }

    // deploy program to motion server
    script_evaluate(botnana, "-work marker -work");
    script_evaluate(botnana, "abort-program");
    sleep(1);
    program_deploy(botnana,pm);

    // 建立子執行緒
    pthread_create(&user_t, NULL, user_input, "User");

    pthread_join(user_t, NULL);
    return 0;

}

