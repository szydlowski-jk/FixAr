/*
 * Comment out that line if you don't want to use Int64 conversion when multiplying
 */ 
#define FIXAR_MULTIPLY_USING_INT64

/*
 * Comment out that line if you don't want to use Int64 conversion when dividing
 */ 
#define FIXAR_DIVIDE_USING_INT64

namespace FixAr;

public struct Unit
{
    /*
     * If FIXAR_DIVIDE_USING_INT64 is commented out division
     * will fail if FIX_POINT_UNIT > 65535
     */
    // private static readonly int FIX_POINT_UNIT = 512;
    private static readonly int FIX_POINT_UNIT = 65535;
    // private static readonly int FIX_POINT_UNIT = 10000;
    public static readonly int MAX_FRACTION_VALUE = FIX_POINT_UNIT;
    public static readonly int MAX_INTEGER_VALUE = Int32.MaxValue / FIX_POINT_UNIT;
    public static readonly int MIN_INTEGER_VALUE = Int32.MinValue / FIX_POINT_UNIT;

    public static readonly Unit PI = Math.PI;
    public static readonly Unit DEG_TO_RAD = 180 / Math.PI;
    private Int32 _Value;

    #region Conversions

    public Unit(Unit other)
    {
        _Value = other._Value;
    }

    public static implicit operator Unit(int value)
    {
        return new Unit {_Value = value * FIX_POINT_UNIT};
    }

    public static implicit operator Unit(float value)
    {
        return new Unit {_Value = (int) (value * FIX_POINT_UNIT)};
    }

    public static implicit operator Unit(double value)
    {
        return new Unit {_Value = (int) (value * FIX_POINT_UNIT)};
    }

    public static explicit operator int(Unit a)
    {
        return a._Value / FIX_POINT_UNIT;
    }

    public static explicit operator float(Unit a)
    {
        return a._Value / (float) FIX_POINT_UNIT;
    }
    
    public int ToInt()
    {
        return (int) this;
    }

    public float ToFloat()
    {
        return (float) this;
    }
    
    #endregion

    #region Operations

    #region Basic Operations

    public static Unit operator +(Unit a)
    {
        return a;
    }

    public static Unit operator +(Unit a, Unit b)
    {
        return new Unit {_Value = a._Value + b._Value};
    }

    public static Unit operator -(Unit a)
    {
        return new Unit {_Value = -a._Value};
    }

    public static Unit operator -(Unit a, Unit b)
    {
        return new Unit {_Value = a._Value - b._Value};
    }

    public static Unit operator *(Unit a, Unit b)
    {

#if FIXAR_MULTIPLY_USING_INT64
        return new Unit {_Value = (Int32) ((Int64) a._Value * b._Value / FIX_POINT_UNIT)};
#else // FIXAR_MULTIPLY_USING_INT64
        Int32 intPartA = a._Value / FIX_POINT_UNIT;
        Int32 intPartB = b._Value / FIX_POINT_UNIT;
        Int32 fracPartA = a._Value % FIX_POINT_UNIT;
        Int32 fracPartB = b._Value % FIX_POINT_UNIT;
        Console.WriteLine($"{intPartA}.{fracPartA} * {intPartB}.{fracPartB}");
        
        Int32 result = 0;
        result += (intPartA * intPartB) * FIX_POINT_UNIT;
        Console.WriteLine(result);
        result += (intPartA * fracPartB);
        Console.WriteLine(result);
        result += (fracPartA * intPartB);
        Console.WriteLine(result);
        result += (fracPartA * fracPartB) / FIX_POINT_UNIT;
        Console.WriteLine(result);
        return new Unit { _Value = result };
#endif // FIXAR_MULTIPLY_USING_INT64
    }

    public static Unit operator /(Unit a, Unit b)
    {
#if FIXAR_DIVIDE_USING_INT64
        return new Unit {_Value = (Int32) ((Int64) a._Value * FIX_POINT_UNIT / b._Value)};
#else // FIXAR_DIVIDE_USING_INT64
        // This division does not use long but in exchange for loss of precision
        UInt32 reciprocal = 1;
        Console.WriteLine($"{reciprocal.ToString()} | {reciprocal.ToString("X")}");
        reciprocal *= (UInt32)(FIX_POINT_UNIT * FIX_POINT_UNIT);
        // reciprocal <<= 31; // loss of precision comes, it should be 1 << 32 but it would use long
        Console.WriteLine($"{reciprocal.ToString()} | {reciprocal.ToString("X")}");
        reciprocal = (UInt32) (reciprocal / b._Value);
        Console.WriteLine($"{reciprocal.ToString()} | {reciprocal.ToString("X")}");

        Int32 result = (Int32)(a._Value * reciprocal / FIX_POINT_UNIT);
        Console.WriteLine($"{result.ToString()} | {result.ToString("X")}");

        return new Unit {_Value = (result << 0)};
#endif // FIXAR_DIVIDE_USING_INT64
    }

