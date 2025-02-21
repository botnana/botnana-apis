CFLAGS = -Isrc
LDFLAGS = ./lib/libbotnana.a -ldl -pthread -O2 -lm

x86_64:
	cargo build --release --target=x86_64-unknown-linux-gnu
	mkdir -p lib
	rm -rf lib/libbotnana_x86_64.*
	cp target/x86_64-unknown-linux-gnu/release/libbotnana.so ./lib/libbotnana_x86_64.so
	cp target/x86_64-unknown-linux-gnu/release/libbotnana.a ./lib/libbotnana_x86_64.a
	cp ./lib/libbotnana_x86_64.a ./lib/libbotnana.a

	
arm64:
	cargo build --release --target=aarch64-unknown-linux-gnu
	mkdir -p lib
	rm -rf lib/libbotnana_arm64.*
	cp target/aarch64-unknown-linux-gnu/release/libbotnana.so ./lib/libbotnana_arm64.so
	cp target/aarch64-unknown-linux-gnu/release/libbotnana.a ./lib/libbotnana_arm64.a
	cp ./lib/libbotnana_arm64.a ./lib/libbotnana.a

%.o: 
	gcc $(CFLAGS) -O2 -c ./examples/$*.c -o $@ 	

words: clean words.o  
	gcc -o botnana $@.o $(LDFLAGS)

config_slave: clean config_slave.o
	gcc -o botnana $@.o $(LDFLAGS)

config_motion: clean config_motion.o
	gcc -o botnana $@.o $(LDFLAGS)

config_group: clean config_group.o
	gcc -o botnana $@.o $(LDFLAGS)
	
config_axis: clean config_axis.o
	gcc -o botnana $@.o $(LDFLAGS)

group1d: clean group1d.o
	gcc -o botnana $@.o $(LDFLAGS)
	
version: clean version.o
	gcc -o botnana $@.o $(LDFLAGS)

drive_pp: clean drive_pp.o
	gcc -o botnana $@.o $(LDFLAGS)
	

recorder: clean recorder.o
	gcc -lpthread -o botnana $@.o $(LDFLAGS)
	
target_reached: clean target_reached.o
	gcc -lpthread -o botnana $@.o $(LDFLAGS)
	
reboot: clean reboot.o
	gcc -lpthread -o botnana $@.o $(LDFLAGS)

run:
	./botnana
	
debug:
	RUST_BACKTRACE=1 ./botnana		
	

.PHONY: clean all

all: build words config_slave config_motion config_group config_axis group1d version drive_pp recorder
	 
clean: 
	-rm *.o botnana */*.c.orig */*.h.orig
			
