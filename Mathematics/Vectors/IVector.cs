using System.Numerics;
using Mathematics.Matrix;

namespace Mathematics.Vectors;

public interface IVector<T,R> where T : struct,INumber<T>{
    public abstract static int Dimension { get; }

    public T Length();
    public T LengthSquared();

    public bool IsNormalized() => Numerics.IsOne(Length());

    public R Normalized();

    public T Max();
    public T Min();

    public T[] ToArray();

    public R Unbox();
}

public interface IVector3Values<T> where T : struct, INumber<T> {
    public T X { get; set; }
    public T Y { get; set; }
    public T Z { get; set; }
}

public interface IVector3<NumberType, VecType> : IVector3Values<NumberType>, IVector<NumberType, VecType>
    where NumberType : struct, INumber<NumberType> where VecType : IVector3<NumberType, VecType> {
    public abstract static VecType Build(NumberType x, NumberType y, NumberType z);

    public void Normalize();

    public new VecType Normalized();

    static int IVector<NumberType,VecType>.Dimension => 3;

    NumberType IVector<NumberType,VecType>.LengthSquared() => X * X + Y * Y + Z * Z;

    VecType IVector<NumberType,VecType>.Normalized() => this / Length();

    NumberType IVector<NumberType,VecType>.Max() => NumberType.Max(NumberType.Max(X, Y), Z);

    NumberType IVector<NumberType, VecType>.Min() => NumberType.Min(NumberType.Min(X, Y), Z);

    public abstract static VecType Zero { get; }
    public abstract static VecType One { get; }
    public abstract static VecType UnitX { get; }
    public abstract static VecType UnitY { get; }
    public abstract static VecType UnitZ { get; }

    public abstract static VecType CatmullRom(
        IVector3<NumberType,VecType> value1,
        IVector3<NumberType,VecType> value2,
        IVector3<NumberType,VecType> value3,
        IVector3<NumberType,VecType> value4,
        NumberType amount
    );

    public abstract static VecType Barycentric(
        IVector3<NumberType,VecType> value1,
        IVector3<NumberType,VecType> value2,
        IVector3<NumberType,VecType> value3,
        NumberType amount1,
        NumberType amount2
    );

    public abstract static VecType Hermite(
        IVector3<NumberType,VecType> value1,
        IVector3<NumberType,VecType> tangent1,
        IVector3<NumberType,VecType> value2,
        IVector3<NumberType,VecType> tangent2,
        NumberType amount
    );

    public static VecType SmoothStep(IVector3<NumberType,VecType> start, IVector3<NumberType,VecType> end, NumberType amount) 
        => start.Lerp(end, MathUtil.SmoothStep(amount));

    public VecType Add(IVector3<NumberType,VecType> right) => VecType.Build(X + right.X, Y + right.Y, Z + right.Z);
    public VecType Add(NumberType scalar) => VecType.Build(X + scalar, Y + scalar, Z + scalar);
    public VecType Subtract(IVector3<NumberType,VecType> right) => VecType.Build(X - right.X, Y - right.Y, Z - right.Z);
    public VecType Subtract(NumberType scalar) => VecType.Build(X - scalar, Y - scalar, Z - scalar);
    public VecType Multiply(IVector3<NumberType,VecType> right) => VecType.Build(X * right.X, Y * right.Y, Z * right.Z);
    public VecType Multiply(NumberType scalar) => VecType.Build(X * scalar, Y * scalar, Z * scalar);
    public VecType Divide(IVector3<NumberType,VecType> right) => VecType.Build(X / right.X, Y / right.Y, Z / right.Z);
    public VecType Divide(NumberType scalar)  => VecType.Build(X / scalar, Y / scalar, Z / scalar);
        
    public VecType Cross(IVector3<NumberType,VecType> right) =>
        VecType.Build((Y * right.Z) - (Z * right.Y),
                (Z * right.X) - (X * right.Z),
                (X * right.Y) - (Y * right.X));

    public NumberType Dot(IVector3<NumberType,VecType> right) => X * right.X + Y * right.Y + Z * right.Z;

    public VecType Reflect(IVector3<NumberType, VecType> normal) {
        var dot = X * normal.X + Y * normal.Y + Z * normal.Z;
        return VecType.Build(X - (dot + dot) * normal.X, 
                       Y - (dot + dot) * normal.Y, 
                       Z - (dot + dot) * normal.Z);
    }

    public static VecType operator +(IVector3<NumberType,VecType> left, IVector3<NumberType,VecType> right) => left.Add(right);
    public static VecType operator +(IVector3<NumberType,VecType> left, NumberType right) => left.Add(right);
    public static VecType operator +(NumberType right, IVector3<NumberType, VecType> left) => left.Add(right);

    public static VecType operator -(IVector3<NumberType, VecType> left, IVector3<NumberType, VecType> right) => left.Subtract(right);
    public static VecType operator -(IVector3<NumberType,VecType> left, NumberType right) => left.Subtract(right);

    public static VecType operator *(IVector3<NumberType,VecType> left, IVector3<NumberType,VecType> right) => left.Multiply(right);
    public static VecType operator *(IVector3<NumberType, VecType> left, NumberType scalar) => left.Multiply(scalar);
    public static VecType operator *(NumberType scalar, IVector3<NumberType, VecType> left) => left.Multiply(scalar);
    
    public static VecType operator /(IVector3<NumberType, VecType> left, IVector3<NumberType, VecType> right) => left.Divide(right);
    public static VecType operator /(IVector3<NumberType, VecType> left, NumberType scalar) => left.Divide(scalar);

    public VecType Max(IVector3<NumberType, VecType> right) => VecType.Build(NumberType.Max(X, right.X), NumberType.Max(Y, right.Y), NumberType.Max(Z, right.Z));
    public VecType Min(IVector3<NumberType, VecType> right) => VecType.Build(NumberType.Min(X, right.X), NumberType.Min(Y, right.Y), NumberType.Min(Z, right.Z));

    public VecType Clamp(IVector3<NumberType, VecType> min, IVector3<NumberType, VecType> max) =>
        VecType.Build(NumberType.Clamp(X, min.X, max.X),
                NumberType.Clamp(Y, min.Y, max.Y),
                NumberType.Clamp(Z, min.Z, max.Z));

    public VecType Lerp(IVector3<NumberType, VecType> max, NumberType amount)=>
        VecType.Build(MathUtil.Lerp(X, max.X, amount),
                MathUtil.Lerp(Y, max.Y, amount),
                MathUtil.Lerp(Z, max.Z, amount));

    public NumberType Distance(IVector3<NumberType, VecType> right);
    public NumberType DistanceSquared(IVector3<NumberType, VecType> right) => Subtract(right).LengthSquared();
    public VecType Abs() => VecType.Build(NumberType.Abs(X), NumberType.Abs(Y), NumberType.Abs(Z));

    public VecType Transform(IMatrix4X4Values<NumberType> transform) =>
        VecType.Build(X * transform.M11 + Y * transform.M21 + Z * transform.M31 + transform.M41,
                X * transform.M12 + Y * transform.M22 + Z * transform.M32 + transform.M42,
                X * transform.M13 + Y * transform.M23 + Z * transform.M33 + transform.M43);

    public  VecType TransformCoordinate(IMatrix4X4Values<NumberType> transform) {
        var x = X * transform.M11 + Y * transform.M21 + Z * transform.M31 + transform.M41;
        var y = X * transform.M12 + Y * transform.M22 + Z * transform.M32 + transform.M42;
        var z = X * transform.M13 + Y * transform.M23 + Z * transform.M33 + transform.M43;
        var w = NumberType.One / (X * transform.M14 + Y * transform.M24 + Z * transform.M34 + transform.M44);
        return VecType.Build(x * w, y * w, z * w);
    }

    public IVector3<NumberType, VecType> TransformNormal(IMatrix4X4Values<NumberType> transform) =>
        VecType.Build(X * transform.M11 + Y * transform.M21 + Z * transform.M31,
                X * transform.M12 + Y * transform.M22 + Z * transform.M32,
                X * transform.M13 + Y * transform.M23 + Z * transform.M33);

    public VecType Negate() => VecType.Build(-X, -Y, -Z);

    public static VecType operator -(IVector3<NumberType,VecType> vec) => vec.Negate();

    NumberType[] IVector<NumberType, VecType>.ToArray() => new[] {X, Y, Z};

    public TOther ConvertTo<Number, TOther>(TOther v) where Number : struct, INumber<Number>
                                                          where TOther : IVector3<Number, TOther> {
        var x = Number.CreateTruncating(X);
        var y = Number.CreateTruncating(Y);
        var z= Number.CreateTruncating(Z);
        return TOther.Build(x, y, z);
    }
}

