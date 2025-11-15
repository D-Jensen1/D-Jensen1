using MortgageCalcLibrary;
using Spectre.Console;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

decimal[] memoryArray = new decimal[5];

bool toContinue = true;
var pageHeaderRule = new Rule();
var font = FigletFont.Load("colossal.flf");
var menuBoxPanel = new Panel("");

do
{
    AnsiConsole.Write(
        new FigletText(font, "Mortgage")
            //.Centered()
            .Color(Color.LightSteelBlue1));
    AnsiConsole.Write(
        new FigletText(font, "   Calculator")
            //.Centered()
            .Color(Color.LightSteelBlue1));

    var menuPrompt = new SelectionPrompt<string>()
        .Title("What would you like to do?")
           .PageSize(7)
           .AddChoices(

                   [
                     "Calculate my monthly payment",
                     "Calculate my payment breakdown for a given payment",
                     "Calculate my interest for biweekly payment",
                     "Show me amortization tables",
                     "Use demo save data",
                     "Clear saved information",
                     "Quit this application"
                   ]
               );

    Style myStyle = new Style(Color.Green3, Color.Orange4);
    menuPrompt.HighlightStyle = myStyle;

    string choice = AnsiConsole.Prompt<string>(menuPrompt);
    Console.Clear();

    switch (choice)

    {
        case "Calculate my monthly payment":
            CalculateMonthPayment();
            break;
        case "Calculate my payment breakdown for a given payment":
            CalculatePaymentBreak();
            break;
        case "Calculate my interest for biweekly payment":
            CalculateBiWeeklyInterest();
            break;
        case "Show me amortization tables":
            ShowAmortizationTables();
            break;
        case "Use demo save data":
            memoryArray = [375_000m, 6.55m, 30, 25_000m, 400_000m];
            break;
        case "Clear saved information":
            ClearSavedArray();
            break;
        case "Quit this application":
            toContinue = false;
            break;
        default:
            break;
    }
} while (toContinue);

void CalculateMonthPayment()
{
    pageHeaderRule = new Rule("Calculate Monthly Payment");
    AnsiConsole.Write(pageHeaderRule);

    decimal result = 0.0m;
    if (TestMemoryEmpty())
    {
        AddToMemory();
    }
    Console.Clear();
    AnsiConsole.Write(pageHeaderRule);

    result = (decimal)Calculations.CalculateMonthlyPayment(memoryArray[0], memoryArray[1], (int)memoryArray[2]);

    /* Needed to create a grid with single column at
       width(40) to constrain Rule inside of panel */
    var grid = new Grid();
    grid.AddColumn(new GridColumn().Width(40));
    var panel = new Panel(
        grid.AddRow(
            // Adding rows to grid
            new Rows(
                new Markup($"Property Price: {memoryArray[4]:C0}"),
                new Markup($"Down Payment:   {memoryArray[3]:C0}"),
                new Markup($"Principal:      {memoryArray[0]:C0}"),
                new Markup($"Interest Rate:  {memoryArray[1]}%"),
                new Markup($"Loan Term:      {memoryArray[2]} years"),
                new Markup("\n"),
                new Rule($"Your monthly payment is {result:C}") {Style = Style.Parse("green") }
                )
            )
        )
    {
        Width = 40,
        Border = BoxBorder.Rounded
    };
    
    AnsiConsole.Write(panel);

    Console.WriteLine("\nPress Enter to return to the main menu...");
    Console.ReadLine();
    Console.Clear();
}

void CalculatePaymentBreak()
{
    pageHeaderRule = new Rule("Calculate Payment Breakdown");
    AnsiConsole.Write(pageHeaderRule);
    decimal[] result = new decimal[3];

    if (TestMemoryEmpty())
    {
        AddToMemory();
    }
    Console.Clear();
    AnsiConsole.Write(pageHeaderRule);

    int paymentNumber = AnsiConsole.Prompt(
    new TextPrompt<int>("Please enter the payment to analyze: "));
    Console.Clear();
    AnsiConsole.Write(pageHeaderRule);

    decimal monthlyPayment = (decimal)Calculations.CalculateMonthlyPayment(memoryArray[0], memoryArray[1], (int)memoryArray[2]);
    result = Calculations.CalculateMonthlyBreakdown(memoryArray[0], memoryArray[1], paymentNumber, monthlyPayment);

    var panel = new Panel(

        new Rows(
            new Markup($"Property Price:         {memoryArray[4]:C0}"),
            new Markup($"Down Payment:           {memoryArray[3]:C0}"),
            new Markup($"Principal:              {memoryArray[0]:C0}"),
            new Markup($"Interest Rate:          {memoryArray[1]}%"),
            new Markup($"Loan Term:              {memoryArray[2]} years"),
            new Markup($"Payment Being Analyzed: {paymentNumber}"),

            new Markup("\n"),
            new Markup($"[green]Interest Paid: {result[0]:C} | Principal Paid: {result[1]:C} | Remaining Principal: {result[2]:C}[/]")
            )
        )
    {
        Border = BoxBorder.Rounded
    };
    AnsiConsole.Write(panel);

    Console.WriteLine("\nPress Enter to return to the main menu...");
    Console.ReadLine();
    Console.Clear();
}

