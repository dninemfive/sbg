using System.Numerics;

namespace d9.bgp;
public static class AdjacencyRules<T>
    where T : INumberBase<T>
{
    public static AdjacencyRuleDef<T> WithinDistance<U>(DistanceMetric<Point<T>, U> metric, U maximum, U? minimum = null)
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
    public static AdjacencyRuleDef<T> SameColumnOrRow()
         => new("(same column or row)", (a, b) => SameColumn(a, b) || SameRow(a, b));
    public static AdjacencyRuleDef<T> NotSelf => new("not self", (a, b) => a != b);
    public static AdjacencyRuleDef<T> SameDiagonalParity(int parity = 0, int modulo = 2)
        => new($"parity mod {modulo} is {parity}", (a, b) => a.Parity() % modulo == parity && b.Parity() % modulo == parity);
}