using CQRS.Commands;
using System.Threading.Tasks;

namespace CQRS.Tests.CommandsAsync.Scenarios.ValidCommand
{
    public class MakeMeTeaAsyncCommandHandler : IHandleCommandAsync<MakeMeTeaAsyncCommand>
    {
        private readonly IMakeMeTeaAsyncCommandHandlerShouldInvokeMe _makeMeTeaAsyncCommandHandlerShouldInvokeMe;

        public MakeMeTeaAsyncCommandHandler(IMakeMeTeaAsyncCommandHandlerShouldInvokeMe makeMeTeaAsyncCommandHandlerShouldInvokeMe)
        {
            _makeMeTeaAsyncCommandHandlerShouldInvokeMe = makeMeTeaAsyncCommandHandlerShouldInvokeMe;
        }

        public Task Handle(MakeMeTeaAsyncCommand command)
        {
            _makeMeTeaAsyncCommandHandlerShouldInvokeMe.Execute();
            return Task.Run(() => { });
        }
    }
}