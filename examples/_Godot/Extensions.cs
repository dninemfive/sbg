namespace d9.sbg.examples;
public static class Extensions {
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
}