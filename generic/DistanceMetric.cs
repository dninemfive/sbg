using System.Numerics;

namespace d9.bgp;
public delegate U DistanceMetric<T, U>(T a, T b) 
    where U : INumberBase<U>;
public static class DistanceMetrics<T, U>
    where T : INumberBase<T>
    where U : INumberBase<U>
{
    public static U Taxicab(Point<T> a, Point<T> b)
        => U.CreateChecked(T.Abs(a.X - b.X) + T.Abs(a.Y - b.Y));
}