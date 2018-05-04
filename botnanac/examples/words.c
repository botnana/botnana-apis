#include <stdio.h>
#include <unistd.h>
#include "botnana.h"

// 處理主站傳回的資料
void handle_meaasge (const char * src)
{
    printf("***CMSG: %s ***\n", src);
}

int main()
{
    // 連結主站
    struct Botnana * botnana = botnana_connect("192.168.7.2", handle_meaasge);

    // 送出命令 `words`
    botnana_motion_evaluate(botnana, "words");

    while(1)
    {
        sleep(1);
    }
    return 0;

}
