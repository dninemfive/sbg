namespace d9.bgp.baghchal;
public class BaghChalGame(IBaghChalAgent sheep, IBaghChalAgent wolf)
{
    public IBaghChalAgent Sheep { get; private set; } = sheep;
    public IBaghChalAgent Wolf { get; private set; } = wolf;
    public BaghChalState CurrentState = BaghChalState.Initial;
    public int Turn = 1;
    public BaghChalPlayer CurrentPlayer => Turn % 2 == 1 ? BaghChalPlayer.Sheep : BaghChalPlayer.Wolf;
    public IBaghChalAgent CurrentAgent => CurrentPlayer switch
    {
        BaghChalPlayer.Sheep => Sheep,
        BaghChalPlayer.Wolf => Wolf,
        _ => throw new ArgumentOutOfRangeException(nameof(CurrentPlayer))
    };
    public IEnumerable<BaghChalAction> PossibleActions
        => CurrentState.PossibleActionsFor(CurrentPlayer);
    public void DoTurn()
    {
        BaghChalAction action = CurrentAgent.SelectAction(PossibleActions);
    }
}
