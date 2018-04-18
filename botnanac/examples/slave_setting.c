#include <stdio.h>
#include "botnana.h"
#include "json_api.h"

void handle_meaasge (const char * src)
{
	printf("C handle_meaasge: %s \n", src);

}


int main() {

	struct Botnana * botnana = connect_to_botnana("192.168.7.2:3012", handle_meaasge);
	get_slave(botnana, 1);
	sleep(1);
	set_slave(botnana, 1, "homing_method", 33);
	sleep(1);
	set_slave(botnana, 1, "home_offset", 0);
	sleep(1);
	set_slave(botnana, 1, "homing_speed_1", 100000);
	sleep(1);
	set_slave(botnana, 1, "homing_speed_2", 1000);
	sleep(1);
	set_slave(botnana, 1, "homing_acceleration", 2000000);
	sleep(1);
	set_slave(botnana, 1, "profile_velocity", 200000);
	sleep(1);
	set_slave(botnana, 1, "profile_acceleration", 2000000);
	sleep(1);
	set_slave(botnana, 1, "profile_deceleration", 2000000);
	sleep(1);
	get_slave_diff(botnana, 1);
	sleep(1);
	set_slave(botnana, 1, "homing_method", 35);
	sleep(1);
	get_slave_diff(botnana, 1);
	sleep(1);
	set_slave_config(botnana, 1, "homing_method", 33);
	sleep(1);
	set_slave_config(botnana, 1, "home_offset", 0);
	sleep(1);
	set_slave_config(botnana, 1, "homing_speed_1", 100000);
	sleep(1);
	set_slave_config(botnana, 1, "homing_speed_2", 1000);
	sleep(1);
	set_slave_config(botnana, 1, "homing_acceleration", 123456);
	sleep(1);
	set_slave_config(botnana, 1, "profile_velocity", 1234567);
	sleep(1);
	set_slave_config(botnana, 1, "profile_acceleration", 1234567);
	sleep(1);
	set_slave_config(botnana, 1, "profile_deceleration", 1234567);
	sleep(1);
	save_config(botnana);
	while (1)
	{
		sleep(1);
	}
	return 0;

}
