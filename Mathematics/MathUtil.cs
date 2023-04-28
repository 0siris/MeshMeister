using Mathematics.Vectors;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Mathematics;

public static class MathUtil {
    public static double Cot(double alpha) => 1.0 / Math.Tan(alpha);
    public static double Atanh(double x) => (Math.Log(1 + x) - Math.Log(1 - x)) / 2.0;

    public static T Lerp<T>(T from, T to, T amount) where T : struct, INumber<T> =>
        (T.One - amount) * from + amount * to;

    public static T SmoothStep<T>(T amount) where T : struct, INumber<T> {
        var two = Constants<T>.T2;
        var three = Constants<T>.T3;
        return (amount <= T.Zero) ? T.Zero
               : (amount >= T.One) ? T.One
               : amount * amount * (three - (two * amount));
    }


    public static (T X, T Y, T Z) Hermite<T,R>(
        IVector3<T,R> value1,
        IVector3<T,R> tangent1,
        IVector3<T,R> value2,
        IVector3<T,R> tangent2,
        T amount
    ) where T : struct, INumber<T> where R : IVector3<T, R> {
        var two = Constants<T>.T2;
        var three = Constants<T>.T3;

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

    public static (T X, T Y, T Z) Barycentric<T,R>(
        IVector3<T,R> value1,
        IVector3<T,R> value2,
        IVector3<T,R> value3,
        T amount1,
        T amount2
    ) where T : struct, INumber<T> where R : IVector3<T, R> =>
        (value1.X + amount1 * (value2.X - value1.X) + amount2 * (value3.X - value1.X),
         value1.Y + amount1 * (value2.Y - value1.Y) + amount2 * (value3.Y - value1.Y),
         value1.Z + amount1 * (value2.Z - value1.Z) + amount2 * (value3.Z - value1.Z));

    public static (T X, T Y, T Z) CatmullRom<T,R>(
        IVector3<T,R> value1,
        IVector3<T,R> value2,
        IVector3<T,R> value3,
        IVector3<T,R> value4,
        T amount
    ) where T : struct, INumber<T> where R : IVector3<T, R> {
        var squared = amount * amount;
        var cubed = amount * squared;

        var half = Constants<T>.Half;
        var two = Constants<T>.T2;
        var four = Constants<T>.T4;
        var three = Constants<T>.T3;

        var five = Constants<T>.T5;
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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap<T>(ref T v1, ref T v2) => (v1, v2) = (v2, v1);


    public static Vector3 Centroid(this IEnumerable<Vector3F> positions) => Centroid<float, Vector3F>(positions);
    public static Vector3D Centroid(this IEnumerable<Vector3D> positions) => Centroid<double, Vector3D>(positions);

    public static VectorType Centroid<NumType, VectorType>(IEnumerable<VectorType> positions)  where VectorType: struct, IVector3<NumType, VectorType> where NumType : struct, INumber<NumType> {
        var center = VectorType.Zero;
        var count = 0;
        foreach (var position in positions) {
            center += position;
            count++;
        }

        center /= NumType.CreateTruncating(count);
        return center;
    }
}
