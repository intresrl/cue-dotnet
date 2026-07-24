using Cuelang.Cue;
using Cue.Generator;

// Usage: dotnet run -- [input.cue] [output.cs]
// Defaults: ../simple.cue -> ../simple.cs
var input = args.Length > 0 ? args[0] : Path.Combine("..", "..", "..", "..", "simple.cue");
var output = args.Length > 1 ? args[1] : Path.Combine("..", "..", "..", "..", "simple.cs");

using var ctx = new CueContext();
using var value = ctx.Compile(File.ReadAllText(input));

var node = value.ToCueValueNode();

var gen = new RoslynGenerator();
var code = gen.GenerateCode(node);

File.WriteAllText(output, code);
Console.WriteLine($"Wrote {output}");
