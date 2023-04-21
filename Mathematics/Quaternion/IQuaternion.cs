using System.Numerics;
using Mathematics.Vectors;

namespace Mathematics.Quaternion; 

public interface IQuaternion<T> : IVector4<T> where T : struct,INumber<T>  {
    protected IQuaternion<T> Create(T x, T y, T z, T w);

    /// <summary>
    /// Conjugates the quaternion.
    /// </summary>
    public IQuaternion<T> Conjugate() 
        => Create(-X, -Y, -Z, W);
    
    /// <summary>
    /// Conjugates and renormalizes the quaternion.
    /// </summary>
    public IQuaternion<T> Invert() {
        var lenSq = LengthSquared();
        if (Numerics.IsZero(lenSq))
            throw new ArgumentException();

        lenSq = T.One / lenSq;
        return Create(-X * lenSq, -Y * lenSq, -Z * lenSq, W * lenSq);
    }

    /// <summary>
    /// Gets the angle of the quaternion.
    /// </summary>
    /// <value>The quaternion's angle.</value>
    public T Angle { get; }


}
