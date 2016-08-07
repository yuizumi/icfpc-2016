#!/bin/bash

set -e

declare -ar DEPS=(
    /r:packages/Newtonsoft.Json.9.0.1/lib/net45/Newtonsoft.Json.dll
    /r:packages/Rationals.1.2.0/lib/net461/Rationals.dll
)
declare -ar REFS=(
    /r:System.Numerics
)

_nf() (
    set -x
    mono --debug nflat/bin/nf.exe "${DEPS[@]}" "$@"
)

_mcs() (
    set -x
    mcs /debug+ "${DEPS[@]}" "${REFS[@]}" "$@"
)

cd "${0%/*}"
mkdir -p bin

for dep in "${DEPS[@]}" ; do
    file="${dep#/r:}"
    [[ -e "bin/${file##*/}" ]] || ln "${file}" "bin/${file##*/}"
done

_nf alias/*.nf src/basic.nf > bin/basic.cs
_mcs /debug+ /t:library bin/basic.cs

_nf alias/*.nf src/json.nf > bin/json.cs
_mcs /debug+ /t:library bin/json.cs

_nf alias/*.nf src/geom.nf > bin/geom.cs
_mcs /debug+ /t:library bin/geom.cs

_nf /r:bin/geom.dll alias/*.nf src/polygon.nf > bin/polygon.cs
_mcs /debug+ /r:bin/geom.dll /t:library bin/polygon.cs

_nf /r:bin/basic.dll /r:bin/json.dll alias/*.nf src/fetch.nf > bin/fetch.cs
_mcs /debug+ /r:bin/basic.dll /r:bin/json.dll bin/fetch.cs

_nf /r:bin/json.dll alias/*.nf src/findme.nf > bin/findme.cs
_mcs /debug+ /r:bin/json.dll bin/findme.cs

_nf /r:bin/geom.dll alias/*.nf src/xyswap.nf > bin/xyswap.cs
_mcs /debug+ /r:bin/geom.dll bin/xyswap.cs

_nf /r:bin/geom.dll alias/*.nf src/rotate.nf > bin/rotate.cs
_mcs /debug+ /r:bin/geom.dll bin/rotate.cs
