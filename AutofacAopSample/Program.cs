// Install-Package Autofac.Extras.DynamicProxy2

using Autofac;
using Autofac.Extras.DynamicProxy2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAopSample
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.Register(i => new Logger(Console.Out));
            builder.RegisterType<ConsoleOutput>().As<IOutput>().EnableInterfaceInterceptors();
            builder.RegisterType<TodayWriter>().As<IDateWriter>(); //.EnableInterfaceInterceptors();
            Container = builder.Build();

            // The WriteDate method is where we'll make use
            // of our dependency injection. We'll define that
            // in a bit.
            WriteDate();
            Console.ReadLine();
        }


        public static void WriteDate()
        {
            // Create the scope, resolve your IDateWriter,
            // use it, then dispose of the scope.
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IDateWriter>();
                writer.WriteDate();
            }
        }
    }
}
