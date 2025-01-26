namespace Restaurant.Services.RewardAPI.Messaging.IMessaging
{
    public interface IAzureServiceBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
