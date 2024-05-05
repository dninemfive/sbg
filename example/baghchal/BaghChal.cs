namespace d9.sbg.example.baghchal;
public class BaghChal : BoardGameRules<Point<int>, BaghChalPlayer?>
{
    public static readonly BaghChal Rules = new();
    public override AdjacencyRuleDef<Point<int>> Adjacency => PointAdjacencyRules<int>.NotSelf
        & ((PointAdjacencyRules<int>.WithinDistance(DistanceMetrics<int, int>.Taxicab, 1) & PointAdjacencyRules<int>.SameColumnOrRow())
        |  (PointAdjacencyRules<int>.WithinDistance(DistanceMetrics<int, int>.Taxicab, 2) & PointAdjacencyRules<int>.SameDiagonalParity()));
    public override bool AreAdjacent(Point<int> a, Point<int> b)
        => Adjacency.AreAdjacent(a, b);
    public override IBoard<Point<int>, BaghChalPlayer?> NewBoard()
        => BaghChalBoard.Initial;
}
