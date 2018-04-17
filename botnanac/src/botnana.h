struct Botnana;

void hello_botnana ();

struct Botnana * connect_to_botnana(const char * address, void (* fn)(const char * str));

void motion_evaluate (struct Botnana * desc, const char * cmd);

void attach_function_to_event(struct Botnana * desc, const char * event, unsigned int count, void  (* fn)(const char * str));

