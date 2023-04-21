using System.Numerics;
using Mathematics.Matrix;

namespace Mathematics.Vectors;

public interface IVector<T> where T : struct,INumber<T> {
    public abstract static int Dimension { get; }

    public T Length();
    public T LengthSquared();

    public bool IsNormalized() => Numerics.IsOne(Length());

    public IVector<T> Normalized();

    public T Max();
    public T Min();

    public T[] ToArray();
}

public interface IVector3<T> : IVector<T> where T : struct,INumber<T> {
    static int IVector<T>.Dimension => 3;

    protected IVector3<T> Create(T x, T y, T z);

    T IVector<T>.LengthSquared() => X * X + Y * Y + Z * Z;

    IVector<T> IVector<T>.Normalized() => this / Length();

    T IVector<T>.Max() => T.Max(T.Max(X, Y), Z);

    T IVector<T>.Min() => T.Min(T.Min(X, Y), Z);

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
        IVector3<T> value4,
        T amount
    );

    public abstract static IVector3<T> Barycentric(
        IVector3<T> value1,
        IVector3<T> value2,
        IVector3<T> value3,
        T amount1,
        T amount2
    );

    public abstract static IVector3<T> Hermite(
        IVector3<T> value1,
        IVector3<T> tangent1,
        IVector3<T> value2,
        IVector3<T> tangent2,
        T amount
    );

    public static IVector3<T> SmoothStep(IVector3<T> start, IVector3<T> end, T amount) =>
        start.Lerp(end, MathUtil.SmoothStep(amount));

    public IVector3<T> Add(IVector3<T> right) => Create(X + right.X, Y + right.Y, Z + right.Z);
    public IVector3<T> Add(T scalar) => Create(X + scalar, Y + scalar, Z + scalar);
    public IVector3<T> Subtract(IVector3<T> right) => Create(X - right.X, Y - right.Y, Z - right.Z);
    public IVector3<T> Subtract(T scalar) => Create(X - scalar, Y - scalar, Z - scalar);
    public IVector3<T> Multiply(IVector3<T> right) => Create(X * right.X, Y * right.Y, Z * right.Z);
    public IVector3<T> Multiply(T scalar) => Create(X * scalar, Y * scalar, Z * scalar);
    public IVector3<T> Divide(IVector3<T> right) => Create(X / right.X, Y / right.Y, Z / right.Z);
    public IVector3<T> Divide(T scalar)  => Create(X / scalar, Y / scalar, Z / scalar);

    public IVector3<T> Cross(IVector3<T> right) =>
        Create((Y * right.Z) - (Z * right.Y),
               (Z * right.X) - (X * right.Z),
               (X * right.Y) - (Y * right.X));

    public T Dot(IVector3<T> right) => X * right.X + Y * right.Y + Z * right.Z;

    public IVector3<T> Reflect(IVector3<T> normal) {
        var dot = X * normal.X + Y * normal.Y + Z * normal.Z;
        return Create(X - (dot + dot) * normal.X, 
                      Y - (dot + dot) * normal.Y, 
                      Z - (dot + dot) * normal.Z);
    }

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

    public IVector3<T> Max(IVector3<T> right) => Create(T.Max(X, right.X), T.Max(Y, right.Y), T.Max(Z, right.Z));
    public IVector3<T> Min(IVector3<T> right) => Create(T.Min(X, right.X), T.Min(Y, right.Y), T.Min(Z, right.Z));

    public IVector3<T> Clamp(IVector3<T> min, IVector3<T> max) =>
        Create(T.Clamp(X, min.X, max.X),
               T.Clamp(Y, min.Y, max.Y),
               T.Clamp(Z, min.Z, max.Z));

    public IVector3<T> Lerp(IVector3<T> max, T amount)=>
        Create(MathUtil.Lerp(X, max.X, amount),
               MathUtil.Lerp(Y, max.Y, amount),
               MathUtil.Lerp(Z, max.Z, amount));

    public T Distance(IVector3<T> right);
    public T DistanceSquared(IVector3<T> right) => Subtract(right).LengthSquared();
    public IVector3<T> Abs() => Create(T.Abs(X), T.Abs(Y), T.Abs(Z));

    public IVector3<T> Transform(IMatrix4x4<T> transform) =>
        Create(X * transform.M11 + Y * transform.M21 + Z * transform.M31 + transform.M41,
               X * transform.M12 + Y * transform.M22 + Z * transform.M32 + transform.M42,
               X * transform.M13 + Y * transform.M23 + Z * transform.M33 + transform.M43);

    public IVector3<T> TransformCoordinate(IMatrix4x4<T> transform) {
        var x = X * transform.M11 + Y * transform.M21 + Z * transform.M31 + transform.M41;
        var y = X * transform.M12 + Y * transform.M22 + Z * transform.M32 + transform.M42;
        var z = X * transform.M13 + Y * transform.M23 + Z * transform.M33 + transform.M43;
        var w = T.One / (X * transform.M14 + Y * transform.M24 + Z * transform.M34 + transform.M44);
        return Create(x * w, y * w, z * w);
    }

    public IVector3<T> TransformNormal(IMatrix4x4<T> transform) =>
        Create(X * transform.M11 + Y * transform.M21 + Z * transform.M31,
               X * transform.M12 + Y * transform.M22 + Z * transform.M32,
               X * transform.M13 + Y * transform.M23 + Z * transform.M33);

    public IVector3<T> Negate() => Create(-X, -Y, -Z);

    public static IVector3<T> operator -(IVector3<T> vec) => vec.Negate();

    T[] IVector<T>.ToArray() => new[] {X, Y, Z};
}
public interface IVector4<T> : IVector<T> where T : struct, INumber<T> {
    static int IVector<T>.Dimension => 4;

