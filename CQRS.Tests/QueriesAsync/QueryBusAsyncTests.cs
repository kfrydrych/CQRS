using Autofac;
using CQRS.Autofac;
using CQRS.Tests.Events.Scenarios.ActionImposible;
using CQRS.Tests.QueriesAsync.Scenarios.InvalidQuery;
using CQRS.Tests.QueriesAsync.Scenarios.NoValidation;
using CQRS.Tests.QueriesAsync.Scenarios.ValidQuery;
using Moq;
using NUnit.Framework;
using System.Reflection;
using System.Threading.Tasks;

namespace CQRS.Tests.QueriesAsync
{
    [TestFixture]
    public class QueryBusAsyncTests
    {
        private readonly IContainer _container;
        private readonly Mock<IActionImposibleHandlerInvokesMe> _actionImposibleHadlerInvokesMe;

        public QueryBusAsyncTests()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterComponents.Execute(containerBuilder, Assembly.GetExecutingAssembly());

            _actionImposibleHadlerInvokesMe = new Mock<IActionImposibleHandlerInvokesMe>();
            containerBuilder.RegisterInstance(_actionImposibleHadlerInvokesMe.Object)
                .As<IActionImposibleHandlerInvokesMe>();

            _container = containerBuilder.Build();
        }

        [Test]
        public async Task SendQueryAsync_WhenNoValidation_ReturnsViewModel()
        {
            var mediator = _container.Resolve<IMediator>();

            var result = await mediator.Send(new GetNoValidationAsyncQuery());

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task SendQueryAsync_WhenValidQuery_ReturnsViewModel()
        {
            var mediator = _container.Resolve<IMediator>();

            var result = await mediator.Send(new GetMeSomethingValidAsyncQuery());

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task SendQueryAsync_WhenInvalidQuery_ReturnsNull()
        {
            var mediator = _container.Resolve<IMediator>();

            var result = await mediator.Send(new GetMeSomethingInvalidAsyncQuery());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task SendQueryAsync_WhenInvalidQuery_RaiseActionImposibleEvent()
        {
            var mediator = _container.Resolve<IMediator>();

            var result = await mediator.Send(new GetMeSomethingInvalidAsyncQuery());

            _actionImposibleHadlerInvokesMe.Verify(x => x.Execute(), Times.Once);
        }
    }
}
