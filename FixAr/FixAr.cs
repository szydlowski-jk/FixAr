﻿// Comment out that line if you don't want to use Int64 conversion when multiplying 
#define FIXAR_MULTIPLY_USING_INT64
// Comment out that line if you don't want to use Int64 conversion when dividing 
#define FIXAR_DIVIDE_USING_INT64

namespace FixAr;

public struct Unit
{
    private static readonly int FIX_POINT_UNIT = 512;
    private Int32 _Value;

    #region Conversions
    public static implicit operator Unit(int value)
    {
        return new Unit {_Value = value * FIX_POINT_UNIT};
    }

    public static implicit operator Unit(float value)
    {
        return new Unit {_Value = (int)(value * FIX_POINT_UNIT)};
    }
    #endregion
    
    #region Operations
    public static Unit operator +(Unit a, Unit b)
    {
        return new Unit {_Value = a._Value + b._Value};
    }

    public static Unit operator -(Unit a, Unit b)
    {
        return new Unit {_Value = a._Value - b._Value};
    }

    public static Unit operator *(Unit a, Unit b)
    {

#if FIXAR_MULTIPLY_USING_INT64
        return new Unit { _Value = (Int32) ((Int64) a._Value * b._Value / FIX_POINT_UNIT)};
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
        return new Unit {_Value = (Int32)((Int64)a._Value * FIX_POINT_UNIT / b._Value)};
#else // FIXAR_DIVIDE_USING_INT64         
#endif // FIXAR_DIVIDE_USING_INT64
    }
    
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