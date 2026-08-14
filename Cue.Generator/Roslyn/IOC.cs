using Microsoft.Extensions.DependencyInjection;

namespace Cue.Generator.Roslyn;

public static class Ioc
{
    public static void RegisterGenerator(this ServiceCollection services, TextWriter? debugWriter)
    {
        if (debugWriter is not null)
        {
            services.AddTransient<TextWriter>(_ => debugWriter);
        }
        
        services.AddTransient<ITypeStore, TypeStore>();
        services.AddTransient<IIdentifierNamer, IdentifierNamer>();
        services.AddTransient<IEqualityComparer<CueStructValue>, CueStructValueEqualityComparer>();
        services.AddTransient<IRoslynGenerator, RoslynGenerator>();
    }
}