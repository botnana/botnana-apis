#include <stdio.h>
#include <unistd.h>
#include "botnana.h"

// handle message
void handle_meaasge (const char * src)
{
    printf("***CMSG: %s ***\n", src);
}

int main()
{
    // connect to motion server
    struct Botnana * botnana = botnana_connect("192.168.7.2", handle_meaasge);

    // send `words` of real time script
    botnana_motion_evaluate(botnana, "words");

    while(1)
    {
        sleep(1);
    }
    return 0;

}
