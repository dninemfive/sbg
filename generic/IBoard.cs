namespace d9.bgp;
public interface IBoard<TCoordinate, TValue>
{
    public IEnumerable<TCoordinate> AllSpaces { get; }
    public virtual bool Contains(TCoordinate p)
        => AllSpaces.Contains(p);
    public TValue this[TCoordinate coordinate] { get; }
}