public interface IVector4Values<T> where T : struct, INumber<T> {
    public T X { get; set; }
    public T Y { get; set; }
    public T Z { get; set; }
    public T W { get; set; }
}

public interface IVector4<NumberType,VecType> :IVector4Values<NumberType>, IVector<NumberType, VecType> where NumberType : struct, INumber<NumberType> where VecType: IVector4<NumberType,VecType> {
    public abstract static VecType Build(NumberType x, NumberType y, NumberType z, NumberType w);

    static int IVector<NumberType,VecType>.Dimension => 4;

    NumberType IVector<NumberType, VecType>.Max() => NumberType.Max(NumberType.Max(X, Y), NumberType.Max(Z,W));

    NumberType IVector<NumberType, VecType>.Min() => NumberType.Min(NumberType.Min(X, Y), NumberType.Min(Z,W));
    VecType IVector<NumberType, VecType>.Normalized() => this / Length();

    NumberType IVector<NumberType, VecType>.LengthSquared() => X * X + Y * Y + Z * Z + W * W;

    public abstract static VecType Zero { get; }
    public abstract static VecType One { get; }
    public abstract static VecType UnitX { get; }
    public abstract static VecType UnitY { get; }
    public abstract static VecType UnitZ { get; }
    public abstract static VecType UnitW { get; }

