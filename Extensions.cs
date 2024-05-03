using d9.utl;
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
    // all valid diagonal moves either enter or exit these points
    // ... or they hop over them, technically
    // (but those are attacks in the logic)
    public static bool IsBaghChalDiagonalCenter(this Point p)
        => p is (1, 1) or (3, 1) or (3, 3) or (1, 3);
    public static bool IsBaghChalAdjacentTo(this Point a, Point b)
    {
        if (a == b)
            return false;
        (int ax, int ay) = a;
        (int bx, int by) = b;
        int xDiff = Math.Abs(ax - bx), yDiff = Math.Abs(ay - by);
        if (xDiff is not (0 or 1) || yDiff is not (0 or 1))
            return false;
        if (xDiff != yDiff) // one space off in x xor y dimensions
            return true;
        return a.IsBaghChalDiagonalCenter() || b.IsBaghChalDiagonalCenter();
    }
    public static IEnumerable<Point> AllCoordinates<T>(this T[,] array)
    {
        for (int x = 0; x < array.GetLength(0); x++)
            for (int y = 0; y < array.GetLength(1); y++)
                yield return (x, y);
    }
    public static IEnumerable<Point> BaghChalAdjacentPoints(this Point a)
    {
        static bool inBounds(Point p)
            => p.x is >= 0 and < 5 && p.y is >= 0 and < 5;
        (int ax, int ay) = a;
        for(int xo = -1; xo <= 1; xo++)
            for(int yo = -1; yo <= 1; yo++)
            {
                if (xo == 0 && yo == 0)
                    continue;
                Point p = (ax + xo, ay + yo);
                if (inBounds(p) && a.IsBaghChalAdjacentTo(p))
                    yield return p;
            }
    }
    public static string IndentLines(this string s, int n = 1, string tab = "  ")
        => $"{tab.Repeated(n)}{s.Replace("\n", $"\n{tab.Repeated(n)}")}";
}
