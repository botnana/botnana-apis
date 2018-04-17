#include <stdio.h>
#include "botnana.h"

void handle_meaasge (const char * src)
{
	printf("C handle_meaasge: %s \n", src);
	
}

int main() {

	struct Botnana * botnana = connect_to_botnana("192.168.7.2:3012", handle_meaasge);
	
	while (1)
	{
		sleep(1);
	}
	return 0;

}

