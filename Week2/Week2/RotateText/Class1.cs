using Spectre.Console;
using System.Drawing;

namespace RotateTextLibrary
{
    public class IsometricRotation
    {
        public static void AlterrnateLetters(string word)
        {
            //string[] word
            var fonts = new[] {
                FigletFont.Load("isometric1.flf"),
                FigletFont.Load("isometric2.flf"),
                FigletFont.Load("isometric3.flf"),
                FigletFont.Load("isometric4.flf")
            };

/*
            foreach (char letter in word)
            {
                AnsiConsole.Write(
                new FigletText(font, letter.ToString())
                    .LeftJustified()
                    .Color(Spectre.Console.Color.Red));
            }
*/
/*
            AnsiConsole.Write(
                new FigletText(font, "Random")
                    .LeftJustified()
                    .Color(Color.Red));
            AnsiConsole.Write(
                new FigletText("Card")
                    .Centered()
                    .Color(Color.Gold1));
            AnsiConsole.Write(
                new FigletText("Dealer")
                    .RightJustified()
                    .Color(Color.Blue));
*/

        }
    }
}
