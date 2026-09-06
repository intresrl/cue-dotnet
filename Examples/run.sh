#!/usr/bin/env bash
set -euo pipefail

cd "$(dirname "$0")"

for cue_file in *.cue; do
  name="${cue_file%.cue}"

  # Convert filename to namespace: remove leading digits and hyphens, replace - with _
  namespace="Examples.$(echo "$name" | sed 's/^[0-9]*-//' | tr '-' '_')"

  echo "Running $(basename "$name")..."

  dotnet run --project ../Cue.Generator -- \
    "$cue_file" \
    "$name.cs" \
    --debug "$name.debug.log" \
    --namespace "$namespace"
  
  # verify the generated output compiles
  dotnet build ./Examples.csproj 
done