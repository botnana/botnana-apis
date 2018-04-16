struct Botnana;

void hello_botnana ();

struct Botnana * connect_to_botnana(char * address, void  (* fn)(char * str));

void motion_evaluate (struct Botnana * desc, char * cmd);

void send_message (struct Botnana * desc, char * cmd_msg, char * expect_msg);

void get_words (struct Botnana * desc);
