using System.Numerics;

namespace Mathematics.Vectors; 

public struct Vector3D: IVector3<double> {
    public double Length() => throw new NotImplementedException();

    public bool IsZero() => throw new NotImplementedException();

    public bool IsNormalized() => throw new NotImplementedException();

    public IVector3<double> Create(double x, double y, double z) => throw new NotImplementedException();

    public Vector3D(double x, double y, double z) {
        X = x;
        y = y;
        Z = Z;
    }

    public static IVector3<double> Zero => new Vector3D(0, 0, 0);
    public static IVector3<double> One => new Vector3D(1, 1, 1);
    public static IVector3<double> UnitX => new Vector3D(1, 0, 0);
    public static IVector3<double> UnitY => new Vector3D(0, 1, 0);
    public static IVector3<double> UnitZ => new Vector3D(0, 0, 1);
    public double X { get; init; }
    public double Y { get; init; }
    public double Z { get; init; }
    public static IVector3<double> CatmullRom(IVector3<double> value1, IVector3<double> value2, IVector3<double> value3, IVector3<double> value4, double amount) {
        var (x, y, z) = MathUtil.CatmullRom(value1,value2,value3,value4, amount);
        return new Vector3D(x, y, z);
    }

    public static IVector3<double> Barycentric(IVector3<double> value1, IVector3<double> value2, IVector3<double> value3, double amount1, double amount2) {
        var (x, y, z) = MathUtil.Barycentric(value1, value2,value3, amount1, amount2);
        return new Vector3D(x, y, z);
    }

    public static IVector3<double> Hermite(IVector3<double> value1, IVector3<double> tangent1, IVector3<double> value2, IVector3<double> tangent2, double amount) {
        var (x, y, z) = MathUtil.Hermite(value1, tangent1, value2, tangent2, amount);
        return new Vector3D(x, y, z);
    }

    public double Distance(IVector3<double> right) => Math.Sqrt(((IVector3<double>) this).DistanceSquared(right));
}
