using Microsoft.Agents.AI;
using Spectre.Console;

namespace MultiTurnConversations;

public class AgentThreadManager
{
    private readonly AIAgent _agent;
    private readonly Dictionary<string, AgentThread> _threads;
    private readonly ThreadStorageService _storageService;
    private string? _currentThreadName;

    public AgentThreadManager(AIAgent agent, ThreadStorageService storageService)
    {
        _agent = agent;
        _threads = new Dictionary<string, AgentThread>();
        _storageService = storageService;
    }

    public int ThreadCount => _threads.Count;
    public string? CurrentThreadName => _currentThreadName;
    public IEnumerable<string> ThreadNames => _threads.Keys;

    public async Task InitializeAsync()
    {
        await LoadPersistedThreadsAsync();
    }

    private async Task LoadPersistedThreadsAsync()
    {
        try
        {
            var storage = await _storageService.LoadThreadsAsync();
            
            if (storage.Threads.Any())
            {
                AnsiConsole.MarkupLine($"[green]Loading {storage.Threads.Count} saved thread(s)...[/]");
                
                foreach (var threadData in storage.Threads)
                {
                    // Create new AgentThread instances for each saved thread
                    var thread = _agent.GetNewThread();
                    _threads[threadData.Name] = thread;
                }

                _currentThreadName = storage.CurrentThreadName;
                
                if (_currentThreadName != null && !_threads.ContainsKey(_currentThreadName))
                {
                    _currentThreadName = _threads.Keys.FirstOrDefault();
                }

                AnsiConsole.MarkupLine($"[green]Successfully loaded threads. Current thread: {_currentThreadName ?? "None"}[/]");
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[yellow]Warning: Could not load persisted threads: {ex.Message}[/]");
        }
    }

    private async Task SaveThreadsAsync()
    {
        try
        {
            var storage = new ThreadStorage
            {
                CurrentThreadName = _currentThreadName,
                Threads = _threads.Select(kvp => new ThreadData
                {
                    Name = kvp.Key,
                    ThreadId = Guid.NewGuid().ToString(), // We'll use this for future thread reconstruction
                    CreatedAt = DateTime.UtcNow, // This would ideally be tracked from creation
                    LastUsed = DateTime.UtcNow,
                    IsCurrentThread = kvp.Key == _currentThreadName
                }).ToList()
            };

            await _storageService.SaveThreadsAsync(storage);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error saving threads: {ex.Message}[/]");
        }
    }

    public string GetStatusText()
    {
        var status = $"[bold]Threads:[/] {_threads.Count}";
        if (_currentThreadName != null)
            status += $" | [bold]Current:[/] [green]{_currentThreadName}[/]";
        else
            status += " | [bold]Current:[/] [red]None[/]";
        
        return status;
    }

    public void ShowHelp()
    {
        var table = new Table()
            .AddColumn("[bold]Command[/]")
            .AddColumn("[bold]Description[/]")
            .Border(TableBorder.Rounded);

        table.AddRow("[yellow]help[/]", "Show this help message");
        table.AddRow("[yellow]create[/] or [yellow]create <name>[/]", "Create a new thread (with optional name)");
        table.AddRow("[yellow]switch[/] or [yellow]switch <name>[/]", "Switch to a different thread");
        table.AddRow("[yellow]list[/]", "List all threads");
        table.AddRow("[yellow]delete[/] or [yellow]delete <name>[/]", "Delete a thread");
        table.AddRow("[yellow]save[/]", "Manually save threads to disk");
        table.AddRow("[yellow]clear[/]", "Clear the console");
        table.AddRow("[yellow]quit/exit/q[/]", "Exit the application");
        table.AddRow("[dim]<message>[/]", "[dim]Send a message to the current thread[/]");

        AnsiConsole.Write(table);
    }

    public void ListThreads()
    {
        if (_threads.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No threads created yet.[/]");
            return;
        }

        var table = new Table()
            .AddColumn("[bold]Thread Name[/]")
            .AddColumn("[bold]Status[/]")
            .Border(TableBorder.Rounded);

        foreach (var kvp in _threads)
        {
            var status = kvp.Key == _currentThreadName ? "[green]Current[/]" : "[dim]Inactive[/]";
            table.AddRow(kvp.Key, status);
        }

        AnsiConsole.Write(table);
    }

    public async Task CreateNewThreadAsync(string? name = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            name = AnsiConsole.Ask<string>("Enter thread name:");
        }

        if (_threads.ContainsKey(name))
        {
            AnsiConsole.MarkupLine($"[red]Thread '{name}' already exists![/]");
            return;
        }

        await AnsiConsole.Status()
            .StartAsync($"Creating thread '{name}'...", async ctx =>
            {
                var thread = _agent.GetNewThread();
                _threads[name] = thread;
                _currentThreadName = name;
                await SaveThreadsAsync();
            });

        AnsiConsole.MarkupLine($"[green]Created and switched to thread '{name}'![/]");
    }