    public VecType Add(VecType right) => VecType.Build(X + right.X, Y + right.Y, Z + right.Z, W + right.W);
    public VecType Add(IVector4<NumberType, VecType> right) => VecType.Build(X + right.X, Y + right.Y, Z + right.Z, W + right.W);

    public VecType Add(NumberType scalar) => VecType.Build(X + scalar, Y + scalar, Z + scalar, W + scalar);
    public VecType Subtract(VecType right) => VecType.Build(X - right.X, Y - right.Y, Z - right.Z, W - right.W);
    public VecType Subtract(IVector4<NumberType, VecType> right) => VecType.Build(X - right.X, Y - right.Y, Z - right.Z, W - right.W);
    public VecType Subtract(NumberType scalar) => VecType.Build(X - scalar, Y - scalar, Z - scalar, W - scalar);

    public VecType Multiply(VecType right) => VecType.Build(X * right.X, Y * right.Y, Z * right.Z, W * right.W);
    public VecType Multiply(IVector4<NumberType, VecType> right) => VecType.Build(X * right.X, Y * right.Y, Z * right.Z, W * right.W);
    public VecType Multiply(NumberType scalar) => VecType.Build(X * scalar, Y * scalar, Z * scalar, W * scalar);

    public VecType Divide(VecType right) => VecType.Build(X / right.X, Y / right.Y, Z / right.Z, W / right.W);
    public VecType Divide(IVector4<NumberType, VecType> right) => VecType.Build(X / right.X, Y / right.Y, Z / right.Z, W / right.W);
    public VecType Divide(NumberType scalar) => VecType.Build(X / scalar, Y / scalar, Z / scalar, W / scalar);

    public NumberType Dot(VecType right) => X * right.X + Y * right.Y + Z * right.Z + W * right.W;

    public static VecType operator +(IVector4<NumberType, VecType> left, IVector4<NumberType, VecType> right) => left.Add(right);
    public static VecType operator +(VecType left, IVector4<NumberType, VecType> right) => left.Add(right);
    public static VecType operator +(IVector4<NumberType, VecType> left, NumberType right) => left.Add(right);
    public static VecType operator +(NumberType right, IVector4<NumberType, VecType> left) => left.Add(right);

    public static VecType operator -(IVector4<NumberType, VecType> left, IVector4<NumberType, VecType> right) => left.Subtract(right);
    public static VecType operator -(IVector4<NumberType, VecType> left, NumberType right) => left.Subtract(right);

    public static VecType operator *(IVector4<NumberType, VecType> left, IVector4<NumberType, VecType> right) => left.Multiply(right);
    public static VecType operator *(IVector4<NumberType, VecType> left, NumberType scalar) => left.Multiply(scalar);
    public static VecType operator *(NumberType scalar, IVector4<NumberType, VecType> left) => left.Multiply(scalar);

