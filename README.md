# cue-api-dotnet

This folder contains the .NET 10 rewrite of the CUE API with direct P/Invoke into `libcue`.

## Projects

- `Cue.Api`: class library
- `Cue.Api.Tests`: xUnit test project

## Prerequisites

- .NET 10 SDK
- Native `libcue` available on your library search path as `cue`

## Run tests

```bash
dotnet test "C:\Users\mgg\git\cue-api-java\dotnet\Cue.Api.Tests\Cue.Api.Tests.csproj" -v minimal
```

