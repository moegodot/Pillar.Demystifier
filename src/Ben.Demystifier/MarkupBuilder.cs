using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Utopia.Demystifier;

public class MarkupBuilder
{
    public List<IRenderable> Markups { get; init; } = new(256);

    public MarkupBuilder Append(string? markup)
    {
        Markups.Add(new Markup(Markup.Escape(markup ?? string.Empty)));
        return this;
    }

    public MarkupBuilder Append(int? @int)
    {
        Markups.Add(new Markup(@int.HasValue ? @int.Value.ToString() : string.Empty));
        return this;
    }

    public MarkupBuilder AddMarkup(string markup)
    {
        Markups.Add(new Markup(markup));
        return this;
    }

    public MarkupBuilder AddTextPath(string path)
    {
        var textPath = new TextPath(path)
            .LeafStyle(new Style(foreground: Color.Yellow2));

        Markups.Add(textPath);

        return this;
    }

    public MarkupBuilder NewLine()
    {
        Markups.Add(new Markup("\n"));
        return this;
    }

    public MarkupBuilder AppendLine()
    {
        return this.NewLine();
    }

    public void WriteTo(IAnsiConsole console)
    {
        foreach(var markup in Markups)
        {
            console.Write(markup);
        }
    }
}
