using System.Threading.Tasks;

namespace CQRS.Commands
{
    public interface IHandleCommandAsync : IHandleCommand
    {
    }

    public interface IHandleCommandAsync<in TCommand> : IHandleCommandAsync
        where TCommand : IAsyncCommand
    {
        Task Handle(TCommand command);
    }
}
