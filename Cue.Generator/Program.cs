using Cuelang.Cue;
using Cue.Generator;
using Cue.Generator.Roslyn;
using Microsoft.Extensions.DependencyInjection;

// Parse optional arguments
var (input, output, debugOutputPath, namespaceName) = args switch
{
    [var i, var o, "--debug", var d, "--namespace", var ns] => (i, o, d, ns),
    [var i, var o, "--namespace", var ns] => (i, o, null, ns),
    [var i, var o, "--debug", var d] => (i, o, d, null),
    [var i, var o] => (i, o, null, null),
    _ => (null, null, null, null)
};

if (input is null || output is null)
{
    Console.WriteLine("Usage: dotnet run -- <input.cue> <output.cs> [--debug [debug-output-path]] [--namespace <namespace>]");
    Console.WriteLine();
    Console.WriteLine("Arguments:");
    Console.WriteLine("  input.cue             Path to the CUE schema file to compile");
    Console.WriteLine("  output.cs             Path where the generated C# code will be written");
    Console.WriteLine();
    Console.WriteLine("Options:");
    Console.WriteLine("  --debug path          Enable debug output to that file.");
    Console.WriteLine("  --namespace ns        Wrap generated code in the specified namespace.");
    return 1;
}

using var ctx = new CueContext();
using var value = ctx.Compile(File.ReadAllText(input));


TextWriter? debugWriter = null;
try
{
    if (debugOutputPath != null)
    {
        var dir = Path.GetDirectoryName(debugOutputPath);
        if (!string.IsNullOrEmpty(dir))
        {
            Directory.CreateDirectory(dir);
        }
        debugWriter = new StreamWriter(debugOutputPath, append: false);
    }

    var node = CueValueVisitor.VisitRoot(value, debugWriter);

    var services = new ServiceCollection();
    services.RegisterGenerator(debugWriter);
    using var serviceProvider = services.BuildServiceProvider();

    var gen = serviceProvider.GetRequiredService<IRoslynGenerator>();

    var code = gen.GenerateCode(node, namespaceName);

    File.WriteAllText(output, code);
    Console.WriteLine($"Wrote {output}");

    if (debugOutputPath != null)
    {
        Console.WriteLine($"Debug output written to {debugOutputPath}");
    }

    return 0;
}
finally
{
    debugWriter?.Dispose();
}
