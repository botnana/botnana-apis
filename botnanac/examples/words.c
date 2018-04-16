#include <stdio.h>
#include "botnana.h"


void * botnana;


void handle_meaasge (char * src)
{
	printf("handle_meaasge: %s \n", src);

}


int main() {

	botnana = connect_to_botnana("192.168.7.2:3012", handle_meaasge);
	get_words(botnana);

	while (1)
	{
		sleep(1);
	}
	return 0;

}
