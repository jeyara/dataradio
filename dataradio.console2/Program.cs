using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataradio.console2
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Endpoint=sb://radiomedium.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=0wKUj9d42lmOos6V7MsBV0zkSRFnq+Km+sKTvwoJr38=";

            SubscriptionClient SubClient = SubscriptionClient.CreateFromConnectionString(connectionString, "DataRadioBroadcast", "AllMessages");

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

            Console.ReadLine();

        }
    }
}
