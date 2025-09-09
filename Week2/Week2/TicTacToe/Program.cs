using TicTacToe;
// Create 2-dim array, 3x3 char
// Each slot should be initialized to nullable char - char?


using System.ComponentModel;

char?[,] board = Game.GetBoard();

// Display the board

Game.DisplayBoard(board);

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
while (Game.PlaceNextHand(board,nextHand)) // placeNextHand promt user for the position after collecting position
// reprint the board
{
    // flip nextHand from O=>X X=>O
    Game.DisplayBoard(board);
    nextHand = nextHand == 'O' ? 'X' : 'O';
}
Game.DisplayBoard(board);
Console.WriteLine($"Game ends with {Game.DetermineOutcome(board)}");

string outcome = Game.DetermineOutcome(board); 
