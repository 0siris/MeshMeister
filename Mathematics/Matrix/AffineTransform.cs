using System.Numerics;
using System.Transactions;
using Mathematics.Quaternion;
using Mathematics.Vectors;

namespace Mathematics.Matrix;

public struct AffineTransform<T, VecType, QuaternionType, MatrixType> where T : struct, INumber<T>
                                                                      where VecType : struct, IVector4<T, VecType>
                                                                      where QuaternionType : struct,
                                                                      IQuaternion<T, QuaternionType>
                                                                      where MatrixType : struct,
                                                                      IMatrix4X4<T, MatrixType, VecType>
{
    public AffineTransform() { }

    public VecType ScaleVector { get; set; } = VecType.One;
    public QuaternionType RotationQuaternion { get; set; } = QuaternionType.Identity;
    public VecType TranslationVec { get; set; } = VecType.Zero;
    public VecType RotationCenter { get; set; } = VecType.Zero;

    public MatrixType ToSrtMatrix() => Scale * Rotation * Translation;
    public MatrixType ToSrrtMatrix() => Scale 
                                        * MatrixType.Translation(-RotationCenter) 
                                        * Rotation 
                                        * MatrixType.Translation(RotationCenter) 
                                        * Translation;

    public MatrixType Scale {
        get {
            var m = MatrixType.Identity;
            m.M11 = ScaleVector.X;
            m.M22 = ScaleVector.Y;
            m.M33 = ScaleVector.Z;
            return m;
        }
    }

    public MatrixType Translation {
        get {
            var m = MatrixType.Identity;
            m.M41 = TranslationVec.X;
            m.M42 = TranslationVec.Y;
            m.M43 = TranslationVec.Z;
            return m;
        }
    }

    public MatrixType Rotation => MatrixType.Rotation(RotationQuaternion);
}
