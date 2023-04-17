using System.Numerics;
using Mathematics.Vectors;

namespace Mathematics;

public static class Constants<T> where T : INumber<T> {
    public static readonly T Two = T.One + T.One;
    public static readonly T Three = Two + T.One;
    public static readonly T Four = Three + T.One;
    public static readonly T Five = Four + T.One;
    public static readonly T Half = T.One / Two;
}

public static class MathUtil {
    public static T Lerp<T>(T from, T to, T amount) where T : struct, INumber<T> 
        => (T.One - amount) * from + amount * to;

    public static T SmoothStep<T>(T amount) where T : struct, INumber<T> {
        var two = Constants<T>.Two;
        var three = Constants<T>.Three;
        return (amount <= T.Zero) ? T.Zero
               : (amount >= T.One) ? T.One
               : amount * amount * (three - (two * amount));
    }


    public static (T X, T Y, T Z) Hermite<T>(
        IVector3<T> value1,
        IVector3<T> tangent1,
        IVector3<T> value2,
        IVector3<T> tangent2,
        T amount
    ) where T : struct, INumber<T> {
        var two = Constants<T>.Two;
        var three = Constants<T>.Three;

        var squared = amount * amount;
        var cubed = amount * squared;

        var part1 = two * cubed - three * squared + T.One;
        var part2 = -two * cubed + three * squared;
        var part3 = cubed - two * squared + amount;
        var part4 = cubed - squared;

        var x = value1.X * part1 + value2.X * part2 + tangent1.X * part3 + tangent2.X * part4;
        var y = value1.Y * part1 + value2.Y * part2 + tangent1.Y * part3 + tangent2.Y * part4;
        var z = value1.Z * part1 + value2.Z * part2 + tangent1.Z * part3 + tangent2.Z * part4;
        return (x, y, z);
    }

    public static (T X, T Y, T Z) Barycentric<T>(
        IVector3<T> value1,
        IVector3<T> value2,
        IVector3<T> value3,
        T amount1,
        T amount2
    ) where T : struct, INumber<T> =>
        (value1.X + amount1 * (value2.X - value1.X) + amount2 * (value3.X - value1.X),
         value1.Y + amount1 * (value2.Y - value1.Y) + amount2 * (value3.Y - value1.Y),
         value1.Z + amount1 * (value2.Z - value1.Z) + amount2 * (value3.Z - value1.Z));

    public static (T X, T Y, T Z) CatmullRom<T>(
        IVector3<T> value1,
        IVector3<T> value2,
        IVector3<T> value3,
        IVector3<T> value4,
        T amount
    ) where T : struct, INumber<T> {
        var squared = amount * amount;
        var cubed = amount * squared;

        var half = Constants<T>.Half;
        var two = Constants<T>.Two;
        var four = Constants<T>.Four;
        var three = Constants<T>.Three;

        var five = Constants<T>.Five;
        var x = half * (two * value2.X + (-value1.X + value3.X) * amount +
                        (two * value1.X - five * value2.X + four * value3.X - value4.X) * squared +
                        (-value1.X + three * value2.X - three * value3.X + value4.X) * cubed);
        var y = half * (two * value2.Y + (-value1.Y + value3.Y) * amount +
                        (two * value1.Y - five * value2.Y + four * value3.Y - value4.Y) * squared +
                        (-value1.Y + three * value2.Y - three * value3.Y + value4.Y) * cubed);
        var z = half * (two * value2.Z + (-value1.Z + value3.Z) * amount +
                        (two * value1.Z - five * value2.Z + four * value3.Z - value4.Z) * squared +
                        (-value1.Z + three * value2.Z - three * value3.Z + value4.Z) * cubed);
        return (x, y, z);
    }
}
