using Point = (int x, int y);

namespace d9.bgp.baghchal;
public class BaghChalAction(string name, Func<BaghChalState, BaghChalState?> function)
{
    public string Name { get; private set; } = name;
    public BaghChalState? ApplyTo(BaghChalState state) => function(state);
    public static BaghChalAction PlaceSheepAt(Point p)
        => new($"Place sheep at {p}", delegate (BaghChalState state)
        {
            if (state.UnplacedSheep < 1
             || state.CapturedSheep >= 5
             || !state.Board[p].IsEmpty())
                return null;
            return new(state.Board.Spaces.With(BaghChalPlayer.Sheep, [p]), state.UnplacedSheep - 1, state.CapturedSheep);
        });
    public static BaghChalAction Move(BaghChalPlayer player, Point source, Point destination)
        => new($"Move {player} from {source} to {destination}", delegate (BaghChalState state)
        {
        if (player is not BaghChalPlayer.Sheep or BaghChalPlayer.Wolf
         || state.Board[source] != player
         || !state.Board[destination].IsEmpty()
         || !source.IsBaghChalAdjacentTo(destination))
                return null;
            return new(state.Board.Spaces.With((source, null), (destination, player)), state.UnplacedSheep, state.CapturedSheep);
        });
    public static BaghChalAction Capture(Point source, Point destination)
        => throw new NotImplementedException();
}