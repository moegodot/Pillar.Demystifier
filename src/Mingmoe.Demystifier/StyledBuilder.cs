using Cysharp.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mingmoe.Demystifier;
public class StyledBuilder : IDisposable
{
    private Utf16ValueStringBuilder builder = ZString.CreateStringBuilder();

    public StyledBuilder Append(string text)
    {
        builder.Append(text);
        return this;
    }

    public StyledBuilder Append(Style style,string text)
    {
        builder.Append(style.ToAnsiCode());
        builder.Append(text);
        builder.Append(Style.ClearStyleAnsiCode);
        return this;
    }

    public StyledBuilder AppendPath(Style style,string path,bool shortenPath)
    {
        return this.Append(style,
            shortenPath ? Path.GetFileName(path) : path);
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
