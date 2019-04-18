using CQRS.Commands;
using System.Threading.Tasks;

namespace CQRS.Tests.CommandsAsync.Scenarios.NoValidation
{
    public class MakeSomethingAsyncCommandHandler : IHandleCommandAsync<MakeSomethingAsyncCommand>
    {
        private readonly IMakeSomethingAsyncCommandHandlerShouldInvokeMe _makeSomethingAsyncCommandHandlerShouldInvokeMe;

        public MakeSomethingAsyncCommandHandler(IMakeSomethingAsyncCommandHandlerShouldInvokeMe makeSomethingAsyncCommandHandlerShouldInvokeMe)
        {
            _makeSomethingAsyncCommandHandlerShouldInvokeMe = makeSomethingAsyncCommandHandlerShouldInvokeMe;
        }

        public Task Handle(MakeSomethingAsyncCommand command)
        {
            _makeSomethingAsyncCommandHandlerShouldInvokeMe.Execute();
            return Task.Run(() => { });
        }
    }
}
