using System.Numerics;
using Mathematics.Quaternion;
using Mathematics.Vectors;

namespace Mathematics.Matrix; 

public struct Matrix4X4F: IMatrix4X4<float,Matrix4X4F, Vector4F> {
    private static readonly Matrix4x4 ZeroMatrix = new(0, 0, 0, 0,
                                                       0, 0, 0, 0,
                                                       0, 0, 0, 0,
                                                       0, 0, 0, 0);

    

    public static Matrix4X4F Identity => new(Matrix4x4.Identity);

    public static Matrix4X4F Zero => new(ZeroMatrix);

    public Matrix4x4 Matrix;

    public Matrix4X4F(Matrix4x4 matrix) => Matrix = matrix;

    public static Matrix4X4F Translation(Vector4F v) 
        => new(Matrix4x4.CreateTranslation(v.ToXyz<Vector3F>()));

    public static Matrix4X4F Create(
        float m11, float m12, float m13, float m14,
        float m21, float m22, float m23, float m24,
        float m31, float m32, float m33, float m34,
        float m41, float m42, float m43, float m44
    ) =>
        new(new Matrix4x4(m11, m12, m13, m14,
                          m21, m22, m23, m24,
                          m31, m32, m33, m34,
                          m41, m42, m43, m44));

    public static Matrix4X4F Rotation<QuaternionType>(QuaternionType q)
        where QuaternionType : struct, IQuaternion<float, QuaternionType> {

        var m = Matrix4x4.Identity;

        var xx = q.X * q.X;
        var yy = q.Y * q.Y;
        var zz = q.Z * q.Z;

        var xy = q.X * q.Y;
        var wz = q.Z * q.W;
        var xz = q.Z * q.X;
        var wy = q.Y * q.W;
        var yz = q.Y * q.Z;
        var wx = q.X * q.W;

        m.M11 = 1.0f - 2.0f * (yy + zz);
        m.M12 = 2.0f * (xy + wz);
        m.M13 = 2.0f * (xz - wy);

        m.M21 = 2.0f * (xy - wz);
        m.M22 = 1.0f - 2.0f * (zz + xx);
        m.M23 = 2.0f * (yz + wx);

        m.M31 = 2.0f * (xz + wy);
        m.M32 = 2.0f * (yz - wx);
        m.M33 = 1.0f - 2.0f * (yy + xx);

        return new Matrix4X4F(m);
    }

    public float M11 { get => Matrix.M11; set => Matrix.M11 = value; }
    public float M12 { get => Matrix.M12; set => Matrix.M12 = value; }
    public float M13 { get => Matrix.M13; set => Matrix.M13 = value; }
    public float M14 { get => Matrix.M14; set => Matrix.M14 = value; }

    public float M21 { get => Matrix.M11; set => Matrix.M11 = value; }
    public float M22 { get => Matrix.M12; set => Matrix.M12 = value; }
    public float M23 { get => Matrix.M13; set => Matrix.M13 = value; }
    public float M24 { get => Matrix.M14; set => Matrix.M14 = value; }

    public float M31 { get => Matrix.M11; set => Matrix.M11 = value; }
    public float M32 { get => Matrix.M12; set => Matrix.M12 = value; }
    public float M33 { get => Matrix.M13; set => Matrix.M13 = value; }
    public float M34 { get => Matrix.M14; set => Matrix.M14 = value; }

    public float M41 { get => Matrix.M11; set => Matrix.M11 = value; }
    public float M42 { get => Matrix.M12; set => Matrix.M11 = value; }
    public float M43 { get => Matrix.M13; set => Matrix.M11 = value; }
    public float M44 { get => Matrix.M14; set => Matrix.M11 = value; }
    
    public bool IsIdentity => Matrix.IsIdentity;

    public Matrix4X4F Multiply(Matrix4X4F right) => new(Matrix * right.Matrix);
    public Matrix4X4F Transpose() => Matrix4x4.Transpose(Matrix);
    public float Determinant() => Matrix.GetDeterminant();

    public Matrix4X4F Invert() {
        if (Matrix4x4.Invert(Matrix, out var res))
            return res;
        throw new InvalidOperationException("Matrix inversion not possible");
    }

    public static implicit operator Matrix4X4F(Matrix4x4 m) => new(m);

    public static Matrix4X4F operator *(Matrix4X4F left, Matrix4X4F right) => left.Multiply(right);
}
