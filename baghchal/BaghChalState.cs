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
            // todo: add captures
            return PossibleMovesFor(BaghChalPlayer.Wolf);
        }
    }
    public IEnumerable<BaghChalAction> PossibleMovesFor(BaghChalPlayer player)
    {
        if (player is not (BaghChalPlayer.Sheep or BaghChalPlayer.Wolf))
            throw new ArgumentOutOfRangeException(nameof(player));
        foreach(Point<int> source in Board.SpacesWithPlayer(player))
            foreach (Point<int> neighbor in Board.EmptySpaces)
                if (BaghChalBoard.AdjacencyRule.AreAdjacent(source, neighbor))
                    yield return BaghChalAction.Move(player, source, neighbor);
    }
    public IEnumerable<BaghChalAction> PossibleCaptures()
    {
        foreach(Point<int> wolf in Board.SpacesWithPlayer(BaghChalPlayer.Wolf))
        {
            foreach(Point<int> neighbor in BaghChalBoard.AdjacencyRule.NeighborsOf(wolf, Board.AllSpaces))
            {
                if (Board[neighbor] is BaghChalPlayer.Sheep)
                {
                    Point<int> offset = neighbor - wolf,
                               landingPoint = neighbor + offset;
                    if (!Board.Contains(landingPoint) || Board[landingPoint] is not null)
                        continue;
                    yield return BaghChalAction.Capture(wolf, neighbor, landingPoint);
                }
            }
        }
    }
    public bool GameOver
        => CapturedSheep >= 5 || !PossibleActionsFor(BaghChalPlayer.Wolf).Any();
    public static BaghChalState operator +(BaghChalState state, BaghChalAction action) => action.ApplyTo(state);
    public BaghChalState With(int sheepPlaced = 0, int sheepCaptured = 0, params (Point<int> p, BaghChalPlayer? v)[] differences)
        => new(Board.Spaces.With(differences), UnplacedSheep - sheepPlaced, CapturedSheep + sheepCaptured);
}