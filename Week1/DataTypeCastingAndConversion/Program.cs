// Combine int and string as a string
int first = 2;
string second = "4";
string result = first + second;
Console.WriteLine(result);
Console.WriteLine(new string('=', 30));

// Implicitly convert int to decimal
int myInt = 3;
Console.WriteLine($"int: {myInt}");

decimal myDecimal = myInt;
Console.WriteLine($"decimal: {myDecimal}");
Console.WriteLine(new string('=', 30));

// Casting decimal to int
decimal myDecimal2 = 3.14m;
Console.WriteLine($"decimal: {myDecimal2}");

int myInt2 = (int)myDecimal;
Console.WriteLine($"int: {myInt2}");
Console.WriteLine(new string('=', 30));

// Casting decimal to float
decimal myDecimal3 = 1.23456789m;
float myFloat = (float)myDecimal3;

Console.WriteLine($"Decimal: {myDecimal}");
Console.WriteLine($"Float  : {myFloat}");
Console.WriteLine(new string('=', 30));

// Convert int to string using .ToString()
int firstNum = 5;
int secondNum = 7;
string message = firstNum.ToString() + secondNum.ToString();
Console.WriteLine(message);
Console.WriteLine(new string('=', 30));

// Convert string to int using int.Parse()
string x = "5";
string y = "7";
int sum = int.Parse(x) + int.Parse(y);
Console.WriteLine(sum);
Console.WriteLine(new string('=', 30));

//Convert string to int using Convert class
string value1 = "5";
string value2 = "7";
int results = Convert.ToInt32(value1) * Convert.ToInt32(value2);
Console.WriteLine(results);
Console.WriteLine(new string('=', 30));

// Comparing casting and converting decimal to int
int value3 = (int)1.5m; // casting truncates
Console.WriteLine(value3);

int value4 = Convert.ToInt32(1.5m); // converting rounds up
Console.WriteLine(value4);