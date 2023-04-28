using System.Numerics;
using Mathematics.Quaternion;
using Mathematics.Vectors;

namespace Mathematics.Matrix;

public interface IMatrix4X4Values<T> where T : INumber<T> {
    public T M11 { get; set; }
    public T M12 { get; set;}
    public T M13 { get; set;}
    public T M14 { get; set;}
    //row 2
    public T M21 { get; set;}
    public T M22 { get; set;}
    public T M23 { get; set;}
    public T M24 { get; set;}
    //row 3
    public T M31 { get; set;}
    public T M32 { get; set;}
    public T M33 { get; set;}
    public T M34 { get;set; }
    //row 4
    public T M41 { get; set;}
    public T M42 { get; set;}
    public T M43 { get; set;}
    public T M44 { get; set;}

    public bool IsIdentity { get; }
}

/// <summary>
/// Row major matrix representation
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="MatrixType">The final type of the matrix struct</typeparam>
/// <typeparam name="VectorType">Vector type of the matrix</typeparam>
public interface IMatrix4X4<T, MatrixType, VectorType> : IMatrix4X4Values<T>
    where T : struct, INumber<T>
    where VectorType : struct, IVector4<T, VectorType>
    where MatrixType : struct, IMatrix4X4<T, MatrixType, VectorType> {

    public abstract static MatrixType Identity { get; }
    public abstract static MatrixType Zero { get; }

    public static MatrixType ConvertFrom<NumType, TOtherMatrix, TOtherVector>(IMatrix4X4<NumType, TOtherMatrix,TOtherVector> m)
        where NumType : struct, INumber<NumType> 
        where TOtherMatrix : struct, IMatrix4X4<NumType, TOtherMatrix,TOtherVector> 
        where TOtherVector : struct, IVector4<NumType, TOtherVector> =>

        MatrixType.Create(T.CreateTruncating(m.M11), T.CreateTruncating(m.M12), T.CreateTruncating(m.M13), T.CreateTruncating(m.M14),
                          T.CreateTruncating(m.M11), T.CreateTruncating(m.M12), T.CreateTruncating(m.M13), T.CreateTruncating(m.M14),
                          T.CreateTruncating(m.M11), T.CreateTruncating(m.M12), T.CreateTruncating(m.M13), T.CreateTruncating(m.M14),
                          T.CreateTruncating(m.M11), T.CreateTruncating(m.M12), T.CreateTruncating(m.M13), T.CreateTruncating(m.M14));

    public abstract static MatrixType Rotation<QuaternionType>(QuaternionType q)
        where QuaternionType : struct, IQuaternion<T, QuaternionType>;

    public abstract static MatrixType Translation(VectorType v);

    public abstract static MatrixType Create(
        T m11, T m12, T m13, T m14, 
        T m21, T m22, T m23, T m24,
        T m31, T m32, T m33, T m34,
        T m41, T m42, T m43, T m44);


    public T Determinant() {
        var t1 = M33 * M44 - M34 * M43;
        var t2 = M32 * M44 - M34 * M42;
        var t3 = M32 * M43 - M33 * M42;
        var t4 = M31 * M44 - M34 * M41;
        var t5 = M31 * M43 - M33 * M41;
        var t6 = M31 * M42 - M32 * M41;

        return M11 * (M22 * t1 - M23 * t2 + M24 * t3) - 
               M12 * (M21 * t1 - M23 * t4 + M24 * t5) +
               M13 * (M21 * t2 - M22 * t4 + M24 * t6) -
               M14 * (M21 * t3 - M22 * t5 + M23 * t6);
    }

    public MatrixType Invert() {
        var b0 = M31 * M42 - M32 * M41;
        var b1 = M31 * M43 - M33 * M41;
        var b2 = M34 * M41 - M31 * M44;
        var b3 = M32 * M43 - M33 * M42;
        var b4 = M34 * M42 - M32 * M44;
        var b5 = M33 * M44 - M34 * M43;

        var d11 = M22 * b5 + M23 * b4 + M24 * b3;
        var d12 = M21 * b5 + M23 * b2 + M24 * b1;
        var d13 = M21 * -b4 +M22 * b2 + M24 * b0;
        var d14 = M21 * b3 + M22 * -b1 + M23 * b0;

        var det = M11 * d11 - M12 * d12 + M13 * d13 - M14 * d14;
        
        if (T.Abs(det) == T.Zero) {
            return MatrixType.Zero;
        }

        det = T.One / det;

        var a0 = M11 * M22 - M12 * M21;
        var a1 = M11 * M23 - M13 * M21;
        var a2 = M14 * M21 - M11 * M24;
        var a3 = M12 * M23 - M13 * M22;
        var a4 = M14 * M22 - M12 * M24;
        var a5 = M13 * M24 - M14 * M23;

        var d21 = M12 * b5 + M13 * b4 + M14 * b3;
        var d22 = M11 * b5 + M13 * b2 + M14 * b1;
        var d23 = M11 * -b4+ M12 * b2 + M14 * b0;
        var d24 = M11 * b3 + M12 * -b1+ M13 * b0;

        var d31 = M42 * a5 + M43 * a4 + M44 * a3;
        var d32 = M41 * a5 + M43 * a2 + M44 * a1;
        var d33 = M41 * -a4+ M42 * a2 + M44 * a0;
        var d34 = M41 * a3 + M42 * -a1+ M43 * a0;

        var d41 = M32 * a5 + M33 * a4 + M34 * a3;
        var d42 = M31 * a5 + M33 * a2 + M34 * a1;
        var d43 = M31 * -a4+ M32 * a2 + M34 * a0;
        var d44 = M31 * a3 + M32 * -a1+ M33 * a0;

        var m11 = +d11 * det;
        var m12 = -d21 * det;
        var m13 = +d31 * det;
        var m14 = -d41 * det;
        var m21 = -d12 * det;
        var m22 = +d22 * det;
        var m23 = -d32 * det;
        var m24 = +d42 * det;
        var m31 = +d13 * det;
        var m32 = -d23 * det;
        var m33 = +d33 * det;
        var m34 = -d43 * det;
        var m41 = -d14 * det;
        var m42 = +d24 * det;
        var m43 = -d34 * det;
        var m44 = +d44 * det;

        return MatrixType.Create(m11, m12, m13, m14,
                                 m21, m22, m23, m24,
                                 m31, m32, m33, m34,
                                 m41, m42, m43, m44);
    }

    public MatrixType Transpose() =>
        MatrixType.Create(M11, M21, M31, M41,
               M12, M22, M23, M24,
               M31, M32, M33, M34,
               M41, M42, M43, M44);

    public MatrixType Multiply(MatrixType right) {
        
        var m11 = M11 * right.M11 + M12 * right.M21 + M13 * right.M31 + M14 * right.M41;
        var m12 = M11 * right.M12 + M12 * right.M22 + M13 * right.M32 + M14 * right.M42;
        var m13 = M11 * right.M13 + M12 * right.M23 + M13 * right.M33 + M14 * right.M43;
        var m14 = M11 * right.M14 + M12 * right.M24 + M13 * right.M34 + M14 * right.M44;
        var m21 = M21 * right.M11 + M22 * right.M21 + M23 * right.M31 + M24 * right.M41;
        var m22 = M21 * right.M12 + M22 * right.M22 + M23 * right.M32 + M24 * right.M42;
        var m23 = M21 * right.M13 + M22 * right.M23 + M23 * right.M33 + M24 * right.M43;
        var m24 = M21 * right.M14 + M22 * right.M24 + M23 * right.M34 + M24 * right.M44;
        var m31 = M31 * right.M11 + M32 * right.M21 + M33 * right.M31 + M34 * right.M41;
        var m32 = M31 * right.M12 + M32 * right.M22 + M33 * right.M32 + M34 * right.M42;
        var m33 = M31 * right.M13 + M32 * right.M23 + M33 * right.M33 + M34 * right.M43;
        var m34 = M31 * right.M14 + M32 * right.M24 + M33 * right.M34 + M34 * right.M44;
        var m41 = M41 * right.M11 + M42 * right.M21 + M43 * right.M31 + M44 * right.M41;
        var m42 = M41 * right.M12 + M42 * right.M22 + M43 * right.M32 + M44 * right.M42;
        var m43 = M41 * right.M13 + M42 * right.M23 + M43 * right.M33 + M44 * right.M43;
        var m44 = M41 * right.M14 + M42 * right.M24 + M43 * right.M34 + M44 * right.M44;
        
        return MatrixType.Create(m11, m12, m13, m14,
                                 m21, m22, m23, m24,
                                 m31, m32, m33, m34,
                                 m41, m42, m43, m44);
    }


    public abstract static MatrixType operator *(
        MatrixType left,
        MatrixType right
    );
}