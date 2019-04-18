using CQRS.Events;
using CQRS.Events.Default;

namespace CQRS.Tests.Events.Scenarios.CommandComplete
{
    public class WhenCommandCompleted_DoSomeWork : IHandleEvent<CommandCompletedEvent>
    {
        private readonly ICommandCompletedHandlerInvokesMe _commandCompletedHandlerInvokesMe;

        public WhenCommandCompleted_DoSomeWork(ICommandCompletedHandlerInvokesMe commandCompletedHandlerInvokesMe)
        {
            _commandCompletedHandlerInvokesMe = commandCompletedHandlerInvokesMe;
        }

        public void Handle(CommandCompletedEvent @event)
        {
            _commandCompletedHandlerInvokesMe.Execute();
        }
    }
}
