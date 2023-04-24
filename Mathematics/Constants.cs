using System.Numerics;
using System.Runtime.CompilerServices;

namespace Mathematics;

public static class Constants<T> where T : struct, INumber<T> {
    public static readonly T T2 = Convert(2);
    public static readonly T T3 = Convert(3);
    public static readonly T T4 = Convert(4);
    public static readonly T T5 = Convert(5);
    public static readonly T T10 = Convert(10);
    public static readonly T Half = Convert(0.5);

    public static readonly T T180 = Convert(180);
    public static readonly T T360 = Convert(360);

    public static readonly T Pi = Convert(Math.PI);
    public static readonly T TwoPi = T2 * Pi;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static T Convert<TOther>(TOther value) where TOther : INumberBase<TOther> 
        => T.CreateTruncating(value);
}
