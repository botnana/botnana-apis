#include <stdio.h>
#include <stdlib.h>
#include <string.h>
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

void end_of_program (const char * src)
{
    printf("end_of_program!!\n");
}

int deployed_ok = 0;
void deployed_cb (const char * src)
{
    deployed_ok = 1;
}

int has_params = 0;
void group_type_cb (const char * src)
{
    has_params |= 0x01;
    if (strcmp(src, "1D") == 0)
    {
        has_params |= 0x10;
    }
}

void group_mapping_cb (const char * src)
{
    has_params |= 0x02;
    if (strcmp(src, "1") == 0)
    {
        has_params |= 0x20;
    }

}

void axis_slave_position_cb (const char * src)
{
    int pos = atoi(src);
    has_params |= 0x04;
    if (pos == 1)
    {
        has_params |= 0x40;
    }
}

void axis_drive_channel_cb (const char * src)
{
    int ch = atoi(src);
    has_params |= 0x08;
    if (ch == 1)
    {
        has_params |= 0x80;
    }
}

double acs = 0.0;
void acs_cb (const char * src)
{
    acs = atof(src);
}

double pcs = 0.0;
void pcs_cb (const char * src)
{
    pcs = atof(src);
}


int main()
{
    // connect to motion server
    struct Botnana * botnana = botnana_connect("192.168.7.2", on_ws_error_cb);
    //botnana_set_on_message_cb(botnana, on_message_cb);
    //botnana_set_on_send_cb(botnana, on_send_cb);

    botnana_set_tag_cb(botnana, "end-of-program", 0, end_of_program);
    botnana_set_tag_cb(botnana, "deployed", 0, deployed_cb);
    //botnana_set_tag_cb(botnana, "real_position.1.1", 0, real_position_cb);

    botnana_set_tag_cb(botnana, "group_type.1", 0, group_type_cb);
    botnana_set_tag_cb(botnana, "group_mapping.1", 0, group_mapping_cb);
    botnana_set_tag_cb(botnana, "axis_slave_position.1", 0, axis_slave_position_cb);
    botnana_set_tag_cb(botnana, "axis_drive_channel.1", 0, axis_drive_channel_cb);
    botnana_set_tag_cb(botnana, "ACS.1", 0, acs_cb);
    botnana_set_tag_cb(botnana, "PCS.1", 0, pcs_cb);

    // new program
    struct Program * pm = program_new("group1d");

    motion_evaluate(botnana, "abort-program");
    motion_evaluate(botnana, "1 .slave 1 .grpcfg 1 .axiscfg");

    while ((has_params & 0xF) != 0xF)
    {
        sleep(1);
    }
    if (has_params != 0xFF)
    {
        config_group_set_string(botnana, 1, "gtype", "1D");
        config_group_set_mapping(botnana, 1, "1");
        config_axis_set_integer(botnana, 1, "slave_position", 1);
        config_axis_set_integer(botnana, 1, "drive_channel", 1);
        config_save(botnana);
        printf("Change parameters and reboot botnana-control !!\n");
        sleep(1);
        exit(1);
    }

    program_line(pm, "1 1 reset-fault");
    program_line(pm,"1 1 until-no-fault");
    program_line(pm,"pp 1 1 op-mode!");
    program_line(pm,"1 1 servo-on");
    program_line(pm,"1 1 until-servo-on");
    program_line(pm,"1 1 go");
    program_line(pm,"1 1 until-target-reached");
    program_line(pm,"csp 1 1 op-mode!");

    program_line(pm,"+coordinator");
    program_line(pm,"1 group! 0path +group");
    program_line(pm,"0.0e move1d");
    program_line(pm,"0.5e line1d");
    program_line(pm,"-0.5e line1d");
    program_line(pm,"0.0e line1d");
    program_line(pm,"0.05e vcmd! start");
    program_line(pm,"pause pause");
    program_line(pm,"begin 1 group! gstop? not while pause repeat");
    program_line(pm,"1 group! 0path -group -coordinator");

    // deploy program to motion server
    motion_evaluate(botnana, "-work marker -work");
    program_deploy(botnana,pm);

    while (deployed_ok == 0)
    {
        sleep(1);
    }

    // run program
    program_run(botnana, pm);
    while (1)
    {
        motion_evaluate(botnana, "1 .slave-diff 1 .axis 1 .group");
        printf("ACS: %8.4f   PCS: %8.4f\n", acs, pcs);
        sleep(1);
    }
    return 0;

}

