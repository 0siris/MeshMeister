namespace Mathematics.Vectors; 

public struct Vector3D: IVector3<double, Vector3D> {
    public double Length() => Math.Sqrt(((IVector3<double,Vector3D>) this).LengthSquared());
    public Vector3D Unbox() => this;

    public static Vector3D Build(double x, double y, double z) 
        => new(x,y,z);

    public void Normalize() {
        throw new NotImplementedException();
    }

    public Vector3D Normalized() => throw new NotImplementedException();

    public Vector3D Create(double x, double y, double z) => new(x,y,z);
    public Vector3D(double x, double y, double z) {
        X = x;
        Y = y;
        Z = Z;
    }

    public static Vector3D Zero => new(0, 0, 0);
    public static Vector3D One => new(1, 1, 1);
    public static Vector3D UnitX => new(1, 0, 0);
    public static Vector3D UnitY => new(0, 1, 0);
    public static Vector3D UnitZ => new(0, 0, 1);
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public static Vector3D CatmullRom(IVector3<double, Vector3D> value1, IVector3<double, Vector3D> value2, IVector3<double, Vector3D> value3, IVector3<double, Vector3D> value4, double amount) {
        var (x, y, z) = MathUtil.CatmullRom(value1,value2,value3,value4, amount);
        return new Vector3D(x, y, z);
    }

    public static Vector3D Barycentric(IVector3<double, Vector3D> value1, IVector3<double, Vector3D> value2, IVector3<double, Vector3D> value3, double amount1, double amount2) {
        var (x, y, z) = MathUtil.Barycentric(value1, value2,value3, amount1, amount2);
        return new Vector3D(x, y, z);
    }

    public static Vector3D Hermite(IVector3<double, Vector3D> value1, IVector3<double, Vector3D> tangent1, IVector3<double, Vector3D> value2, IVector3<double, Vector3D> tangent2, double amount) {
        var (x, y, z) = MathUtil.Hermite(value1, tangent1, value2, tangent2, amount);
        return new Vector3D(x, y, z);
    }

    public double Distance(IVector3<double, Vector3D> right) => Math.Sqrt(((IVector3<double,Vector3D>) this).DistanceSquared(right));
}
