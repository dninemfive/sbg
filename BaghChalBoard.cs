using Point = (int x, int y);
namespace bgp;
public enum SpaceState { Empty, Sheep, Wolf }
public readonly struct BaghChalBoard
{
    public readonly SpaceState[,] Spaces;
    public BaghChalBoard(SpaceState[,] spaces)
    {
        if (spaces.GetLength(0) != 5 || spaces.GetLength(1) != 5)
            throw new ArgumentException("A bagh chal board must be 5x5!", nameof(spaces));
        Spaces = spaces;
    }
    public static implicit operator BaghChalBoard(SpaceState[,] spaces)
        => new(spaces);
    public static BaghChalBoard InitialState
    {
        get
        {
            SpaceState[,] result = new SpaceState[5, 5];
            return result.With(SpaceState.Wolf, result.Corners());
        }
    }
}
public static class Extensions
{
    public static T[,] With<T>(this T[,] original, params (Point p, T value)[] differences)
    {
        T[,] result = (T[,])original.Clone();
        foreach(((int x, int y), T value) in differences)
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
}