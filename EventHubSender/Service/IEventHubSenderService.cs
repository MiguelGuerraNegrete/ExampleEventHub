namespace EventHubSender.Service
{
    public interface IEventHubSenderService
    {
        Task SendEventsAsync(IEnumerable<string> events);
    }
}
