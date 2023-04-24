using NumQuaternion =  System.Numerics.Quaternion;
using Mathematics.Vectors;

namespace Mathematics.Quaternion;

public struct QuaternionF : IQuaternion<float, QuaternionF> {
    private System.Numerics.Quaternion quaternion;

    public float X {
        get => quaternion.X;
        set => quaternion.X = value;
    }

    public float Y {
        get => quaternion.Y;
        set => quaternion.Y = value;
    }

    public float Z {
        get => quaternion.Z;
        set => quaternion.Z = value;
    }

    public float W {
        get => quaternion.W;
        set => quaternion.W = value;
    }

    public float Length() => quaternion.Length();

    public QuaternionF Unbox() => this;

    public static QuaternionF Build(float x, float y, float z, float w) => new(new NumQuaternion(x, y, z, w));

    public QuaternionF(NumQuaternion sysQuaternion) => quaternion = sysQuaternion;

    public float Angle {
        get {
            var length = (X * X) + (Y * Y) + (Z * Z);
            if (Numerics.IsZero(length))
                return 0.0f;
            
            return 2.0f * MathF.Acos(float.Clamp(W, -1f, 1f));
        }
    }

    public (float x, float y, float z) Axis {
        get {
            var length = X * X + Y * Y + Z * Z;
            if (Numerics.IsZero(length))
                return (1f, 0f, 0f);

            var inv = 1.0f / (float) Math.Sqrt(length);
            return (X * inv, Y * inv, Z * inv);
        }
    }

    public QuaternionF Invert() => new(NumQuaternion.Inverse(quaternion));

    public QuaternionF Multiply(QuaternionF right) => new(NumQuaternion.Multiply(quaternion, right.quaternion));

    public QuaternionF Multiply(IVector4<float, QuaternionF> right) => Multiply(right.Unbox());

    public QuaternionF Lerp(IVector4<float, QuaternionF> max, float amount) => Lerp(max.Unbox(), amount);

    public QuaternionF Lerp(QuaternionF max, float amount) => new (NumQuaternion.Lerp(quaternion, max.quaternion, amount));

    public static QuaternionF Identity { get; } = new(NumQuaternion.Identity);
    public static QuaternionF Zero => new(NumQuaternion.Zero);
    public static QuaternionF One => Build(1, 1, 1, 1);
    public static QuaternionF UnitX => Build(1, 0, 0, 0);
    public static QuaternionF UnitY => Build(0, 1, 0, 0);
    public static QuaternionF UnitZ => Build(0, 0, 1, 0);
    public static QuaternionF UnitW => Build(0, 0, 0, 1);
    public float Distance(QuaternionF right) => throw new NotImplementedException();
}
