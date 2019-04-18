using CQRS.Events;
using CQRS.Events.Default;
using CQRS.Validation;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace CQRS.Query
{
    internal class QueryBus : IQueryBus
    {
        private readonly Func<Type, Type, IHandleQuery> _handlersFactory;
        private readonly Func<Type, Type, IHandleQueryAsync> _asyncHandlersFactory;
        private readonly Func<Type, IValidateQuery> _validatorsFactory;
        private readonly IEventsBus _eventsBus;

        public QueryBus(Func<Type, Type, IHandleQuery> handlersFactory,
                        Func<Type, Type, IHandleQueryAsync> asyncHandlersFactory,
                        Func<Type, IValidateQuery> validatorsFactory,
                        IEventsBus eventsBus)
        {
            _handlersFactory = handlersFactory;
            _asyncHandlersFactory = asyncHandlersFactory;
            _validatorsFactory = validatorsFactory;
            _eventsBus = eventsBus;
        }

        public TResult Send<TResult>(IQuery<TResult> query)
        {
            var queryType = query.GetType();

            var validator = _validatorsFactory(queryType);

            if (validator != null)
            {
                var validatorType = validator.GetType();

                var actionPossible = (ActionPossible)validatorType.InvokeMember("Execute", BindingFlags.InvokeMethod, null, validator, new object[] { query });

                if (actionPossible.IsImpossible)
                {
                    _eventsBus.Publish(new ActionImposibleEvent(actionPossible.Errors));
                    return default(TResult);
                }
            }

            var handler = _handlersFactory(queryType, typeof(TResult));

            var handlerType = handler.GetType();

            return (TResult)handlerType.InvokeMember("Handle", BindingFlags.InvokeMethod, null, handler, new object[] { query });

        }

        public async Task<TResult> Send<TResult>(IAsyncQuery<TResult> query)
        {

            var queryType = query.GetType();

            var validator = _validatorsFactory(queryType);

            if (validator != null)
            {
                var validatorType = validator.GetType();

                var actionPossible = (ActionPossible)validatorType.InvokeMember("Execute", BindingFlags.InvokeMethod, null, validator, new object[] { query });

                if (actionPossible.IsImpossible)
                {
                    _eventsBus.Publish(new ActionImposibleEvent(actionPossible.Errors));
                    return default(TResult);
                }
            }

            var handler = _asyncHandlersFactory(queryType, typeof(TResult));

            var handlerType = handler.GetType();

            return await (Task<TResult>)handlerType.InvokeMember("Handle", BindingFlags.InvokeMethod, null, handler, new object[] { query });

        }
    }
}