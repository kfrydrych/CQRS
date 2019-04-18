using CQRS.Commands;

namespace CQRS.Tests.Commands.Scenarios.NoValidation
{
    public class MakeSomethingCommandHandler : IHandleCommand<MakeSomethingCommand>
    {
        private readonly IMakeSomethingHandlerShouldInvokeMe _makeSomethingHandlerShouldInvokeMe;

        public MakeSomethingCommandHandler(IMakeSomethingHandlerShouldInvokeMe makeSomethingHandlerShouldInvokeMe)
        {
            _makeSomethingHandlerShouldInvokeMe = makeSomethingHandlerShouldInvokeMe;
        }

        public void Handle(MakeSomethingCommand command)
        {
            _makeSomethingHandlerShouldInvokeMe.Execute();
        }
    }
}