void CalculateBiWeeklyInterest()
{
    pageHeaderRule = new Rule("Calculate Bi-Weekly Payments");
    AnsiConsole.Write(pageHeaderRule);
    decimal result = 0.0m;

    if (TestMemoryEmpty())
    {
        AddToMemory();
    }
    Console.Clear();
    AnsiConsole.Write(pageHeaderRule);

    decimal totalInterest = Calculations.TotalInterest(memoryArray[0], memoryArray[1], (int)memoryArray[2]);

    result = (decimal)Calculations.BiWeeklyInterest(memoryArray[0], memoryArray[1], (int)memoryArray[2]);

    var panel = new Panel(

        new Rows(
            new Markup($"Property Price: {memoryArray[4]:C0}"),
            new Markup($"Down Payment:   {memoryArray[3]:C0}"),
            new Markup($"Principal:      {memoryArray[0]:C0}"),
            new Markup($"Interest Rate:  {memoryArray[1]}%"),
            new Markup($"Loan Term:      {memoryArray[2]} years"),

            new Markup("\n"),
            new Markup($"[green]Total Interest Paid with Bi-Weekly Payments: {result:C}[/]"),
            new Markup ($"[red]Total Interest Paid with Monthly Payments: {totalInterest:C}[/]")
            )
        )
    {
        Border = BoxBorder.Rounded
    };
    AnsiConsole.Write(panel);

    Console.WriteLine("\nPress Enter to return to the main menu...");
    Console.ReadLine();
    Console.Clear();
}

void ShowAmortizationTables()
{
    pageHeaderRule = new Rule("Amortization Tables");
    AnsiConsole.Write(pageHeaderRule);
    
    var menuPrompt = new SelectionPrompt<string>()
        .AddChoices(["Monthly Payment Table","Bi-Weekly Payment Table"]
        );
    Style myStyle = new Style(Color.Green3, Color.Orange4);
    menuPrompt.HighlightStyle = myStyle;

    string choice = AnsiConsole.Prompt(menuPrompt);
    
    if (TestMemoryEmpty())
    {
        AddToMemory();
    }
    Console.Clear();
    AnsiConsole.Write(pageHeaderRule);

    decimal[,] rows = null;
    switch (choice)
    {
        case "Monthly Payment Table":
            rows = Calculations.MonthlyAmortization(memoryArray[0], memoryArray[1], (int)memoryArray[2]);
            break;
        case "Bi-Weekly Payment Table":
            rows = Calculations.BiWeeklyAmortization(memoryArray[0], memoryArray[1], (int)memoryArray[2]);
            break;
    }

    var table = new Table();
    AnsiConsole.Live(table)
    .Start(ctx =>
    {
        // Add some columns
        table.AddColumn(new TableColumn("Payment Date").Centered());
        ctx.Refresh();
        Thread.Sleep(100);

        table.AddColumn(new TableColumn("Principal Paid").Centered());
        ctx.Refresh();
        Thread.Sleep(100);

        table.AddColumn(new TableColumn("Interest Paid").Centered());
        ctx.Refresh();
        Thread.Sleep(100);

        table.AddColumn(new TableColumn("Principal Remaining").Centered());
        ctx.Refresh();
        Thread.Sleep(100);

        table.AddColumn(new TableColumn("Interest Accumulated").Centered());
        ctx.Refresh();
        Thread.Sleep(100);

        // Add some rows
        for (int i = 0; i < rows.GetLength(0); i++)
        {
            table.AddRow($"{rows[i, 0]}", $"{rows[i, 1]:C}", $"{rows[i, 2]:C}", $"{rows[i, 3]:C}", $"{rows[i, 4]:C}");
            ctx.Refresh();
        }
    });


    Console.WriteLine("\nPress Enter to return to the main menu...");
    Console.ReadLine();
    Console.Clear();
}

void AddToMemory()
{
    decimal price = AnsiConsole.Prompt(
        new TextPrompt<decimal>("Please enter the price of your property: "));
    Console.Clear();
    AnsiConsole.Write(pageHeaderRule);

    decimal downPayment = AnsiConsole.Prompt(
    new TextPrompt<decimal>("Please enter the down payment: "));
    Console.Clear();

    AnsiConsole.Write(pageHeaderRule);
    decimal interestRate = AnsiConsole.Prompt(
    new TextPrompt<decimal>("Please enter the interest rate (enter 4.0 for 4%): "));
    Console.Clear();
    AnsiConsole.Write(pageHeaderRule);

    int loanTerm = AnsiConsole.Prompt(
    new TextPrompt<int>("Please enter the loan term in years: "));
    
    memoryArray[0] = price - downPayment;
    memoryArray[1] = interestRate;
    memoryArray[2] = loanTerm;
    memoryArray[3] = downPayment;
    memoryArray[4] = price;
}

void ClearSavedArray() => Array.Clear(memoryArray);

bool TestMemoryEmpty() => memoryArray[0] == default ? true : false;
