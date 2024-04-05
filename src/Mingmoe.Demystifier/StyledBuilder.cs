using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mingmoe.Demystifier;
public class StyledBuilder
{
    private StringBuilder builder = new();

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
    public override string ToString()
    {
        return builder.ToString();
    }
}
