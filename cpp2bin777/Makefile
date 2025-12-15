CPP=g++
CPPFLAGS=-g --std=c++20
RM=rm
OBJS=main.o rom.o romSetup.o

cpp2bin777: $(OBJS)
	$(CPP) -o cpp2bin777 $^

rom.cpp:
	# Could copy, but might be clearer to just complain.
	echo "Requires rom.cpp from PD777."

semiclean:
	$(RM) -f cpp2bin777 rom.o

clean:
	$(RM) -f cpp2bin777 $(OBJS)
