using System.Text.RegularExpressions;

namespace Utopia.Demystifier.Test
{
    internal static class LineEndingsHelper
    {
        private static readonly Regex ReplaceLineEndings = new Regex(" in [^\n\r]+");

        public static string RemoveLineEndings(string original)
        {
            return ReplaceLineEndings.Replace(original, "");
        }
    }
}
