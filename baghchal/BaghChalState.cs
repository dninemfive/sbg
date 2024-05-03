using Point = (int x, int y);

namespace d9.bgp.baghchal;
public readonly struct BaghChalState(BaghChalBoard board, int unplacedSheep, int capturedSheep)
{
    public readonly int UnplacedSheep = unplacedSheep,
                        CapturedSheep = capturedSheep;
    public readonly BaghChalBoard Board = board;
    public static BaghChalState Initial => new(BaghChalBoard.Initial, 20, 0);
    public IEnumerable<BaghChalAction> PossibleActionsFor(BaghChalPlayer player)
    {
        if (player is not (BaghChalPlayer.Sheep or BaghChalPlayer.Wolf))
            throw new ArgumentOutOfRangeException(nameof(player));
        if(player is BaghChalPlayer.Sheep)
        {
            if (UnplacedSheep > 0)
            {
                return Board.EmptySpaces.Select(BaghChalAction.PlaceSheepAt);
            }
            else
            {
                return PossibleMovesFor(BaghChalPlayer.Sheep);
            }
        } 
        else
        {
            // todo: add moves
            return PossibleMovesFor(BaghChalPlayer.Wolf);
        }
    }
    public IEnumerable<BaghChalAction> PossibleMovesFor(BaghChalPlayer player)
    {
        if (player is not (BaghChalPlayer.Sheep or BaghChalPlayer.Wolf))
            throw new ArgumentOutOfRangeException(nameof(player));
        BaghChalBoard _board = Board; // have to copy to a local variable because you can't use struct fields in anonymous methods for some reason
        IEnumerable<Point> sources = Board.Spaces.AllCoordinates().Where(x => _board[x] == player);
        IEnumerable<(Point source, Point destination)> pairs = sources.Zip(sources.SelectMany(x => x.BaghChalAdjacentPoints()))
                                                                                  .Where(x => _board[x.Second].IsEmpty());
        return pairs.Select(x => BaghChalAction.Move(player, x.source, x.destination));
    }
    public bool GameOver
        => CapturedSheep >= 5 || !PossibleActionsFor(BaghChalPlayer.Wolf).Any();
    public static BaghChalState operator +(BaghChalState state, BaghChalAction action) => action.ApplyTo(state);
}