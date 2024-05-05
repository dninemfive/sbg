namespace d9.sbg.example.baghchal;
public interface IBaghChalAgent
{
    public BaghChalAction SelectAction(IEnumerable<BaghChalAction> possibleActions);
}
