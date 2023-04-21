using System.Numerics;
using System.Runtime.CompilerServices;

namespace Mathematics.Angle; 

public record struct Degree<T>(T Value) where T: struct, INumber<T> {
    public override string ToString() => $"{Value:F2}°";

    public static implicit operator T(Degree<T> d) => d.Value;

    public Radian<T> ToRadian() => new(Value*Constants<T>.Pi/Constants<T>.T180);

    public static T ToRadian(T degree) => degree * Constants<T>.Pi / Constants<T>.T180;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree<T> FromRadian(T rad) => new(rad*Constants<T>.T180/Constants<T>.Pi);

    public static implicit operator Degree<T>(T deg) => new(deg);
}
