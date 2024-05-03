namespace d9.bgp.baghchal;
public readonly struct BaghChalBoard : IBoard<Point<int>, BaghChalPlayer?>
{
    public static readonly AdjacencyRuleDef<int> AdjacencyRule = AdjacencyRules<int>.NotSelf
        & ((AdjacencyRules<int>.WithinDistance(DistanceMetrics<int, int>.Taxicab, 1) & AdjacencyRules<int>.SameColumnOrRow())
        |  (AdjacencyRules<int>.WithinDistance(DistanceMetrics<int, int>.Taxicab, 2) & AdjacencyRules<int>.SameDiagonalParity())); 
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
    public static BaghChalBoard Initial
    {
        get
        {
            BaghChalPlayer?[,] result = new BaghChalPlayer?[5, 5];
            return result.With(BaghChalPlayer.Wolf, result.Corners());
        }
    }
    public BaghChalPlayer? this[int x, int y]
        => Spaces[x, y];
    public BaghChalPlayer? this[Point<int> p]
        => this[p.X, p.Y];
    private string RowString(int y)
    {
        string result = "";
        for (int x = 0; x < 5; x++)
            result += this[x, y] switch
            {
                BaghChalPlayer.Sheep => "O",//"◯",
                BaghChalPlayer.Wolf => "x",//"◆",
                _ => " "
            };
        return result;
    }
    public override string ToString()
    {
        string result = "┌─────┐\n";
        for (int y = 0; y < 5; y++)
            result += $"│{RowString(y)}│\n";
        result += "└─────┘";
        return result;
    }
    public int Width => Spaces.GetLength(0);
    public int Height => Spaces.GetLength(1);
    public IEnumerable<Point<int>> AllSpaces
    {
        get
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Width; y++)
                    yield return (x, y);
        }
    }
    public bool Contains(Point<int> p)
    {
        (int x, int y) = p;
        return x is >= 0 and < 5 && y is >= 0 and < 5;
    }
    public IEnumerable<Point<int>> SpacesWithPlayer(BaghChalPlayer? player)
    {
        foreach (Point<int> p in AllSpaces)
            if (this[p] == player)
                yield return p;
    }
    public IEnumerable<Point<int>> EmptySpaces
        => SpacesWithPlayer(null);
    public IEnumerable<Point<int>> WolfSpaces
        => SpacesWithPlayer(BaghChalPlayer.Wolf);
    public IEnumerable<Point<int>> SheepSpaces
        => SpacesWithPlayer(BaghChalPlayer.Sheep);
    public bool AreAdjacent(Point<int> a, Point<int> b)
        => AdjacencyRule.AreAdjacent(a, b);
}