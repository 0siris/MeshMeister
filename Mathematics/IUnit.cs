using System.Globalization;
using System.Numerics;

namespace Mathematics;


public readonly struct Prefix {
    public readonly string Name;
    public readonly char ShortName;
    public readonly int Base;
    private Prefix(string name, char shortName, int @base) {
        Name = name;
        ShortName = shortName;
        Base = @base;
    }

    public static readonly Prefix None = new("None", ' ' , 0);
    public static readonly Prefix Dezi = new("Dezi", 'c' , -1);
    public static readonly Prefix Zenti = new("Zenti", 'c' , -2);
    public static readonly Prefix Milli = new("Milli", 'm' , -3);
    public static readonly Prefix Mikro = new("Mikro", 'µ' , -6);
    public static readonly Prefix Nano = new("Nano", 'n' , -3);
    public static readonly Prefix Pico = new("Pico", 'p' , -3);
}

public record struct  Meter<T>(int Prefix, T Value):INumber<Meter<T>> where T: struct, INumber<T> {
    public int CompareTo(object? obj) => throw new NotImplementedException();

    public int CompareTo(Meter<T> other) => throw new NotImplementedException();

    public string ToString(string? format, IFormatProvider? formatProvider) => throw new NotImplementedException();

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider) => throw new NotImplementedException();

    public static Meter<T> Parse(string s, IFormatProvider? provider) => throw new NotImplementedException();

    public static bool TryParse(string? s, IFormatProvider? provider, out Meter<T> result) => throw new NotImplementedException();

    public static Meter<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => throw new NotImplementedException();

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Meter<T> result) => throw new NotImplementedException();

    public static Meter<T> operator +(Meter<T> left, Meter<T> right) => throw new NotImplementedException();

    public static Meter<T> AdditiveIdentity { get; set; }
    public static bool operator >(Meter<T> left, Meter<T> right) => throw new NotImplementedException();

    public static bool operator >=(Meter<T> left, Meter<T> right) => throw new NotImplementedException();

    public static bool operator <(Meter<T> left, Meter<T> right) => throw new NotImplementedException();

    public static bool operator <=(Meter<T> left, Meter<T> right) => throw new NotImplementedException();

    public static Meter<T> operator --(Meter<T> value) => throw new NotImplementedException();

    public static Meter<T> operator /(Meter<T> left, Meter<T> right) => throw new NotImplementedException();

    public static Meter<T> operator ++(Meter<T> value) => throw new NotImplementedException();

    public static Meter<T> operator %(Meter<T> left, Meter<T> right) => throw new NotImplementedException();

    public static Meter<T> MultiplicativeIdentity { get; set; }
    public static Meter<T> operator *(Meter<T> left, Meter<T> right) => throw new NotImplementedException();

    public static Meter<T> operator -(Meter<T> left, Meter<T> right) => throw new NotImplementedException();

    public static Meter<T> operator -(Meter<T> value) => throw new NotImplementedException();

    public static Meter<T> operator +(Meter<T> value) => throw new NotImplementedException();

    public static Meter<T> Abs(Meter<T> value) => throw new NotImplementedException();

    public static bool IsCanonical(Meter<T> value) => throw new NotImplementedException();

    public static bool IsComplexNumber(Meter<T> value) => throw new NotImplementedException();

    public static bool IsEvenInteger(Meter<T> value) => throw new NotImplementedException();

    public static bool IsFinite(Meter<T> value) => throw new NotImplementedException();

    public static bool IsImaginaryNumber(Meter<T> value) => throw new NotImplementedException();

    public static bool IsInfinity(Meter<T> value) => throw new NotImplementedException();

    public static bool IsInteger(Meter<T> value) => throw new NotImplementedException();

    public static bool IsNaN(Meter<T> value) => throw new NotImplementedException();

    public static bool IsNegative(Meter<T> value) => throw new NotImplementedException();

    public static bool IsNegativeInfinity(Meter<T> value) => throw new NotImplementedException();

    public static bool IsNormal(Meter<T> value) => throw new NotImplementedException();

    public static bool IsOddInteger(Meter<T> value) => throw new NotImplementedException();

    public static bool IsPositive(Meter<T> value) => throw new NotImplementedException();

    public static bool IsPositiveInfinity(Meter<T> value) => throw new NotImplementedException();

    public static bool IsRealNumber(Meter<T> value) => throw new NotImplementedException();

    public static bool IsSubnormal(Meter<T> value) => throw new NotImplementedException();

    public static bool IsZero(Meter<T> value) => throw new NotImplementedException();

    public static Meter<T> MaxMagnitude(Meter<T> x, Meter<T> y) => throw new NotImplementedException();

    public static Meter<T> MaxMagnitudeNumber(Meter<T> x, Meter<T> y) => throw new NotImplementedException();

    public static Meter<T> MinMagnitude(Meter<T> x, Meter<T> y) => throw new NotImplementedException();

    public static Meter<T> MinMagnitudeNumber(Meter<T> x, Meter<T> y) => throw new NotImplementedException();

    public static Meter<T> Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider) => throw new NotImplementedException();

    public static Meter<T> Parse(string s, NumberStyles style, IFormatProvider? provider) => throw new NotImplementedException();

    public static bool TryConvertFromChecked<TOther>(TOther value, out Meter<T> result) where TOther : INumberBase<TOther> => throw new NotImplementedException();

    public static bool TryConvertFromSaturating<TOther>(TOther value, out Meter<T> result) where TOther : INumberBase<TOther> => throw new NotImplementedException();

    public static bool TryConvertFromTruncating<TOther>(TOther value, out Meter<T> result) where TOther : INumberBase<TOther> => throw new NotImplementedException();

    public static bool TryConvertToChecked<TOther>(Meter<T> value, out TOther result) where TOther : INumberBase<TOther> => throw new NotImplementedException();

    public static bool TryConvertToSaturating<TOther>(Meter<T> value, out TOther result) where TOther : INumberBase<TOther> => throw new NotImplementedException();

    public static bool TryConvertToTruncating<TOther>(Meter<T> value, out TOther result) where TOther : INumberBase<TOther> => throw new NotImplementedException();

    public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out Meter<T> result) => throw new NotImplementedException();

    public static bool TryParse(string? s, NumberStyles style, IFormatProvider? provider, out Meter<T> result) => throw new NotImplementedException();

    public static Meter<T> One { get; set; }
    public static int Radix { get; set; }
    public static Meter<T> Zero { get; set; }
}

