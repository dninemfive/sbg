using System.Numerics;
using Point = (int x, int y);
namespace d9.bgp;
public delegate bool AdjacencyRule(Point a, Point b);
public class AdjacencyRuleDef(string name, AdjacencyRule areAdjacent)
{
    public string Name => name;
    public bool AreAdjacent(Point a, Point b) => areAdjacent(a, b);
    public override string ToString()
        => Name;
    public static AdjacencyRuleDef operator |(AdjacencyRuleDef ruleA, AdjacencyRuleDef ruleB)
        => new($"({ruleA} or {ruleB})", (a, b) => ruleA.AreAdjacent(a, b) || ruleB.AreAdjacent(a, b));
    public static AdjacencyRuleDef operator &(AdjacencyRuleDef ruleA, AdjacencyRuleDef ruleB)
        => new($"({ruleA} and {ruleB})", (a, b) => ruleA.AreAdjacent(a, b) && ruleB.AreAdjacent(a, b));
    public static AdjacencyRuleDef operator !(AdjacencyRuleDef rule)
        => new($"not {rule}", (a, b) => !rule.AreAdjacent(a, b));
    public static AdjacencyRuleDef operator ^(AdjacencyRuleDef ruleA, AdjacencyRuleDef ruleB)
        => new($"(either {ruleA} or {ruleB})", (a, b) => ruleA.AreAdjacent(a, b) != ruleB.AreAdjacent(a, b));
}
public delegate T DistanceMetric<T>(Point a, Point b) where T : IFloatingPoint<T>;
public static class AdjacencyRules
{
    public static AdjacencyRuleDef WithinDistance<T>(DistanceMetric<T> metric, T maximum, T? minimum = null)
        where T : struct, IFloatingPoint<T>
        => minimum is null ? new($"distance within {maximum} {"unit".Plural(maximum)}", (a, b) => metric(a, b) <= maximum)
                           : new($"distance between {minimum} and {maximum} units", (a, b) =>
                           {
                               T distance = metric(a, b);
                               return distance > minimum && distance <= maximum;
                           });
    public static bool SameColumn(Point a, Point b) => a.x == b.x;
    public static bool SameRow(Point a, Point b) => a.y == b.y;
    public static AdjacencyRuleDef SameColumnOrRow => new("(same column or row)", (a, b) => SameColumn(a, b) || SameRow(a, b));
    public static AdjacencyRuleDef NotSelf => new("not self", (a, b) => a != b);
    public static AdjacencyRuleDef SameDiagonalParity(int parity = 0, int modulo = 2)
        => new($"parity mod {modulo} is {parity}", (a, b) => a.Parity() % modulo == parity && b.Parity() % modulo == parity);
}
public static class DistanceMetrics
{
    public static double Taxicab(Point a, Point b)
        => Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
}