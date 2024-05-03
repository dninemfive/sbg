using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d9.bgp.baghchal;
public class BaghChal
{
    public static readonly AdjacencyRuleDef<Point<int>> AdjacencyRule = PointAdjacencyRules<int>.NotSelf
        & ((PointAdjacencyRules<int>.WithinDistance(DistanceMetrics<int, int>.Taxicab, 1) & PointAdjacencyRules<int>.SameColumnOrRow())
        |  (PointAdjacencyRules<int>.WithinDistance(DistanceMetrics<int, int>.Taxicab, 2) & PointAdjacencyRules<int>.SameDiagonalParity()));
    public static bool AreAdjacent(Point<int> a, Point<int> b)
        => AdjacencyRule.AreAdjacent(a, b);
}
