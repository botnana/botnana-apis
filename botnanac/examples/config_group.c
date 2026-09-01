#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include "botnana.h"

static int ws_open = 0;

void on_ws_open_cb(void *data, const char *message)
{
    (void)data;
    (void)message;
    ws_open = 1;
}

void on_ws_error_cb(void *data, const char *message)
{
    (void)data;
    fprintf(stderr, "WS client error: %s\n", message);
    exit(1);
}

void on_message_cb(void *data, const char *message)
{
    (void)data;
    printf("on_message: %s\n", message);
}

int main(void)
{
    struct Botnana *botnana = botnana_new("192.168.7.2");
    botnana_set_on_open_cb(botnana, NULL, on_ws_open_cb);
    botnana_set_on_error_cb(botnana, NULL, on_ws_error_cb);
    botnana_set_on_message_cb(botnana, NULL, on_message_cb);
    botnana_connect(botnana);

    while (!ws_open) {
        sleep(1);
    }

    config_group_get(botnana, 1);
    sleep(1);
    return 0;
}
