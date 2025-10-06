using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LearnFunction;

public class Function1
{
    private readonly ILogger _logger;

    public Function1(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<Function1>();
    }

    [Function("TimerDemo")]
    [QueueOutput("myqueue-items",Connection="AzureWebJobsStorage")]
    public string[] Run([TimerTrigger("*/10 * * * * *")] TimerInfo myTimer)
    {
        //1 sec of 128mb * 8 = 1 gbs
        _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTime.Now);
        
        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
        }

        List<string> messages = new();
        for (int i = 0; i < 100; i++)
        {
            messages.Add($"this is a message {i}");
        }
        return messages.ToArray();
    }
}