using Cuelang.Cue;
using Cue.Generator;

if (args.Length < 2)
{
    Console.WriteLine("Usage: dotnet run -- <input.cue> <output.cs>");
    Console.WriteLine();
    Console.WriteLine("Arguments:");
    Console.WriteLine("  input.cue   Path to the CUE schema file to compile");
    Console.WriteLine("  output.cs   Path where the generated C# code will be written");
    return 1;
}

var input = args[0];
var output = args[1];

using var ctx = new CueContext();
using var value = ctx.Compile(File.ReadAllText(input));

var node = value.ToCueValueNode();

var gen = new RoslynGenerator();
var code = gen.GenerateCode(node);

File.WriteAllText(output, code);
Console.WriteLine($"Wrote {output}");
return 0;
