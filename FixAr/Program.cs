// See https://aka.ms/new-console-template for more information

using FixAr;

Console.WriteLine("----[ FixAr Tests start ]----");

Unit a = 20f;
Unit b = 2.5f;
var result = a / b;

Console.WriteLine(a.Debug());
Console.WriteLine(b.Debug());
Console.WriteLine(result);
Console.WriteLine(result.Debug());