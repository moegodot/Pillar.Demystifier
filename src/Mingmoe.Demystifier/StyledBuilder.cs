using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Text;

namespace Mingmoe.Demystifier;
public class StyledBuilder : IDisposable
{
    private Utf16ValueStringBuilder builder = ZString.CreateStringBuilder();

    public StyledBuilder Append(string text)
    {
        builder.Append(text);
        return this;
    }

    public StyledBuilder Append(Style style, string text)
    {
        builder.Append(style.ToAnsiCode());
        builder.Append(text);
        builder.Append(Style.ClearStyleAnsiCode);
        return this;
    }

    public StyledBuilder AppendPath(Style pathStyle, Style fileStyle, string path, bool shortenPath)
    {
        if (shortenPath)
        {
            return this.Append(
                fileStyle,
                Path.GetFileName(path));
        }
        else
        {
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
        builder.AppendLine(text);
        return this;
    }

    public StyledBuilder AppendLine()
    {
        builder.AppendLine();
        return this;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            builder.Dispose();
        }
    }

    public override string ToString()
    {
        return builder.ToString();
    }
}
