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
	-rm *.o botnana
	gcc -Isrc -c ./examples/empty.c
	gcc empty.o -Ltarget/release -lbotnana -o botnana
	./botnana 

	
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
	
program: build
	gcc ./examples/program.c -Isrc  -Ltarget/release -lbotnana -o botnana
	RUST_BACKTRACE=full ./botnana		
	
	
test: build    
	gcc ./examples/test.c -Isrc  -Ltarget/release -lbotnana -o botnana
	RUST_BACKTRACE=1 ./botnana			
	
clean:
	-rm *.o botnana
			