// operations/openapi_doc.cue - Reference documentation only.
//
// DEPRECATED: This file is kept for reference only.
// The gen_openapi.py script is now domain-agnostic and auto-discovers resources.
//
// Previously, this file exported OpenAPIDoc which was a single hardcoded value.
// Now, gen_openapi.py independently exports:
// - APISpec from operations/api.cue
// - Schemas from operations/schemas.cue
// - Tag and PathItems from each resource/*/paths.cue (auto-discovered)
//
// This means:
// 1. Adding a new resource no longer requires updating this file or openapi_doc.cue
// 2. The Python script works with ANY CUE project following the pattern
// 3. Resources are completely decoupled from the OpenAPI assembly logic
//
// The old pattern was:
//   cue export ./operations/... -e OpenAPIDoc --out json
//
// The new pattern is:
//   python scripts/gen_openapi.py
//
// Benefits:
// - No domain-specific hardcoding in Python
// - Scales to any number of resources
// - Python logic is reusable across projects
// - Simpler to add new resources

package operations
