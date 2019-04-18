using CQRS.Commands;

namespace CQRS.Tests.Commands.Scenarios.InvalidCommand
{
    public class MakeMeCoffeCommandHandler : IHandleCommand<MakeMeCoffeCommand>
    {
        private readonly IMakeMeCoffeHandlerShouldNotInvokeMe _makeMeCoffeHandlerShouldNotInvokeMe;

        public MakeMeCoffeCommandHandler(IMakeMeCoffeHandlerShouldNotInvokeMe makeMeCoffeHandlerShouldNotInvokeMe)
        {
            _makeMeCoffeHandlerShouldNotInvokeMe = makeMeCoffeHandlerShouldNotInvokeMe;
        }

        public void Handle(MakeMeCoffeCommand command)
        {
            _makeMeCoffeHandlerShouldNotInvokeMe.Execute();
        }
    }
}