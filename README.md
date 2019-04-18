# CQRS
My own implementation of CQRS/Mediator pattern

========================
----- CREATE QUERY -----
========================

    public class GetDataQuery : IAsyncQuery<string>
    {
    }

==========================
----- CREATE HANDLER -----
==========================

    public class GetDataQueryHandler : IHandleQueryAsync<GetDataQuery, string>
    {
        public async Task<string> Handle(GetDataQuery query)
        {
            var webClient = new WebClient();
            return await webClient.DownloadStringTaskAsync("http://msdn.microsoft.com");
        }
    }

==========================================
----- AUTOFAC SETUP & EXAMPLE OF USE -----
==========================================

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            RegisterComponents.Execute(builder, Assembly.GetExecutingAssembly());

            var container = builder.Build();

            var requestBus = container.Resolve<IRequestBus>();

            var task = requestBus.Send(new GetDataQuery());

            var result = task.Result;

            Console.WriteLine(result);

            Console.ReadLine();


        }
    }