    public async Task SwitchThreadAsync()
    {
        if (_threads.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No threads available. Create one first![/]");
            return;
        }

        var threadNames = _threads.Keys.ToArray();
        var selectedThread = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a thread to switch to:")
                .AddChoices(threadNames));

        await SwitchToThreadAsync(selectedThread);
    }

    public async Task SwitchToThreadAsync(string threadName)
    {
        if (!_threads.ContainsKey(threadName))
        {
            AnsiConsole.MarkupLine($"[red]Thread '{threadName}' does not exist![/]");
            return;
        }

        _currentThreadName = threadName;
        await SaveThreadsAsync();
        AnsiConsole.MarkupLine($"[green]Switched to thread '{threadName}'![/]");
    }

    public async Task DeleteThreadAsync(string? name = null)
    {
        if (_threads.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No threads available to delete![/]");
            return;
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            var threadNames = _threads.Keys.ToArray();
            name = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select a thread to delete:")
                    .AddChoices(threadNames));
        }

        if (!_threads.ContainsKey(name))
        {
            AnsiConsole.MarkupLine($"[red]Thread '{name}' does not exist![/]");
            return;
        }

        var confirm = AnsiConsole.Confirm($"Are you sure you want to delete thread '{name}'?");
        if (!confirm)
        {
            AnsiConsole.MarkupLine("[yellow]Deletion cancelled.[/]");
            return;
        }

        _threads.Remove(name);
        
        if (_currentThreadName == name)
        {
            _currentThreadName = _threads.Keys.FirstOrDefault();
            if (_currentThreadName != null)
                AnsiConsole.MarkupLine($"[yellow]Switched to thread '{_currentThreadName}'[/]");
        }

        await SaveThreadsAsync();
        AnsiConsole.MarkupLine($"[green]Deleted thread '{name}'![/]");
    }

    public async Task SaveManuallyAsync()
    {
        await AnsiConsole.Status()
            .StartAsync("Saving threads...", async ctx =>
            {
                await SaveThreadsAsync();
            });
        
        AnsiConsole.MarkupLine($"[green]Threads saved to: {_storageService.GetStorageLocation()}[/]");
    }

    public async Task SendMessageToCurrentThreadAsync(string message)
    {
        if (_currentThreadName == null || !_threads.ContainsKey(_currentThreadName))
        {
            AnsiConsole.MarkupLine("[red]No active thread! Create or switch to a thread first.[/]");
            return;
        }

        var currentThread = _threads[_currentThreadName];
        
        AnsiConsole.MarkupLine($"[bold blue]You ({_currentThreadName}):[/] {message}");

        try
        {
            await AnsiConsole.Status()
                .StartAsync("AI is thinking...", async ctx =>
                {
                    var response = await _agent.RunAsync(message, currentThread);
                    AnsiConsole.MarkupLine($"[bold green]AI:[/] {response}");
                });
            
            // Save threads after each message to preserve conversation state
            await SaveThreadsAsync();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
        }
    }

    public async Task ShutdownAsync()
    {
        await SaveThreadsAsync();
    }
}