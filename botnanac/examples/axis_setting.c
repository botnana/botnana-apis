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
	botnana_set_axis_config(botnana, 1, "A1", 0, 2000000 ,"Pulse", 1);
	botnana_get_axis_config(botnana, 1);
	botnana_motion_evaluate(botnana, "1 .axiscfg");
	sleep(1);
	botnana_set_axis_config_name(botnana, 1, "A1N");
	botnana_set_axis_config_encoder_length_unit(botnana, 1, "Meter");
	botnana_set_axis_config_ppu(botnana, 1, 1000000);
	botnana_set_axis_config_home_offset(botnana, 1, 0.5);
	botnana_set_axis_config_encoder_direction(botnana, 1, -1);
	botnana_get_axis_config(botnana, 1);
	botnana_motion_evaluate(botnana, "1 .axiscfg");
	sleep(1);

	botnana_motion_evaluate(botnana, "15000.0e 1 enc-ppu!");
	botnana_motion_evaluate(botnana, "2 1 enc-u!");
	botnana_motion_evaluate(botnana, "1 1 enc-dir!");
	botnana_motion_evaluate(botnana, "0.2e 1 hmofs!");
	botnana_get_axis_config(botnana, 1);
	botnana_motion_evaluate(botnana, "1 .axiscfg");
	sleep(1);
	botnana_save_config(botnana);
	while (1)
	{
	    sleep(1);
	}
	return 0;

}


