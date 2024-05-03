using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace d9.bgp;
public readonly struct Point<T>(T x, T y)
    where T : INumberBase<T>
{
    public readonly T X = x, Y = y;
    public void Deconstruct(out T x, out T y)
    {
        x = X;
        y = Y;
    }
    public static Point<T> operator +(Point<T> a, Point<T> b)
        => (a.X + b.X, a.Y + b.Y);
    public static Point<T> operator -(Point<T> p)
        => (-p.X, -p.Y);
    public static Point<T> operator -(Point<T> a, Point<T> b)
        => a + -b;
    public static implicit operator Point<T>((T x, T y) tuple)
        => new(tuple.x, tuple.y);
    public static bool operator ==(Point<T> a, Point<T> b)
        => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Point<T> a, Point<T> b)
        => !(a == b);
    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is Point<T> other && this == other;
    public override int GetHashCode()
        => HashCode.Combine(X, Y);
    public override string ToString()
        => $"({X}, {Y})";
    public int Parity(int mod = 2)
        => Magnitude(DistanceMetrics<T, int>.Taxicab) % mod;
    public U DistanceTo<U>(Point<T> other, DistanceMetric<Point<T>, U> metric)
        where U : INumberBase<U>
        => metric(this, other);
    public U Magnitude<U>(DistanceMetric<Point<T>, U> metric)
        where U : INumberBase<U>
        => metric((T.Zero, T.Zero), this);
}
