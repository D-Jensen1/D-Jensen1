// See https://aka.ms/new-console-template for more information
Console.WriteLine("Signed integral types:");

Console.WriteLine($"sbyte   :   {sbyte.MinValue, 30:N0} to {sbyte.MaxValue,30:N0}");
Console.WriteLine($"short   :   {short.MinValue, 30:N0} to {short.MaxValue, 30:N0}");
Console.WriteLine($"int     :   {int.MinValue, 30:N0} to {int.MaxValue, 30:N0}");
Console.WriteLine($"long    :   {long.MinValue, 30:N0} to {long.MaxValue, 30:N0}");

Console.WriteLine();
Console.WriteLine(new String('=',80));
Console.WriteLine();

Console.WriteLine("Unsigned integral types:");

Console.WriteLine($"byte    :   {byte.MinValue, 30:N0} to {byte.MaxValue, 30:N0}");
Console.WriteLine($"ushort  :   {ushort.MinValue, 30:N0} to {ushort.MaxValue, 30:N0}");
Console.WriteLine($"uint    :   {uint.MinValue, 30:N0} to {uint.MaxValue, 30:N0}");
Console.WriteLine($"ulong   :   {ulong.MinValue, 30:N0} to {ulong.MaxValue, 30:N0}");