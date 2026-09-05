#!/usr/bin/env bash
set -euo pipefail

cd "$(dirname "$0")"

for cue_file in *.cue; do
  name="${cue_file%.cue}"

  echo "Running $(basename "$name")..."

  dotnet run --project ../Cue.Generator -- \
    "$cue_file" \
    "$name.cs" \
    --debug "$name.debug.log"
done