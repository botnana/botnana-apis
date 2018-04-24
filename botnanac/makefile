CFLAGS := -Isrc
LDFLAGS := -Ltarget/release -lbotnana


build:
	cargo fmt
	cargo build --release

	
words: build   
	gcc $(CFLAGS) -g -c ./examples/words.c
	gcc -o botnana words.o $(LDFLAGS)
	RUST_BACKTRACE=1 ./botnana 
	
empty: build
	gcc $(CFLAGS) -g -c ./examples/empty.c
	gcc -o botnana empty.o $(LDFLAGS)
	RUST_BACKTRACE=1 ./botnana 	 

	
verbose: build    
	gcc ./examples/verbose.c -Isrc  -Ltarget/release -lbotnana -o verbose
	#./verbose 192.168.7.2:3012
	
slave_setting: build
	gcc $(CFLAGS) -g -c ./examples/slave_setting.c
	gcc -o botnana slave_setting.o $(LDFLAGS)
	RUST_BACKTRACE=1 ./botnana 	
    
version: build    
	gcc $(CFLAGS) -g -c ./examples/version.c
	gcc -o botnana version.o $(LDFLAGS)
	RUST_BACKTRACE=1 ./botnana 	
	
version_debug: build    
	gcc $(CFLAGS) -g -c ./examples/version_debug.c
	gcc -o botnana version_debug.o $(LDFLAGS)
	RUST_BACKTRACE=1 ./botnana 		
	
	
program: build
	gcc $(CFLAGS) -g -c ./examples/program.c
	gcc -o botnana program.o $(LDFLAGS)
	RUST_BACKTRACE=1 ./botnana 	 		
	
program_raw: build
	gcc ./examples/program_raw.c -Isrc  -Ltarget/release -lbotnana -o botnana
	RUST_BACKTRACE=1 ./botnana		
	
	
test: build    
	gcc ./examples/test.c -Isrc  -Ltarget/release -lbotnana -o botnana
	RUST_BACKTRACE=1 ./botnana			
	
clean:
	-rm *.o botnana
			