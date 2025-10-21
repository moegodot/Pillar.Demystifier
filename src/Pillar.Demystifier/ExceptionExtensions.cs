// Copyright (c) Ben A Adams. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Pillar.Demystifier;
using System.Collections.Generic;
using System.Collections.Generic.Enumerable;
using System.IO;
using System.Reflection;
using System.Text;

namespace System.Diagnostics
{
    public static class ExceptionExtensions
    {
        private static readonly FieldInfo? StackTraceString = typeof(Exception).GetField("_stackTraceString", BindingFlags.Instance | BindingFlags.NonPublic);

        private static void SetStackTracesString(this Exception exception, string value)
            => StackTraceString?.SetValue(exception, value);

        /// <summary>
        /// Demystifies the given <paramref name="exception"/> and tracks the original stack traces for the whole exception tree.
        /// </summary>
        public static T Demystify<T>(this T exception,StyledBuilderOption? option = null) where T : Exception
        {
            option = option ?? StyledBuilderOption.GlobalOption;
            try
            {
                var stackTrace = new EnhancedStackTrace(exception);

                if (stackTrace.FrameCount > 0)
                {
                    exception.SetStackTracesString(stackTrace.ToColoredString(option));
                }

                if (exception is AggregateException aggEx)
                {
                    foreach (var ex in EnumerableIList.Create(aggEx.InnerExceptions))
                    {
                        ex.Demystify(option);
                    }
                }

                exception.InnerException?.Demystify(option);
            }
            catch
            {
                // Processing exceptions shouldn't throw exceptions; if it fails
            }

            return exception;
        }

        public static void PrintColoredStringDemystified(this Exception exception,StyledBuilderOption? option = null,TextWriter? writer = null)
            => (writer ?? Console.Out)
                .Write(new StyledBuilder().AppendDemystified(exception,
                option ?? StyledBuilderOption.GlobalOption));

        [Contracts.Pure]
        public static string ToStringDemystified(this Exception exception, StyledBuilderOption? option = null)
            => new StyledBuilder().AppendDemystified(exception,
                option ?? StyledBuilderOption.GlobalOption).ToString();
    }
}
