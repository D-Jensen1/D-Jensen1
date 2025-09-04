string customerName = "Ms. Barros";

string currentProduct = "Magic Yield";
int currentShares = 2975000;
decimal currentReturn = 0.1275m;
decimal currentProfit = 55000000.0m;

string newProduct = "Glorious Future";
decimal newReturn = 0.13125m;
decimal newProfit = 63000000.0m;

string message = $"""
    Dear {customerName},
    As a customer of our {currentProduct} offering we are excited to tell you about a new financial product that would dramatically increase your return.
    Currently, you own {currentShares:N} shares at a return of {currentReturn:P2}.
    Our new product, {newProduct} offers a return of {newReturn:P2}. Given your current volume, your potential profit would be {newProfit:C}.


    """;
Console.WriteLine(message);

Console.WriteLine("Here's a quick comparison:\n");

string comparisonMessage = "";

Console.WriteLine($"{currentProduct}\t\t{currentReturn:P2}\t{currentProfit:C}");
Console.WriteLine($"{newProduct}\t\t{newReturn:P2}\t{newProfit:C}");

Console.WriteLine(comparisonMessage);