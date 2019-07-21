#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "botnana.h"

// 處理主站傳回的資料
void on_message_cb (void * data, const char * src)
{
    printf("on_message:  %s\n", src);
}

int ws_open = 0;
// 處理 WebSocket on_open
void on_ws_open_cb (void * data, const char * src)
{
    ws_open = 1;
}

// 處理 WebSocket 連線異常
void on_ws_error_cb (void * data, const char * src)
{
    printf("WS client error: %s\n", src);
    exit(1);
}

void on_send_cb (void * data, const char * src)
{
    printf("on_send: %s\n", src);
}

void end_of_program (void * data, const char * src)
{
    printf("end_of_program|%s\n",src);
}

void deployed_cb (void * data, const char * src)
{
    int * deployed_ok  = (int *) data;
    *deployed_ok = 1;
}


void group_type_cb (void * data, const char * src)
{
    int * has_params  = (int *) data;
    *has_params |= 0x01;
    if (strcmp(src, "1D") == 0)
    {
        *has_params |= 0x10;
    }
}

void group_mapping_cb (void * data, const char * src)
{
    int * has_params  = (int *) data;
    * has_params |= 0x02;
    if (strcmp(src, "1") == 0)
    {
        * has_params |= 0x20;
    }

}

void drive_slave_position_cb (void * data, const char * src)
{
    int * has_params  = (int *) data;
    int pos = atoi(src);
    *has_params |= 0x04;
    if (pos == 1)
    {
        *has_params |= 0x40;
    }
}

void drive_channel_cb (void * data, const char * src)
{
    int * has_params  = (int *) data;
    int ch = atoi(src);
    *has_params |= 0x08;
    if (ch == 1)
    {
        *has_params |= 0x80;
    }
}

void acs_cb (void * data, const char * src)
{
    double * acs = (double *) data;
    *acs = atof(src);
}

void pcs_cb (void * data, const char * src)
{
    double * pcs = (double *) data;
    *pcs = atof(src);
}

void error_cb (void * data, const char * src)
{
    printf("error|%s\n",src);
}

int main()
{
    int has_params = 0;
    double acs = 0.0;
    double pcs = 0.0;
    int deployed_ok = 0;

    // connect to motion server
    struct Botnana * botnana = botnana_new("192.168.7.2");
    botnana_set_on_open_cb(botnana, NULL, on_ws_open_cb);
    botnana_set_on_error_cb(botnana, NULL, on_ws_error_cb);
    //botnana_set_on_message_cb(botnana, on_message_cb);
    //botnana_set_on_send_cb(botnana, on_send_cb);

    botnana_set_tag_cb(botnana, "end-of-program", 0, NULL, end_of_program);
    botnana_set_tag_cb(botnana, "deployed", 0, (void *) & deployed_ok, deployed_cb);
    //botnana_set_tag_cb(botnana, "real_position.1.1", 0, real_position_cb);

    botnana_set_tag_cb(botnana, "group_type.1", 0, (void *) & has_params, group_type_cb);
    botnana_set_tag_cb(botnana, "group_mapping.1", 0, (void *) & has_params, group_mapping_cb);
    botnana_set_tag_cb(botnana, "drive_slave_position.1", 0, (void *) & has_params, drive_slave_position_cb);
    botnana_set_tag_cb(botnana, "drive_channel.1", 0, (void *) & has_params, drive_channel_cb);
    botnana_set_tag_cb(botnana, "ACS.1", 0, (void *) & acs, acs_cb);
    botnana_set_tag_cb(botnana, "PCS.1", 0, (void *) & pcs, pcs_cb);
    botnana_set_tag_cb(botnana, "error", 0, NULL, error_cb);

    // new program
    struct Program * pm = program_new("group1d");

    botnana_connect(botnana);
    while (ws_open == 0)
    {
        sleep(1);
    }
    script_evaluate(botnana, "abort-program");
    script_evaluate(botnana, "1 .slave 1 .grpcfg 1 .axiscfg");

    while ((has_params & 0xF) != 0xF)
    {
        sleep(1);
    }
    if (has_params != 0xFF)
    {
        config_group_set_type_as_1d(botnana, 1, 1);
        config_axis_set_drive_alias(botnana, 1, 0);
        config_axis_set_drive_slave_position(botnana, 1, 1);
        config_axis_set_drive_channel(botnana, 1, 1);
        config_save(botnana);
        printf("Change parameters and reboot Botnana-control !!\n");
        sleep(1);
        exit(1);
    }

    program_line(pm, "1 1 reset-fault");
    program_line(pm,"1 1 until-no-fault");
    program_line(pm,"pp 1 1 op-mode!");
    program_line(pm,"until-no-requests");
    program_line(pm,"1 1 drive-on");
    program_line(pm,"1 1 until-drive-on");
    program_line(pm,"1 1 go");
    program_line(pm,"1 1 until-target-reached");
    program_line(pm,"csp 1 1 op-mode!");
    program_line(pm,"until-no-requests");

    program_line(pm,"+coordinator");
    program_line(pm,"1 group! 0path 1 0axis-ferr +group");
    program_line(pm,"0.0e move1d");
    program_line(pm,"0.5e line1d");
    program_line(pm,"-0.5e line1d");
    program_line(pm,"0.0e line1d");
    program_line(pm,"0.05e vcmd! start-job");
    program_line(pm,"pause pause");
    program_line(pm,"begin 1 group! gstop? not while pause repeat");
    program_line(pm,"1 group! 0path -group -coordinator");

    // deploy program to motion server
    script_evaluate(botnana, "-work marker -work");
    program_deploy(botnana,pm);

    while (deployed_ok == 0)
    {
        sleep(1);
    }

    // run program
    program_run(botnana, pm);
    while (1)
    {
        script_evaluate(botnana, "1 .slave-diff 1 .axis 1 .group");
        printf("ACS: %8.4f   PCS: %8.4f\n", acs, pcs);
        sleep(1);
    }
    return 0;

}

