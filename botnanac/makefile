build:
	cargo fmt
	cargo build --release

	
words: build    
	gcc ./examples/words.c -Isrc  -Ltarget/release -lbotnana -o botnana
	./botnana 
	
empty: build
	gcc ./examples/empty.c -Isrc  -Ltarget/release -lbotnana -o botnana
	./botnana 

	
verbose: build    
	gcc ./examples/verbose.c -Isrc  -Ltarget/release -lbotnana -o botnana
	./botnana
	
	
	
test: build    
	gcc ./examples/test.c -Isrc  -Ltarget/release -lbotnana -o botnana
	RUST_BACKTRACE=1 ./botnana			