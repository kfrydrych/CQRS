using CQRS.Commands;
using CQRS.Events;
using CQRS.Query;
using System.Threading.Tasks;

namespace CQRS
{
    public interface IMediator
    {
        void Send(ICommand command);

        Task Send(IAsyncCommand command);

        TResult Send<TResult>(IQuery<TResult> query);

        Task<TResult> Send<TResult>(IAsyncQuery<TResult> query);

        void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
