using System.Numerics;

namespace Mathematics.Angle;

public record struct Radian<T>(T Value) where T : struct, INumber<T> {
    public static readonly Radian<T> Zero = new(T.Zero);

    public Degree<T> ToDegree() => new(Value * Constants<T>.T180 / Constants<T>.Pi);

    public static T ToDegree(T radian) => radian * Constants<T>.T180 / Constants<T>.Pi;

    public static implicit operator T(Radian<T>? d) => d?.Value ?? T.One;

    public static Radian<T> operator -(Radian<T> r) => new(-r.Value);

    public override string ToString() => $"{(Value / Constants<T>.Pi):F2}\u03C0 ({ToDegree()})";

    public static Radian<T> Negate(Radian<T> item) => new(-item.Value);

    public static Radian<T> FromDegree(T degree) => new(degree * Constants<T>.Pi / Constants<T>.T180);

    public override int GetHashCode() => Value.GetHashCode();
}
