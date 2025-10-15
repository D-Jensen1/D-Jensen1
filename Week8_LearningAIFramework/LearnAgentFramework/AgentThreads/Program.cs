using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using OpenAI;
using System.ClientModel;
using System.ComponentModel;
using System.Text;
using Spectre.Console;

// Load configuration
var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

// Create the base AI client
var aiClient = new AzureOpenAIClient(
    new Uri(config["endpoint"]),
    new ApiKeyCredential(config["key"]))
    .GetChatClient(config["model"])
    .AsIChatClient(); // Convert to IChatClient

// Main program logic
var threadManager = new ThreadManager(aiClient);

// Welcome screen
AnsiConsole.Clear();
var welcomePanel = new Panel(
    new Markup("[bold yellow]Welcome to the AI Thread Manager![/]\n\n" +
               "[dim]This application manages 3 different AI agents with unique personalities.[/]\n" +
               "[dim]Use the menu to interact with different AI assistants.[/]"))
    .Border(BoxBorder.Double)
    .BorderColor(Color.Blue)
    .Header("[bold blue] AI Agent Manager [/]")
    .Padding(1, 1);

AnsiConsole.Write(welcomePanel);
AnsiConsole.WriteLine();
AnsiConsole.Write("Press any key to start...");
Console.ReadKey();

bool running = true;
while (running)
{
    var choice = threadManager.DisplayMenu();
    
    switch (choice)
    {
        case "switch":
            var threadId = threadManager.SelectThread();
            if (threadId.HasValue)
            {
                threadManager.SwitchThread(threadId.Value);
            }
            break;
        case "message":
            var message = threadManager.GetMessageInput();
            if (!string.IsNullOrWhiteSpace(message))
            {
                await threadManager.SendMessageAsync(message);
            }
            break;
        case "exit":
            running = false;
            var goodbyePanel = new Panel("[bold green]Thank you for using AI Thread Manager![/]\n[dim]Goodbye![/]")
                .Border(BoxBorder.Rounded)
                .BorderColor(Color.Green);
            AnsiConsole.Write(goodbyePanel);
            break;
    }
}

// Thread Manager class to handle multiple threads with Spectre.Console
public class ThreadManager
{
    private readonly Dictionary<int, (AIAgent Agent, AgentThread Thread, string Name, string Instructions, string Emoji)> _threads;
    private int _currentThreadId = 1;

    public ThreadManager(IChatClient chatClient)
    {
        _threads = new Dictionary<int, (AIAgent, AgentThread, string, string, string)>();
        
        // Initialize 3 threads with different agent instructions
        InitializeThreads(chatClient);
    }

    private void InitializeThreads(IChatClient chatClient)
    {
        [Description("Get the predicted date for the end of the world.")]
        static DateTime EndOfWorld()
            => new DateTime(2030, 1, 1);

        [Description("This is a reference for some of my favorite things.")]
        static string MyPreferences()
            => "Favorite color: red. Favorite movie: The Bee Movie. Favorite drink: Sweet Tea";

        // Thread 1: Pirate Assistant
        var pirateAgent = chatClient.CreateAIAgent(
            instructions: "You are an everyday assistant that is a pirate at heart. Everything you say should sound like a pirate saying it.",
            tools: [AIFunctionFactory.Create(EndOfWorld)]);
        var pirateThread = pirateAgent.GetNewThread();
        _threads[1] = (pirateAgent, pirateThread, "Pirate Assistant", "Conqueror of the seas", "🏴‍☠️");

        // Thread 2: Technical Assistant
        var techAgent = chatClient.CreateAIAgent(
            instructions: "You are a technical programming assistant specializing in C# and .NET. Provide clear, concise technical explanations and code examples.");
        var techThread = techAgent.GetNewThread();
        _threads[2] = (techAgent, techThread, "Tech Assistant", "C# and .NET specialist", "💻");

        // Thread 3: Creative Writer
        var defaultAgent = chatClient.CreateAIAgent(
            instructions: "You are a helpful chatbot",
            tools: [AIFunctionFactory.Create(MyPreferences)]);
        var defaultThread = defaultAgent.GetNewThread();
        _threads[3] = (defaultAgent, defaultThread, "Creative Writer", "Storytelling and poetry specialist", "✍️");
    }

