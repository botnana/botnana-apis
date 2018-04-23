#include <stdio.h>
#include "botnana.h"


void handle_meaasge (const char * src)
{
	printf("***CMSG: %s ***\n", src);

}


int main() {

	struct Botnana * botnana = botnana_connect("192.168.7.2", handle_meaasge);
	botnana_motion_evaluate(botnana, "words");

	while(1){
		sleep(1);
	}
	return 0;

}
