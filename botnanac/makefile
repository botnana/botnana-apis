TARGET = $(shell uname -p)-unknown-linux-gnu
LIBBOTNANA = ./target/$(TARGET)/release/libbotnana.a

CFLAGS = -Isrc
LDFLAGS = $(LIBBOTNANA) -ldl -pthread -lssl -lcrypto


deploy:
	cp target/$(TARGET)/release/libbotnana.so ./lib/$(TARGET)
	cp target/$(TARGET)/release/libbotnana.a ./lib/$(TARGET)	

build:
	cargo fmt
	astyle --style=allman -r *.c *.h
	cargo build --release --target=$(TARGET)

%.o:
	gcc $(CFLAGS) -c ./examples/$*.c -o $@ 	

words: clean words.o  
	gcc -o botnana $@.o $(LDFLAGS)
	./botnana 
	
empty: clean empty.o
	gcc -o botnana $@.o $(LDFLAGS)
	./botnana 

slave_setting: clean slave_setting.o
	gcc -o botnana $@.o $(LDFLAGS)
	./botnana 

motion_setting: clean motion_setting.o
	gcc -o botnana $@.o $(LDFLAGS)
	./botnana
	
group_setting: clean group_setting.o
	gcc -o botnana $@.o $(LDFLAGS)
	./botnana 
	
axis_setting: clean axis_setting.o
	gcc -o botnana $@.o $(LDFLAGS)
	./botnana 

group1d: clean group1d.o
	gcc -o botnana $@.o $(LDFLAGS)
	./botnana 
	
group2d: clean group2d.o
	gcc -o botnana $@.o $(LDFLAGS)
	./botnana 
	
group3d: clean group3d.o
	gcc -o botnana $@.o $(LDFLAGS)
	./botnana 		

group1d2d: clean group1d2d.o
	gcc -o botnana $@.o $(LDFLAGS)
	./botnana 


version: clean version.o
	gcc -o botnana $@.o $(LDFLAGS)
	./botnana 
	
version_debug: clean version_debug.o
	gcc -o botnana $@.o $(LDFLAGS)
	./botnana 


program: clean program.o
	gcc -o botnana $@.o $(LDFLAGS)
	./botnana 
	
	
program_raw: clean program_raw.o
	gcc -o botnana $@.o $(LDFLAGS)
	./botnana 

io: clean io.o
	gcc -o botnana $@.o $(LDFLAGS)
	./botnana 		
	
	
verbose: clean verbose.o 
	gcc -o verbose $@.o $(LDFLAGS)

.PHONY: clean
	
clean:
	-rm *.o botnana
			