#include <stdio.h>
#include "botnana.h"


void handle_meaasge (const char * src)
{
	printf("handle_meaasge: %s \n", src);
	
}

int main() {

	struct Botnana * botnana = botnana_connect("192.168.7.2", handle_meaasge);
	botnana_enable_debug (botnana);
	botnana_motion_evaluate(botnana, "empty  marker empty");
	
	while (1)
	{
		sleep(1);
	}
	return 0;

}

