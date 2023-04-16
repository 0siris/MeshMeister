using System.Numerics;

namespace Mathematics.Matrix; 

public struct Matrix4x4F: IMatrix4x4<float> {
    public Matrix4x4 Matrix;

    public Matrix4x4F(IMatrix4x4<float> matrix) {
        throw new NotImplementedException();
    }
}
