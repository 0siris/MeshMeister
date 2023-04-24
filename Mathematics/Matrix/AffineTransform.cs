using System.Numerics;
using Mathematics.Quaternion;
using Mathematics.Vectors;

namespace Mathematics.Matrix; 

public struct AffineTransform<T,V,Q> where T: struct, INumber<T> where V : IVector4<T, V> where Q : struct, IQuaternion<T, Q> {
    public AffineTransform() { }

    public IVector4<T, V> Scale { get; set; } = V.One;
    public IQuaternion<T, Q> Rotation { get; set; } = Q.Identity;
    public IVector4<T, V> Translation { get; set; } = V.Zero;
    public IVector4<T,V> RotationCenter { get; set; } = V.Zero;
    public static AffineTransform<T,V,Q> Identity { get; set; }

    public IMatrix4x4<T> ToMatrix4X4() => IMatrix4x4<T>.Identity;
}
