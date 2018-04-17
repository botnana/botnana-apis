struct Botnana;

void hello_botnana ();

struct Botnana * connect_to_botnana(char * address, void (* fn)(char * str));

void motion_evaluate (struct Botnana * desc, char * cmd);

void attach_function_to_event(struct Botnana * desc, char * event, unsigned int count, void  (* fn)(char * str));

