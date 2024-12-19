// <auto-generated />

using System.Runtime.InteropServices;
using System.Text;

#pragma warning disable

#if FeatureMemory

namespace Polyfills;

using System;

static partial class Polyfill
{
#if NETCOREAPP2_0 || NETFRAMEWORK || NETSTANDARD2_0
    /// <summary>
    /// When overridden in a derived class, calculates the number of bytes produced by encoding the characters in the specified character span.
    /// </summary>
    /// <param name="encoding"></param>
    /// <param name="chars">The span of characters to encode.</param>
    /// <returns>The number of bytes produced by encoding the specified character span.</returns>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getbytecount#system-text-encoding-getbytecount(system-readonlyspan((system-char)))
    public static int GetByteCount(this Encoding target, ReadOnlySpan<char> chars) =>
        target.GetByteCount(chars.ToArray());
#endif

#if AllowUnsafeBlocks && !NETCOREAPP2_1_OR_GREATER
    /// <summary>When overridden in a derived class, encodes into a span of bytes a set of characters from the specified read-only span.</summary>
    /// <param name="chars">The span containing the set of characters to encode.</param>
    /// <param name="bytes">The byte span to hold the encoded bytes.</param>
    /// <returns>The number of encoded bytes.</returns>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getbytes#system-text-encoding-getbytes(system-readonlyspan((system-char))-system-span((system-byte)))
    public static unsafe int GetBytes(this Encoding target, ReadOnlySpan<char> chars, Span<byte> bytes)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        fixed (char* charsPtr = chars)
        fixed (byte* bytesPtr = bytes)
        {
            return target.GetBytes(charsPtr, chars.Length, bytesPtr, bytes.Length);
        }
    }

    /// <summary>When overridden in a derived class, decodes all the bytes in the specified byte span into a string.</summary>
    /// <param name="bytes">A read-only byte span to decode to a Unicode string.</param>
    /// <returns>A string that contains the decoded bytes from the provided read-only span.</returns>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getstring#system-text-encoding-getstring(system-readonlyspan((system-byte)))
    public static unsafe string GetString(this Encoding target, ReadOnlySpan<byte> bytes)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        fixed (byte* bytesPtr = bytes)
        {
            return target.GetString(bytesPtr, bytes.Length);
        }
    }
#endif
}

#endif
