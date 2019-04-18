using CQRS.Events;
using CQRS.Events.Default;

namespace CQRS.Tests.Events.Scenarios.ActionImposible
{
    public class WhenActionImposible_DoSomeWork : IHandleEvent<ActionImposibleEvent>
    {
        private readonly IActionImposibleHandlerInvokesMe _actionImposibleHandlerInvokesMe;

        public WhenActionImposible_DoSomeWork(IActionImposibleHandlerInvokesMe actionImposibleHandlerInvokesMe)
        {
            _actionImposibleHandlerInvokesMe = actionImposibleHandlerInvokesMe;
        }

        public void Handle(ActionImposibleEvent @event)
        {
            _actionImposibleHandlerInvokesMe.Execute();
        }
    }
}
