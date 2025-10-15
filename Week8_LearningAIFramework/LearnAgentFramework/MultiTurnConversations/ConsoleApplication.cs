using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;
using System.ClientModel;
using Spectre.Console;

namespace MultiTurnConversations;

public class ConsoleApplication
{
    private readonly AgentThreadManager _threadManager;

    public ConsoleApplication(IConfiguration config)
    {
        // Initialize AI Agent
        var agent = new AzureOpenAIClient(
            new Uri(config["endpoint"]),
            new ApiKeyCredential(config["key"]))
            .GetChatClient(config["model"])
            .CreateAIAgent(name: "You are a helpful AI assistant that can tell jokes and answer questions.");

        var storageService = new ThreadStorageService();
        _threadManager = new AgentThreadManager(agent, storageService);
    }

    public async Task RunAsync()
    {
        await InitializeAsync();
        DisplayWelcomeMessage();

        // Main application loop
        while (true)
        {
            try
            {
                ShowStatus();
                var input = GetUserInput();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                var shouldExit = await ProcessCommandAsync(input);
                if (shouldExit)
                    break;
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
            }

            AnsiConsole.WriteLine();
        }

        await ShutdownAsync();
    }

    private async Task InitializeAsync()
    {
        await AnsiConsole.Status()
            .StartAsync("Initializing application...", async ctx =>
            {
                await _threadManager.InitializeAsync();
            });
    }

    private async Task ShutdownAsync()
    {
        await AnsiConsole.Status()
            .StartAsync("Saving application state...", async ctx =>
            {
                await _threadManager.ShutdownAsync();
            });
    }

    private void DisplayWelcomeMessage()
    {
        AnsiConsole.Write(
            new FigletText("AI Agent Console")
                .LeftJustified()
                .Color(Color.Cyan1));

        AnsiConsole.MarkupLine("[bold yellow]Welcome to the AI Agent Console![/]");
        AnsiConsole.MarkupLine("[dim]Type commands to interact with AI threads. Use 'help' to see available commands.[/]");
        
        if (_threadManager.ThreadCount > 0)
        {
            AnsiConsole.MarkupLine($"[green]? Restored {_threadManager.ThreadCount} thread(s) from previous session[/]");
        }
        
        AnsiConsole.WriteLine();
    }

    private void ShowStatus()
    {
        var statusPanel = new Panel(_threadManager.GetStatusText())
            .Header("[bold]Status[/]")
            .BorderColor(Color.Blue);
        AnsiConsole.Write(statusPanel);
    }

    private string GetUserInput()
    {
        return AnsiConsole.Ask<string>("[bold green]>[/] ");
    }

    private async Task<bool> ProcessCommandAsync(string input)
    {
        var command = input.Trim().ToLower();

        switch (command)
        {
            case "help":
                _threadManager.ShowHelp();
                return false;

            case "quit" or "exit" or "q":
                AnsiConsole.MarkupLine("[bold red]Goodbye![/]");
                return true;

            case "list":
                _threadManager.ListThreads();
                return false;

            case "create":
                await _threadManager.CreateNewThreadAsync();
                return false;

            case "switch":
                await _threadManager.SwitchThreadAsync();
                return false;

            case "delete":
                await _threadManager.DeleteThreadAsync();
                return false;

            case "save":
                await _threadManager.SaveManuallyAsync();
                return false;

            case "clear":
                AnsiConsole.Clear();
                return false;

            default:
                return await ProcessParameterizedCommandAsync(input, command);
        }
    }

    private async Task<bool> ProcessParameterizedCommandAsync(string input, string command)
    {
        if (command.StartsWith("create "))
        {
            var threadName = input.Substring(7).Trim();
            await _threadManager.CreateNewThreadAsync(threadName);
        }
        else if (command.StartsWith("switch "))
        {
            var threadName = input.Substring(7).Trim();
            await _threadManager.SwitchToThreadAsync(threadName);
        }
        else if (command.StartsWith("delete "))
        {
            var threadName = input.Substring(7).Trim();
            await _threadManager.DeleteThreadAsync(threadName);
        }
        else
        {
            // Send message to current thread
            await _threadManager.SendMessageToCurrentThreadAsync(input);
        }

        return false;
    }
}