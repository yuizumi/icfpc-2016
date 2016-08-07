TARGETS=bin/fetch.exe bin/findme.exe bin/rotate.exe bin/xyswap.exe

DEPS=bin/Newtonsoft.Json.dll bin/Rationals.dll
REFS=System.Numerics

JSON_NET_DLL=packages/Newtonsoft.Json.9.0.1/lib/net45/Newtonsoft.Json.dll
RATIONALS_DLL=packages/Rationals.1.2.0/lib/net461/Rationals.dll

all: $(TARGETS)

clean:
	-rm -fr bin/*.cs bin/*.dll bin/*.mdb bin/*.exe

bin/fetch.cs: bin/basic.dll bin/json.dll
bin/fetch.exe: bin/basic.dll bin/json.dll

bin/findme.cs: bin/basic.dll bin/json.dll
bin/findme.exe: bin/basic.dll bin/json.dll

bin/polygon.cs: bin/geom.dll
bin/polygon.dll: bin/geom.dll

bin/probutil.cs: bin/geom.dll
bin/probutil.dll: bin/geom.dll

bin/rotate.cs: bin/probutil.dll bin/geom.dll
bin/rotate.exe: bin/probutil.dll bin/geom.dll

bin/xyswap.cs: bin/probutil.dll bin/geom.dll
bin/xyswap.exe: bin/probutil.dll bin/geom.dll

bin/Newtonsoft.Json.dll: $(JSON_NET_DLL)
	ln $< $@

bin/Rationals.dll: $(RATIONALS_DLL)
	ln $< $@

bin/%.cs: $(DEPS) alias/*.nf src/%.nf
	mono --debug nflat/bin/nf.exe $(^:%.dll=/r:%.dll) > $@

bin/%.dll: $(DEPS) bin/%.cs
	mcs /debug+ /t:library /out:$@ $(REFS:%=/r:%) $(^:%.dll=/r:%.dll)

bin/%.exe: $(DEPS) bin/%.cs
	mcs /debug+ /out:$@ $(REFS:%=/r:%) $(^:%.dll=/r:%.dll)

.PHONY: clean
