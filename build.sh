#!/bin/bash

set -e

declare -ar REFS=(
    /r:System.Numerics
)
declare -ar DEPS=(
    /r:packages/Newtonsoft.Json.9.0.1/lib/net45/Newtonsoft.Json.dll
    /r:packages/Rationals.1.2.0/lib/net461/Rationals.dll
)

compile() {
    local name="$1" ; shift
    ln -f "${DEPS[@]/\/r:/}" bin
    mono --debug nflat/bin/nf.exe "${DEPS[@]/\/r:/}" "$@" > "bin/${name}.cs"
    mcs /debug+ "${REFS[@]}" "${DEPS[@]}" "bin/${name}.cs"
}

cd "${0%/*}"
mkdir -p bin
compile fetch alias/*.nf src/{basic,json,fetch}.nf
