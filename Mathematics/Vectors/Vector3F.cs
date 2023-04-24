using System.Numerics;
using Mathematics.Matrix;

namespace Mathematics.Vectors;

public struct Vector3F : IVector3<float,Vector3F> {
    public Vector3 V;

    private Vector3F(Vector3 v) => V = v;

    private Vector3F(IVector3Values<float> c) => V = new Vector3(c.X, c.Y, c.Z);

    public Vector3F(float x, float y, float z) => V = new Vector3(x, y, z);


    public Vector3F TransformCoordinate(IMatrix4x4<float> transform) => throw new NotImplementedException();

    public static Vector3F Zero { get; } = new(Vector3.Zero);

    public static Vector3F One { get; } = new(Vector3.One);

    public static Vector3F UnitX { get; } = new(Vector3.UnitX);
    public static Vector3F UnitY { get; } = new(Vector3.UnitY);
    public static Vector3F UnitZ { get; } = new(Vector3.UnitZ);

    Vector3F IVector3<float,Vector3F>.Normalized() => new (Vector3.Normalize(V));
    

    public static int Dimension => 3;

    public float Length() => V.Length();

    public float LengthSquared() => V.LengthSquared();

    public float X {
        get => V.X;
        set => V.X = value;
    }

    public float Y {
        get => V.Y;
        set => V.Y = value;
    }

    public float Z {
        get => V.Z;
        set => V.Z = value;
    }

    public bool IsNormalized() => throw new NotImplementedException();

    public static Vector3F Barycentric(
        IVector3<float, Vector3F> value1,
        IVector3<float, Vector3F> value2,
        IVector3<float, Vector3F> value3,
        float amount1,
        float amount2
    ) {
        var (x, y, z) = MathUtil.Barycentric(value1, value2, value3, amount1, amount2);
        return new Vector3F(x, y, z);
    }

    public static Vector3F Hermite(
        IVector3<float, Vector3F> value1,
        IVector3<float, Vector3F> tangent1,
        IVector3<float, Vector3F> value2,
        IVector3<float, Vector3F> tangent2,
        float amount
    ) {
        var (x, y, z) = MathUtil.Hermite(value1, tangent1, value2, tangent2, amount);
        return new Vector3F(x, y, z);
    }

    public Vector3F Add(IVector3<float, Vector3F> right) {
        if (right is Vector3F vf)
            return new Vector3F(Vector3.Add(V, vf.V));
        return new Vector3F(Vector3.Add(V, new Vector3F(right).V));
    }

    public Vector3F Add(float scalar) => new Vector3F(Vector3.Add(V, new Vector3(scalar)));

    public Vector3F Subtract(IVector3<float, Vector3F> right) => new(Vector3.Subtract(V, right.Unbox().V));

    public Vector3F Subtract(float scalar) => new(Vector3.Subtract(V, new Vector3(scalar)));

    public Vector3F Multiply(IVector3<float,Vector3F> right) => new Vector3F(Vector3.Multiply(V, right.Unbox().V));

    public Vector3F Multiply(float scalar) => new Vector3F(Vector3.Multiply(V, new Vector3(scalar)));

    public Vector3F Divide(IVector3<float, Vector3F> right) => new Vector3F(Vector3.Divide(V, right.Unbox().V));

    public Vector3F Divide(float scalar) => new Vector3F(Vector3.Divide(V, new Vector3(scalar)));

    public Vector3F Cross(IVector3<float, Vector3F> right) => new Vector3F(Vector3.Cross(V, right.Unbox().V));

    public static Vector3F operator -(Vector3F v) => new(-v.X, -v.Y, -v.Z);

    public float Dot(IVector3<float, Vector3F> right) => Vector3.Dot(V, right.Unbox().V);

    public Vector3F Normalized() => new Vector3F(Vector3.Normalize(V));
    public float Max() => MathF.Max(X, MathF.Max(X, Y));
    public float Min() => MathF.Min(X, MathF.Min(X, Y));

    public Vector3F Max(IVector3<float, Vector3F> right) => new Vector3F(Vector3.Max(V, right.Unbox().V));

    public Vector3F Min(IVector3<float, Vector3F> right) => new Vector3F(Vector3.Min(V, right.Unbox().V));

    public Vector3F Abs() => new Vector3F(Vector3.Abs(V));

    public Vector3F Clamp(IVector3<float, Vector3F> min, IVector3<float, Vector3F> max) =>
        new(Vector3.Clamp(V, min.Unbox().V, max.Unbox().V));

    public Vector3F Lerp(IVector3<float, Vector3F> max, float amount) =>
        new(Vector3.Lerp(V, max.Unbox().V, amount));

    public float Distance(IVector3<float, Vector3F> right) => Vector3.Distance(V, right.Unbox().V);

    public float DistanceSquared(IVector3<float, Vector3F> right) => Vector3.DistanceSquared(V, right.Unbox().V);

    public static Vector3F CatmullRom(
        IVector3<float, Vector3F> value1,
        IVector3<float, Vector3F> value2,
        IVector3<float, Vector3F> value3,
        IVector3<float, Vector3F> value4,
        float amount
    ) {
        var (x, y, z) = MathUtil.CatmullRom(value1,value2, value3, value4, amount);
        return new Vector3F(x, y, z);
    }

    public float Distance(Vector3F right) => Vector3.Distance(V, right.V);
    public float DistanceSquared(Vector3F right) => Vector3.DistanceSquared(V, right.V);

    public Vector3F Transform(IMatrix4x4<float> matrix) {
        if (matrix is not Matrix4x4F m) m = new Matrix4x4F(matrix);
        return new Vector3F(Vector3.Transform(V, m.Matrix));
    }


    public Vector3F TransformNormal(IMatrix4x4<float> matrix) {
        if (matrix is not Matrix4x4F m) m = new Matrix4x4F(matrix);
        return new Vector3F(Vector3.TransformNormal(V, m.Matrix));
    }

    public Vector3F Negate() => new(Vector3.Negate(V));

    public Vector3F Unbox() => this;

    public static implicit operator Vector3(Vector3F v) => v.V;
    public static implicit operator Vector3F(Vector3 v) => new(v);

    public static Vector3F Build(float x, float y, float z) 
        => new(x, y, z);

    public void Normalize() {
        var l = Length();
        X /= l;
        Y /= l;
        Z /= l;
    }


}
