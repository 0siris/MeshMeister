using System.Numerics;
using System.Runtime.CompilerServices;
using Mathematics.Matrix;

namespace Mathematics.Vectors;

public readonly struct Vector3F : IVector3<float> {
    public readonly Vector3 V;

    private Vector3F(Vector3 v) => V = v;

    private Vector3F(IVector3<float> c) => V = new Vector3(c.X, c.Y, c.Z);

    public Vector3F(float x, float y, float z) => V = new Vector3(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IVector3<float> Create(float x, float y, float z) => new Vector3F(x, y, z);

    public static IVector3<float> Zero { get; } = new Vector3F(Vector3.Zero);

    public static IVector3<float> One { get; } = new Vector3F(Vector3.One);

    public static IVector3<float> UnitX { get; } = new Vector3F(Vector3.UnitX);
    public static IVector3<float> UnitY { get; } = new Vector3F(Vector3.UnitY);
    public static IVector3<float> UnitZ { get; } = new Vector3F(Vector3.UnitZ);

    public static int Dimension => 3;

    public float Length() => V.Length();

    public float LengthSquared() => V.LengthSquared();

    public float X {
        get => V.X;
        init => V.X = value;
    }

    public float Y {
        get => V.Y;
        init => V.Y = value;
    }

    public float Z {
        get => V.Z;
        init => V.Z = value;
    }

    public bool IsZero() => throw new NotImplementedException();

    public bool IsNormalized() => throw new NotImplementedException();

    public static IVector3<float> Barycentric(
        IVector3<float> value1,
        IVector3<float> value2,
        IVector3<float> value3,
        float amount1,
        float amount2
    ) {
        var (x, y, z) = MathUtil.Barycentric(value1, value2, value3, amount1, amount2);
        return new Vector3F(x, y, z);
    }

    public static IVector3<float> Hermite(
        IVector3<float> value1,
        IVector3<float> tangent1,
        IVector3<float> value2,
        IVector3<float> tangent2,
        float amount
    ) {
        var (x, y, z) = MathUtil.Hermite(value1, tangent1, value2, tangent2, amount);
        return new Vector3F(x, y, z);
    }

    public IVector3<float> Add(IVector3<float> right) {
        if (right is Vector3F vf)
            return new Vector3F(Vector3.Add(V, vf.V));
        return new Vector3F(Vector3.Add(V, new Vector3F(right).V));
    }

    public IVector3<float> Add(float scalar) => new Vector3F(Vector3.Add(V, new Vector3(scalar)));

    public IVector3<float> Subtract(IVector3<float> right) => new Vector3F(Vector3.Subtract(V, right.ToVector3F().V));

    public IVector3<float> Subtract(float scalar) => new Vector3F(Vector3.Subtract(V, new Vector3(scalar)));

    public IVector3<float> Multiply(IVector3<float> right) => new Vector3F(Vector3.Multiply(V, right.ToVector3F().V));

    public IVector3<float> Multiply(float scalar) => new Vector3F(Vector3.Multiply(V, new Vector3(scalar)));

    public IVector3<float> Divide(IVector3<float> right) => new Vector3F(Vector3.Divide(V, right.ToVector3F().V));

    public IVector3<float> Divide(float scalar) => new Vector3F(Vector3.Divide(V, new Vector3(scalar)));

    public IVector3<float> Cross(IVector3<float> right) => new Vector3F(Vector3.Cross(V, right.ToVector3F().V));

    public float Dot(IVector3<float> right) => Vector3.Dot(V, right.ToVector3F().V);

    public IVector<float> Normalized() => new Vector3F(Vector3.Normalize(V));
    public float Max() => MathF.Max(X, MathF.Max(X, Y));
    public float Min() => MathF.Min(X, MathF.Min(X, Y));

    public IVector3<float> Max(IVector3<float> right) => new Vector3F(Vector3.Max(V, right.ToVector3F().V));

    public IVector3<float> Min(IVector3<float> right) => new Vector3F(Vector3.Min(V, right.ToVector3F().V));

    public IVector3<float> Abs() => new Vector3F(Vector3.Abs(V));

    public IVector3<float> Clamp(IVector3<float> min, IVector3<float> max) =>
        new Vector3F(Vector3.Clamp(V, min.ToVector3F().V, max.ToVector3F().V));

    public IVector3<float> Lerp(IVector3<float> max, float amount) =>
        new Vector3F(Vector3.Lerp(V, max.ToVector3F().V, amount));

    public float Distance(IVector3<float> right) => Vector3.Distance(V, right.ToVector3F().V);

    public float DistanceSquared(IVector3<float> right) => Vector3.DistanceSquared(V, right.ToVector3F().V);

    public static IVector3<float> CatmullRom(
        IVector3<float> value1,
        IVector3<float> value2,
        IVector3<float> value3,
        IVector3<float> value4,
        float amount
    ) {
        var (x, y, z) = MathUtil.CatmullRom(value1,value2, value3, value4, amount);
        return new Vector3F(x, y, z);
    }

    public float Distance(Vector3F right) => Vector3.Distance(V, right.V);
    public float DistanceSquared(Vector3F right) => Vector3.DistanceSquared(V, right.V);

    public IVector3<float> Transform(IMatrix4x4<float> matrix) {
        if (matrix is not Matrix4x4F m) m = new Matrix4x4F(matrix);
        return new Vector3F(Vector3.Transform(V, m.Matrix));
    }

    public IVector3<float> TransformNormal(IMatrix4x4<float> matrix) {
        if (matrix is not Matrix4x4F m) m = new Matrix4x4F(matrix);
        return new Vector3F(Vector3.TransformNormal(V, m.Matrix));
    }

    public IVector3<float> Negate() => new Vector3F(Vector3.Negate(V));

    public static implicit operator Vector3(Vector3F v) => v.V;
    public static implicit operator Vector3F(Vector3 v) => new(v);
}
