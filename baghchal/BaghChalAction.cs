namespace d9.bgp.baghchal;
public class BaghChalAction(string name, Func<BaghChalState, BaghChalState> function)
{
    public string Name { get; private set; } = name;
    public BaghChalState ApplyTo(BaghChalState state) => function(state);
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
        List<Func<BaghChalState, Exception?>> validators = [
            (_)     => player is not (BaghChalPlayer.Sheep or BaghChalPlayer.Wolf) ? new($"{player} is not a valid player!")            : null,
            (state) => state.Board[source] != player                               ? new($"{source} is not {player}!")                  : null,
            (state) => !state.Board[destination].IsEmpty()                         ? new($"{destination} is not empty!")                : null,
            (_)     => !BaghChal.Rules.AreAdjacent(source, destination)            ? new($"{source} is not adjacent to {destination}!") : null
        ];
        return new(name, delegate (BaghChalState state)
        {
            IEnumerable<Exception?> exceptions = validators.Select(x => x(state)).Where(x => x is not null);
            if (exceptions.Any())
                throw exceptions.First()!;
            return new(state.Board.Spaces.With((source, null), (destination, player)), state.UnplacedSheep, state.CapturedSheep);
        });
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