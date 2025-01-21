namespace Restaurant.Services.EmailAPI.Messaging.IMessaging
{
    public interface IAzureServiceBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
