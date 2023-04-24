using System.Numerics;
using System.Runtime.CompilerServices;
using Mathematics.Matrix;

namespace Mathematics.Vectors;

public struct Vector4F : IVector4<float, Vector4F> {
    public Vector4 V;

    private Vector4F(Vector4 v) {
        V = v;
    }

    public Vector4F(IVector4<float, Vector4F> v) {
        V = new Vector4(v.X, v.Y, v.Z, v.W);
    }

    public Vector4F(IVector4Values<float> v) {
        V = new Vector4(v.X, v.Y, v.Z, v.W);
    }

    private Vector4F(float x, float y, float z, float w) {
        V = new Vector4(x, y, z, w);
    }

    public static Vector4F Build(float x, float y, float z, float w) => new(x, y, z, w);

    public static int Dimension => 4;

    public float Length() => V.Length();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector4F Create(float x, float y, float z, float w) => new(new Vector4(x, y, z, w));

    public float LengthSquared() => V.LengthSquared();

    public bool IsNormalized() => throw new NotImplementedException();

    public Vector4F Normalized() => new(Vector4.Normalize(V));

    public float Max() => MathF.Max(MathF.Max(X, Y), MathF.Max(Z, W));

    public float Min() => MathF.Min(MathF.Min(X, Y), MathF.Min(Z, W));

    public Vector4F Unbox() => this;

    public static Vector4F Zero => new(Vector4.Zero);
    public static Vector4F One => new(Vector4.One);
    public static Vector4F UnitX => new(Vector4.UnitX);
    public static Vector4F UnitY => new(Vector4.UnitY);
    public static Vector4F UnitZ => new(Vector4.UnitZ);
    public static Vector4F UnitW => new(Vector4.UnitW);

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

    public float W {
        get => V.W;
        set => V.W = value;
    }

    public Vector4F Add(IVector4<float, Vector4F> right) => new(Vector4.Add(V, right.Unbox().V));
    public Vector4F Add(Vector4F right) => new(Vector4.Add(V, right.V));

    public Vector4F Add(float scalar) => new(Vector4.Add(V, new Vector4(scalar)));

    public Vector4F Subtract(IVector4<float, Vector4F> right) => new(Vector4.Subtract(V, right.Unbox().V));
    public Vector4F Subtract(Vector4F right) => new(Vector4.Subtract(V, right.V));

    public Vector4F Subtract(float scalar) => new(Vector4.Subtract(V, new Vector4(scalar)));

    public Vector4F Multiply(IVector4<float, Vector4F> right) => throw new NotImplementedException();
    public Vector4F Multiply(Vector4F right) => throw new NotImplementedException();

    public Vector4F Multiply(float scalar) => new(Vector4.Multiply(V, new Vector4(scalar)));

    public Vector4F Divide(IVector4<float, Vector4F> right) => new(Vector4.Divide(V, right.Unbox().V));
    public Vector4F Divide(Vector4F right) => new(Vector4.Divide(V, right.V));

    public Vector4F Divide(float scalar) => new(Vector4.Divide(V, new Vector4(scalar)));

    public float Dot(IVector4<float, Vector4F> right) => Vector4.Dot(V, right.Unbox().V);

    public Vector4F Max(IVector4<float, Vector4F> right) => new(Vector4.Max(V, right.Unbox().V));

    public Vector4F Min(IVector4<float, Vector4F> right) => new(Vector4.Min(V, right.Unbox().V));

    public Vector4F Clamp(IVector4<float, Vector4F> min, IVector4<float, Vector4F> max) =>
        new(Vector4.Clamp(V, min.Unbox().V, max.Unbox().V));

    public Vector4F Lerp(IVector4<float, Vector4F> max, float amount) => new(Vector4.Lerp(V, max.Unbox().V, amount));

    public float Distance(Vector4F right) => throw new NotImplementedException();

    public float Distance(IVector4<float, Vector4F> right) => Vector4.Distance(V, right.Unbox().V);

    public float DistanceSquared(IVector4<float, Vector4F> right) => Vector4.DistanceSquared(V, right.Unbox().V);

    public Vector4F Abs() => new(Vector4.Abs(V));

    public Vector4F Transform(IMatrix4x4<float> matrix) => new(Vector4.Transform(V, matrix.ToMatrix4x4F().Matrix));

    public Vector4F Negate() => new(Vector4.Negate(V));
}
