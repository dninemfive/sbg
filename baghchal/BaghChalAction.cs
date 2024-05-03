using Point = (int x, int y);

namespace d9.bgp.baghchal;
public class BaghChalAction(string name, Func<BaghChalState, BaghChalState> function)
{
    public string Name { get; private set; } = name;
    public BaghChalState ApplyTo(BaghChalState state) => function(state);
    public static BaghChalAction PlaceSheepAt(Point p)
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
    public static BaghChalAction Move(BaghChalPlayer player, Point source, Point destination)
    {
        string name = $"Move {player} from {source} to {destination}";
        return new(name, delegate (BaghChalState state)
        {
            if (player is not BaghChalPlayer.Sheep or BaghChalPlayer.Wolf
             || state.Board[source] != player
             || !state.Board[destination].IsEmpty()
             || !source.IsBaghChalAdjacentTo(destination))
                throw new Exception($"`{name}` is not a valid action for the current state!");
            return new(state.Board.Spaces.With((source, null), (destination, player)), state.UnplacedSheep, state.CapturedSheep);
        });
    }
    public static BaghChalAction Capture(Point source, Point destination)
        => throw new NotImplementedException();
}