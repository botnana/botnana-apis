TARGET = $(shell uname -m)-unknown-linux-gnu

update:
	cargo update

build:
	cargo build --release --target=$(TARGET)
	
arm64:
	cargo build --release --target=aarch64-unknown-linux-gnu

words:
	cargo run --release --example=words

run:
	./botnana
	
debug:
	RUST_BACKTRACE=1 ./botnana

.PHONY: clean
	 
clean:
	cargo clean
			