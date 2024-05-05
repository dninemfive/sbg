using d9.utl;
using System.Numerics;
namespace d9.sbg;
public static class Extensions
{
    public static string IndentLines(this string s, int n = 1, string tab = "  ")
        => $"{tab.Repeated(n)}{s.Replace("\n", $"\n{tab.Repeated(n)}")}";
    public static string Plural<T>(this string s, T n, string ifPlural = "s")
        where T : INumberBase<T>
        => $"{s}{(n != T.One ? ifPlural : "")}";
    public static IEnumerable<T> NonNullElements<T>(this IEnumerable<T?> maybeNullElements)
    {
        foreach (T? element in maybeNullElements)
            if (element is T t)
                yield return t;
    }
}
