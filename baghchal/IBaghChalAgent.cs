namespace d9.bgp.baghchal;
public interface IBaghChalAgent
{
    public BaghChalAction SelectAction(IEnumerable<BaghChalAction> possibleActions);
}
