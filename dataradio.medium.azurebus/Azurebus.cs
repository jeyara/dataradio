using System;
using System.Collections.Generic;
using dataradio.core;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus;

namespace dataradio.medium.azurebus
{
    public class Azurebus : IMedium
    {
        public Azurebus()
        {
            // Configure Topic Settings.
            TopicDescription td = new TopicDescription("TestTopic");
            td.MaxSizeInMegabytes = 1;
            td.DefaultMessageTimeToLive = new TimeSpan(0, 0, 0, 30);

            // Create a new Topic with custom settings.
            //string connectionString =
            //    CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            string connectionString = "Endpoint=sb://radiomedium.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=0wKUj9d42lmOos6V7MsBV0zkSRFnq+Km+sKTvwoJr38=";

            var namespaceManager =
                NamespaceManager.CreateFromConnectionString(connectionString);

            if (!namespaceManager.TopicExists("DataRadioBroadcast"))
            {
                namespaceManager.CreateTopic(td);
            }

            if (!namespaceManager.SubscriptionExists("DataRadioBroadcast", "AllMessages"))
            {
                namespaceManager.CreateSubscription("DataRadioBroadcast", "AllMessages");
            }
        }

        public void Broadcast(Packet packet, List<ITransReceiver> to)
        {
            throw new NotImplementedException();
        }
    }
}
