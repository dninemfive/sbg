using d9.utl;

namespace d9.sbg.examples.baghchal;
public static class BaghChalActionValidators
{
    public static ActionValidator<BaghChalState> PlayerMustBe(BaghChalPlayer actual, params BaghChalPlayer[] expected)
        => (name, _) => !expected.Contains(actual)
            ? new Exception($"{name} can only apply to players {expected.NaturalLanguageList()}, not {actual.PrintNull("`empty`")}!")
            : null;
    public static ActionValidator<BaghChalState> MustContainPlayer(Point<int> p, params BaghChalPlayer?[] values)
        => (name, state) => !values.Contains(state.Board[p]) 
            ? new Exception($"{name} requires the player at {p} to be {values.NaturalLanguageList()}, not {state.Board[p]}!") 
            : null;
    public static ActionValidator<BaghChalState> MustBeEmpty(Point<int> p)
        => (name, state) => state.Board[p] is not null
            ? new Exception($"{name} requires that {p} be empty, but it contains {state.Board[p]}!")
            : null;
    public static ActionValidator<BaghChalState> MustBeAdjacent(Point<int> a, Point<int> b)
        => (name, state) => !BaghChal.Rules.AreAdjacent(a, b) 
            ? new($"{a} must be adjacent to {b}!")
            : null;
    public static Exception? MustHaveUnplacedSheep(string name, BaghChalState state)
        => state.UnplacedSheep <= 0
            ? new($"{name} requires there to be unplaced sheep!")
            : null;
    public static ActionValidator<BaghChalState> MustBeInALine(params Point<int>[] points)
    {
        // this error would be related to calling this function rather than a failure of game state
        if (points.Length < 1)
            return (_, _) => null;
        static bool areInALine(Point<int>[] points)
        {
            Point<int> offset = points[1] - points[0];
            for(int i = 2; i < points.Length; i++)
                if (points[i] - points[i - 1] != offset)
                    return false;
            return true;
        }
        return (name, _) => areInALine(points) ? new($"{points.NaturalLanguageList()} must be in a line!") : null;
    }
}
