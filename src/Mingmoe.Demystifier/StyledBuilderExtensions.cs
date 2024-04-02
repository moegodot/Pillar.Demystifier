using System;
using System.Collections.Generic;
using System.Collections.Generic.Enumerable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mingmoe.Demystifier;

public static class StyledBuilderExtensions
{

    public static StyledBuilder AppendColoredDemystified(this StyledBuilder builder, Exception exception, StyledBuilderOption option)
    {
        try
        {
            var stackTrace = new System.Diagnostics.EnhancedStackTrace(exception);

            builder.Append(exception.GetType().ToString());
            if (!string.IsNullOrEmpty(exception.Message))
            {
                builder.Append(": ").Append(option.MessageStyle, exception.Message);
            }
            builder.Append(Environment.NewLine);

            if (stackTrace.FrameCount > 0)
            {
                stackTrace.Append(builder, option);
            }

            if (exception is AggregateException aggEx)
            {
                foreach (var ex in EnumerableIList.Create(aggEx.InnerExceptions))
                {
                    builder.AppendColoredInnerException(ex, option);
                }
            }

            if (exception.InnerException != null)
            {
                builder.AppendColoredInnerException(exception.InnerException, option);
            }
        }
        catch (Exception e)
        {
            // Processing exceptions shouldn't throw exceptions; if it fails
            throw new AggregateException("inner exception", e, exception);
        }

        return builder;
    }

    private static void AppendColoredInnerException(
        this StyledBuilder builder,
        Exception exception,
        StyledBuilderOption option)
        => builder.Append(" ---> ")
            .AppendLine()
            .AppendColoredDemystified(exception, option)
            .AppendLine()
            .Append("   --- End of inner exception stack trace ---");
}
