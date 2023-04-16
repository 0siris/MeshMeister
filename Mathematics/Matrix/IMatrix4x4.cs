using System.Numerics;

namespace Mathematics.Matrix; 

public interface IMatrix4x4<T> where T : struct, INumber<T> {
    
}

public static class MatrixExtensions {
    public static Matrix4x4F ToMatrix4x4F(this IMatrix4x4<float> matrix) {
        if (matrix is not Matrix4x4F m)
            m = new Matrix4x4F(matrix);
        return m;
    }
}
