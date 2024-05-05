namespace d9.sbg.baghchal;
public class BaghChalAction(string name, TransitionFunction<BaghChalState> transitionFunction, params ActionValidator<BaghChalState>[] validators)
    : IGameAction<BaghChalState>
{
    public string Name { get; } = name;
    public IReadOnlyCollection<ActionValidator<BaghChalState>> Validators { get; } = validators.ToList();
    BaghChalState IGameAction<BaghChalState>.ApplyToInternal(BaghChalState state) => transitionFunction(state);
    public BaghChalAction(string name, TransitionFunction<BaghChalState> tf, IEnumerable<ActionValidator<BaghChalState>> validators)
        : this(name, tf, validators.ToArray()) { }
    public static BaghChalAction PlaceSheepAt(Point<int> p)
    {
        string name = $"Place sheep at {p}";
        return new(name, delegate (BaghChalState state)
        {
            if (state.UnplacedSheep < 1
             || state.CapturedSheep >= 5
             || !state.Board[p].IsEmpty())
                throw new Exception($"`{name}` is not a valid action for the current state!");
            return new(state.Board.Spaces.With(BaghChalPlayer.Sheep, [p]), state.UnplacedSheep - 1, state.CapturedSheep);
        });
    }
    public static BaghChalAction Move(BaghChalPlayer player, Point<int> source, Point<int> destination)
    {
        string name = $"Move {player} from {source} to {destination}";
        List<ActionValidator<BaghChalState>> validators = [
            (_)     => player is not (BaghChalPlayer.Sheep or BaghChalPlayer.Wolf) ? new($"{player} is not a valid player!")            : null,
            (state) => state.Board[source] != player                               ? new($"{source} is not {player}!")                  : null,
            (state) => !state.Board[destination].IsEmpty()                         ? new($"{destination} is not empty!")                : null,
            (_)     => !BaghChal.Rules.AreAdjacent(source, destination)            ? new($"{source} is not adjacent to {destination}!") : null
        ];
        return new(name, (state) => state.With(differences: [(source, null), (destination, player)]), validators);
    }
    public static BaghChalAction Capture(Point<int> source, Point<int> sheep, Point<int> destination)
    {
        string name = $"Capture {sheep} with {source} -> {destination}";
        // todo: validators e.g. source, sheep, destination all on board and in line (in order)
        return new(name, delegate (BaghChalState state)
        {
            return state.With(sheepCaptured: 1, differences: [(source, null), (sheep, null), (destination, BaghChalPlayer.Wolf)]);
        });
    }
}