    public static VecType operator /(IVector4<NumberType, VecType> left, IVector4<NumberType, VecType> right) => left.Divide(right);
    public static VecType operator /(IVector4<NumberType, VecType> left, NumberType scalar) => left.Divide(scalar);

    public static VecType operator -(IVector4<NumberType, VecType> vec) => vec.Negate();

    public VecType Max(IVector4<NumberType, VecType> right) =>
        VecType.Build(NumberType.Max(X, right.X),
                      NumberType.Max(Y, right.Y),
                      NumberType.Max(Z, right.Z),
                      NumberType.Max(W, right.W));

    public VecType Max(VecType right) =>
        VecType.Build(NumberType.Max(X, right.X),
                      NumberType.Max(Y, right.Y),
                      NumberType.Max(Z, right.Z),
                      NumberType.Max(W, right.W));


    public VecType Min(IVector4<NumberType, VecType> right) =>
        VecType.Build(NumberType.Min(X, right.X),
                      NumberType.Min(Y, right.Y),
                      NumberType.Min(Z, right.Z),
                      NumberType.Min(W, right.W));

    

    public VecType Min(VecType right) =>
        VecType.Build(NumberType.Min(X, right.X),
                      NumberType.Min(Y, right.Y),
                      NumberType.Min(Z, right.Z),
                      NumberType.Min(W, right.W));

    public VecType Clamp(IVector4<NumberType, VecType> min, IVector4<NumberType, VecType> max) =>
        VecType.Build(NumberType.Clamp(X, min.X, max.X),
                      NumberType.Clamp(Y, min.Y, max.Y),
                      NumberType.Clamp(Z, min.Z, max.Z),
                      NumberType.Clamp(W, min.W, max.W));

    public VecType Clamp(VecType min, VecType max) =>
        VecType.Build(NumberType.Clamp(X, min.X, max.X),
                      NumberType.Clamp(Y, min.Y, max.Y),
                      NumberType.Clamp(Z, min.Z, max.Z),
                      NumberType.Clamp(W, min.W, max.W));

    public VecType Lerp(IVector4<NumberType, VecType> max, NumberType amount) =>
        VecType.Build(MathUtil.Lerp(X, max.X, amount),
                      MathUtil.Lerp(Y, max.Y, amount),
                      MathUtil.Lerp(Z, max.Z, amount),
                      MathUtil.Lerp(W, max.W, amount));

    public VecType Lerp(VecType max, NumberType amount) =>
        VecType.Build(MathUtil.Lerp(X, max.X, amount),
                      MathUtil.Lerp(Y, max.Y, amount),
                      MathUtil.Lerp(Z, max.Z, amount),
                      MathUtil.Lerp(W, max.W, amount));

    public NumberType Distance(VecType right);

    public NumberType DistanceSquared(IVector4<NumberType, VecType> right) => Subtract(right).LengthSquared();
    public NumberType DistanceSquared(VecType right) => Subtract(right).LengthSquared();

    public VecType Abs() => VecType.Build(NumberType.Abs(X), NumberType.Abs(Y), NumberType.Abs(Z), NumberType.Abs(W));

    public VecType Transform(IMatrix4X4Values<NumberType> transform) =>
        VecType.Build(X * transform.M11 + Y * transform.M21 + Z * transform.M31 + W * transform.M41,
                      X * transform.M12 + Y * transform.M22 + Z * transform.M32 + W * transform.M42,
                      X * transform.M13 + Y * transform.M23 + Z * transform.M33 + W * transform.M43,
                      X * transform.M14 + Y * transform.M24 + Z * transform.M34 + W * transform.M44);

    public VecType Negate() => VecType.Build(-X, -Y, -Z, -W);

    NumberType[] IVector<NumberType, VecType>.ToArray() => new[] {X, Y, Z, W};

    public TOther ConvertTo<Number, TOther>(TOther v) where Number : struct, INumber<Number>
                                                      where TOther : IVector4<Number, TOther> {
        var x = Number.CreateTruncating(X);
        var y = Number.CreateTruncating(Y);
        var z= Number.CreateTruncating(Z);
        var w= Number.CreateTruncating(W);
        return TOther.Build(x, y, z, w);
    }

    public Vector3Type ToXyz<Vector3Type>() where Vector3Type : IVector3<NumberType, Vector3Type>;
}