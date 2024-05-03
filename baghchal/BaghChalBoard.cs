using Point = (int x, int y);
namespace d9.bgp.baghchal;
public readonly struct BaghChalBoard
{
    public readonly BaghChalPlayer?[,] Spaces;
    public BaghChalBoard(BaghChalPlayer?[,] spaces)
    {
        if (spaces.GetLength(0) != 5 || spaces.GetLength(1) != 5)
            throw new ArgumentException("A bagh chal board must be 5x5!", nameof(spaces));
        Spaces = spaces;
    }
    public static implicit operator BaghChalBoard(BaghChalPlayer?[,] spaces)
        => new(spaces);
    public static implicit operator BaghChalPlayer[,]?(BaghChalBoard board)
        => (BaghChalPlayer[,]?)board.Spaces.Clone();
    public static BaghChalBoard InitialBoard
    {
        get
        {
            BaghChalPlayer?[,] result = new BaghChalPlayer?[5, 5];
            return result.With(BaghChalPlayer.Wolf, result.Corners());
        }
    }
    public BaghChalPlayer? this[Point p]
        => Spaces[p.x, p.y];
}