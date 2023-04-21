using System.Numerics;

namespace Mathematics.Matrix; 

public struct Matrix4x4F: IMatrix4x4<float> {
    public Matrix4x4 Matrix;

    public Matrix4x4F(IMatrix4x4<float> matrix) {
        throw new NotImplementedException();
    }

    public IMatrix4x4<float> Create(
        float m11, float m12, float m13, float m14,
        float m21, float m22, float m23, float m24,
        float m31, float m32, float m33, float m34,
        float m41, float m42, float m43, float m44
    ) =>
        throw new NotImplementedException();

    public IMatrix4x4<float> Create(float value) => throw new NotImplementedException();

    public float M11 { get; }
    public float M12 { get; }
    public float M13 { get; }
    public float M14 { get; }
    public float M21 { get; }
    public float M22 { get; }
    public float M23 { get; }
    public float M24 { get; }
    public float M31 { get; }
    public float M32 { get; }
    public float M33 { get; }
    public float M34 { get; }
    public float M41 { get; }
    public float M42 { get; }
    public float M43 { get; }
    public float M44 { get; }
}
