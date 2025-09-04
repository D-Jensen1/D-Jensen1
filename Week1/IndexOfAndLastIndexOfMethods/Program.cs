string message = "hello there!";

int first_h = message.IndexOf('h');
int last_h = message.LastIndexOf('h');

Console.WriteLine($"For the message: '{message}', the first 'h' is at position {first_h} and the last 'h' is at position {last_h}.");

Console.WriteLine(new string('=', 30));

//Retrieve the last occurrence of a sub string
string message1 = "(What if) I am (only interested) in the last (set of parentheses)?";
int openingPosition = message1.LastIndexOf('(');

openingPosition += 1;
int closingPosition = message1.LastIndexOf(')');
int length = closingPosition - openingPosition;
Console.WriteLine(message1.Substring(openingPosition, length));

Console.WriteLine(new string('=', 30));

//Retrieve all instances of substrings inside parentheses
string message2 = "(What if) there are (more than) one (set of parentheses)?";
while (true)
{
    int openingPosition2 = message2.IndexOf('(');
    if (openingPosition2 == -1) break;

    openingPosition2 += 1;
    int closingPosition2 = message2.IndexOf(')');
    int length2 = closingPosition2 - openingPosition2;
    Console.WriteLine(message2.Substring(openingPosition2, length2));

    // Note the overload of the Substring to return only the remaining 
    // unprocessed message:
    message2 = message2.Substring(closingPosition2 + 1);
}

Console.WriteLine(new string('=', 30));

//Working with different types of symbols
string message3 = "(What if) I have [different symbols] but every {open symbol} needs a [matching closing symbol]?";

// The IndexOfAny() helper method requires a char array of characters. 
// You want to look for:

char[] openSymbols = { '[', '{', '(' };

// You'll use a slightly different technique for iterating through 
// the characters in the string. This time, use the closing 
// position of the previous iteration as the starting index for the 
//next open symbol. So, you need to initialize the closingPosition3 
// variable to zero:

int closingPosition3 = 0;

while (true)
{
    int openingPosition3 = message3.IndexOfAny(openSymbols, closingPosition3);

    if (openingPosition3 == -1) break;

    string currentSymbol = message3.Substring(openingPosition3, 1);

    // Now  find the matching closing symbol
    char matchingSymbol = ' ';

    switch (currentSymbol)
    {
        case "[":
            matchingSymbol = ']';
            break;
        case "{":
            matchingSymbol = '}';
            break;
        case "(":
            matchingSymbol = ')';
            break;
    }

    // To find the closingPosition3, use an overload of the IndexOf method to specify 
    // that the search for the matchingSymbol should start at the openingPosition3 in the string. 

    openingPosition3 += 1;
    closingPosition3 = message3.IndexOf(matchingSymbol, openingPosition3);

    // Finally, use the techniques you've already learned to display the sub-string:

    int length3 = closingPosition3 - openingPosition3;
    Console.WriteLine(message3.Substring(openingPosition3, length3));
}