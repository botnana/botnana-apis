struct Botnana;

void hello_botnana ();

struct Botnana * connect_to_botnana(char * address, void  (* fn)(char * str));

void motion_evaluate (struct Botnana * desc, char * cmd);


