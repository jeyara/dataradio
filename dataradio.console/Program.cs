using Autofac;
using dataradio.core;
using dataradio.medium;
using dataradio.radio;
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

                var radio1 = scope.Resolve<ITransReceiver>();
                radio1.ReceiverId = "R 1";
                var radio2 = scope.Resolve<ITransReceiver>();
                radio2.ReceiverId = "R 2";
                var radio3 = scope.Resolve<ITransReceiver>();
                radio3.ReceiverId = "R 3";

                radio1.TransReceiversInRange = new List<ITransReceiver> { radio2 };
                radio2.TransReceiversInRange = new List<ITransReceiver> { radio1, radio3 };
                radio3.TransReceiversInRange = new List<ITransReceiver> { radio2 };

                Packet packet = new Packet { SourceId = radio1.ReceiverId, Message = $"Hello from {radio1.ReceiverId}" };

                radio1.Broadcast(air, packet);
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
    }
}
