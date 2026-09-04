#!/usr/bin/env bash
set -euo pipefail

script_directory="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
examples_directory="${1:-"$script_directory/Examples"}"
configuration="${2:-Debug}"
generator_project="$script_directory/Cue.Generator/Cue.Generator.csproj"

if [[ ! -d "$examples_directory" ]]; then
    printf 'Examples directory does not exist: %s\n' "$examples_directory" >&2
    exit 1
fi

found_examples=false

while IFS= read -r -d '' example; do
    found_examples=true
    output_path="${example%.cue}.cs"
    debug_path="${example%.cue}.debug.log"

    printf 'Generating %s\n' "$example"
    dotnet run --project "$generator_project" --configuration "$configuration" -- \
        "$example" "$output_path" --debug "$debug_path"
done < <(find "$examples_directory" -type f -name '*.cue' -print0 | sort -z)

if [[ "$found_examples" == false ]]; then
    printf 'No .cue files found under: %s\n' "$examples_directory" >&2
    exit 1
fi
