namespace d9.bgp.generic;
public abstract class BoardGameRules<TCoordinate, TValue, TBoard>
    where TBoard : IBoard<TCoordinate, TValue>
{
    public abstract AdjacencyRule<int> AdjacencyRule { get; }
}
