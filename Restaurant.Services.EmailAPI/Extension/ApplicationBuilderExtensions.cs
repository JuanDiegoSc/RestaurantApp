using Restaurant.Services.EmailAPI.Messaging.IMessaging;

namespace Restaurant.Services.EmailAPI.Extension
{
    public static class ApplicationBuilderExtensions
    {
        private static IAzureServiceBusConsumer ServiceBusConsumer { get; set; }
        public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app)
        {
            ServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
            var hostAppLicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostAppLicationLife.ApplicationStarted.Register(OnStart);
            hostAppLicationLife.ApplicationStopping.Register(OnStop);

            return app;
        }

        private static void OnStop()
        {
            ServiceBusConsumer.Stop();
        }

        private static void OnStart()
        {
           ServiceBusConsumer.Start();
        }
    }
}
