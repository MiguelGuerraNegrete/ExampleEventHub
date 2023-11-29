using EventHubSender.Service;


class Program
{
    static async Task Main(string[] args)   
    {
        int numOfEvents = 3;

        IEventHubSenderService eventHubService = new EventHubSenderService(
            "Endpoint=sb://contosoehub.servicebus.usgovcloudapi.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=MpATADqoYGj+KECDep8Hi0Vrp+ae5Hfdv+AEhIY+mdA=",
            "myeventhub");

        var events = Enumerable.Range(1, numOfEvents).Select(i => $"Event {i}");

        try
        {
            await eventHubService.SendEventsAsync(events);
            Console.WriteLine($"A batch of {numOfEvents} events has been published.");
            Console.ReadLine();
        }
        finally
        {
            await ((EventHubSenderService)eventHubService).DisposeAsync();

        }
    }
}
