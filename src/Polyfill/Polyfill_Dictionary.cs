// <auto-generated />
#pragma warning disable

namespace Polyfills;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

static partial class Polyfill
{
#if !NET7_0_OR_GREATER
    /// <summary>
    /// Returns a read-only <see cref="ReadOnlyDictionary{TKey,TValue}"/> wrapper for the current dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    /// <param name="dictionary">The dictionary to wrap.</param>
    /// <returns>An object that acts as a read-only wrapper around the current <see cref="IDictionary{TKey, TValue}"/>.</returns>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly#system-collections-generic-collectionextensions-asreadonly-2(system-collections-generic-idictionary((-0-1)))
    public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> target)
        where TKey : notnull =>
        new(target);
#endif

#if NETFRAMEWORK || NETSTANDARD2_0

    /// <summary>
    /// Attempts to add the specified key and value to the <see cref="IDictionary{TKey,TValue}"/>.
    /// </summary>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="value">The value of the element to add. It can be <see langword="null"/>.</param>
    /// <returns><c>true</c> if the key/value pair was added to the dictionary successfully; otherwise, <c>false</c>.</returns>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.tryadd
    public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> target, TKey key, TValue value)
        where TKey : notnull
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        if (!target.ContainsKey(key))
        {
            target.Add(key, value);
            return true;
        }

        return false;
    }

#endif

#if NETFRAMEWORK || NETSTANDARD2_0

    /// <summary>
    /// Removes the value with the specified key from the <see cref="IDictionary{TKey,TValue}"/>, and copies the element
    /// to the value parameter.
    /// </summary>
    /// <param name="target">A dictionary with keys of type TKey and values of type TValue.</param>
    /// <param name="key">The key of the element to remove.</param>
    /// <param name="value">The removed element.</param>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <returns><code>true</code> if the element is successfully found and removed; otherwise, <code>false</code>.</returns>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.remove
    public static bool Remove<TKey, TValue>(
        this IDictionary<TKey, TValue> target,
        TKey key,
        [MaybeNullWhen(false)] out TValue value)
        where TKey : notnull
    {
        target.TryGetValue(key, out value);
        return target.Remove(key);
    }

#endif
}
