using d9.bgp.baghchal;
using System.Numerics;
namespace d9.bgp;
public delegate bool AdjacencyRule<T>(Point<T> a, Point<T> b)
    where T : INumberBase<T>;
public class AdjacencyRuleDef<T>(string name, AdjacencyRule<T> areAdjacent)
    where T : INumberBase<T>
{
    public string Name => name;
    public bool AreAdjacent(Point<T> a, Point<T> b) => areAdjacent(a, b);
    public override string ToString()
        => Name;
    public static AdjacencyRuleDef<T> operator |(AdjacencyRuleDef<T> ruleA, AdjacencyRuleDef<T> ruleB)
        => new($"({ruleA} or {ruleB})", (a, b) => ruleA.AreAdjacent(a, b) || ruleB.AreAdjacent(a, b));
    public static AdjacencyRuleDef<T> operator &(AdjacencyRuleDef<T> ruleA, AdjacencyRuleDef<T> ruleB)
        => new($"({ruleA} and {ruleB})", (a, b) => ruleA.AreAdjacent(a, b) && ruleB.AreAdjacent(a, b));
    public static AdjacencyRuleDef<T> operator !(AdjacencyRuleDef<T> rule)
        => new($"not {rule}", (a, b) => !rule.AreAdjacent(a, b));
    public static AdjacencyRuleDef<T> operator ^(AdjacencyRuleDef<T> ruleA, AdjacencyRuleDef<T> ruleB)
        => new($"(either {ruleA} or {ruleB})", (a, b) => ruleA.AreAdjacent(a, b) != ruleB.AreAdjacent(a, b));
    public IEnumerable<Point<T>> NeighborsOf(Point<T> p, BaghChalBoard b)
    {
        foreach (Point<T> other in b.Spaces.AllCoordinates())
            if (AreAdjacent(p, other))
                yield return p;
    }
}