using d9.utl;
using System.Numerics;
using Point = (int x, int y);
namespace d9.bgp;
public static class Extensions
{
    public static T[,] With<T>(this T[,] original, params (Point p, T value)[] differences)
    {
        T[,] result = (T[,])original.Clone();
        foreach (((int x, int y), T value) in differences)
            result[x, y] = value;
        return result;
    }
    public static T[,] With<T>(this T[,] original, T value, IEnumerable<Point> positions)
        => original.With(positions.Select(x => (x, value)).ToArray());
    public static IEnumerable<Point> Corners<T>(this T[,] array)
    {
        int x = array.GetLength(0) - 1, y = array.GetLength(1) - 1;
        return [
            (0, 0),
            (x, 0),
            (x, y),
            (0, y)
        ];
    }
    public static int TaxicabDistanceTo(this Point a, Point b)
        => Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    public static bool IsBaghChalAdjacentTo(this Point a, Point b)
        => a.TaxicabDistanceTo(b) switch
        {
            1 => true,
            2 => (a.x - a.y).IsEven(),
            _ => false
        };
    public static IEnumerable<Point> AllCoordinates<T>(this T[,] array)
    {
        for (int x = 0; x < array.GetLength(0); x++)
            for (int y = 0; y < array.GetLength(1); y++)
                yield return (x, y);
    }
    public static string IndentLines(this string s, int n = 1, string tab = "  ")
        => $"{tab.Repeated(n)}{s.Replace("\n", $"\n{tab.Repeated(n)}")}";
    public static bool IsEven(this int i)
        => i % 2 == 0;
    public static int Parity(this Point p)
        => p.x - p.y;
    public static string Plural<T>(this string s, T n, string ifPlural = "s")
        where T : INumberBase<T>
        => $"{s}{(n != T.One ? ifPlural : "")}";
}
