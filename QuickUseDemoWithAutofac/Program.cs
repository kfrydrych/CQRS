using Autofac;
using CQRS;
using CQRS.Autofac;
using System;
using System.Reflection;

namespace QuickUseDemoWithAutofac
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            RegisterComponents.Execute(builder, Assembly.GetExecutingAssembly());

            var container = builder.Build();

            var mediator = container.Resolve<IMediator>();

            var task = mediator.Send(new GetDataQuery());

            var result = task.Result;

            Console.WriteLine(result);

            Console.ReadLine();
        }
    }
}
