namespace d9.bgp.generic;
public abstract class BoardGameRules<TCoordinate, TValue>
{
    public abstract AdjacencyRuleDef<TCoordinate> Adjacency { get; }
    public virtual bool AreAdjacent(TCoordinate a, TCoordinate b)
        => Adjacency.AreAdjacent(a, b);
    public abstract IBoard<TCoordinate, TValue> NewBoard();
}
