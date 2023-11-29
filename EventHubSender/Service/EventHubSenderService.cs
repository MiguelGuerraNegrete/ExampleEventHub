using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System.Text;

namespace EventHubSender.Service
{
    public class EventHubSenderService : IEventHubSenderService
    {
        private readonly EventHubProducerClient _producerClient;

        public EventHubSenderService(string connectionString, string eventHubName)
        {
            _producerClient = new EventHubProducerClient(connectionString, eventHubName);
        }

        public async Task SendEventsAsync(IEnumerable<string> events)
        {
            using (EventDataBatch eventBatch = await _producerClient.CreateBatchAsync())
            {
                foreach (var eventData in events.Select(e => new EventData(Encoding.UTF8.GetBytes(e))))
                {
                    if (!eventBatch.TryAdd(eventData))
                    {
                        throw new Exception($"Event is too large for the batch and cannot be sent.");
                    }
                }

                await _producerClient.SendAsync(eventBatch);
            }
        }

        public async Task DisposeAsync()
        {
            await _producerClient.DisposeAsync();
        }

        
    }
}
