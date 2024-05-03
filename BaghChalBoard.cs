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
    public static implicit operator SpaceState[,](BaghChalBoard board)
        => (SpaceState[,])board.Spaces.Clone();
    public static BaghChalBoard InitialBoard
    {
        get
        {
            SpaceState[,] result = new SpaceState[5, 5];
            return result.With(SpaceState.Wolf, result.Corners());
        }
    }
    public SpaceState this[Point p]
        => Spaces[p.x, p.y];
}
public readonly struct BaghChalState(BaghChalBoard board, int unplacedSheep, int capturedSheep)
{
    public readonly int UnplacedSheep = unplacedSheep,
                        CapturedSheep = capturedSheep;
    public readonly BaghChalBoard Board = board;
    public static BaghChalState InitialState => new(BaghChalBoard.InitialBoard, 20, 0);
}
public class BaghChalAction(string name, Func<BaghChalState, BaghChalState?> function)
{
    public string Name { get; private set; } = name;
    public BaghChalState? ApplyTo(BaghChalState state) => function(state);
    public static BaghChalAction PlaceSheepAt(Point p)
        => new($"Place sheep at {p}", delegate (BaghChalState state)
        {
            if (state.UnplacedSheep < 1
             || state.CapturedSheep >= 5
             || state.Board[p] != SpaceState.Empty)
                return null;
            return new(state.Board.Spaces.With(SpaceState.Sheep, [p]), state.UnplacedSheep - 1, state.CapturedSheep);
        });
    public static BaghChalAction Move(SpaceState player, Point source, Point destination)
        => new($"Move {player} from {source} to {destination}", delegate (BaghChalState state)
        {
            if (player is not SpaceState.Sheep or SpaceState.Wolf
             || state.Board[source] != player
             || state.Board[destination] != SpaceState.Empty
             || !source.IsBaghChalAdjacentTo(destination))
                return null;
            return new(state.Board.Spaces.With((source, SpaceState.Empty), (destination, player)), state.UnplacedSheep, state.CapturedSheep);
        });
    public static BaghChalAction Capture(Point source, Point destination)
        => throw new NotImplementedException();
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
    public static bool IsBaghChalAdjacentTo(this Point a, Point b)
        => throw new NotImplementedException();
}
