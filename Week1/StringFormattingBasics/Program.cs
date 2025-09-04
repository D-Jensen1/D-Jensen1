// Composite Formatting
string first = "Hello";
string second = "World";
Console.WriteLine("{1} {0}!", first, second);
Console.WriteLine("{0} {0} {0}!", first, second);

Console.WriteLine(new string('=', 30));

// String Interpolation
string first2 = "Hello";
string second2 = "World";
Console.WriteLine($"{first2} {second2}!");
Console.WriteLine($"{second2} {first2}!");
Console.WriteLine($"{first2} {first2} {first2}!");

Console.WriteLine(new string('=', 30));

// Formatting Currency
decimal price = 123.45m;
int discount = 50;
Console.WriteLine($"Price: {price:C} (Save {discount:C})");

Console.WriteLine(new string('=', 30));

// Formatting Numbers
decimal measurement = 123456.78912m;
Console.WriteLine($"Measurement: {measurement:N4} units");

Console.WriteLine(new string('=', 30));

//Formatting Percentages
decimal tax = .36785m;
Console.WriteLine($"Tax rate: {tax:P2}");

Console.WriteLine(new string('=', 30));

//Combined Formats
decimal price2 = 67.55m;
decimal salePrice = 59.99m;

string yourDiscount = String.Format("You saved {0:C2} off the regular {1:C2} price. ", (price2 - salePrice), price2);

yourDiscount += $"A discount of {((price2 - salePrice) / price2):P2}!"; //inserted
Console.WriteLine(yourDiscount);