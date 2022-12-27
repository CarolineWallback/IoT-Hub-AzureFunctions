using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctions.Functions
{
    public class ViewMessages
    {
        private readonly ILogger _logger;

        public ViewMessages(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ViewMessages>();
        }

        [Function("ViewMessages")]
        public void Run([EventHubTrigger("iothub-ehub-azureassig-23588075-ab5daf03c6", Connection = "IotHubEndpoint", ConsumerGroup = "azurefunctions")] string[] input)
        {
            _logger.LogInformation($"First Event Hubs triggered message: {input[0]}");
        }
    }
}
