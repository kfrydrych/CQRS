using CQRS.Events;
using CQRS.Events.Default;
using CQRS.Validation;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace CQRS.Commands
{
    internal class CommandsBus : ICommandsBus
    {
        private readonly Func<Type, IHandleCommand> _handlersFactory;
        private readonly Func<Type, IHandleCommandAsync> _asyncHandlersFactory;
        private readonly Func<Type, IValidateCommand> _validatorsFactory;
        private readonly IEventsBus _eventsBus;

        public CommandsBus(Func<Type, IHandleCommand> handlersFactory,
                           Func<Type, IHandleCommandAsync> asyncHandlersFactory,
                           Func<Type, IValidateCommand> validatorsFactory,
                           IEventsBus eventsBus)
        {
            _handlersFactory = handlersFactory;
            _asyncHandlersFactory = asyncHandlersFactory;
            _validatorsFactory = validatorsFactory;
            _eventsBus = eventsBus;
        }

        public void Send(ICommand command)
        {
            var commandType = command.GetType();

            var validator = _validatorsFactory(commandType);

            if (validator != null)
            {
                var validatorType = validator.GetType();

                var actionPossible = (ActionPossible)validatorType.InvokeMember("Execute", BindingFlags.InvokeMethod, null, validator, new object[] { command });

                if (actionPossible.IsImpossible)
                {
                    _eventsBus.Publish(new ActionImposibleEvent(actionPossible.Errors));
                    return;
                }
            }

            var handler = _handlersFactory(commandType);

            var handlerType = handler.GetType();

            handlerType.InvokeMember("Handle", BindingFlags.InvokeMethod, null, handler, new object[] { command });

            _eventsBus.Publish(new CommandCompletedEvent(commandType.Name));
        }

        public async Task Send(IAsyncCommand command)
        {
            var commandType = command.GetType();

            var validator = _validatorsFactory(commandType);

            if (validator != null)
            {
                var validatorType = validator.GetType();

                var actionPossible = (ActionPossible)validatorType.InvokeMember("Execute", BindingFlags.InvokeMethod, null, validator, new object[] { command });

                if (actionPossible.IsImpossible)
                {
                    _eventsBus.Publish(new ActionImposibleEvent(actionPossible.Errors));
                    return;
                }
            }

            var handler = _asyncHandlersFactory(commandType);

            var handlerType = handler.GetType();

            await (Task)handlerType.InvokeMember("Handle", BindingFlags.InvokeMethod, null, handler, new object[] { command });

            _eventsBus.Publish(new CommandCompletedEvent(commandType.Name));
        }

    }
}
