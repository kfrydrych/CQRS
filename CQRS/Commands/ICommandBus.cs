using System.Threading.Tasks;

namespace CQRS.Commands
{
    internal interface ICommandsBus
    {
        void Send(ICommand command);
        Task Send(IAsyncCommand command);
    }
}
