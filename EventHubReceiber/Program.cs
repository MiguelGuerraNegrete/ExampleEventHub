using EventHubReceiber.Service;

class Program
{
    static async Task Main(string[] args)
    {
        IEventHubReceiberService eventHubReceiberService = new EventHubReceiberService(
            "Endpoint=sb://contosoehub.servicebus.usgovcloudapi.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=MpATADqoYGj+KECDep8Hi0Vrp+ae5Hfdv+AEhIY+mdA=",
            "myeventhub",
            "DefaultEndpointsProtocol=https;AccountName=practicestorage;AccountKey=gmNn5IPlkRt0ChQJreGOorVsX6CJ1JwMJfh/i0bn7wuLuboOgDaCoajp10cyAm+6DN0uH9yEyR63+AStGYJwJA==;EndpointSuffix=core.usgovcloudapi.net",
            "conatainerexample");

        try
        {
            await eventHubReceiberService.StartProcessingAsync();
            Console.WriteLine("Event processing started. Press Enter to stop.");
            Console.ReadLine();
        }
        finally
        {
            await eventHubReceiberService.StopProcessingAsync();
        }
    }
}