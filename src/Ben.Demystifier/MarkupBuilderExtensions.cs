using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Collections.Generic.Enumerable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utopia.Demystifier;

public static class MarkupBuilderExtensions
{
    public static MarkupBuilder AppendColoredDemystified(this MarkupBuilder builder, Exception exception)
    {
        try
        {
            var stackTrace = new EnhancedStackTrace(exception);

            builder.Append(exception.GetType().ToString());
            if (!string.IsNullOrEmpty(exception.Message))
            {
                builder.Append(": ").AddMarkup($"[red]{Markup.Escape(exception.Message)}[/]");
            }
            builder.Append(Environment.NewLine);

            if (stackTrace.FrameCount > 0)
            {
                stackTrace.Append(builder);
            }

            if (exception is AggregateException aggEx)
            {
                foreach (var ex in EnumerableIList.Create(aggEx.InnerExceptions))
                {
                    builder.AppendColoredInnerException(ex);
                }
            }

            if (exception.InnerException != null)
            {
                builder.AppendColoredInnerException(exception.InnerException);
            }
        }
        catch(Exception e)
        {
            // Processing exceptions shouldn't throw exceptions; if it fails
            throw new AggregateException("inner exception",e,exception);
        }

        return builder;
    }

    private static void AppendColoredInnerException(this MarkupBuilder builder, Exception exception)
        => builder.Append(" ---> ")
            .NewLine()
            .AppendColoredDemystified(exception)
            .AppendLine()
            .Append("   --- End of inner exception stack trace ---");
}
