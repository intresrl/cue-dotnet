# cue-dotnet

.NET 10 bindings and code-generation tooling for CUE

See `LICENSE` in this repository and the license of the `libcue`/CUE
project for the respective licensing terms.

## Project structure

> [!WARNING]
> `cue-dotnet` depends on the separate [libcue](https://github.com/intresrl/libcue)
> project. The native library must be built first and copied to the root
> of this repository before building, testing, or running the generator.

This project integrates with the native Go implementation of [cue](https://github.com/cuelang/cue) through a CGO adapter forked from [libcue](https://github.com/cuelang/libcue):

```mermaid
flowchart LR
    A["cuelang/cue<br/>Go"]
    B["intresrl/libcue<br/>Go + CGO"]
    D["this repository<br/>Cue.Api<br/>P/Invoke"]
    E[".NET consumers"]
    F["this repository<br/>Cue.Generator<br/>C#"]
    G["Generated C#"]

    A --> B --> D
    D --> E
    D --> F --> G
```

This repository has the following layout:

- **`Cue.Api`** --- managed .NET API over `libcue` using P/Invoke.
- **`Cue.Generator`** --- CLI that compiles CUE schemas and generates C# code.
- **`Examples`** --- sample CUE schemas, generated C# files, andgenerator debug output.

## Building

### Prerequisites

- .NET 10 SDK.
- Go 1.25.0: CGO must be enabled and a compatible C compiler must be installed.

To check CGO run:

```bash
go env CGO_ENABLED # should output '1'
```

### Building `libcue`

A convenient checkout layout is:

```text
<your_clone_directory>/
├── libcue/
└── cue-dotnet/
```

The output of the `libcue` build must be generated or copied into the root of
`cue-dotnet`.

**On Linux:**

```shell
cd libcue && go build -buildmode=c-shared -o ../cue-dotnet/libcue.so
```

**On Windows (Git Bash, msys2 or similar):**

> [!CAUTION]
> On Windows, keep the "lib" prefix in libcue.dll to avoid overwriting cue.h in `libcue`.

```shell
cd libcue && go build -buildmode=c-shared -o ../cue-dotnet/libcue.dll
```

### Build `cue-dotnet`

After the native dependency is present in the repository root:

```bash
dotnet restore
dotnet build
```

> [!WARNING]
> Run a **rebuild** every time you make changes in `libcue`. The DLL or shared library is copied in the output directory of `Cue.Api`.

## `Cue.Api` (CUE to dotnet adapter library)

CUE operations begin with a context:

```csharp
using Cuelang.Cue;

using var ctx = new CueContext();
using var value = ctx.Compile("name: string");
```

`CueContext` owns the native CUE context while `Value` represents a
managed wrapper around a native CUE value.

Keep the context alive for the lifetime of values created from it and
dispose native-backed objects appropriately.

## `Cue.Generator`

`Cue.Generator` is a .NET CLI that compiles a CUE schema and generates
C# source.

You may execute it like this:

```bash
# dotnet run --project Cue.Generator -- <input.cue> <output.cs>
dotnet run --project Cue.Generator -- Examples/simple.cue generated.cs
```

An optional debug output path can be supplied via the `--debug` parameter.

To regenerate every example, use the shell script from the repository
root. It discovers all `.cue` files beneath `Examples` automatically and writes
the corresponding `.cs` and `.debug.log` files alongside each schema:

```shell
bash ./run-generator-examples.sh
```

### Generator concepts

The current implementation and tests cover CUE concepts including:

- structs;
- lists;
- definitions;
- references;
- nullable values;
- disjunctions;
- constrained primitive values;
- expressions;
- discriminated alternatives;
- `matchN` expressions.

The generated representation can model alternatives as interfaces and
record implementations instead of arbitrarily reducing a CUE disjunction
to one type.
