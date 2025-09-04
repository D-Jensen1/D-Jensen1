string message = "Find what is (inside the parentheses)";

int openingPosition = message.IndexOf('(') + 1;
int closingPosition = message.IndexOf(')');

// Console.WriteLine(openingPosition);
// Console.WriteLine(closingPosition);

int length = closingPosition - openingPosition;
Console.WriteLine(message.Substring(openingPosition, length));

//BREAK
string message1 = "What is the value <span>between the tags</span>?";

const string openSpan = "<span>";
const string closeSpan = "</span>";

int openingPosition1 = message1.IndexOf(openSpan);
int closingPosition1 = message1.IndexOf(closeSpan);

openingPosition1 += openSpan.Length;
int length1 = Math.Abs(closingPosition1 - openingPosition1);
Console.WriteLine(message1.Substring(openingPosition1, length1));