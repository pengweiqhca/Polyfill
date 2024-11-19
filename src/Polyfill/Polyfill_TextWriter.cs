// <auto-generated />
#pragma warning disable

namespace Polyfills;

using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
#if FeatureMemory
using System.Buffers;
#endif

static partial class Polyfill
{
#if !NET8_0_OR_GREATER

    //https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/IO/TextWriter.cs#L670

    /// <summary>
    /// Asynchronously clears all buffers for the current writer and causes any buffered data to
    /// be written to the underlying device.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous flush operation.</returns>
    /// <exception cref="ObjectDisposedException">The text writer is disposed.</exception>
    /// <exception cref="InvalidOperationException">The writer is currently in use by a previous write operation.</exception>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.flushasync#system-io-textwriter-flushasync(system-threading-cancellationtoken)
    public static Task FlushAsync(this TextWriter target, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        return target.FlushAsync()
            .WaitAsync(cancellationToken);
    }

#endif

#if !NETCOREAPP3_0_OR_GREATER
    /// <summary>
    /// Equivalent to Write(stringBuilder.ToString()) however it uses the
    /// StringBuilder.GetChunks() method to avoid creating the intermediate string
    /// </summary>
    /// <param name="value">The string (as a StringBuilder) to write to the stream</param>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write#system-io-textwriter-write(system-text-stringbuilder)
    public static void Write(this TextWriter target, StringBuilder? value)
    {
        if (value == null)
        {
            return;
        }

#if FeatureMemory
        foreach (ReadOnlyMemory<char> chunk in value.GetChunks())
        {
            target.Write(chunk.Span);
        }
#else
        target.Write(value.ToString());
#endif
    }

    /// <summary>
    /// Equivalent to WriteAsync(stringBuilder.ToString()) however it uses the
    /// StringBuilder.GetChunks() method to avoid creating the intermediate string
    /// </summary>
    /// <param name="value">The string (as a StringBuilder) to write to the stream</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken)
    public static Task WriteAsync(this TextWriter target, StringBuilder? value, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        if (value == null)
        {
            return Task.CompletedTask;
        }

        return WriteAsyncCore(value, cancellationToken);

        async Task WriteAsyncCore(StringBuilder builder, CancellationToken cancel)
        {
#if FeatureValueTask && FeatureMemory
            foreach (ReadOnlyMemory<char> chunk in builder.GetChunks())
            {
                await target.WriteAsync(chunk, cancel).ConfigureAwait(false);
            }
#else
            await target.WriteAsync(builder.ToString())
                .WaitAsync(cancellationToken);
#endif
        }
    }
#endif

#if (NETFRAMEWORK || NETSTANDARD2_0 || NETCOREAPP2_0) && FeatureMemory
#if FeatureValueTask

    /// <summary>
    /// Asynchronously writes a character memory region to the stream.
    /// </summary>
    /// <param name="buffer">The character memory region to write to the stream.</param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests.
    /// The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken)
    public static ValueTask WriteAsync(
        this TextWriter target,
        ReadOnlyMemory<char> buffer,
        CancellationToken cancellationToken = default)
    {
        // StreamReader doesn't accept cancellation token (pre-netstd2.1)
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray(buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        var task = target.WriteAsync(segment.Array!, segment.Offset, segment.Count)
            .WaitAsync(cancellationToken);
        return new(task);
    }

    /// <summary>
    /// Asynchronously writes the text representation of a character memory region to the stream, followed by a line terminator.
    /// </summary>
    /// <param name="buffer">The character memory region to write to the stream.</param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests.
    /// The default value is <see cref="CancellationToken.None"/>.
    /// </param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync#system-io-textwriter-writelineasync(system-readonlymemory((system-char))-system-threading-cancellationtoken)
    public static ValueTask WriteLineAsync(
        this TextWriter target,
        ReadOnlyMemory<char> buffer,
        CancellationToken cancellationToken = default)
    {
        // StreamReader doesn't accept cancellation token (pre-netstd2.1)
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray(buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        var task = target.WriteLineAsync(segment.Array!, segment.Offset, segment.Count)
            .WaitAsync(cancellationToken);
        return new(task);
    }

#endif

    /// <summary>
    /// Writes a character span to the text stream.
    /// </summary>
    /// <param name="buffer">The character span to write.</param>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write#system-io-textwriter-write(system-readonlyspan((system-char)))
    public static void Write(
        this TextWriter target,
        ReadOnlySpan<char> buffer)
    {
        var pool = ArrayPool<char>.Shared;
        var array = pool.Rent(buffer.Length);

        try
        {
            buffer.CopyTo(new(array));
            target.Write(array, 0, buffer.Length);
        }
        finally
        {
            pool.Return(array);
        }
    }

    /// <summary>
    /// Writes the text representation of a character span to the text stream, followed by a line terminator.
    /// </summary>
    /// <param name="buffer">The char span value to write to the text stream.</param>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeline#system-io-textwriter-writeline(system-readonlyspan((system-char)))
    public static void WriteLine(
        this TextWriter target,
        ReadOnlySpan<char> buffer)
    {
        var pool = ArrayPool<char>.Shared;
        var array = pool.Rent(buffer.Length);

        try
        {
            buffer.CopyTo(new(array));
            target.WriteLine(array, 0, buffer.Length);
        }
        finally
        {
            pool.Return(array);
        }
    }
#endif
}