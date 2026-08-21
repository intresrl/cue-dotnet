using System.Runtime.CompilerServices;
using System.Text;

namespace Cue.Generator.Roslyn;

[InterpolatedStringHandler]
public class TypeName
{
    private readonly StringBuilder _template;
    private string? _typePath;
    private string? _baseTypePath;

    public TypeName(int literalLength, int formattedCount)
    {
        _template = new StringBuilder(literalLength);
        _typePath = null;
    }

    public static TypeName FromDefinitionRef(string path)
    { 
        var tn = new TypeName(4, 0) { _typePath = path };
        tn._template.Append("{0}");
        return tn;
    }
    
    public static TypeName FromDisjunctionRef(string path)
    { 
        var tn = new TypeName(4, 0) { _baseTypePath = path };
        tn._template.Append("{0}");
        return tn;
    }

    public void AppendLiteral(string value)
    {
        _template.Append(value);
    }

    public void AppendFormatted(TypeName value)
    {
        _template.Append(value._template);

        if (_typePath is not null || _baseTypePath is not null)
        {
            throw new InvalidOperationException("path already present in this type name");
        }
        
        _typePath = value._typePath;
        _baseTypePath = value._baseTypePath;
    }

    public string Format(Func<string, string> typeFormatter, Func<string, string> baseTypeFormatter)
    {
        if (_baseTypePath is not null)
        {
            return string.Format(_template.ToString(), baseTypeFormatter(_baseTypePath));   
        }
        
        return _typePath is { } t 
            ? string.Format(_template.ToString(), typeFormatter(t)) 
            : _template.ToString();
    }
}