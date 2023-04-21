using System.Numerics;
using System.Runtime.CompilerServices;
using Mathematics.Matrix;

namespace Mathematics.Vectors; 

public readonly struct Vector4F: IVector4<float> {
    public readonly Vector4 V;

    private Vector4F(Vector4 v) => V = v;

    public Vector4F(IVector4<float> v) => V = new Vector4(v.X, v.Y, v.Z, v.W);

    public static int Dimension => 4;

    public float Length() => V.Length();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IVector4<float> Create(float x, float y, float z, float w) 
        => new Vector4F(new Vector4(x,y,z,w));

    public float LengthSquared() 
        => V.LengthSquared();

    public bool IsNormalized() => throw new NotImplementedException();

    public IVector<float> Normalized() 
        => new Vector4F(Vector4.Normalize(V));

    public float Max() 
        => MathF.Max(MathF.Max(X,Y), MathF.Max(Z,W));

    public float Min() 
        => MathF.Min(MathF.Min(X,Y), MathF.Min(Z,W));

    public static IVector4<float> Zero => new Vector4F(Vector4.Zero);
    public static IVector4<float> One => new Vector4F(Vector4.One);
    public static IVector4<float> UnitX => new Vector4F(Vector4.UnitX);
    public static IVector4<float> UnitY => new Vector4F(Vector4.UnitY);
    public static IVector4<float> UnitZ => new Vector4F(Vector4.UnitZ);
    public static IVector4<float> UnitW => new Vector4F(Vector4.UnitW);
    
    public float X { 
        get => V.X;
        init=> V.X = value;
    }
    public float Y {
        get => V.Y;
        init => V.Y = value;
    }

    public float Z {
        get => V.Z;
        init => V.Z = value;
    }
    public float W {
        get => V.W;
        init => V.W = value;
    }

    public IVector4<float> Add(IVector4<float> right) 
        => new Vector4F(Vector4.Add(V, right.ToVector4F().V));

    public IVector4<float> Add(float scalar) 
        => new Vector4F(Vector4.Add(V, new Vector4(scalar)));

    public IVector4<float> Subtract(IVector4<float> right) 
        => new Vector4F(Vector4.Subtract(V, right.ToVector4F().V));

    public IVector4<float> Subtract(float scalar) 
        => new Vector4F(Vector4.Subtract(V, new Vector4(scalar)));

    public IVector4<float> Multiply(IVector4<float> right) 
        => new Vector4F();

    public IVector4<float> Multiply(float scalar) 
        => new Vector4F(Vector4.Multiply(V, new Vector4(scalar)));

    public IVector4<float> Divide(IVector4<float> right) 
        => new Vector4F(Vector4.Divide(V, right.ToVector4F().V));

    public IVector4<float> Divide(float scalar) 
        => new Vector4F(Vector4.Divide(V, new Vector4(scalar)));

    public float Dot(IVector4<float> right) 
        => Vector4.Dot(V, right.ToVector4F().V);

    public IVector4<float> Max(IVector4<float> right) 
        => new Vector4F(Vector4.Max(V, right.ToVector4F().V));

    public IVector4<float> Min(IVector4<float> right) 
        => new Vector4F(Vector4.Min(V, right.ToVector4F().V));

    public IVector4<float> Clamp(IVector4<float> min, IVector4<float> max) 
        => new Vector4F(Vector4.Clamp(V, min.ToVector4F().V, max.ToVector4F().V));

    public IVector4<float> Lerp(IVector4<float> max, float amount) 
        => new Vector4F(Vector4.Lerp(V, max.ToVector4F().V, amount));

    public float Distance(IVector4<float> right) 
        => Vector4.Distance(V, right.ToVector4F().V);

    public float DistanceSquared(IVector4<float> right) 
        => Vector4.DistanceSquared(V, right.ToVector4F().V);

    public IVector4<float> Abs() => new Vector4F(Vector4.Abs(V));

    public IVector4<float> Transform(IMatrix4x4<float> matrix) 
        => new Vector4F(Vector4.Transform(V, matrix.ToMatrix4x4F().Matrix));

    public IVector4<float> Negate() 
        => new Vector4F(Vector4.Negate(V));
}
