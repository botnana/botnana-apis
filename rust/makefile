TARGET = $(shell uname -p)-unknown-linux-gnu

fmt:
	cargo fmt

update:
	cargo update

build: fmt
	cargo build --release --target=$(TARGET)
	
arm: fmt
	cargo build --release --target=armv7-unknown-linux-gnueabihf

words: fmt  
	cargo run --release --example=words

run:
	./botnana
	
debug:
	RUST_BACKTRACE=1 ./botnana		
	


.PHONY: clean
	 
clean: fmt
	cargo clean
			