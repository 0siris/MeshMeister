using System.Numerics;

namespace Mathematics;

public static class Constants<T> where T : struct, INumber<T> {
    public static readonly T T2 = Build(2);
    public static readonly T T3 = Build(3);
    public static readonly T T4 = Build(4);
    public static readonly T T5 = Build(5);
    public static readonly T Half = Build(0.5);

    public static readonly T T180 = Build(180);
    public static readonly T T360 = Build(360);

    public static readonly T Pi = Build(Math.PI);
    public static readonly T TwoPi = T2 * Pi;


    private static T Build(dynamic value) => (T) value;
}
