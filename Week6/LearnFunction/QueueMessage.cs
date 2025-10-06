using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LearnFunction;

public class QueueMessageFunction
{
    private readonly ILogger<QueueMessage> _logger;

    public QueueMessageFunction(ILogger<QueueMessage> logger)
    {
        _logger = logger;
    }

    [Function(nameof(QueueMessageFunction))]
    public void Run([QueueTrigger("myqueue-items", Connection = "AzureWebJobsStorage")] QueueMessage message)
    {
        _logger.LogInformation("received: {messageText}", message.Body.ToString());
    }
}