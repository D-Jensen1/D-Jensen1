string[] values = { "12.3", "45", "ABC", "11", "DEF" };
string message = "";
decimal total = 0;

for (int i = 0; i < values.Length; i++)
{

    if (decimal.TryParse(values[i], out decimal result))
    { total += result; }
    else
    { message += values[i]; }
}

Console.WriteLine($"Message: {message}");
Console.WriteLine($"Total: {total}");