using System;
using Azure.Messaging.EventHubs;
using AzureFunctions.Contexts;
using AzureFunctions.Models.SaveData;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctions.Functions
{
    public class SaveData
    {
        private readonly ILogger _logger;
        private readonly DataContext _context;

        public SaveData(ILoggerFactory loggerFactory, DataContext context)
        {
            _logger = loggerFactory.CreateLogger<SaveData>();
            _context = context;
        }

        [Function("SaveData")]
        public async Task Run([EventHubTrigger("iothub-ehub-azureassig-23588075-ab5daf03c6", Connection = "IotHubEndpoint", ConsumerGroup = "cosmosdb")] string[] messages, FunctionContext context)
        {
            Console.WriteLine(messages.Length);
            for (int i = 0; i < messages.Length; i++)
            {
                var message = messages[i];
                _logger.LogInformation($"message: {message}");

                var systemPropertiesArray = context.BindingContext.BindingData["systemPropertiesArray"]?.ToString();
                var data = JsonConvert.DeserializeObject<dynamic>(systemPropertiesArray!);
                _logger.LogInformation($"systemproperties: {data}");

                _context.Messages.Add(new SaveMessage
                {
                    Message = message,
                    SystemProperties = systemPropertiesArray!
                });
                await _context.SaveChangesAsync();
            }

        }
    }
}
