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
	botnana_enable_debug(botnana);
	botnana_set_motion_config(botnana, 3000, 3, 5);
	botnana_get_motion_config(botnana);
	botnana_motion_evaluate(botnana, ".motion");
	sleep(1);
	botnana_set_motion_config_period_us(botnana, 2000);
	botnana_set_motion_config_group_capacity(botnana,2);
	botnana_set_motion_config_axis_capacity(botnana,6);
	botnana_get_motion_config(botnana);
	botnana_motion_evaluate(botnana, ".motion");
	sleep(1);
	botnana_save_config(botnana);
	while (1)
	{
		sleep(1);
	}
	return 0;

}
