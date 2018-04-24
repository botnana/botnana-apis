#include <stdio.h>
#include "botnana.h"

unsigned int message_count = 0;

void handle_meaasge (const char * src)
{
	message_count = message_count + 1;
	printf("handle_meaasge (%u): %s \n", message_count, src);
}


int main() {
	struct Botnana * botnana = botnana_connect("192.168.7.2", handle_meaasge);
	//botnana_enable_debug(botnana);
	botnana_set_group_config(botnana, 1, "G1", "1D", "1", 0.5, 2.0, 80.0);
	botnana_get_group_config(botnana, 1);
	botnana_motion_evaluate(botnana, "1 .grpcfg");
	sleep(1);
	botnana_set_group_config_name(botnana, 1, "G1N");
	botnana_set_group_config_gtype(botnana, 1, "2D", "1,2");
	botnana_set_group_config_vmax(botnana, 1, 0.55);
	botnana_set_group_config_amax(botnana, 1, 2.76);
	botnana_set_group_config_jmax(botnana, 1, 75.3);
	botnana_get_group_config(botnana, 1);
	botnana_motion_evaluate(botnana, "1 .grpcfg");
	sleep(1);
	botnana_motion_evaluate(botnana, "0.45e 1 gvmax!");
	botnana_motion_evaluate(botnana, "2.5e 1 gamax!");
	botnana_motion_evaluate(botnana, "76.0e 1 gjmax!");
	botnana_motion_evaluate(botnana, "2 3 1 map2d");
	botnana_get_group_config(botnana, 1);
	botnana_motion_evaluate(botnana, "1 .grpcfg");
	sleep(1);
	botnana_set_group_config(botnana, 2, "G2", "1D", "2", 0.6, 3, 70);
	sleep(1);
	botnana_get_group_config(botnana, 2);
	sleep(1);
	botnana_save_config(botnana);
	while (1)
	{
		sleep(1);
	}
	return 0;

}
