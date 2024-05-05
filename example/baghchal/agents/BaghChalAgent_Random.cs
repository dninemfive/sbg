using d9.utl;

namespace d9.sbg.example.baghchal;
public class BaghChalAgent_Random : IBaghChalAgent
{
    public BaghChalAction SelectAction(IEnumerable<BaghChalAction> possibleActions)
        => possibleActions.RandomElement();
}
