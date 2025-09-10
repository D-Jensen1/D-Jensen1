//create deck of cards and randomly deal 5 cards

//52 cards consisting of 13 cards in 4 suits (no joker)
using DeckLibrary;
using Spectre.Console;
using System.Text;
using RotateTextLibrary;

//IsometricRotation.AlternateLetters("Card");
Console.OutputEncoding = System.Text.Encoding.UTF8;

//var font = FigletFont.Load("isometric2.flf");
AnsiConsole.Write(
                new FigletText("Card Dealer")
                    .Centered()
                    .Color(Color.Gold1));




bool toContinue = true;
do
{

    var menuPrompt = new SelectionPrompt<string>()
           .Title("What can I do for you?")
           .PageSize(5)
           .AddChoices(
                   [
                    "Show me a new deck",
                    "Show me a shuffled deck",
                    "Deal me some cards",
                    "Quit"
                   ]
               );
    Style myStyle = new Style(Color.Black, Color.Cyan1);
    menuPrompt.HighlightStyle = myStyle;

    string choice = AnsiConsole.Prompt<string>(menuPrompt);

    // Echo the fruit back to the terminal
    switch (choice)
    {
        case "Show me a new deck":
            ShowDeck();
            break;
        case "Show me a shuffled deck":
            ShowShuffledDeck();
            break;
        case "Deal me some cards":
            DealCards(); break;
        default:
            toContinue = false;
            break;
    }
} while (toContinue);




void DealCards()
    {
    var deck = Deck.GenerateDeck();
    int handSize = default;
    Deck.Shuffle(deck);

    TextPrompt<int> handSizePrompt = new TextPrompt<int> ("How many cards would you like?");
    handSizePrompt.DefaultValue(5);
    //handSizePrompt.Validate(input => input >= 1 && input <= 52, "A deck only has 52 cards.");
    handSizePrompt.Validate(i => (i >= 1 && i <= deck.Length), "A deck only has 52 cards.");

    handSize = AnsiConsole.Prompt<int>(handSizePrompt);
    
    var cards = Deck.DrawCards(ref deck, handSize);


    DisplaySpectreTable(cards);
}

void ShowDeck()
{
    string[] deck = Deck.GenerateDeck();
    Deck.Shuffle(deck);
    //DisplaySpectreTable()
}

void ShowShuffledDeck()
{
    string[] deck = Deck.GenerateDeck();
    Deck.Shuffle(deck);
    string[] randomCards = Deck.DrawCards(ref deck,5);
    Console.OutputEncoding = Encoding.UTF8;

    Console.WriteLine(string.Join(", ",randomCards));
    Console.WriteLine("Press Enter to close the window...");
    Console.ReadLine();
}

void DisplaySpectreTable(string[] cardsToDisplay)
{
    // Create a table
    var table = new Table()
        ;


    AnsiConsole.Live(table)
    .Start(ctx =>
    {

        // Sets the border
        //table.Border(TableBorder.Ascii);
        table.SquareBorder();


        //column header = suits
        table.AddColumn(new TableColumn("Spades").Centered());
        ctx.Refresh();
        Thread.Sleep(300);

        table.AddColumn(new TableColumn("Clubs").Centered());
        ctx.Refresh();
        Thread.Sleep(300);

        table.AddColumn(new TableColumn("Diamonds").Centered());
        ctx.Refresh();
        Thread.Sleep(300);

        table.AddColumn(new TableColumn("Hearts").Centered());
        ctx.Refresh();
        Thread.Sleep(300);

        //table.Columns[0].PadLeft(3).PadRight(5);

        var spadesList = new List<string>();
        var heartsList = new List<string>();
        var diamondsList = new List<string>();
        var clubsList = new List<string>();
        var unknownList = new List<string>();

        //Add cards to appropriate columns
        foreach (string card in cardsToDisplay)
        {
            int code = Char.ConvertToUtf32(card.ToString(), 0);
            

            if (code >= 0x1F0A1 && code <= 0x1F0AE) spadesList.Add(card); //Spades
            else if (code >= 0x1F0B1 && code <= 0x1F0BE) heartsList.Add(card); //Hearts
            else if (code >= 0x1F0C1 && code <= 0x1F0CE) diamondsList.Add(card);//Diamonds
            else if (code >= 0x1F0D1 && code <= 0x1F0DE) clubsList.Add(card);//Clubs
            else unknownList.Add(card);
        }

        spadesList.Sort();
        heartsList.Sort();
        diamondsList.Sort();
        clubsList.Sort();


        int rowCount = new int[] { spadesList.Count, heartsList.Count, diamondsList.Count, clubsList.Count }.Max();

        for (int i = 0; i < rowCount; i++)
        {
            string spade = i < spadesList.Count ? spadesList[i] : "";
            string heart = i < heartsList.Count ? heartsList[i] : "";
            string diamond = i < diamondsList.Count ? diamondsList[i] : "";
            string club = i < clubsList.Count ? clubsList[i] : "";


            table.AddRow(spade, club, diamond, heart);
            ctx.Refresh();
            Thread.Sleep(100);
        }
    });
}
//An expression that can define a method (lemda)
//(int i) => (i >= 1 && i <= 52)
//i => (i >= 1 && i <= 52)
//input => input >= 1 && input <= 52
/*
bool blah(int i)
{
    return (i >= 1 && i <= 52);
}
*/
