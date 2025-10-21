using Cysharp.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar.Demystifier;

public sealed class StyledBuilder : IDisposable
{
    private Utf16ValueStringBuilder _builder = ZString.CreateStringBuilder();

    public StyledBuilder Append(string text)
    {
        _builder.Append(text);
        return this;
    }

    public StyledBuilder Append(Style style,string text)
    {
        var ansi = style.ToAnsiCode();
        _builder.Append(ansi);
        _builder.Append(text);
        if (!string.IsNullOrWhiteSpace(ansi))
        {
            _builder.Append(Style.ClearStyleAnsiCode);
        }
        return this;
    }

    public StyledBuilder AppendPath(Style pathStyle,Style fileStyle,string path,bool shortenPath)
    {
        if (shortenPath)
        {
            return this.Append(
                fileStyle,
                Path.GetFileName(path));
        }
        else {
            return this.Append(
                pathStyle,
                Path.GetDirectoryName(path) ?? string.Empty)
                .Append(Path.DirectorySeparatorChar.ToString())
                .Append(
                fileStyle,
                Path.GetFileName(path));
        }
    }

    public StyledBuilder AppendLine(string text)
    {
        _builder.Append(text);
        _builder.Append('\n');
        return this;
    }

    public StyledBuilder AppendLine()
    {
        _builder.Append('\n');
        return this;
    }

    public void Dispose()
    {
        _builder.Dispose();
    }

    public override string ToString()
    {
        return _builder.ToString();
    }
}
