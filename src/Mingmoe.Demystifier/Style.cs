using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mingmoe.Demystifier;

/// <summary>
/// The color
/// </summary>
public class Color
{
    public readonly byte R = 0;
    public readonly byte G = 0;
    public readonly byte B = 0;

    public Color(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }

    public override string ToString()
    {
        return $"{R};{G};{B}";
    }

    public static readonly Color Black = new Color(byte.MinValue, byte.MinValue, byte.MinValue);

    public static readonly Color White = new Color(byte.MaxValue, byte.MaxValue, byte.MaxValue);

    public static readonly Color Gray = new Color(128, 128, 128);

    public static readonly Color Red = new Color(255, 0, 0);
    public static readonly Color Blue = new Color(0, 0, 255);
    public static readonly Color Yellow = new Color(255, 255, 0);
    public static readonly Color Lime = new Color(0, 255, 0);
    public static readonly Color Green = new Color(0, 128, 0);
    public static readonly Color Aqua = new Color(0, 255, 255);
    public static readonly Color Fuchsia = new Color(255, 0, 255);
    public static readonly Color Blueviolet = new Color(95, 0, 255);
    public static readonly Color Cornflowerblue = new Color(95, 135, 255);
    public static readonly Color Darkviolet = new Color(135, 0, 215);
    public static readonly Color Darkkhaki = new Color(175, 175, 95);
    public static readonly Color Mediumorchid1 = new Color(215, 95, 255);
}

/// <summary>
/// The style
/// </summary>
public class Style
{

    public Color? BackgroundColor { get; set; } = null;

    public Color? ForeColor { get; set; } = null;

    public bool isBold { get; set; } = false;

    public bool isItalic { get; set; } = false;

    public bool isUnderline { get; set; } = false;

    public Style()
    {

    }

    internal string ToAnsiCode()
    {
        StringBuilder sb = new StringBuilder();

        if (BackgroundColor != null)
        {
            sb.Append($"\x001B[48;2;{BackgroundColor}m");
        }
        if (ForeColor != null)
        {
            sb.Append($"\x001B[38;2;{ForeColor}m");
        }
        if (isBold)
        {
            sb.Append($"\x001B[1m");
        }
        if (isItalic)
        {
            sb.Append($"\x001B[3m");
        }
        if (isUnderline)
        {
            sb.Append($"\x001B[4m");
        }

        return sb.ToString();
    }

    internal const string ClearStyleAnsiCode = "\x001B[0m";
}
