using System.Numerics;

namespace d9.bgp;
public static class PointAdjacencyRules<T>
    where T : INumberBase<T>
{
    public static AdjacencyRuleDef<Point<T>> WithinDistance<U>(DistanceMetric<Point<T>, U> metric, U maximum, U? minimum = null)
        where U : struct, INumberBase<U>, IComparisonOperators<U, U, bool>
        => minimum is null ? new($"distance within {maximum} {"unit".Plural(maximum)}", (a, b) => metric(a, b) <= maximum)
                           : new($"distance between {minimum} and {maximum} units", (a, b) =>
                           {
                               U distance = metric(a, b);
                               return distance > minimum && distance <= maximum;
                           });
    public static bool SameColumn(Point<T> a, Point<T> b)
        => a.X == b.X;
    public static bool SameRow(Point<T> a, Point<T> b)
        => a.Y == b.Y;
    public static AdjacencyRuleDef<Point<T>> SameColumnOrRow()
         => new("(same column or row)", (a, b) => SameColumn(a, b) || SameRow(a, b));
    public static AdjacencyRuleDef<Point<T>> NotSelf => new("not self", (a, b) => a != b);
    public static AdjacencyRuleDef<Point<T>> SameDiagonalParity(int parity = 0, int modulo = 2)
        
        => new($"parity mod {modulo} is {parity}", (a, b) => a.Parity() % modulo == parity && b.Parity() % modulo == parity);
}