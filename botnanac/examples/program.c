#include <stdio.h>
#include "botnana.h"


void handle_meaasge (const char * src)
{
	printf("handle_meaasge: %s \n", src);

}

void end_of_program(const char * src)
{
	printf("end-of-program: %s \n", src);

}


int main() {

	struct Botnana * botnana = botnana_connect("192.168.7.2", handle_meaasge);
	struct Program * pm = botnana_new_program("test");

	botnana_push_program_line(pm, "1 reset-fault");
	botnana_push_program_line(pm, "100 ms");
	botnana_push_program_line(pm, "4 1 pds-goal!");
	botnana_push_program_line(pm, "100 ms");
	botnana_push_program_line(pm, "hm 1 op-mode!");
	botnana_push_program_line(pm, "100 ms");
	botnana_push_program_line(pm, "1 go");
	botnana_push_program_line(pm, "1 until-target-reached");
	botnana_push_program_line(pm, "pp 1 op-mode!");
	botnana_push_program_line(pm, "100 ms");
	botnana_push_program_line(pm, "25000 1 target-p!");
	botnana_push_program_line(pm, "pause pause pause pause");
	botnana_push_program_line(pm, "1 go");
	botnana_push_program_line(pm, "1 until-target-reached");

	botnana_attach_event(botnana, "end-of-program", 1, end_of_program);
	botnana_empty(botnana);
	sleep(1);
	botnana_deploy_program(botnana,pm);
	sleep(1);
	botnana_run_program(botnana, pm);


	while (1)
	{
		sleep(1);
	}
	return 0;

}

