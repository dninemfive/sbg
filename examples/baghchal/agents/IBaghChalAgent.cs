namespace d9.sbg.examples.baghchal;
public interface IBaghChalAgent
{
    public BaghChalAction SelectAction(IEnumerable<BaghChalAction> possibleActions);
}
