using Autofac;
using CQRS.Autofac;
using CQRS.Tests.Events.Scenarios.ActionImposible;
using CQRS.Tests.Queries.Scenarios.InvalidQuery;
using CQRS.Tests.Queries.Scenarios.NoValidation;
using CQRS.Tests.Queries.Scenarios.ValidQuery;
using Moq;
using NUnit.Framework;
using System.Reflection;

namespace CQRS.Tests.Queries
{
    [TestFixture]
    public class QueryBusTests
    {
        private readonly IContainer _container;
        private readonly Mock<IActionImposibleHandlerInvokesMe> _actionImposibleHadlerInvokesMe;

        public QueryBusTests()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterComponents.Execute(containerBuilder, Assembly.GetExecutingAssembly());

            _actionImposibleHadlerInvokesMe = new Mock<IActionImposibleHandlerInvokesMe>();
            containerBuilder.RegisterInstance(_actionImposibleHadlerInvokesMe.Object)
                .As<IActionImposibleHandlerInvokesMe>();

            _container = containerBuilder.Build();
        }

        [Test]
        public void SendQuery_WhenNoValidation_ReturnsViewModel()
        {
            var queryBus = _container.Resolve<IMediator>();

            var result = queryBus.Send(new GetNoValidationQuery());

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SendQuery_WhenValidQuery_ReturnsViewModel()
        {
            var mediator = _container.Resolve<IMediator>();

            var result = mediator.Send(new GetMeSomethingValidQuery());

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SendQuery_WhenInvalidQuery_ReturnsNull()
        {
            var mediator = _container.Resolve<IMediator>();

            var result = mediator.Send(new GetMeSomethingInvalidQuery());

            Assert.That(result, Is.Null);
        }

        [Test]
        public void SendQuery_WhenInvalidQuery_RaiseActionImposibleEvent()
        {
            var mediator = _container.Resolve<IMediator>();

            var result = mediator.Send(new GetMeSomethingInvalidQuery());

            _actionImposibleHadlerInvokesMe.Verify(x => x.Execute(), Times.Once);
        }
    }
}