    protected IVector4<T> Create(T x, T y, T z, T w);
    T IVector<T>.Max() => T.Max(T.Max(X, Y), T.Max(Z,W));

    T IVector<T>.Min() => T.Min(T.Min(X, Y), T.Min(Z,W));
    IVector<T> IVector<T>.Normalized() => this / Length();

    T IVector<T>.LengthSquared() => X * X + Y * Y + Z * Z + W * W;

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

    public IVector4<T> Add(IVector4<T> right) => Create(X + right.X, Y + right.Y, Z + right.Z, W + right.W);
    public IVector4<T> Add(T scalar) => Create(X + scalar, Y + scalar, Z + scalar, W + scalar);
    public IVector4<T> Subtract(IVector4<T> right) => Create(X - right.X, Y - right.Y, Z - right.Z, W - right.W);
    public IVector4<T> Subtract(T scalar) => Create(X - scalar, Y - scalar, Z - scalar, W - scalar);
    public IVector4<T> Multiply(IVector4<T> right) => Create(X * right.X, Y * right.Y, Z * right.Z, W * right.W);
    public IVector4<T> Multiply(T scalar) => Create(X * scalar, Y * scalar, Z * scalar, W * scalar);
    public IVector4<T> Divide(IVector4<T> right) => Create(X / right.X, Y / right.Y, Z / right.Z, W / right.W);
    public IVector4<T> Divide(T scalar) => Create(X / scalar, Y / scalar, Z / scalar, W / scalar);
    public T Dot(IVector4<T> right) => X * right.X + Y * right.Y + Z * right.Z + W * right.W;

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


    public IVector4<T> Max(IVector4<T> right) =>
        Create(T.Max(X, right.X),
               T.Max(Y, right.Y),
               T.Max(Z, right.Z),
               T.Max(W, right.W));

    public IVector4<T> Min(IVector4<T> right) =>
        Create(T.Min(X, right.X),
               T.Min(Y, right.Y),
               T.Min(Z, right.Z),
               T.Min(W, right.W));

    public IVector4<T> Clamp(IVector4<T> min, IVector4<T> max) =>
        Create(T.Clamp(X, min.X, max.X),
               T.Clamp(Y, min.Y, max.Y),
               T.Clamp(Z, min.Z, max.Z),
               T.Clamp(W, min.W, max.W));

    public IVector4<T> Lerp(IVector4<T> max, T amount) =>
        Create(MathUtil.Lerp(X, max.X, amount),
               MathUtil.Lerp(Y, max.Y, amount),
               MathUtil.Lerp(Z, max.Z, amount),
               MathUtil.Lerp(W, max.W, amount));

    public T Distance(IVector4<T> right);

    public T DistanceSquared(IVector4<T> right) => Subtract(right).LengthSquared();

    public IVector4<T> Abs() => Create(T.Abs(X), T.Abs(Y), T.Abs(Z), T.Abs(W));

    public IVector4<T> Transform(IMatrix4x4<T> transform) =>
        Create(X * transform.M11 + Y * transform.M21 + Z * transform.M31 + W * transform.M41,
               X * transform.M12 + Y * transform.M22 + Z * transform.M32 + W * transform.M42,
               X * transform.M13 + Y * transform.M23 + Z * transform.M33 + W * transform.M43,
               X * transform.M14 + Y * transform.M24 + Z * transform.M34 + W * transform.M44);

    public IVector4<T> Negate() => Create(-X, -Y, -Z, -W);

    public static IVector4<T> operator -(IVector4<T> vec) => vec.Negate();

    T[] IVector<T>.ToArray() => new[] {X, Y, Z, W};
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