CFLAGS = -Isrc
LDFLAGS = ./lib/libbotnana.a -ldl -pthread -O2 -lm

./lib/libbotnana_x86_64.a:
	cargo build --release --target=x86_64-unknown-linux-gnu
	mkdir -p lib
	rm -rf lib/libbotnana_x86_64.*
	cp target/x86_64-unknown-linux-gnu/release/libbotnana.so ./lib/libbotnana_x86_64.so
	cp target/x86_64-unknown-linux-gnu/release/libbotnana.a ./lib/libbotnana_x86_64.a
	cp ./lib/libbotnana_x86_64.a ./lib/libbotnana.a

	
./lib/libbotnana_arm64.a:
	cargo build --release --target=aarch64-unknown-linux-gnu
	mkdir -p lib
	rm -rf lib/libbotnana_arm64.*
	cp target/aarch64-unknown-linux-gnu/release/libbotnana.so ./lib/libbotnana_arm64.so
	cp target/aarch64-unknown-linux-gnu/release/libbotnana.a ./lib/libbotnana_arm64.a
	cp ./lib/libbotnana_arm64.a ./lib/libbotnana.a

%.o: 
	gcc $(CFLAGS) -O2 -c ./examples/$*.c -o $@ 	

words: ./lib/libbotnana_arm64.a ./libbotnana_x86_64.a words.o
	mkdir -p target/bin
	gcc -o target/bin/$@ $@.o $(LDFLAGS)

config_slave: ./lib/libbotnana_arm64.a ./libbotnana_x86_64.a config_slave.o
	mkdir -p target/bin
	gcc -o $@ $@.o $(LDFLAGS)

config_motion: ./lib/libbotnana_arm64.a ./libbotnana_x86_64.a config_motion.o
	mkdir -p target/bin
	gcc -o $@ $@.o $(LDFLAGS)

config_group: ./lib/libbotnana_arm64.a ./libbotnana_x86_64.a config_group.o
	mkdir -p target/bin
	gcc -o $@ $@.o $(LDFLAGS)
	
config_axis: ./lib/libbotnana_arm64.a ./libbotnana_x86_64.a config_axis.o
	mkdir -p target/bin
	gcc -o $@ $@.o $(LDFLAGS)

group1d: ./lib/libbotnana_arm64.a ./libbotnana_x86_64.a group1d.o
	mkdir -p target/bin
	gcc -o $@ $@.o $(LDFLAGS)
	
version: ./lib/libbotnana_arm64.a ./libbotnana_x86_64.a version.o
	mkdir -p target/bin
	gcc -o $@ $@.o $(LDFLAGS)

drive_pp: ./lib/libbotnana_arm64.a ./libbotnana_x86_64.a drive_pp.o
	mkdir -p target/bin
	gcc -o $@ $@.o $(LDFLAGS)
	

recorder: ./lib/libbotnana_arm64.a ./libbotnana_x86_64.a recorder.o
	mkdir -p target/bin
	gcc -lpthread -o $@ $@.o $(LDFLAGS)
	
target_reached: ./lib/libbotnana_arm64.a ./libbotnana_x86_64.a target_reached.o
	mkdir -p target/bin
	gcc -lpthread -o $@ $@.o $(LDFLAGS)
	
reboot: ./lib/libbotnana_arm64.a ./libbotnana_x86_64.a reboot.o
	mkdir -p target/bin
	gcc -lpthread -o $@ $@.o $(LDFLAGS)

.PHONY: clean all

clean: 
	-rm *.o
	cargo clean
			