    public string DisplayMenu()
    {
        AnsiConsole.Clear();
        
        // Create title
        var title = new FigletText("AI Agent Manager")
            .LeftJustified()
            .Color(Color.Blue);
        AnsiConsole.Write(title);

        // Create status panel showing current thread
        var currentThread = _threads[_currentThreadId];
        var statusPanel = new Panel($"[bold]{currentThread.Emoji} {Markup.Escape(currentThread.Name)}[/]\n[dim]{Markup.Escape(currentThread.Instructions)}[/]")
            .Header("[bold yellow] Current Agent [/]")
            .Border(BoxBorder.Rounded)
            .BorderColor(Color.Yellow)
            .Padding(1, 0);
        
        AnsiConsole.Write(statusPanel);
        AnsiConsole.WriteLine();

        // Create agents table
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn(new TableColumn("[bold]Agent[/]").Centered())
            .AddColumn(new TableColumn("[bold]Name[/]").Centered())
            .AddColumn(new TableColumn("[bold]Description[/]").LeftAligned());

        foreach (var kvp in _threads.OrderBy(x => x.Key))
        {
            var isActive = kvp.Key == _currentThreadId;
            var marker = isActive ? "[bold green]▶[/]" : " ";
            
            // Build the name and description with proper markup handling
            var nameText = isActive ? $"[bold green]{Markup.Escape(kvp.Value.Name)}[/]" : $"[dim]{Markup.Escape(kvp.Value.Name)}[/]";
            var descText = isActive ? Markup.Escape(kvp.Value.Instructions) : $"[dim]{Markup.Escape(kvp.Value.Instructions)}[/]";
            
            table.AddRow(
                new Markup($"{marker} {kvp.Value.Emoji}"),
                new Markup(nameText),
                new Markup(descText));
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();

        // Menu selection
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold cyan]What would you like to do?[/]")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "message",
                    "switch", 
                    "exit"
                })
                .UseConverter(choice => choice switch
                {
                    "message" => "💬 Send a message to current agent",
                    "switch" => "🔄 Switch to different agent",
                    "exit" => "❌ Exit application",
                    _ => choice
                }));

        return choice;
    }

    public int? SelectThread()
    {
        var choices = _threads.Select(kvp => 
            new { Key = kvp.Key, Display = $"{kvp.Value.Emoji} {Markup.Escape(kvp.Value.Name)} - {Markup.Escape(kvp.Value.Instructions)}" })
            .ToList();

        var selectedDisplay = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold cyan]Select an agent to switch to:[/]")
                .PageSize(10)
                .AddChoices(choices.Select(c => c.Display))
                .UseConverter(display => display));

        var selected = choices.FirstOrDefault(c => c.Display == selectedDisplay);
        return selected?.Key;
    }

    public string GetMessageInput()
    {
        AnsiConsole.WriteLine();
        return AnsiConsole.Prompt(
            new TextPrompt<string>("[bold cyan]Enter your message:[/]")
                .AllowEmpty());
    }

    public bool SwitchThread(int threadId)
    {
        if (_threads.ContainsKey(threadId))
        {
            _currentThreadId = threadId;
            var thread = _threads[threadId];
            
            var successPanel = new Panel($"[bold green]✓ Switched to {thread.Emoji} {Markup.Escape(thread.Name)}[/]")
                .Border(BoxBorder.Rounded)
                .BorderColor(Color.Green);
            
            AnsiConsole.Write(successPanel);
            Thread.Sleep(1000); // Brief pause to show the message
            return true;
        }
        
        var errorPanel = new Panel($"[bold red]✗ Invalid thread ID: {threadId}[/]")
            .Border(BoxBorder.Rounded)
            .BorderColor(Color.Red);
        
        AnsiConsole.Write(errorPanel);
        Thread.Sleep(2000);
        return false;
    }

    public async Task SendMessageAsync(string message)
    {
        try
        {
            var currentThread = _threads[_currentThreadId];
            
            // Show processing status
            await AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots)
                .SpinnerStyle(Style.Parse("yellow"))
                .StartAsync($"[yellow]{currentThread.Emoji} {Markup.Escape(currentThread.Name)} is thinking...[/]", async ctx =>
                {
                    var response = await currentThread.Agent.RunAsync(message, currentThread.Thread);
                    
                    AnsiConsole.Clear();
                    
                    // Extract the response content - try common properties
                    var responseContent = response?.ToString() ?? "No response received.";
                    
                    // Build conversation content more efficiently
                    var conversationContent = new StringBuilder()
                        .Append("[bold cyan]You:[/] ")
                        .Append(Markup.Escape(message))
                        .Append("\n\n[bold green]")
                        .Append(currentThread.Emoji)
                        .Append(' ')
                        .Append(Markup.Escape(currentThread.Name))
                        .Append(":[/] ")
                        .Append(Markup.Escape(responseContent))
                        .ToString();
                    
                    // Display conversation
                    var conversationPanel = new Panel(new Markup(conversationContent))
                        .Header($"[bold blue] Conversation with {Markup.Escape(currentThread.Name)} [/]")
                        .Border(BoxBorder.Double)
                        .BorderColor(Color.Blue)
                        .Padding(1, 1);
                    
                    AnsiConsole.Write(conversationPanel);
                });
        }
        catch (Exception ex)
        {
            var errorPanel = new Panel($"[bold red]Error:[/] {Markup.Escape(ex.Message)}")
                .Border(BoxBorder.Rounded)
                .BorderColor(Color.Red)
                .Header("[bold red] Error [/]");
            
            AnsiConsole.Write(errorPanel);
        }
        
        AnsiConsole.WriteLine();
        AnsiConsole.Write("Press any key to continue...");
        Console.ReadKey();
    }

    public (AIAgent Agent, AgentThread Thread, string Name) GetCurrentThread()
    {
        var current = _threads[_currentThreadId];
        return (current.Agent, current.Thread, current.Name);
    }
}