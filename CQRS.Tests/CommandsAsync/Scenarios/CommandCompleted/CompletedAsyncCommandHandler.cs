using CQRS.Commands;
using System.Threading.Tasks;

namespace CQRS.Tests.CommandsAsync.Scenarios.CommandCompleted
{
    public class CompletedAsyncCommandHandler : IHandleCommandAsync<CompletedAsyncCommand>
    {
        public Task Handle(CompletedAsyncCommand command)
        {
            return Task.Run(() => { });
        }
    }
}
