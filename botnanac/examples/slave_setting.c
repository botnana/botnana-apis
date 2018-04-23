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
	botnana_get_slave(botnana, 1);
	sleep(1);
	botnana_set_slave(botnana, 1, "homing_method", 33);
	sleep(1);
	botnana_set_slave(botnana, 1, "home_offset", 0);
	sleep(1);
	botnana_set_slave(botnana, 1, "homing_speed_1", 100000);
	sleep(1);
	botnana_set_slave(botnana, 1, "homing_speed_2", 1000);
	sleep(1);
	botnana_set_slave(botnana, 1, "homing_acceleration", 2000000);
	sleep(1);
	botnana_set_slave(botnana, 1, "profile_velocity", 200000);
	sleep(1);
	botnana_set_slave(botnana, 1, "profile_acceleration", 2000000);
	sleep(1);
	botnana_set_slave(botnana, 1, "profile_deceleration", 2000000);
	sleep(1);
	botnana_get_slave_diff(botnana, 1);
	sleep(1);
	botnana_set_slave(botnana, 1, "homing_method", 35);
	sleep(1);
	botnana_get_slave_diff(botnana, 1);
	sleep(1);
	botnana_set_slave_config(botnana, 1, "homing_method", 33);
	sleep(1);
	botnana_set_slave_config(botnana, 1, "home_offset", 0);
	sleep(1);
	botnana_set_slave_config(botnana, 1, "homing_speed_1", 100000);
	sleep(1);
	botnana_set_slave_config(botnana, 1, "homing_speed_2", 1000);
	sleep(1);
	botnana_set_slave_config(botnana, 1, "homing_acceleration", 5000);
	sleep(1);
	botnana_set_slave_config(botnana, 1, "profile_velocity", 100000);
	sleep(1);
	botnana_set_slave_config(botnana, 1, "profile_acceleration", 500000);
	sleep(1);
	botnana_set_slave_config(botnana, 1, "profile_deceleration", 500000);
	sleep(1);
	botnana_save_config(botnana);
	while (1)
	{
		sleep(1);
	}
	return 0;

}
