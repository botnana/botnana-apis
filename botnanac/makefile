build:
	cargo fmt
	cargo build --release

	
words: build    
	gcc ./examples/words.c -Isrc  -Ltarget/release -lbotnana -o botnana
	./botnana 
	

	
		