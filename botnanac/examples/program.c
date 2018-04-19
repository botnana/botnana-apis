#include <stdio.h>
#include "botnana.h"
//#include "json_api.h"

void handle_meaasge (const char * src)
{
	printf("C handle_meaasge: %s \n", src);

}

int main() {

	struct Botnana * botnana = connect_to_botnana("192.168.7.2:3012", handle_meaasge);
	struct Program * pm = new_program("test");

	empty_program(botnana);

	push_program_line(pm, "1 reset-fault");
	push_program_line(pm, "500 ms");
	push_program_line(pm, "4 1 pds-goal!");
	push_program_line(pm, "500 ms");
	push_program_line(pm, "hm 1 op-mode!");
	push_program_line(pm, "500 ms");
	push_program_line(pm, "1 go");
	push_program_line(pm, "1 until-target-reached");
	push_program_line(pm, "pp 1 op-mode!");
	push_program_line(pm, "500 ms");
	push_program_line(pm, "25000 1 target-p!");
	push_program_line(pm, "1 go");
	push_program_line(pm, "1 until-target-reached");

	deploy_program(botnana,pm);

	sleep(1);
	run_program(botnana, pm);


	sleep(60);
	abort_program(botnana);
	//get_slave(botnana, 1);

	//run_program(botnana, "test");

	while (1)
	{
		sleep(1);
	}
	return 0;

}

