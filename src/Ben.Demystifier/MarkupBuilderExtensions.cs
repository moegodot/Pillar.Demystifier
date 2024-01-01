using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Collections.Generic.Enumerable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ben.Demystifier;

public static class MarkupBuilderExtensions
{
    public static MarkupBuilder AppendDemystified(this MarkupBuilder builder, Exception exception)
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
                    builder.AppendInnerException(ex);
                }
            }

            if (exception.InnerException != null)
            {
                builder.AppendInnerException(exception.InnerException);
            }
        }
        catch(Exception e)
        {
            // Processing exceptions shouldn't throw exceptions; if it fails
            throw new AggregateException("inner exception",e,exception);
        }

        return builder;
    }

    private static void AppendInnerException(this MarkupBuilder builder, Exception exception)
        => builder.Append(" ---> ")
            .NewLine()
            .AppendDemystified(exception)
            .AppendLine()
            .Append("   --- End of inner exception stack trace ---");
}
