using Point = (int x, int y);
namespace d9.bgp.baghchal;
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