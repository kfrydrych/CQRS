using CQRS.Commands;
using System.Threading.Tasks;

namespace CQRS.Tests.CommandsAsync.Scenarios.InvalidCommand
{
    public class MakeMeCoffeAsyncCommandHandler : IHandleCommandAsync<MakeMeCoffeAsyncCommand>
    {
        private readonly IMakeMeCoffeAsyncCommandHandlerShouldNotInvokeMe _makeMeCofferAsyncCommandHandlerShouldNotInvokeMe;

        public MakeMeCoffeAsyncCommandHandler(IMakeMeCoffeAsyncCommandHandlerShouldNotInvokeMe makeMeCofferAsyncCommandHandlerShouldNotInvokeMe)
        {
            _makeMeCofferAsyncCommandHandlerShouldNotInvokeMe = makeMeCofferAsyncCommandHandlerShouldNotInvokeMe;
        }

        public Task Handle(MakeMeCoffeAsyncCommand command)
        {
            return Task.Run(() =>
            {
                _makeMeCofferAsyncCommandHandlerShouldNotInvokeMe.Execute();
            });
        }
    }
}