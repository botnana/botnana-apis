#include <stdio.h>
#include "botnana.h"
#include "program.h"

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
	struct Program * pm = program_new("test");

	program_push_line(pm, "1 reset-fault");
	program_push_line(pm, "100 ms");
	program_push_line(pm, "4 1 pds-goal!");
	program_push_line(pm, "100 ms");
	program_push_line(pm, "hm 1 op-mode!");
	program_push_line(pm, "until-no-requests");
	program_push_line(pm, "100 ms");
	program_push_line(pm, "1 go");
	program_push_line(pm, "1 until-target-reached");
	program_push_line(pm, "pp 1 op-mode!");
	program_push_line(pm, "until-no-requests");
	program_push_line(pm, "100 ms");
	program_push_line(pm, "25000 1 target-p!");
	program_push_line(pm, "pause pause pause pause");
	program_push_line(pm, "1 go");
	program_push_line(pm, "1 until-target-reached");

	botnana_attach_event(botnana, "end-of-program", 1, end_of_program);
	botnana_empty(botnana);
	sleep(1);
	botnana_abort_program(botnana);
	sleep(1);
	program_deploy(botnana,pm);
	sleep(1);
	program_run(botnana, pm);


	while (1)
	{
		sleep(1);
	}
	return 0;

}

