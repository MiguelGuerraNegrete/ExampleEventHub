using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Messaging.EventHubs;
using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHubReceiber.Service
{
    public class EventHubReceiberService : IEventHubReceiberService
    {
        private readonly EventProcessorClient _processor;

        public EventHubReceiberService(
            string eventHubsConnectionString,
            string eventHubName,
            string blobStorageConnectionString,
            string blobContainerName)
        {
            BlobContainerClient storageClient = new(blobStorageConnectionString, blobContainerName);
            _processor = new EventProcessorClient(storageClient, EventHubConsumerClient.DefaultConsumerGroupName, eventHubsConnectionString, eventHubName);
            _processor.ProcessEventAsync += ProcessEventHandler;
            _processor.ProcessErrorAsync += ProcessErrorHandler;
        }

        public async Task StartProcessingAsync()
        {
            await _processor.StartProcessingAsync();
        }

        public async Task StopProcessingAsync()
        {
            await _processor.StopProcessingAsync();
        }

        private async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {
            Console.WriteLine("\tReceived event: {0}", Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()));
            Console.ReadLine();
            await Task.CompletedTask;
        }

        private async Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            Console.WriteLine($"\tPartition '{eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
            Console.WriteLine(eventArgs.Exception.Message);
            Console.ReadLine();
            await Task.CompletedTask;
        }
    }
}
