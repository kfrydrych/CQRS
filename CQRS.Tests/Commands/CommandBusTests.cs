using Autofac;
using CQRS.Autofac;
using CQRS.Tests.Commands.Scenarios.CommandCompleted;
using CQRS.Tests.Commands.Scenarios.InvalidCommand;
using CQRS.Tests.Commands.Scenarios.NoValidation;
using CQRS.Tests.Commands.Scenarios.ValidCommand;
using CQRS.Tests.Events.Scenarios.ActionImposible;
using CQRS.Tests.Events.Scenarios.CommandComplete;
using Moq;
using NUnit.Framework;
using System.Reflection;

namespace CQRS.Tests.Commands
{
    [TestFixture]
    public class CommandBusTests
    {
        private readonly IContainer _container;
        private readonly Mock<IMakeSomethingHandlerShouldInvokeMe> _makeSomethingHandlerShouldInvokeMe;
        private readonly Mock<IMakeMeCoffeHandlerShouldNotInvokeMe> _makeMeCoffeHandlerShouldNotInvokeMe;
        private readonly Mock<IMakeMeTeaHandlerShouldInvokeMe> _makeMeTeaHandlerShouldInvokeMe;
        private readonly Mock<IActionImposibleHandlerInvokesMe> _actionImposibleHadlerInvokesMe;
        private readonly Mock<ICommandCompletedHandlerInvokesMe> _commandCompletedHandlerInvokesMe;

        public CommandBusTests()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterComponents.Execute(containerBuilder, Assembly.GetExecutingAssembly());

            _makeSomethingHandlerShouldInvokeMe = new Mock<IMakeSomethingHandlerShouldInvokeMe>();
            containerBuilder.RegisterInstance(_makeSomethingHandlerShouldInvokeMe.Object)
                .As<IMakeSomethingHandlerShouldInvokeMe>();

            _makeMeCoffeHandlerShouldNotInvokeMe = new Mock<IMakeMeCoffeHandlerShouldNotInvokeMe>();
            containerBuilder.RegisterInstance(_makeMeCoffeHandlerShouldNotInvokeMe.Object)
                .As<IMakeMeCoffeHandlerShouldNotInvokeMe>();

            _makeMeTeaHandlerShouldInvokeMe = new Mock<IMakeMeTeaHandlerShouldInvokeMe>();
            containerBuilder.RegisterInstance(_makeMeTeaHandlerShouldInvokeMe.Object)
                .As<IMakeMeTeaHandlerShouldInvokeMe>();

            _actionImposibleHadlerInvokesMe = new Mock<IActionImposibleHandlerInvokesMe>();
            containerBuilder.RegisterInstance(_actionImposibleHadlerInvokesMe.Object)
                .As<IActionImposibleHandlerInvokesMe>();

            _commandCompletedHandlerInvokesMe = new Mock<ICommandCompletedHandlerInvokesMe>();
            containerBuilder.RegisterInstance(_commandCompletedHandlerInvokesMe.Object)
                .As<ICommandCompletedHandlerInvokesMe>();

            _container = containerBuilder.Build();
        }

        [Test]
        public void SendCommand_WhenNoValidation_ShouldInvokeHandler()
        {
            var mediator = _container.Resolve<IMediator>();

            mediator.Send(new MakeSomethingCommand());

            _makeSomethingHandlerShouldInvokeMe.Verify(x => x.Execute(), Times.Once);
        }

        [Test]
        public void SendCommand_WhenInvalidCommand_HandlerShouldNotBeInvoked()
        {
            var mediator = _container.Resolve<IMediator>();

            mediator.Send(new MakeMeCoffeCommand());

            _makeMeCoffeHandlerShouldNotInvokeMe.Verify(x => x.Execute(), Times.Never);
        }

        [Test]
        public void SendCommand_WhenInvalidCommand_ActionImposibleEventShouldBeRaised()
        {
            var mediator = _container.Resolve<IMediator>();

            mediator.Send(new MakeMeCoffeCommand());

            _actionImposibleHadlerInvokesMe.Verify(x => x.Execute(), Times.Once);
        }

        [Test]
        public void SendCommand_WhenValidCommand_ShouldInvokeHandler()
        {
            var mediator = _container.Resolve<IMediator>();

            mediator.Send(new MakeMeTeaCommand());

            _makeMeTeaHandlerShouldInvokeMe.Verify(x => x.Execute(), Times.Once);

        }

        [Test]
        public void SendCommand_WhenCommandCompleted_CommandCompletedEventShouldBeRaised()
        {
            var mediator = _container.Resolve<IMediator>();

            mediator.Send(new CompletedCommand());

            _commandCompletedHandlerInvokesMe.Verify(x => x.Execute(), Times.Once);
        }
    }
}