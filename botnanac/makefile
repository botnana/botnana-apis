CFLAGS = -Isrc
LDFLAGS = ./lib/libbotnana.a -ldl -pthread -lssl -lcrypto -O2 -lm

fmt:
	cargo fmt
	astyle --style=allman -r "*.c" "*.h"

i686:
	cargo build --release --target=i686-unknown-linux-gnu
	mkdir -p lib
	rm -rf lib/libbotnana.*
	cp target/i686-unknown-linux-gnu/release/libbotnana.so ./lib/libbotnana_i686.so
	cp target/i686-unknown-linux-gnu/release/libbotnana.a ./lib/libbotnana_i686.a
	cp target/i686-unknown-linux-gnu/release/libbotnana.so ./lib/libbotnana.so
	cp target/i686-unknown-linux-gnu/release/libbotnana.a ./lib/libbotnana.a

x86_64:
	cargo build --release --target=x86_64-unknown-linux-gnu
	mkdir -p lib
	rm -rf lib/libbotnana_x86_64.*
	cp target/x86_64-unknown-linux-gnu/release/libbotnana.so ./lib/libbotnana_x86_64.so
	cp target/x86_64-unknown-linux-gnu/release/libbotnana.a ./lib/libbotnana_x86_64.a
	cp target/x86_64-unknown-linux-gnu/release/libbotnana.so ./lib/libbotnana.so
	cp target/x86_64-unknown-linux-gnu/release/libbotnana.a ./lib/libbotnana.a

	
arm:
	cargo build --release --target=armv7-unknown-linux-gnueabihf
	mkdir -p lib
	rm -rf lib/libbotnana_armv7.*
	cp target/armv7-unknown-linux-gnueabihf/release/libbotnana.so ./lib/libbotnana_armv7.so
	cp target/armv7-unknown-linux-gnueabihf/release/libbotnana.a ./lib/libbotnana_armv7.a

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


tcp-modbus: clean tcp-modbus.o
	gcc -o botnana $@.o $(LDFLAGS)
	

run:
	./botnana
	
debug:
	RUST_BACKTRACE=1 ./botnana		
	

.PHONY: clean all

all: build words config_slave config_motion config_group config_axis group1d version drive_pp recorder
	 
clean: 
	-rm *.o botnana */*.c.orig */*.h.orig
			