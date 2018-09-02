TARGET = $(shell uname -p)-unknown-linux-gnu
LIBBOTNANA = ./lib/$(TARGET)/libbotnana.a

CFLAGS = -Isrc
LDFLAGS = $(LIBBOTNANA) -ldl -pthread -lssl -lcrypto -O2

fmt:
	cargo fmt
	astyle --style=allman -r *.c *.h

build: fmt
	cargo build --release --target=$(TARGET)
	rm -rf lib
	mkdir -p lib/$(TARGET)
	cp target/$(TARGET)/release/libbotnana.so ./lib/$(TARGET)
	cp target/$(TARGET)/release/libbotnana.a ./lib/$(TARGET)	

%.o: 
	gcc $(CFLAGS) -O2 -c ./examples/$*.c -o $@ 	

words: clean words.o  
	gcc -o botnana $@.o $(LDFLAGS)
	
empty: clean empty.o
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
	
group2d: clean group2d.o
	gcc -o botnana $@.o $(LDFLAGS)
	
group3d: clean group3d.o
	gcc -o botnana $@.o $(LDFLAGS)

group1d2d: clean group1d2d.o
	gcc -o botnana $@.o $(LDFLAGS)

version: clean version.o
	gcc -o botnana $@.o $(LDFLAGS)
	
version_debug: clean version_debug.o
	gcc -o botnana $@.o $(LDFLAGS)

drive_pp: clean drive_pp.o
	gcc -o botnana $@.o $(LDFLAGS)
	
	
program_raw: clean program_raw.o
	gcc -o botnana $@.o $(LDFLAGS)

io: clean io.o
	gcc -o botnana $@.o $(LDFLAGS)
	 		
run:
	RUST_BACKTRACE=1 ./botnana	
	
verbose: clean verbose.o 
	gcc -o verbose $@.o $(LDFLAGS)

.PHONY: clean all

all: build words empty slave_setting motion_setting group_setting axis_setting \
	group1d group2d group3d group1d2d version version_debug program program_raw io
	 
clean: fmt
	-rm *.o botnana */*.c.orig */*.h.orig
			