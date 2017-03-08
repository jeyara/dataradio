using Autofac;
using dataradio.core;
using dataradio.medium;
using dataradio.radio;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;

namespace dataradio.console
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainer container = SetupContiner();

            using (var scope = container.BeginLifetimeScope())
            {
                var logger = scope.Resolve<Ilogger>();
                var air = scope.Resolve<IMedium>();

                Azurebus();
                //List<ITransReceiver> radioList = new List<ITransReceiver>();

                //var radioCount = 20;


                //for (int i = 0; i < radioCount; i++)
                //{
                //    ITransReceiver rdo = new Radio(logger);
                //    rdo.ReceiverId = "R" + i.ToString();
                //    radioList.Add(rdo);
                //}

                //for (int i = 0; i < radioCount; i++)
                //{
                //   var current = radioList[i];

                //    var nextRdoCount = new Random().Next(1, 15);

                //    current.TransReceiversInRange = new List<ITransReceiver>();

                //    for (int y = 0; y < nextRdoCount; y++)
                //    {
                //        var rdoIndex = new Random().Next(0, radioCount - 1);
                //        current.TransReceiversInRange.Add(radioList[rdoIndex]);
                //    }

                //}

                //var radio1 = radioList[0];

                //Packet packet = new Packet { BroadcasterId = radio1.ReceiverId, SourceId = radio1.ReceiverId, Message = $"Hello from {radio1.ReceiverId}" };

                //radio1.Broadcast(air, packet);

                //radioList.ForEach(r =>
                //{
                //    var p = r.GetLastPacket();
                //    if (p != null)
                //    {
                //        logger.Log(r.ReceiverId, p.Message, LogLevel.Receive);
                //    }
                //    else
                //    {
                //        logger.Log(r.ReceiverId, "Packet Loss", LogLevel.Error);
                //    }
                //});


                //var radio1 = scope.Resolve<ITransReceiver>();
                //radio1.ReceiverId = "R1";
                //var radio2 = scope.Resolve<ITransReceiver>();
                //radio2.ReceiverId = "R2";
                //var radio3 = scope.Resolve<ITransReceiver>();
                //radio3.ReceiverId = "R3";
                //var radio4 = scope.Resolve<ITransReceiver>();
                //radio4.ReceiverId = "R4";
                //var radio5 = scope.Resolve<ITransReceiver>();
                //radio5.ReceiverId = "R5";
                //var radio6 = scope.Resolve<ITransReceiver>();
                //radio6.ReceiverId = "R6";

                //radio1.TransReceiversInRange = new List<ITransReceiver> { radio2, radio6, radio4 };
                //radio2.TransReceiversInRange = new List<ITransReceiver> { radio1, radio3 };
                //radio3.TransReceiversInRange = new List<ITransReceiver> { radio2 };
                //radio4.TransReceiversInRange = new List<ITransReceiver> { radio3, radio5 };
                //radio5.TransReceiversInRange = new List<ITransReceiver> { radio1, radio4 };
                //radio6.TransReceiversInRange = new List<ITransReceiver> { radio2 };

                //List<ITransReceiver> radios = new List<ITransReceiver> { radio1, radio2, radio3, radio4,radio5, radio6 };

                //Packet packet = new Packet { BroadcasterId = radio1.ReceiverId, SourceId = radio1.ReceiverId, Message = $"Hello from {radio1.ReceiverId}" };

                //radio1.Broadcast(air, packet);

                //radios.ForEach(r =>
                //{
                //    var p = r.GetLastPacket();
                //    if (p != null)
                //    {
                //        logger.Log(r.ReceiverId, p.Message, LogLevel.Receive);
                //    }
                //    else
                //    {
                //        logger.Log(r.ReceiverId, "Packet Loss", LogLevel.Error);
                //    }
                //});

            }

            Console.ReadLine();
        }

        private static IContainer SetupContiner()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleLogger>().As<Ilogger>();
            builder.RegisterType<Radio>().As<ITransReceiver>().UsingConstructor(typeof(Ilogger));
            builder.RegisterType<Wireless>().As<IMedium>().UsingConstructor(typeof(Ilogger));

            return builder.Build();
        }

        public static void Azurebus()
        {

            // Create a new Topic with custom settings.
            //string connectionString =
            //    CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            string connectionString = "Endpoint=sb://radiomedium.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=0wKUj9d42lmOos6V7MsBV0zkSRFnq+Km+sKTvwoJr38=";

            var namespaceManager =
                NamespaceManager.CreateFromConnectionString(connectionString);

            if (!namespaceManager.TopicExists("DataRadioBroadcast"))
            {
                // Configure Topic Settings.
                TopicDescription td = new TopicDescription("DataRadioBroadcast");
                td.MaxSizeInMegabytes = 1024;
                td.DefaultMessageTimeToLive = new TimeSpan(0, 0, 0, 30);
                namespaceManager.CreateTopic(td);
            }

            if (!namespaceManager.SubscriptionExists("DataRadioBroadcast", "AllMessages"))
            {
                namespaceManager.CreateSubscription("DataRadioBroadcast", "AllMessages");
            }

            SubscriptionClient SubClient =  SubscriptionClient.CreateFromConnectionString (connectionString, "DataRadioBroadcast", "AllMessages");

            // Configure the callback options.
            OnMessageOptions options = new OnMessageOptions();
            options.AutoComplete = false;
            options.AutoRenewTimeout = TimeSpan.FromMinutes(1);

            SubClient.OnMessage((message) =>
            {
                try
                {
                    // Process message from subscription.
                    Console.WriteLine("\n**High Messages**");
                    Console.WriteLine("Body: " + message.GetBody<string>());
                    Console.WriteLine("MessageID: " + message.MessageId);
                    Console.WriteLine("Message Number: " +
                        message.Properties["MessageNumber"]);

                    // Remove message from subscription.
                    message.Complete();
                }
                catch (Exception)
                {
                    // Indicates a problem, unlock message in subscription.
                    message.Abandon();
                }
            }, options);

            TopicClient TopicClient = TopicClient.CreateFromConnectionString(connectionString, "DataRadioBroadcast");

            for (int i = 0; i < 5; i++)
            {
                // Create message, passing a string message for the body.
                BrokeredMessage message = new BrokeredMessage("Test message " + i);

                // Set additional custom app-specific property.
                message.Properties["MessageId"] = i;

                // Send message to the topic.
                TopicClient.Send(message);
            }

        }



    }
}
