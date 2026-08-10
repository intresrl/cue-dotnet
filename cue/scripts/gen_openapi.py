#!/usr/bin/env python3
"""Generate OpenAPI 3.0 JSON from the CUE spec.

Usage (from the cue/ directory):
    python scripts/gen_openapi.py

The entire document — paths, operations, parameters, schemas, tags, servers,
info — is defined in the CUE project and exported as a single value:

    cue export ./operations/... -e OpenAPIDoc --out json

This script adds the x-generated-from extension and prints the result.
"""

import subprocess
import json
import sys


def main():
    result = subprocess.run(
        ["cue", "export", "./operations/...", "-e", "OpenAPIDoc", "--out", "json"],
        capture_output=True,
        text=True,
    )
    if result.returncode != 0:
        print(f"ERROR: cue export failed:\n{result.stderr}", file=sys.stderr)
        sys.exit(1)

    doc = json.loads(result.stdout)
    doc["info"]["x-generated-from"] = "CUE spec (operations/openapi_doc.cue)"

    print(json.dumps(doc, indent=2))


if __name__ == "__main__":
    main()
