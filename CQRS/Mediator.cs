using CQRS.Commands;
using CQRS.Events;
using CQRS.Query;
using System.Threading.Tasks;

namespace CQRS
{
    internal class Mediator : IMediator
    {
        private readonly ICommandsBus _commandBus;
        private readonly IQueryBus _queryBus;
        private readonly IEventsBus _eventsBus;

        public Mediator(ICommandsBus commandBus, IQueryBus queryBus, IEventsBus eventsBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
            _eventsBus = eventsBus;
        }

        public void Send(ICommand command)
        {
            _commandBus.Send(command);
        }

        public async Task Send(IAsyncCommand command)
        {
            await _commandBus.Send(command);
        }

        public TResult Send<TResult>(IQuery<TResult> query)
        {
            return _queryBus.Send(query);
        }


        public async Task<TResult> Send<TResult>(IAsyncQuery<TResult> query)
        {
            return await _queryBus.Send(query);
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            _eventsBus.Publish(@event);
        }
    }
}
