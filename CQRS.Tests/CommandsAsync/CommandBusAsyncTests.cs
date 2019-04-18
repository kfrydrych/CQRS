using Autofac;
using CQRS.Autofac;
using CQRS.Tests.CommandsAsync.Scenarios.CommandCompleted;
using CQRS.Tests.CommandsAsync.Scenarios.InvalidCommand;
using CQRS.Tests.CommandsAsync.Scenarios.NoValidation;
using CQRS.Tests.CommandsAsync.Scenarios.ValidCommand;
using CQRS.Tests.Events.Scenarios.ActionImposible;
using CQRS.Tests.Events.Scenarios.CommandComplete;
using Moq;
using NUnit.Framework;
using System.Reflection;
using System.Threading;

namespace CQRS.Tests.CommandsAsync
{
    [TestFixture]
    public class CommandBusAsyncTests
    {
        private readonly IContainer _container;
        private readonly Mock<IMakeSomethingAsyncCommandHandlerShouldInvokeMe> _makeSomethingHandlerShouldInvokeMe;
        private readonly Mock<IMakeMeCoffeAsyncCommandHandlerShouldNotInvokeMe> _makeMeCoffeHandlerShouldNotInvokeMe;
        private readonly Mock<IMakeMeTeaAsyncCommandHandlerShouldInvokeMe> _makeMeTeaCommandHandlerShouldInvokeMe;
        private readonly Mock<IActionImposibleHandlerInvokesMe> _actionImposibleHadlerInvokesMe;
        private readonly Mock<ICommandCompletedHandlerInvokesMe> _commandCompletedHandlerInvokesMe;

        public CommandBusAsyncTests()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterComponents.Execute(containerBuilder, Assembly.GetExecutingAssembly());

            _makeSomethingHandlerShouldInvokeMe = new Mock<IMakeSomethingAsyncCommandHandlerShouldInvokeMe>();
            containerBuilder.RegisterInstance(_makeSomethingHandlerShouldInvokeMe.Object)
                .As<IMakeSomethingAsyncCommandHandlerShouldInvokeMe>();

            _makeMeCoffeHandlerShouldNotInvokeMe = new Mock<IMakeMeCoffeAsyncCommandHandlerShouldNotInvokeMe>();
            containerBuilder.RegisterInstance(_makeMeCoffeHandlerShouldNotInvokeMe.Object)
                .As<IMakeMeCoffeAsyncCommandHandlerShouldNotInvokeMe>();

            _makeMeTeaCommandHandlerShouldInvokeMe = new Mock<IMakeMeTeaAsyncCommandHandlerShouldInvokeMe>();
            containerBuilder.RegisterInstance(_makeMeTeaCommandHandlerShouldInvokeMe.Object)
                .As<IMakeMeTeaAsyncCommandHandlerShouldInvokeMe>();

            _actionImposibleHadlerInvokesMe = new Mock<IActionImposibleHandlerInvokesMe>();
            containerBuilder.RegisterInstance(_actionImposibleHadlerInvokesMe.Object)
                .As<IActionImposibleHandlerInvokesMe>();

            _commandCompletedHandlerInvokesMe = new Mock<ICommandCompletedHandlerInvokesMe>();
            containerBuilder.RegisterInstance(_commandCompletedHandlerInvokesMe.Object)
                .As<ICommandCompletedHandlerInvokesMe>();

            _container = containerBuilder.Build();
        }

        [Test]
        public void SendCommandAsync_WhenNoValidation_ShouldInvokeHandler()
        {
            var mediator = _container.Resolve<IMediator>();

            mediator.Send(new MakeSomethingAsyncCommand());

            _makeSomethingHandlerShouldInvokeMe.Verify(x => x.Execute(), Times.Once);
        }

        [Test]
        public void SendCommandAsync_WhenInvalidCommand_HandlerShouldNotBeInvoked()
        {
            var mediator = _container.Resolve<IMediator>();

            mediator.Send(new MakeMeCoffeAsyncCommand());

            _makeMeCoffeHandlerShouldNotInvokeMe.Verify(x => x.Execute(), Times.Never);
        }

        [Test]
        public void SendCommandAsync_WhenInvalidCommand_ActionImposibleEventShouldBeRaised()
        {
            var mediator = _container.Resolve<IMediator>();

            mediator.Send(new MakeMeCoffeAsyncCommand());

            _actionImposibleHadlerInvokesMe.Verify(x => x.Execute(), Times.Once);
        }

        [Test]
        public void SendCommandAsync_WhenValidCommand_ShouldInvokeHandler()
        {
            var mediator = _container.Resolve<IMediator>();

            mediator.Send(new MakeMeTeaAsyncCommand());

            _makeMeTeaCommandHandlerShouldInvokeMe.Verify(x => x.Execute(), Times.Once);
        }

        [Test]
        public void SendCommandAsync_WhenCommandCompleted_CommandCompletedEventShouldBeRaised()
        {
            var mediator = _container.Resolve<IMediator>();

            mediator.Send(new CompletedAsyncCommand());

            Thread.Sleep(10);

            _commandCompletedHandlerInvokesMe.Verify(x => x.Execute(), Times.Once);
        }
    }
}
