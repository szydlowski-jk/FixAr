// See https://aka.ms/new-console-template for more information

using FixAr;

Console.WriteLine("----[ FixAr Tests start ]----");
Console.WriteLine($"MAX FRAC {Unit.MAX_FRACTION_VALUE} | MAX INT {Unit.MAX_INTEGER_VALUE}");

Unit a = 9.8f;
Unit b = 8;
Unit c = b;
var result = 0.5 * a * 5*5;
Console.WriteLine(a.Debug());
Console.WriteLine(b.Debug());
Console.WriteLine(result.Debug());
