using d9.utl;
using System.Numerics;
namespace d9.bgp;
public static class Extensions
{
    public static T[,] With<T>(this T[,] original, params (Point<int> p, T value)[] differences)
    {
        T[,] result = (T[,])original.Clone();
        foreach (((int x, int y), T value) in differences)
            result[x, y] = value;
        return result;
    }
    public static T[,] With<T>(this T[,] original, T value, IEnumerable<Point<int>> positions)
        => original.With(positions.Select(x => (x, value)).ToArray());
    public static IEnumerable<Point<int>> Corners<T>(this T[,] array)
    {
        int x = array.GetLength(0) - 1, y = array.GetLength(1) - 1;
        return [
            (0, 0),
            (x, 0),
            (x, y),
            (0, y)
        ];
    }
    public static string IndentLines(this string s, int n = 1, string tab = "  ")
        => $"{tab.Repeated(n)}{s.Replace("\n", $"\n{tab.Repeated(n)}")}";
    public static bool IsEven(this int i)
        => i % 2 == 0;
    public static string Plural<T>(this string s, T n, string ifPlural = "s")
        where T : INumberBase<T>
        => $"{s}{(n != T.One ? ifPlural : "")}";
}
