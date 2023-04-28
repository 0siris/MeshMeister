using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mathematics; 

public static class Numerics {

    /// <summary>
    /// https://en.wikipedia.org/wiki/Machine_epsilon
    /// 2^23 ≈ 1.19e-07
    /// </summary>
    public const float FloatEpsilon  = 1.1920928955078125E-07f;

    /// <summary>
    /// https://en.wikipedia.org/wiki/Machine_epsilon
    /// 2^52 ≈ 2.22e-16
    /// </summary>
    public const double DoubleEpsilon  = 2.2204460492503131E-16;

    public static bool IsNearZero(this float value) => MathF.Abs(value) <= FloatEpsilon; 

    public static bool IsNearZero(this double value) => Math.Abs(value) <= DoubleEpsilon;

    public static bool IsZero<T>(T value) where T : struct, INumber<T> =>
        value switch {
            float f  => IsNearZero(f),
            double d => IsNearZero(d),
            _        => T.IsZero(value)
        };


    public static bool IsOne<T>(T value) where T : struct, INumber<T> => IsZero(value - T.One);

    public static float CalculateMachineFloatEpsilon() {
        var eps = 1.0f;

        //don't delete this cast, otherwise it will be transformed in to wrong number
        // ReSharper disable once RedundantCast
        while ((float) (1.0f + eps / 2.0f) > 1.0f)
            eps /= 2.0f;

        return eps;
    }

    public static double CalculateMachineDoubleEpsilon() {
        var eps = 1.0d;
        while (1.0d + eps / 2.0 > 1.0d)
            eps /= 2.0d;

        return eps;
    }

    public static float NextFloat(float s) {
        var singleIntUnion = new FloatIntUnion {Value = s};
        singleIntUnion.Int32++;
        return singleIntUnion.Value;
    }

    public static unsafe int Ulp(float a, float b) {
        var aInt = *(int*) &a;
        var bInt = *(int*) &b;
        return Math.Abs(bInt - aInt);
    }


    /// <summary>
    /// Checks if a and b are almost equals, taking into account the magnitude of floating point numbers.
    /// </summary>
    /// <param name="a">The left value to compare.</param>
    /// <param name="b">The right value to compare.</param>
    /// <returns><c>true</c> if a almost equal to b, <c>false</c> otherwise</returns>
    /// <remarks>
    /// The code is using the technique described by Bruce Dawson in 
    /// <a href="http://randomascii.wordpress.com/2012/02/25/comparing-floating-point-numbers-2012-edition/">Comparing Floating point numbers 2012 edition</a>. 
    /// </remarks>
    public static bool NearEqual(float a, float b) {
        // Check if the numbers are really close
        // needed when comparing numbers near zero.
        if ((a - b).IsNearZero())
            return true;

        var aUnion = new FloatIntUnion(a);
        var bUnion = new FloatIntUnion(b);

        // Different signs means they do not match.
        if ((aUnion.Int32 < 0) != (bUnion.Int32 < 0))
            return false;

        // Find the difference in ULPs.
        var ulp = Math.Abs(aUnion.Int32 - bUnion.Int32);

        // Choose of maxUlp = 4
        const int maxUlp = 4;
        return ulp <= maxUlp;
    }

    public static T Pow10<T>(int exp) where T : struct, INumber<T> {
        switch (exp) {
            case >= 0: {
                var product = T.One;
                for (var i = 0; i < exp; i++) 
                    product *= Constants<T>.T10;
                return product;
            }
            case < 0: {
                var product = T.One;
                for (var i = 0; exp < i; i--) 
                    product /= Constants<T>.T10;
                return product;
            }
        }
    }

    
}

[StructLayout(LayoutKind.Explicit)]
public struct FloatIntUnion {
    [FieldOffset(0)] public float Value;

    [FieldOffset(0)] public int Int32;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FloatIntUnion(int int32) : this() => Int32 = int32;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FloatIntUnion(float value) : this() => Value = value;
}

