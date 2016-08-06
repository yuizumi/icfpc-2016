#!/bin/bash

set -e

declare -ar DEPS=(
    /r:packages/Newtonsoft.Json.9.0.1/lib/net45/Newtonsoft.Json.dll
    /r:packages/Rationals.1.2.0/lib/net461/Rationals.dll
)
declare -ar REFS=(
    /r:System.Numerics
)

_nf() {
    mono --debug nflat/bin/nf.exe "${DEPS[@]}" "$@"
}

_mcs() {
    mcs /debug+ "${DEPS[@]}" "${REFS[@]}" "$@"
}

cd "${0%/*}"
mkdir -p bin

for dep in "${DEPS[@]}" ; do
    file="${dep#/r:}"
    [[ -e "bin/${file##*/}" ]] || ln "${file}" "bin/${file##*/}"
done

_nf alias/*.nf src/basic.nf > bin/basic.cs
_mcs /debug+ /t:library bin/basic.cs

_nf /r:bin/basic.dll alias/*.nf src/{json,fetch}.nf > bin/fetch.cs
_mcs /debug+ /r:bin/basic.dll bin/fetch.cs
