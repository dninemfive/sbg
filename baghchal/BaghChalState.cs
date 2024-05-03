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
                IEnumerable<Point> sources = Board.Spaces.AllCoordinates().Where(Board.IsSheep);
                IEnumerable<(Point source, Point destination)> pairs = sources.Zip(sources.SelectMany(x => x.BaghChalAdjacentPoints()))
                                                                              .Where(Board.DestinationIsEmpty);
                return pairs.Select(x => BaghChalAction.Move(BaghChalPlayer.Sheep, x.source, x.destination));
            }
        } 
        else
        {
            // return moves + captures
            throw new NotImplementedException();
        }
    }
    public bool GameOver
        => CapturedSheep >= 5 || !PossibleActionsFor(BaghChalPlayer.Wolf).Any();
    public static BaghChalState operator +(BaghChalState state, BaghChalAction action) => action.ApplyTo(state);
}