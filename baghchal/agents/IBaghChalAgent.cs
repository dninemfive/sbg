namespace d9.sbg.baghchal;
public interface IBaghChalAgent
{
    public BaghChalAction SelectAction(IEnumerable<BaghChalAction> possibleActions);
}
