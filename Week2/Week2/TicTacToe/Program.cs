// Create 2-dim array, 3x3 char
// Each slot should be initialized to nullable char - char?


using System.ComponentModel;

char?[,] board = GetBoard();

// Display the board

DisplayBoard(board);

/*  Replace 2nd space in each block with char
-------------------
|  O  |  X  |     |
-------------------
|  O  |     |     |
-------------------
|     |     |     |
-------------------
*/

// O starts first, collect position using row, col = 1,0 and x 0,0
char nextHand = 'O';
while (PlaceNextHand(board,nextHand)) // placeNextHand promt user for the position after collecting position
// reprint the board
{
    // flip nextHand from O=>X X=>O
    DisplayBoard(board);
    nextHand = nextHand == 'O' ? 'X' : 'O';
}

Console.WriteLine($"Game ends with {DetermineOutcome(board)}");


string outcome = DetermineOutcome(board); 

char?[,] GetBoard() => new char?[3,3];

void DisplayBoard(char?[,] board)
{
    string horizontalBorder = new string('-', 20);
    Console.WriteLine(horizontalBorder);

    for (int row = 0; row < board.GetLength(0); row++)
    {
        Console.Write('|');
        for (int col = 0; col < board.GetLength(1); col++)
        {
            char? piece;
            if (board[row, col] is not null)
                piece = board[row, col];
            else
                piece = '_';
            Console.Write($"  {piece}  |");
        }
        Console.WriteLine($"\n{horizontalBorder}");
    }
}

bool PlaceNextHand(char?[,] board, char nextHand)
{
    //bool piecePlaced = false;
    while (true)
    {
        Console.WriteLine($"{nextHand} player, please place your next piece with: row col. e.g. 0 1 for row 0 col 1.");
        string input = Console.ReadLine();
        int row = int.Parse(input.Split(' ')[0]);
        int col = int.Parse(input.Split(' ')[1]);

        if (board[row, col] is null)
        {
            board[row, col] = nextHand;
            break;
        }
        else
        {
            Console.WriteLine($"Position {row} {col} is already occupied by {board[row, col]}");
            continue;
        }
    } ;
    return BoardHasSpace(board) && (DetermineOutcome(board) == "Continue");
}

bool BoardHasSpace(char?[,] board)
{
    /*
        for (int row = 0; row < board.GetLength(0); row++)
        {
            for (int col = 0; col < board.GetLength(1); col++)
            {
                if (board[row, col] == null) return true;
            }
        }
        return false;
    */

    foreach (char? piece in board) //this will loop across ALL items in as if it were a 1-D array
    {
        if (!piece.HasValue) return true;
    }
    return false;
}

string DetermineOutcome(char?[,] board)
{
    //check rows
    //check col
    //check diag
    // O wins, X wins, Draw, Continue

    /*
    int[] rows = new int[board[rows,col];
    int[] cols = new int[length];
    int[] diag = new int[2];
    */

    string[] lines = new string[8];

    lines[0] = $"{board[0, 0]}{board[0, 1]}{board[0, 2]}"; //row 0
    lines[1] = $"{board[1, 0]}{board[1, 1]}{board[1, 2]}"; //row 1
    lines[2] = $"{board[2, 0]}{board[2, 1]}{board[2, 2]}"; //row 2


    /*
        lines[3] = $"{board[0,0]}{board[1,0]}{board[2,0]}"; //col 0
        lines[4] = $"{board[0,1]}{board[1,1]}{board[2,1]}"; //col 1
        lines[5] = $"{board[0,2]}{board[1,2]}{board[2,2]}"; //col 2
    */
    for (int i = 0; i < board.GetLength(1); i++)
    {
        lines[i + 3] = $"{board[0, i]}{board[1, i]}{board[2, i]}";
    }

    lines[6] = $"{board[0, 0]}{board[1, 1]}{board[2, 2]}"; //diag \
    lines[7] = $"{board[0, 2]}{board[1, 1]}{board[2, 0]}"; //diag /

    foreach (var item in lines)
    {
        if (item == "OOO")
            return "O wins";

        if (item == "XXX")
            return "X wins";
    }

    //no wins
    if (BoardHasSpace(board))
        return "Continue";
    else
        return "Draw";
}
