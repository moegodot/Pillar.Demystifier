// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic.Enumerable;
using System.Reflection;
using System.Text;
using Ben.Demystifier;
using Spectre.Console;

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

        public MarkupBuilder Append(MarkupBuilder builder)
            => Append(builder, true);

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
                foreach(var param in Parameters)
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


        public MarkupBuilder Append(MarkupBuilder builder, bool fullName)
        {
            if (IsAsync)
            {
                builder.AddMarkup("[white bold]async [/]");
            }

            if (ReturnParameter != null)
            {
                {
                    var sb = new StringBuilder();
                    ReturnParameter.Append(sb);
                    builder.AddMarkup($"[blue]{Markup.Escape(sb.ToString())}[/]");
                }
                builder.Append(" ");
            }

            if (DeclaringType != null)
            {

                if (Name == ".ctor")
                {
                    if (string.IsNullOrEmpty(SubMethod) && !IsLambda)
                        builder.AddMarkup("[yellow]new[/] ");

                    AppendDeclaringTypeName(builder, fullName);
                }
                else if (Name == ".cctor")
                {
                    builder.Append("[yellow]static[/] ");
                    AppendDeclaringTypeName(builder, fullName);
                }
                else
                {
                    AppendDeclaringTypeName(builder, fullName)
                        .Append(".")
                        .AddMarkup($"[yellow bold]{Markup.Escape(Name ?? "null")}[/]");
                }
            }
            else
            {
                builder.AddMarkup($"[yellow]{Markup.Escape(Name ?? "null")}[/]");
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
                builder.AddMarkup($"[lightslateblue]{Markup.Escape(new StringBuilder().Append(SubMethod).ToString())}[/]");
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
                    builder.AddMarkup("[lightslateblue] => { }[/]");

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

        private StringBuilder AppendDeclaringTypeName(StringBuilder builder, bool fullName = true)
        {
            return DeclaringType != null ? builder.AppendTypeDisplayName(DeclaringType, fullName: fullName, includeGenericParameterNames: true) : builder;
        }

        private MarkupBuilder AppendDeclaringTypeName(MarkupBuilder builder, bool fullName = true)
        {
            StringBuilder sb = new();
            AppendDeclaringTypeName(sb,fullName);
            return builder.AddMarkup($"{Markup.Escape(sb.ToString())}");
        }
    }
}
