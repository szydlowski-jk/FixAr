// See https://aka.ms/new-console-template for more information

using FixAr;

Console.WriteLine("----[ FixAr Tests start ]----");
Console.WriteLine($"FRAC Range 0 - {Unit.MAX_FRACTION_VALUE} | INT Range {Unit.MIN_INTEGER_VALUE} - {Unit.MAX_INTEGER_VALUE}");
Console.WriteLine($"PI = {Unit.PI} | DEG_TO_RAD = {Unit.DEG_TO_RAD}");
Console.WriteLine("-----------------------------");


Unit a = 9.8f;
Unit b = 8;
Unit c = b;
var result = 0.5 * a * 5*5;
Console.WriteLine(a.Debug());
Console.WriteLine(b.Debug());
Console.WriteLine(result.Debug());

int x = (int)a;
int y = a.ToInt();
float fa = (float) a;
float fb = a.ToFloat();

Console.WriteLine($"{a} {x} {y} {fa} {fb}");

// TestFreeFall(60);
// TestComparisons();
// TestModulo();
// TestCirclePoints(3);
// TestTan(12);
// TestAtan2();
TestRounding();

void TestRounding()
{
    Console.WriteLine("----[ Rounding test ]----");
    Unit x = 123.456;
    Unit y = -123.456;
    Unit z = 123;
    
    Console.WriteLine($"{x} => {Unit.IntegerPart(x)} + {Unit.FractionalPart(x)} | {Unit.Floor(x)} | {Unit.Ceiling(x)}");
    Console.WriteLine($"{y} => {Unit.IntegerPart(y)} + {Unit.FractionalPart(y)} | {Unit.Floor(y)} | {Unit.Ceiling(y)}");
    Console.WriteLine($"{z} => {Unit.IntegerPart(z)} + {Unit.FractionalPart(z)} | {Unit.Floor(z)} | {Unit.Ceiling(z)}");
    Console.WriteLine($"Min {Unit.Min(x, z)} Max {Unit.Max(x, z)}");
    Console.WriteLine($"Min {Unit.Min(y, z)} Max {Unit.Max(y, z)}");
}

void TestAtan2()
{
    float x = -900f;
    float y = 600f;
    
    Unit ux = x;
    Unit uy = y;

    Console.WriteLine("----[ Atan2 test ]----");
    Console.WriteLine($"{Math.Atan2(y, x) } | {Unit.ATan2(uy, ux)}");
}

void TestTan(int points)
{
    Console.WriteLine("----[ Tan / Atan test ]----");
    Console.WriteLine($"Points: {points}, angle: {360.0 / points}, Unit angle: {(Unit)360 / points}");
    float degToRad = 57.2958f;
    
    float fAngle = 360.0f / points;
    Unit uAngle = (Unit) 360 / points;

    float fActualAngle = fAngle / degToRad;
    Unit uActualAngle = uAngle / degToRad;
    
    for (int i = -points*2; i < points*2; i++)
    {
        Console.WriteLine($"[{i:00}] [{fActualAngle * i}] x: {MathF.Tan(fActualAngle * i):F8} \ty: {MathF.Atan(fActualAngle * i):F8} \t\t[{(Unit)fActualAngle * i}] x: {Unit.Tan(fActualAngle * i)} \ty: {Unit.ATan(fActualAngle * i)}");
        // Console.WriteLine($"[{i:00}] [{fActualAngle * i}] x: {MathF.Tan(fActualAngle * i):F8} \ty: {MathF.Atan(fActualAngle * i):F8} \t\t[{(Unit) fActualAngle * i}]");// x: {Unit.Tan(fActualAngle * i)} \ty: {Unit.ATan(fActualAngle * i)}");
    }
    
}

void TestCirclePoints(int points)
{
    Console.WriteLine("----[ Circle points test ]----");
    Console.WriteLine($"Points: {points}, angle: {360.0 / points}, Unit angle: {(Unit)360 / points}");

    float degToRad = 57.2958f;
    
    float fAngle = 360.0f / points;
    Unit uAngle = (Unit) 360 / points;

    float fActualAngle = fAngle / degToRad;
    Unit uActualAngle = uAngle / degToRad;
    
    for (int i = -points*2; i < points*2; i++)
    {
        Console.WriteLine($"[{i:00}] [{fActualAngle * i}] x: {MathF.Sin(fActualAngle * i):F8} \ty: {MathF.Cos(fActualAngle * i):F8} \t\t[{(Unit)fActualAngle * i}] x: {Unit.Sin(fActualAngle * i)} \ty: {Unit.Cos(fActualAngle * i)}");
    }
}

void TestComparisons()
{
    Console.WriteLine("----[ Comparison test ]----");
    
    Unit a = 0;
    
    Console.WriteLine($"{(Unit)0.1} > {a} = {0.1 > a}");
    Console.WriteLine($"{-(Unit)0.1} > {a} = {-0.1 > a}");
    Console.WriteLine($"{(Unit)0.1} < {a} = {0.1 < a}");
    Console.WriteLine($"{-(Unit)0.1} < {a} = {-0.1 < a}");
    Console.WriteLine($"0 > {a} = {0 > a}");
    Console.WriteLine($"-0 > {a} = {-0 > a}");
    a = 3.14;
    Console.WriteLine($"{(Unit)3.141} > {a} = {3.141 > a}");
    Console.WriteLine($"{(Unit)3.1399} > {a} = {3.1399 > a}");
    
}

void TestModulo()
{
    Console.WriteLine("----[ Modulo test ]----");
    int a = 7;
    int b = 3;
    Console.WriteLine($"Int: {a} % {b}={a%b} \t Unit: {(Unit)a} % {(Unit)b}={(Unit)a%b}");
    a = 3;
    b = 7;
    Console.WriteLine($"Int: {a} % {b}={a%b} \t Unit: {(Unit)a} % {(Unit)b}={(Unit)a%b}");
    a = 11;
    b = 3;
    Console.WriteLine($"Int: {a} % {b}={a%b} \t Unit: {(Unit)a} % {(Unit)b}={(Unit)a%b}");
    a = 28;
    b = 5;
    Console.WriteLine($"Int: {a} % {b}={a%b} \t Unit: {(Unit)a} % {(Unit)b}={(Unit)a%b}");
    float fa = 17.8f;
    float fb = 4.0f;
    Console.WriteLine($"Int: {fa} % {fb}={fa%fb} \t Unit: {(Unit)fa} % {(Unit)fb}={(Unit)fa%fb}");
    fa = 17.8f;
    fb = 4.1f;
    Console.WriteLine($"Int: {fa} % {fb}={fa%fb} \t Unit: {(Unit)fa} % {(Unit)fb}={(Unit)fa%fb}");
    fa = -16.3f;
    fb = 4.1f;
    Console.WriteLine($"Int: {fa} % {fb}={fa%fb} \t Unit: {(Unit)fa} % {(Unit)fb}={(Unit)fa%fb}");

}

void TestFreeFall(int seconds)
{
    Console.WriteLine("----[ Free fall test ]----");
    Console.WriteLine($"Result float       : {0.5 * 9.8 * seconds * seconds}");
    Console.WriteLine($"Result FA Unit     : {(Unit)0.5 * 9.8 * seconds * seconds}");

    Unit vel = 0;
    Unit pos = 0;
    for (int i = 0; i < seconds; i++)
    {
        vel += 9.8;
        pos += vel - (9.8 / 2);
//        Console.WriteLine($"{vel} | {pos}");
    }

    Console.WriteLine($"Result FA Unit loop: {pos}");
    // Console.WriteLine($"V={vel}, D={pos}");
}
