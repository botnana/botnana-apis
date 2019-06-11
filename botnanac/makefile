TARGET = $(shell uname -p)-unknown-linux-gnu
LIBBOTNANA = ./lib/$(TARGET)/libbotnana.a

CFLAGS = -Isrc
LDFLAGS = $(LIBBOTNANA) -ldl -pthread -lssl -lcrypto -O2

fmt:
	cargo fmt
	astyle --style=allman -r *.c *.h

build: 
	cargo build --release --target=$(TARGET)
	rm -rf lib/$(TARGET)
	mkdir -p lib/$(TARGET)
	cp target/$(TARGET)/release/libbotnana.so ./lib/$(TARGET)
	cp target/$(TARGET)/release/libbotnana.a ./lib/$(TARGET)	
	
arm: fmt
	cargo build --release --target=armv7-unknown-linux-gnueabihf
	rm -rf lib/armv7-unknown-linux-gnueabihf
	mkdir -p lib/armv7-unknown-linux-gnueabihf
	cp target/armv7-unknown-linux-gnueabihf/release/libbotnana.so ./lib/armv7-unknown-linux-gnueabihf
	cp target/armv7-unknown-linux-gnueabihf/release/libbotnana.a ./lib/armv7-unknown-linux-gnueabihf		

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
	 
clean: fmt
	-rm *.o botnana */*.c.orig */*.h.orig
			