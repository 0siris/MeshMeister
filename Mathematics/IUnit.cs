using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Mathematics;
//TODO checkout https://github.com/angularsen/UnitsNet

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

public record struct Meter<T>(int Prefix, T Value) : INumber<Meter<T>> where T : struct, INumber<T> {
    public int CompareTo(object? obj) => throw new NotImplementedException();

    public int CompareTo(Meter<T> other) => throw new NotImplementedException();

    public string ToString(string? format, IFormatProvider? formatProvider) => throw new NotImplementedException();

    public bool TryFormat(
        Span<char> destination,
        out int charsWritten,
        ReadOnlySpan<char> format,
        IFormatProvider? provider
    ) =>
        throw new NotImplementedException();

    public static Meter<T> Parse(string s, IFormatProvider? provider) 
        => new(0,T.Parse(s, provider));

    public static bool TryParse(string? s, IFormatProvider? provider, out Meter<T> result) {
        if (T.TryParse(s, provider, out var t)) {
            result = new Meter<T>(0, t);
            return true;
        }

        result = AdditiveIdentity;
        return false;
    }

    public static Meter<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)      
        => new(0,T.Parse(s, provider));

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Meter<T> result) {
        if (T.TryParse(s, provider, out var t)) {
            result = new Meter<T>(0, t);
            return true;
        }

        result = AdditiveIdentity;
        return false;
    }

    public static Meter<T> operator +(Meter<T> left, Meter<T> right) => throw new NotImplementedException();

    public static Meter<T> AdditiveIdentity { get; set; } = new(0, T.Zero);

    public static bool operator >(Meter<T> left, Meter<T> right) => left.ToType() > right.ToType();

    public static bool operator >=(Meter<T> left, Meter<T> right) => left.ToType() >= right.ToType();

    public static bool operator <(Meter<T> left, Meter<T> right) => left.ToType() < right.ToType();

    public static bool operator <=(Meter<T> left, Meter<T> right) => left.ToType() <= right.ToType();

    public static Meter<T> operator --(Meter<T> value) => value with {Value = --value.Value};

    public static Meter<T> operator /(Meter<T> left, Meter<T> right) => throw new NotImplementedException();

    public static Meter<T> operator ++(Meter<T> value) => value with {Value = ++value.Value};

    public static Meter<T> operator %(Meter<T> left, Meter<T> right) => throw new NotImplementedException();

    public static Meter<T> MultiplicativeIdentity { get; set; } =  new(0, T.One);

    public static Meter<T> operator *(Meter<T> left, Meter<T> right) => throw new NotImplementedException();

    public static Meter<T> operator -(Meter<T> left, Meter<T> right) => throw new NotImplementedException();

    public static Meter<T> operator -(Meter<T> value) => throw new NotImplementedException();

    public static Meter<T> operator +(Meter<T> value) => throw new NotImplementedException();

    public static Meter<T> Abs(Meter<T> value) => value with {Value = T.Abs(value.Value)};

    public static bool IsCanonical(Meter<T> value) => T.IsCanonical(value.Value);

    public static bool IsComplexNumber(Meter<T> value) => T.IsComplexNumber(value.Value);

    public static bool IsEvenInteger(Meter<T> value) => T.IsEvenInteger(value.Value);

    public static bool IsFinite(Meter<T> value) => T.IsFinite(value.Value);

    public static bool IsImaginaryNumber(Meter<T> value) => T.IsImaginaryNumber(value.Value);

    public static bool IsInfinity(Meter<T> value) => T.IsInfinity(value.Value);

    public static bool IsInteger(Meter<T> value) => T.IsInteger(value.Value);

    public static bool IsNaN(Meter<T> value) => T.IsNaN(value.Value);

    public static bool IsNegative(Meter<T> value) => T.IsNegative(value.Value);

    public static bool IsNegativeInfinity(Meter<T> value) =>T.IsNegativeInfinity(value.Value);

    public static bool IsNormal(Meter<T> value) => T.IsNormal(value.Value);

    public static bool IsOddInteger(Meter<T> value) => T.IsOddInteger(value.Value);

    public static bool IsPositive(Meter<T> value) => T.IsPositive(value.Value);

    public static bool IsPositiveInfinity(Meter<T> value) => T.IsPositiveInfinity(value.Value);

    public static bool IsRealNumber(Meter<T> value) => T.IsRealNumber(value.Value);

    public static bool IsSubnormal(Meter<T> value) => T.IsSubnormal(value.Value);

    public static bool IsZero(Meter<T> value) => T.IsZero(value.Value);

    public static Meter<T> MaxMagnitude(Meter<T> x, Meter<T> y) => new(0, T.MaxMagnitude(x, y));

    public static implicit operator T(Meter<T> m) => m.ToType();
    public T ToType() => Numerics.Pow10<T>(Prefix) * Value;

    public static Meter<T> MaxMagnitudeNumber(Meter<T> x, Meter<T> y) => new(0, T.MaxMagnitudeNumber(x, y));

    public static Meter<T> MinMagnitude(Meter<T> x, Meter<T> y) => new(0, T.MinMagnitude(x, y));

    public static Meter<T> MinMagnitudeNumber(Meter<T> x, Meter<T> y) => new(0, T.MinMagnitudeNumber(x, y));

    public static Meter<T> Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider) 
        => new(0, T.Parse(s,style,provider));

    public static Meter<T> Parse(string s, NumberStyles style, IFormatProvider? provider)
        => new(0, T.Parse(s,style,provider));

    public static bool TryConvertFromChecked<TOther>(TOther value, out Meter<T> result)
        where TOther : INumberBase<TOther> {
        var tryConvertFromChecked = T.TryConvertFromChecked(value, out var t);
        result = new Meter<T>(0, t);
        return tryConvertFromChecked;
    }

    public static bool TryConvertFromSaturating<TOther>(TOther value, out Meter<T> result)
        where TOther : INumberBase<TOther> {
        var b = T.TryConvertFromSaturating(value, out var t);
        result = new Meter<T>(0, t);
        return b;
    }

    public static bool TryConvertFromTruncating<TOther>(TOther value, out Meter<T> result)
        where TOther : INumberBase<TOther>  {
        var b = T.TryConvertFromTruncating(value, out var t);
        result = new Meter<T>(0, t);
        return b;
    }

    public static bool TryConvertToChecked<TOther>(Meter<T> value, [MaybeNullWhen(false)] out TOther result)
        where TOther : INumberBase<TOther> =>
            T.TryConvertToChecked(value.ToType(), out result);

    public static bool TryConvertToSaturating<TOther>(Meter<T> value, [MaybeNullWhen(false)] out TOther result)
        where TOther : INumberBase<TOther> =>
        T.TryConvertToSaturating(value.ToType(), out result);

    public static bool TryConvertToTruncating<TOther>(Meter<T> value,[MaybeNullWhen(false)] out TOther result)
        where TOther : INumberBase<TOther> =>
            T.TryConvertToTruncating(value.ToType(), out result);

    public static bool TryParse(
        ReadOnlySpan<char> s,
        NumberStyles style,
        IFormatProvider? provider,
        out Meter<T> result
    ) {
        if (T.TryParse(s, style, provider, out var t)) {
            result = new Meter<T>(0, t);
            return true;
        }

        result = new Meter<T>(0, T.Zero);
        return false;
    }

    public static bool TryParse(string? s, NumberStyles style, IFormatProvider? provider, out Meter<T> result) {
        if (T.TryParse(s, style, provider, out var t)) {
            result = new Meter<T>(0, t);
            return true;
        }

        result = new Meter<T>(0, T.Zero);
        return false;
    }

    public static Meter<T> One { get; set; } = new(0, T.One);
    public static int Radix { get; set; } = T.Radix;
    public static Meter<T> Zero { get; set; } = new(0, T.Zero);
}

