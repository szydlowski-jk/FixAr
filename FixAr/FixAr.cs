#define SinV2

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
    public static readonly int MAX_FRACTION_VALUE = FIX_POINT_UNIT;
    public static readonly int MAX_INTEGER_VALUE = Int32.MaxValue / FIX_POINT_UNIT;
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
#if SinV2
        /*
         * Clear Bhaskara I approximation
         *
         *             16x(PI-x)
         * sin x = ------------------
         *         5*PI*PI - 4x(PI-x)
         */
        Unit PI = Math.PI;
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

#else // #if SinV2
        // For now using tinyphysicsengine implementation, needs tests when done
        Int32 sign = 1;
    
        if (radians < 0)
        {
            radians *= -1;
            sign = -1;
        }
    
        float x = 1.2f;
        x = x % 2;
    
        radians %= FIX_POINT_UNIT;

        if (radians > FIX_POINT_UNIT / 2)
        {
            radians -= FIX_POINT_UNIT / 2;
            sign *= -1;
        }

        int tmp = ((Unit)FIX_POINT_UNIT - 2 * radians)._Value;
        Unit UPI2 = 9.8696044 * FIX_POINT_UNIT;

        int PI2 = UPI2._Value;
        int rad = radians._Value;
        
        return new Unit
            {
            _Value = sign * ((( 32 * rad * PI2 ) / FIX_POINT_UNIT) * tmp) /
                     ((PI2 * (5 * FIX_POINT_UNIT - (8 * rad * tmp) /
                             FIX_POINT_UNIT)) / FIX_POINT_UNIT)
        };
#endif // if !SinV2
    }

    public static Unit Cos(Unit radians)
    {
        return Sin(radians + Math.PI / 2);
    }

    #endregion // Trigonometry

    #endregion

    #region Helpers
    public override string ToString()
    {
        return $"{((float)_Value / FIX_POINT_UNIT)}";
    }

    public string Debug()
    {
        return $"Unit DBG: {this.ToString()} | Value = {_Value} | {ToHex()} | {ToBinary()}";
    }

    public string ToBinary()
    {
        return Convert.ToString(_Value, 2);
    }

    public string ToHex()
    {
        return _Value.ToString("X");
    }
    #endregion
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
