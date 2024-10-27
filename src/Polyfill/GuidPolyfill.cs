// <auto-generated />
#pragma warning disable

namespace Polyfills;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Link = System.ComponentModel.DescriptionAttribute;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyPublic
public
#endif
static class GuidPolyfill
{
    /// <summary>
    /// Tries to parse a string into a value.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse#system-guid-tryparse(system-string-system-iformatprovider-system-guid@)")]
    public static bool TryParse(string? target, IFormatProvider? provider, out Guid result) =>
#if NET7_0_OR_GREATER
        Guid.TryParse(target, provider, out result);
#else
        Guid.TryParse(target, out result);
#endif

#if FeatureMemory

    /// <summary>
    /// Converts span of characters representing the GUID to the equivalent Guid structure, provided that the string is in the specified format.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparseexact#system-guid-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-guid@)")]
    public static bool TryParseExact(ReadOnlySpan<char> target, ReadOnlySpan<char> format, out Guid result) =>
#if NETFRAMEWORK || NETCOREAPP2_0 || NETSTANDARD2_0
        Guid.TryParseExact(target.ToString(), format.ToString(), out result);
#else
        Guid.TryParseExact(target, format, out result);
#endif

    /// <summary>
    /// Tries to parse a span of UTF-8 characters into a value.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse#system-guid-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-guid@)")]
    public static bool TryParse(ReadOnlySpan<char> target, IFormatProvider? provider, out Guid result) =>
#if NET7_0_OR_GREATER
        Guid.TryParse(target, provider, out result);
#else
        Guid.TryParse(target.ToString(), out result);
#endif

    /// <summary>
    /// Tries to parse a span of UTF-8 characters into a value.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-8.0#system-guid-tryparse(system-readonlyspan((system-char))-system-guid@)")]
    public static bool TryParse(ReadOnlySpan<char> target, out Guid result) =>
#if NETSTANDARD2_1 || NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
        Guid.TryParse(target, out result);
#else
        Guid.TryParse(target.ToString(), out result);
#endif

#endif
}