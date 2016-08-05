#!/bin/bash

set -e

declare -ar DEPS=(
    /r:packages/Newtonsoft.Json.9.0.1/lib/net45/Newtonsoft.Json.dll
)

compile() {
    local name="$1" ; shift
    ln -f "${DEPS[@]/\/r:/}" bin
    mono --debug nflat/bin/nf.exe "${DEPS[@]/\/r:/}" "$@" > "bin/${name}.cs"
    mcs /debug+ "${DEPS[@]}" "bin/${name}.cs"
}

cd "${0%/*}"
mkdir -p bin
compile fetch src/{aliases,basic,json,fetch}.nf
