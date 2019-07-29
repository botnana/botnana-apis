#include <stdio.h>
#include <stdlib.h>
#include <pthread.h>
#include <unistd.h>
#include <string.h>
#include <time.h>
#include "botnana.h"

struct Descriptor
{
    struct Botnana * botnana;
    struct Program * pm;
    int trigger;
    int ws_open;
    FILE * fp;
};

// 處理主站傳回的資料
void on_message_cb (void * data, const char * src)
{
    if (strstr(src,"test-recorder") == 0)
    {
        printf("%s", src);
    }
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

void on_send_cb (void * data, const char * src)
{
    printf("on_send: %s\n", src);
}

void recorder_cb  (void * data, const char * src)
{
    struct Descriptor * desc = (struct Descriptor *) data;
    if (desc->trigger == 1)
    {
        fprintf(desc->fp,"%s\n",src);
    }
}

void end_cb (void * data, const char * src)
{
    struct Descriptor * desc = (struct Descriptor *) data;
    if (desc->trigger == 1)
    {
        fclose(desc->fp);
        desc->trigger == 0;
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
    struct Descriptor * desc = (struct Descriptor *) data;
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
                desc->fp = fopen(strcat(timebuf,".txt"),"w+");
                fprintf(desc->fp,"target position, real position, target reached, status word(hex)\n");
                desc->trigger = 1;
                program_run(desc->botnana, desc->pm);
            }
            else
            {
                buffer[strlen(buffer)-1] = 0;
                script_evaluate(desc->botnana, buffer);
            }
        }
    }

    // 離開子執行緒
    pthread_exit(NULL);
}


int main()
{
    struct Descriptor desc;
    pthread_t user_t;

    // connect to motion server
    desc.botnana = botnana_new("192.168.7.2");
    botnana_set_on_open_cb(desc.botnana, (void *)& desc.ws_open, on_ws_open_cb);
    botnana_set_on_error_cb(desc.botnana, (void *)& desc.ws_open, on_ws_error_cb);
    botnana_set_on_message_cb(desc.botnana, NULL, on_message_cb);
    //botnana_set_on_send_cb(botnana, on_send_cb);
    botnana_set_tag_cb(desc.botnana, "test-recorder", 0, (void *) & desc, recorder_cb);
    botnana_set_tag_cb(desc.botnana, "end-of-program", 0, (void *) & desc, end_cb);

    // new program
    desc.pm = program_new("target-reached");
    program_line(desc.pm, "1  1 reset-fault");
    program_line(desc.pm, "pp 1  1 op-mode!");
    program_line(desc.pm, "1000 1  1 profile-v!");
    program_line(desc.pm, "1  1 until-no-requests");
    program_line(desc.pm, "1  1 drive-on 1 1 until-drive-on");
    program_line(desc.pm, "200 ms");
    program_line(desc.pm, "1 1 target-p@ 20000 + 1 1 target-p!");
    program_line(desc.pm, "1 1 go");
    program_line(desc.pm, "0 begin 1 + dup 1200 <= while ");
    program_line(desc.pm, ".\" test-recorder|\" 1 1 target-p@ . 44 emit 1 1 real-p@ . 44 emit 1 1 target-reached? . 44 emit 1 1 drive-sw@ h. cr pause");
    program_line(desc.pm, "repeat");
    program_line(desc.pm, "200 ms");

    botnana_connect(desc.botnana);
    while (desc.ws_open == 0)
    {
        sleep(1);
    }

    // deploy program to motion server
    script_evaluate(desc.botnana, "-work marker -work");
    script_evaluate(desc.botnana, "abort-program");
    sleep(1);
    program_deploy(desc.botnana, desc.pm);

    // 建立子執行緒
    pthread_create(&user_t, NULL, user_input, (void *) & desc);

    pthread_join(user_t, NULL);
    return 0;

}

