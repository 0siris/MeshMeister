using System.Numerics;
using Mathematics.Vectors;

namespace Mathematics.Quaternion;

public interface IQuaternion<T, Q> : IVector4<T, Q> where T : struct, INumber<T> where Q : struct, IQuaternion<T, Q> {
    public abstract static Q Identity { get; }

    

    /// <summary>
    ///     Gets the angle of the quaternion.
    /// </summary>
    /// <value>The quaternion's angle.</value>
    public T Angle { get; }

    /// <summary>
    ///     Conjugates the quaternion.
    /// </summary>
    public Q Conjugate() => Q.Build(-X, -Y, -Z, W);

    /// <summary>
    ///     Conjugates and renormalizes the quaternion.
    /// </summary>
    public Q Invert() {
        var lenSq = LengthSquared();
        if (Numerics.IsZero(lenSq))
            throw new ArgumentException();

        lenSq = T.One / lenSq;
        return Q.Build(-X * lenSq, -Y * lenSq, -Z * lenSq, W * lenSq);
    }

    public (T x, T y, T z) Axis { get; }
}