    public static Unit operator %(Unit a, Unit b)
    {
        return new Unit {_Value = a._Value % b._Value};
    }

    public static Unit Sign(Unit a)
    {
        return new Unit(Int32.Sign(a._Value));
    }
    
    public static Unit Abs(Unit a)
    {
        return new Unit {_Value = Math.Abs(a._Value)};
    }

    public static Unit Min(Unit a, Unit b)
    {
        return new Unit {_Value = Math.Min(a._Value, b._Value)};
    }

    public static Unit Max(Unit a, Unit b)
    {
        return new Unit {_Value = Math.Max(a._Value, b._Value)};
    }
    
    public static Unit Floor(Unit a)
    {
        return new Unit {_Value = a._Value / FIX_POINT_UNIT * FIX_POINT_UNIT};
    }

    public static Unit Ceiling(Unit a)
    {
        return FractionalPart(a) == 0 ? a : Floor(a) + 1; 
        //
        // if (FractionalPart(a) == 0)
        // {
        //     return a;
        // }
        // else
        // {
        //     return Floor(a) + 1;
        // }
    }
    
    public static Unit IntegerPart(Unit a)
    {
        return Floor(a);
    }

    public static Unit FractionalPart(Unit a)
    {
        return a - IntegerPart(a);
    }
    
    #endregion //Basic Operations 

    #region Comparisons

    public static bool operator ==(Unit a, Unit b)
    {
        return a._Value == b._Value;
    }

    public static bool operator !=(Unit a, Unit b)
    {
        return a._Value != b._Value;
    }

    public static bool operator <(Unit a, Unit b)
    {
        return a._Value < b._Value;
    }

    public static bool operator >(Unit a, Unit b)
    {
        return a._Value > b._Value;
    }

    public static bool operator <=(Unit a, Unit b)
    {
        return a._Value <= b._Value;
    }

    public static bool operator >=(Unit a, Unit b)
    {
        return a._Value >= b._Value;
    }

    #endregion // Comparisons

    #region Trigonometry

    public static Unit Sin(Unit radians)
    {
        /*
         * Clear Bhaskara I approximation
         *
         *             16x(PI-x)
         * sin x = ------------------
         *         5*PI*PI - 4x(PI-x)
         */
        Unit x = radians % (PI*2);
        Int32 sign = 1;

        if (x < 0)
        {
            x *= -1;
            sign = -1;
        }
        
        if (x > PI)
        {
            x -= PI;
            sign *= -1;
        }
        
        Unit PIx = (PI - x);
        Unit top = 16 * x * PIx;
        Unit bot = 5 * PI * PI - 4 * x * PIx;

        return sign * (top / bot);
    }

    public static Unit Cos(Unit radians)
    {
        return Sin(radians + PI / 2);
    }

    public static Unit Tan(Unit radians)
    {
        Unit cos = Cos(radians);
        if (cos == 0)
        {
            return new Unit {_Value = Int32.MaxValue};
        }
        return Sin(radians) / cos;
    }
    
    // Polynomial values for ATan
    private static readonly Unit ATAN_PI_4 = PI / 4;
    private static readonly Unit ATAN_A = 0.0776509570923569;
    private static readonly Unit ATAN_B = -0.287434475393028;
    private static readonly Unit ATAN_C = (ATAN_PI_4 - ATAN_A - ATAN_B);
    
    public static Unit ATan(Unit radians)
    {
        if (Abs(radians) > 1)
        {
            return Sign(radians) * (PI / 2 - ATan(1 / Abs(radians)));
        }
        
        // Fast polynomial approximation
        Unit radians2 = radians * radians;
        return ((ATAN_A * radians2 + ATAN_B) * radians2 + ATAN_C) * radians;
    }
    
    public static Unit ATan2(Unit y, Unit x)
    {
        if (x > 0)
        {
            return ATan(y / x);
        }
        else if (x < 0)
        {
            if (y >= 0)
            {
                return ATan(y / x) + PI;
            }
            else
            {
                return ATan(y / x) - PI;
            }
        }
        else
        {
            if (y > 0)
            {
                return PI / 2;
            }
            else if (y < 0)
            {
                return -(PI / 2);
            }
            else
            {
                return Unit.MAX_INTEGER_VALUE;
            }
        }
    }    
    #endregion // Trigonometry

    #endregion // Operations

    #region Helpers
    public override string ToString()
    {
        return $"{((float)_Value / FIX_POINT_UNIT)}";
    }

    public string ToStringBinary()
    {
        return Convert.ToString(_Value, 2);
    }

    public string ToStringHex()
    {
        return _Value.ToString("X");
    }
    #endregion // Helpers

    #region Debug

    public string Debug()
    {
        return $"Unit DBG: {this.ToString()} | Value = {_Value} | {ToStringHex()} | {ToStringBinary()}";
    }

    #endregion // D e b u g
}

public struct UnitVec2
{
    public Unit x;
    public Unit y;
}

public struct UnitVec3
{
    public Unit x;
    public Unit y;
    public Unit z;
}
