// Copyright (c) Ben A Adams. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Pillar.Demystifier;
using System.Runtime.Serialization;
using System.Text;

namespace System.Diagnostics
{
    public class ResolvedParameter
    {
        public string? Name { get; set; }

		// this doesn't serialize well for generics
		[IgnoreDataMember]
        public Type ResolvedType { get; set; }

		// Only used if serialized
		public string? ResolvedTypeStr {
			get => _ResolvedTypeStr ??= ResolvedType == null ? string.Empty : TypeAsStr();
			set => _ResolvedTypeStr = value;
		}
		private string? _ResolvedTypeStr;
		private string TypeAsStr(){
			StringBuilder stringBuilder = new();
            stringBuilder.AppendTypeDisplayName(ResolvedType, fullName: false, includeGenericParameterNames: true);
			return stringBuilder.ToString();
		}


        public string? Prefix { get; set; }
        public bool IsDynamicType { get; set; }

        public ResolvedParameter(Type resolvedType) => ResolvedType = resolvedType;

        public override string ToString() => Append(new StringBuilder()).ToString();

        public StringBuilder Append(StringBuilder sb)
        {
            if (ResolvedType?.Assembly.ManifestModule.Name == "FSharp.Core.dll" && ResolvedType?.Name == "Unit")
                return sb;
            
            if (!string.IsNullOrEmpty(Prefix))
            {
                sb.Append(Prefix)
                  .Append(" ");
            }

            if (IsDynamicType)
            {
                sb.Append("dynamic");
            }
            else if (ResolvedType != null || _ResolvedTypeStr != null)
            {
                AppendTypeName(sb);
            }
            else
            {
                sb.Append("?");
            }

            if (!string.IsNullOrEmpty(Name))
            {
                sb.Append(" ")
                  .Append(Name);
            }

            return sb;
        }

        public StyledBuilder Append(StyledBuilder sb,StyledBuilderOption option)
        {
				if (ResolvedType?.Assembly.ManifestModule.Name == "FSharp.Core.dll" && ResolvedType?.Name == "Unit")
                return sb;

            if (!string.IsNullOrEmpty(Prefix))
            {
                sb.Append(option.ParamPrefixStyle, Prefix ?? string.Empty)
                  .Append(" ");
            }

            if (IsDynamicType)
            {
                sb.Append(option.KeywordDynamicStyle,"dynamic");
            }
            else if (ResolvedType != null || _ResolvedTypeStr != null)
            {
                AppendTypeName(sb,option);
            }
            else
            {
                sb.Append("?");
            }

            if (!string.IsNullOrEmpty(Name))
            {
                sb.Append(" ")
                  .Append(option.ParamNameStyle,Name ?? string.Empty);
            }

            return sb;
        }

        protected virtual void AppendTypeName(StringBuilder sb) 
        {
			if (_ResolvedTypeStr != null)
			{
				sb.Append(_ResolvedTypeStr);
				return;
			}
            sb.AppendTypeDisplayName(ResolvedType, fullName: false, includeGenericParameterNames: true);
        }

        protected virtual void AppendTypeName(StyledBuilder sb,StyledBuilderOption option)
        {

            sb.Append(option.ParamTypeStyle, _ResolvedTypeStr ?? TypeAsStr() );
        }
    }
}
