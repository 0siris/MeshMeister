using System.Numerics;
using Mathematics.Matrix;

namespace Mathematics.Vectors;

public interface IVector<T> where T : struct,INumber<T> {
    public abstract static int Dimension { get; }

    public INumber<T> Length();
    public INumber<T> LengthSquared();

    public bool IsZero();
    public bool IsNormalized();

    public IVector<T> Normalized();

    public T Max();
    public T Min();


}

public interface IVector3<T> : IVector<T> where T : struct, INumber<T> {
    public abstract static IVector3<T> Zero { get; }
    public abstract static IVector3<T> One { get; }
    public abstract static IVector3<T> UnitX { get; }
    public abstract static IVector3<T> UnitY { get; }
    public abstract static IVector3<T> UnitZ { get; }
    
    public T X { get; init; }
    public T Y { get; init; }
    public T Z { get; init; }


    public abstract static IVector3<T> CatmullRom(
        IVector3<T> value1,
        IVector3<T> value2,
        IVector3<T> value3,
        Vector3 value4,
        T amount
    );

    public IVector3<T> Barycentric(
        IVector3<T> value1,
        IVector3<T> value2,
        IVector3<T> value3,
        float amount1,
        float amount2
    );

    public IVector3<T> Add(IVector3<T> right);
    public IVector3<T> Add(T scalar);
    public IVector3<T> Subtract(IVector3<T> right);
    public IVector3<T> Subtract(T scalar);
    public IVector3<T> Multiply(IVector3<T> right);
    public IVector3<T> Multiply(T scalar);
    public IVector3<T> Divide(IVector3<T> right);
    public IVector3<T> Divide(T scalar);

    public IVector3<T> Cross(IVector3<T> right);
    public T Dot(IVector3<T> right);

    public static IVector3<T> operator +(IVector3<T> left, IVector3<T> right) => left.Add(right);
    public static IVector3<T> operator +(IVector3<T> left, T right) => left.Add(right);
    public static IVector3<T> operator +(T right, IVector3<T> left) => left.Add(right);

    public static IVector3<T> operator -(IVector3<T> left, IVector3<T> right) => left.Subtract(right);
    public static IVector3<T> operator -(IVector3<T> left, T right) => left.Subtract(right);

    public static IVector3<T> operator *(IVector3<T> left, IVector3<T> right) => left.Multiply(right);
    public static IVector3<T> operator *(IVector3<T> left, T scalar) => left.Multiply(scalar);
    public static IVector3<T> operator *(T scalar, IVector3<T> left) => left.Multiply(scalar);

    public static IVector3<T> operator /(IVector3<T> left, IVector3<T> right) => left.Divide(right);
    public static IVector3<T> operator /(IVector3<T> left, T scalar) => left.Divide(scalar);

    public IVector3<T> Max(IVector3<T> right);
    public IVector3<T> Min(IVector3<T> right);

    public IVector3<T> Clamp(IVector3<T> min, IVector3<T> max);
    public IVector3<T> Lerp(IVector3<T> max, T amount);

    public T Distance(IVector3<T> right);
    public T DistanceSquared(IVector3<T> right);
    public IVector3<T> Abs();
    public IVector3<T> Transform(IMatrix4x4<T> matrix);
    public IVector3<T> TransformNormal(IMatrix4x4<T> matrix);

    public IVector3<T> Negate();
    public static IVector3<T> operator -(IVector3<T> vec) => vec.Negate();

}
public interface IVector4<T> : IVector<T> where T : struct, INumber<T> {
    public abstract static IVector4<T> Zero { get; }
    public abstract static IVector4<T> One { get; }
    public abstract static IVector4<T> UnitX { get; }
    public abstract static IVector4<T> UnitY { get; }
    public abstract static IVector4<T> UnitZ { get; }
    public abstract static IVector4<T> UnitW { get; }
    
    public T X { get; init; }
    public T Y { get; init; }
    public T Z { get; init; }
    public T W { get; init; }

    public IVector4<T> Add(IVector4<T> right);
    public IVector4<T> Add(T scalar);
    public IVector4<T> Subtract(IVector4<T> right);
    public IVector4<T> Subtract(T scalar);
    public IVector4<T> Multiply(IVector4<T> right);
    public IVector4<T> Multiply(T scalar);
    public IVector4<T> Divide(IVector4<T> right);
    public IVector4<T> Divide(T scalar);
    public T Dot(IVector4<T> right);

    public static IVector4<T> operator +(IVector4<T> left, IVector4<T> right) => left.Add(right);
    public static IVector4<T> operator +(IVector4<T> left, T right) => left.Add(right);
    public static IVector4<T> operator +(T right, IVector4<T> left) => left.Add(right);

    public static IVector4<T> operator -(IVector4<T> left, IVector4<T> right) => left.Subtract(right);
    public static IVector4<T> operator -(IVector4<T> left, T right) => left.Subtract(right);

    public static IVector4<T> operator *(IVector4<T> left, IVector4<T> right) => left.Multiply(right);
    public static IVector4<T> operator *(IVector4<T> left, T scalar) => left.Multiply(scalar);
    public static IVector4<T> operator *(T scalar, IVector4<T> left) => left.Multiply(scalar);

    public static IVector<T> operator /(IVector4<T> left, IVector4<T> right) => left.Divide(right);
    public static IVector<T> operator /(IVector4<T> left, T scalar) => left.Divide(scalar);

    public IVector4<T> Max(IVector4<T> right);
    public IVector4<T> Min(IVector4<T> right);

    public IVector4<T> Clamp(IVector4<T> min, IVector4<T> max);
    public IVector4<T> Lerp(IVector4<T> max, T amount);

    public T Distance(IVector4<T> right);
    public T DistanceSquared(IVector4<T> right);
    public IVector4<T> Abs();
    public IVector4<T> Transform(IMatrix4x4<T> matrix);

    public IVector4<T> Negate();
    public static IVector4<T> operator -(IVector4<T> vec) => vec.Negate();
}


public static class VectorExtensions {
    public static Vector3F ToVector3F(this IVector3<float> right) {
        if (right is not Vector3F r) 
            r = new Vector3F(right.X, right.Y, right.Z);
        return r;
    }

    public static Vector4F ToVector4F(this IVector4<float> right) {
        if (right is not Vector4F vf) 
            vf = new Vector4F(right);
        return vf;
    }
}