// <auto-generated />
#pragma warning disable

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Link = System.ComponentModel.DescriptionAttribute;
using System.Linq;

static partial class Polyfill
{

#if !NET9_0_OR_GREATER && FeatureValueTuple

    /// <summary>
    /// https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/overview#linq
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.index#system-linq-enumerable-index-1(system-collections-generic-ienumerable((-0)))")]
    public static IEnumerable<(int Index, TSource Item)> Index<TSource>(this IEnumerable<TSource> source)
    {
        int index = 0;
        foreach (var item in source)
        {
            yield return (index, item);
            index++;
        }
    }

#endif

#if !NET6_0_OR_GREATER

    /// <summary>
    ///   Attempts to determine the number of elements in a sequence without forcing an enumeration.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
    /// <param name="source">A sequence that contains elements to be counted.</param>
    /// <param name="count">
    ///     When this method returns, contains the count of <paramref name="source" /> if successful,
    ///     or zero if the method failed to determine the count.</param>
    /// <returns>
    ///   <see langword="true" /> if the count of <paramref name="source"/> can be determined without enumeration;
    ///   otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>
    ///   The method performs a series of type tests, identifying common subtypes whose
    ///   count can be determined without enumerating; this includes <see cref="ICollection{T}"/>,
    ///   <see cref="ICollection"/> as well as internal types used in the LINQ implementation.
    ///
    ///   The method is typically a constant-time operation, but ultimately this depends on the complexity
    ///   characteristics of the underlying collection implementation.
    /// </remarks>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.trygetnonenumeratedcount")]
    public static bool TryGetNonEnumeratedCount<TSource>(this IEnumerable<TSource> target, out int count)
    {
        if (target is ICollection<TSource> genericCollection)
        {
            count = genericCollection.Count;
            return true;
        }

        if (target is ICollection collection)
        {
            count = collection.Count;
            return true;
        }

        count = 0;
        return false;
    }

#endif

#if NET46X || NET47

    /// <summary>
    /// Appends a value to the end of the sequence.
    /// </summary>
    /// <param name="source">A sequence of values.</param>
    /// <param name="element">The value to append to <paramref name="target" />.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>A new sequence that ends with element.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.append")]
    public static IEnumerable<TSource> Append<TSource>(
        this IEnumerable<TSource> target,
        TSource element)
    {
        foreach (var item in target)
        {
            yield return item;
        }

        yield return element;
    }
#endif

#if NETFRAMEWORK || NETSTANDARD2_0
    /// <summary>
    /// Returns a new enumerable collection that contains the elements from source with the last count elements of the
    /// source collection omitted.
    /// </summary>
    /// <param name="source">An enumerable collection instance.</param>
    /// <param name="count">The number of elements to omit from the end of the collection.</param>
    /// <typeparam name="TSource">The type of the elements in the enumerable collection.</typeparam>
    /// <returns>A new enumerable collection that contains the elements from source minus count elements from the end
    /// of the collection.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.skiplast")]
    public static IEnumerable<TSource> SkipLast<TSource>(
        this IEnumerable<TSource> target,
        int count) =>
        target.Reverse().Skip(count).Reverse();
#endif

#if NET471 || NET46X || NETSTANDARD2_0

    /// <summary>
    /// Creates a HashSet<T> from an IEnumerable<T> using the comparer to compare keys.
    /// </summary>
    /// <param name="source">An IEnumerable<T> to create a HashSet<T> from.</param>
    /// <param name="comparer">An IEqualityComparer<T> to compare keys.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>A HashSet<T> that contains values of type TSource selected from the input sequence.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.tohashset#system-linq-enumerable-tohashset-1(system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0)))")]
    public static HashSet<TSource> ToHashSet<TSource>(
        this IEnumerable<TSource> target,
        IEqualityComparer<TSource>? comparer = null) =>
        new HashSet<TSource>(target, comparer);

#endif
}