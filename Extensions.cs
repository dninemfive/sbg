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
        int x = array.GetLength(0), y = array.GetLength(1);
        return [
            (0, 0),
            (x, 0),
            (x, y),
            (0, y)
        ];
    }
    public static bool IsBaghChalAdjacentTo(this Point a, Point b)
        => throw new NotImplementedException();
}
