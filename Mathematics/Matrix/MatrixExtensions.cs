using System.Numerics;
using Mathematics.Vectors;

namespace Mathematics.Matrix;

public static class MatrixExtensions {
    public static Matrix4X4F ToMatrix4X4F<NumType, InType,InVec>(this IMatrix4X4<NumType, InType,InVec> matrix)
        where InType : struct,IMatrix4X4<NumType, InType, InVec>
        where NumType : struct, INumber<NumType>
        where InVec : struct, IVector4<NumType, InVec> {
        if (matrix is not Matrix4X4F m)
            m = IMatrix4X4<float, Matrix4X4F, Vector4F>.ConvertFrom(matrix);
        return m;
    }

}
