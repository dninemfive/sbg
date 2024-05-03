using d9.bgp.baghchal;
using System.Numerics;
namespace d9.bgp;
public delegate bool AdjacencyRule<T>(T a, T b);
public class AdjacencyRuleDef<T>(string name, AdjacencyRule<T> areAdjacent)
{
    public string Name => name;
    public bool AreAdjacent(T a, T b) => areAdjacent(a, b);
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
    public IEnumerable<T> NeighborsOf(T p, IEnumerable<T> searchSpace)
    {
        foreach (T other in searchSpace)
            if (AreAdjacent(p, other))
                yield return p;
    }
}