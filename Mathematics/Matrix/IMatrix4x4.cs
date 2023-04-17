using System.Numerics;

namespace Mathematics.Matrix; 

public interface IMatrix4x4<T> where T : struct, INumber<T> {
    public T M11 { get; }
    public T M12 { get; }
    public T M13 { get; }
    public T M14 { get; }

    public T M21 { get; }
    public T M22 { get; }
    public T M23 { get; }
    public T M24 { get; }

    public T M31 { get; }
    public T M32 { get; }
    public T M33 { get; }
    public T M34 { get; }

    public T M41 { get; }
    public T M42 { get; }
    public T M43 { get; }
    public T M44 { get; }
}

public static class MatrixExtensions {
    public static Matrix4x4F ToMatrix4x4F(this IMatrix4x4<float> matrix) {
        if (matrix is not Matrix4x4F m)
            m = new Matrix4x4F(matrix);
        return m;
    }
}
