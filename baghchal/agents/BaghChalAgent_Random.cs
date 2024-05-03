using d9.utl;

namespace d9.bgp.baghchal;
public class BaghChalAgent_Random : IBaghChalAgent
{
    public BaghChalAction SelectAction(IEnumerable<BaghChalAction> possibleActions)
        => possibleActions.RandomElement();
}
