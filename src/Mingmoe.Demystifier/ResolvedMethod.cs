// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic.Enumerable;
using System.Reflection;
using System.Text;
using Mingmoe.Demystifier;

namespace System.Diagnostics
{
    public class ResolvedMethod
    {
        public MethodBase? MethodBase { get; set; }

        public Type? DeclaringType { get; set; }

        public bool IsAsync { get; set; }

        public bool IsLambda { get; set; }

        public ResolvedParameter? ReturnParameter { get; set; }

        public string? Name { get; set; }

        public int? Ordinal { get; set; }

        public string? GenericArguments { get; set; }

        public Type[]? ResolvedGenericArguments { get; set; }

        public MethodBase? SubMethodBase { get; set; }

        public string? SubMethod { get; set; }

        public EnumerableIList<ResolvedParameter> Parameters { get; set; }

        public EnumerableIList<ResolvedParameter> SubMethodParameters { get; set; }
        public int RecurseCount { get; internal set; }

        internal bool IsSequentialEquivalent(ResolvedMethod obj)
        {
            return
                IsAsync == obj.IsAsync &&
                DeclaringType == obj.DeclaringType &&
                Name == obj.Name &&
                IsLambda == obj.IsLambda &&
                Ordinal == obj.Ordinal &&
                GenericArguments == obj.GenericArguments &&
                SubMethod == obj.SubMethod;
        }

        public override string ToString() => Append(new StringBuilder()).ToString();

        public StringBuilder Append(StringBuilder builder)
            => Append(builder, true);

        public StyledBuilder Append(
            StyledBuilder builder,
            StyledBuilderOption option)
            => Append(builder, true, option);

        public StringBuilder Append(StringBuilder builder, bool fullName)
        {
            if (IsAsync)
            {
                builder.Append("async ");
            }

            if (ReturnParameter != null)
            {
                ReturnParameter.Append(builder);
                builder.Append(" ");
            }

            if (DeclaringType != null)
            {

                if (Name == ".ctor")
                {
                    if (string.IsNullOrEmpty(SubMethod) && !IsLambda)
                        builder.Append("new ");

                    AppendDeclaringTypeName(builder, fullName);
                }
                else if (Name == ".cctor")
                {
                    builder.Append("static ");
                    AppendDeclaringTypeName(builder, fullName);
                }
                else
                {
                    AppendDeclaringTypeName(builder, fullName)
                        .Append(".")
                        .Append(Name);
                }
            }
            else
            {
                builder.Append(Name);
            }
            builder.Append(GenericArguments);

            builder.Append("(");
            if (MethodBase != null)
            {
                var isFirst = true;
                foreach (var param in Parameters)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                    }
                    else
                    {
                        builder.Append(", ");
                    }
                    param.Append(builder);
                }
            }
            else
            {
                builder.Append("?");
            }
            builder.Append(")");

            if (!string.IsNullOrEmpty(SubMethod) || IsLambda)
            {
                builder.Append("+");
                builder.Append(SubMethod);
                builder.Append("(");
                if (SubMethodBase != null)
                {
                    var isFirst = true;
                    foreach (var param in SubMethodParameters)
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                        }
                        else
                        {
                            builder.Append(", ");
                        }
                        param.Append(builder);
                    }
                }
                else
                {
                    builder.Append("?");
                }
                builder.Append(")");
                if (IsLambda)
                {
                    builder.Append(" => { }");

                    if (Ordinal.HasValue)
                    {
                        builder.Append(" [");
                        builder.Append(Ordinal);
                        builder.Append("]");
                    }
                }
            }

            if (RecurseCount > 0)
            {
                builder.Append($" x {RecurseCount + 1:0}");
            }

            return builder;
        }


        public StyledBuilder Append(StyledBuilder builder, bool fullName, StyledBuilderOption option)
        {
            if (IsAsync)
            {
                builder.Append(option.KeywordAsyncStyle, "async ");
            }

            if (ReturnParameter != null)
            {
                {
                    var sb = new StringBuilder();
                    ReturnParameter.Append(sb);
                    builder.Append(option.MethodReturnTypeStyle, sb.ToString());
                }
                builder.Append(" ");
            }

            if (DeclaringType != null)
            {

                if (Name == ".ctor")
                {
                    if (string.IsNullOrEmpty(SubMethod) && !IsLambda)
                        builder.Append(option.KeywordNewStyle, "new ");

                    AppendDeclaringTypeName(builder, fullName, option);
                }
                else if (Name == ".cctor")
                {
                    builder.Append(option.KeywordStaticStyle, "static ");
                    AppendDeclaringTypeName(builder, fullName, option);
                }
                else
                {
                    AppendDeclaringTypeName(builder, fullName, option)
                        .Append(".")
                        .Append(option.MethodNameStyle, Name ?? "null");
                }
            }
            else
            {
                builder.Append(option.MethodNameStyle, Name ?? "null");
            }
            builder.Append(option.GenericArgumentStyle, GenericArguments ?? string.Empty);

            builder.Append("(");
            if (MethodBase != null)
            {
                var isFirst = true;
                foreach (var param in Parameters)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                    }
                    else
                    {
                        builder.Append(", ");
                    }
                    param.Append(builder, option);
                }
            }
            else
            {
                builder.Append("?");
            }
            builder.Append(")");

            if (!string.IsNullOrEmpty(SubMethod) || IsLambda)
            {
                builder.Append("+");
                builder.Append(option.SubMethodOrLambdaStyle, new StringBuilder().Append(SubMethod).ToString());
                builder.Append("(");
                if (SubMethodBase != null)
                {
                    var isFirst = true;
                    foreach (var param in SubMethodParameters)
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                        }
                        else
                        {
                            builder.Append(", ");
                        }
                        param.Append(builder, option);
                    }
                }
                else
                {
                    builder.Append("?");
                }
                builder.Append(")");
                if (IsLambda)
                {
                    builder.Append(option.SubMethodOrLambdaStyle, " => { }");

                    if (Ordinal.HasValue)
                    {
                        builder.Append(" [");
                        builder.Append(Ordinal.ToString() ??
                            throw new InvalidOperationException());
                        builder.Append("]");
                    }
                }
            }

            if (RecurseCount > 0)
            {
                builder.Append($" x {RecurseCount + 1:0}");
            }

            return builder;
        }

        private StringBuilder AppendDeclaringTypeName(StringBuilder builder, bool fullName = true)
        {
            return DeclaringType != null ? builder.AppendTypeDisplayName(DeclaringType, fullName: fullName, includeGenericParameterNames: true) : builder;
        }

        private StyledBuilder AppendDeclaringTypeName(StyledBuilder builder, bool fullName, StyledBuilderOption option)
        {
            StringBuilder sb = new();
            AppendDeclaringTypeName(sb, fullName);
            return builder.Append(option.DeclaringTypeOfMethodStyle, sb.ToString());
        }
    }
}
