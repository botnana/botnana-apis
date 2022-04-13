#include <stdio.h>
#include <stdlib.h>
#include "botnana.h"

int main()
{
    // connect to motion server
    printf("Creating botnana client to 192.168.7.2\n");
    struct Botnana * botnana = botnana_new("192.168.7.2");
    printf("Connecting to modbus server at 192.168.7.2\n");
    botnana_mb_connect(botnana);
    printf("Modbus server connected\n");
    // 等待連線成功
    while (1)
    {
        botnana_mb_update(botnana);
        printf("bit[10001]: %d, i16[30001]: %d, u16[30001]: %d, i32[30002]: %d, u32[30002]: %u\n",
            botnana_mb_bit(botnana, 10001),
            botnana_mb_i16(botnana, 30001),
            botnana_mb_u16(botnana, 30001),
            botnana_mb_i32(botnana, 30002),
            botnana_mb_u32(botnana, 30002));
        sleep(2);
    }

    return 0;

}
