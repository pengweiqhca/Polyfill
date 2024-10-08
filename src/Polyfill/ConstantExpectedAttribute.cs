﻿// <auto-generated />
#pragma warning disable

#if !NET7_0_OR_GREATER

#nullable enable
namespace System.Diagnostics.CodeAnalysis;

using Diagnostics;
using Link = ComponentModel.DescriptionAttribute;

/// <summary>
/// Indicates that the specified method parameter expects a constant.
/// </summary>
/// <remarks>
/// This can be used to inform tooling that a constant should be used as an argument for the annotated parameter.
/// </remarks>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
[Link("https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.constantexpectedattribute")]
#if PolyPublic
public
#endif
sealed class ConstantExpectedAttribute : Attribute
{
    /// <summary>
    /// Indicates the minimum bound of the expected constant, inclusive.
    /// </summary>
    public object? Min { get; set; }
    /// <summary>
    /// Indicates the maximum bound of the expected constant, inclusive.
    /// </summary>
    public object? Max { get; set; }
}
#endif