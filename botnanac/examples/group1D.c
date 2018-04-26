#include <stdio.h>
#include "botnana.h"
#include "program.h"


void handle_meaasge (const char * src)
{
	//printf("handle_meaasge: %s \n", src);

}

void end_of_program(const char * src)
{
	printf("end-of-program: %s \n", src);

}

void handle_acs(const char * src)
{
	printf("ACS: %s ,", src);
}

void handle_pcs(const char * src)
{
	printf("PCS: %s \n", src);
}

void handle_pos(const char * src)
{
	printf("POS: %s ", src);
}

void handle_feedback(const char * src)
{
	printf("FEEDBACK: %s ", src);
}



int main() {

	struct Botnana * botnana = botnana_connect("192.168.7.2", handle_meaasge);
	struct Program * pm = program_new("test");
	//botnana_enable_debug(botnana);
	botnana_attach_event(botnana, "end-of-program", 0, end_of_program);
	botnana_attach_event(botnana, "ACS.1", 0, handle_acs);
	botnana_attach_event(botnana, "PCS.1", 0, handle_pcs);
	botnana_attach_event(botnana, "axis_command_position.1", 0, handle_pos);
	botnana_attach_event(botnana, "axis_corrected_position.1", 0, handle_feedback);
	botnana_motion_evaluate(botnana, "1 .axis 1 .group");
	sleep(1);
	program_push_line(pm, "-coordinator");
	program_push_line(pm, "1 group! -group 0path");
	program_push_line(pm, "1 reset-fault");
	program_push_line(pm, "1 until-no-fault");
	program_push_line(pm, "8 1  op-mode!");
	program_push_line(pm, "until-no-requests");
	program_push_line(pm, "1 servo-on");
	program_push_line(pm, "1 until-servo-on");
	program_push_line(pm, "2000 ms");
	program_push_line(pm, "+coordinator");
	program_push_line(pm, "start");
	program_push_line(pm, "2000 ms");
	program_push_line(pm, "1 group! +group");
	program_push_line(pm, "0.0e move1d");
	program_push_line(pm, "0.1e feedrate!");
	program_push_line(pm, "0.5e line1d");
	program_push_line(pm, "0.1e feedrate!");
	program_push_line(pm, "1.0e line1d");
	program_push_line(pm, "0.2e feedrate!");
	program_push_line(pm, "0.0e line1d");
	program_push_line(pm, "0.5e vcmd!");
	program_push_line(pm, "1 until-grp-end");
	program_push_line(pm, "-coordinator");

	botnana_abort_program(botnana);
	sleep(1);
	botnana_empty(botnana);
	sleep(1);
	program_deploy(botnana,pm);
	sleep(1);
	program_run(botnana, pm);


	while (1)
	{

		botnana_motion_evaluate(botnana, "1 .axis 1 .group");
		sleep(1);
	}
	return 0;

}

