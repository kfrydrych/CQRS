using CQRS.Commands;

namespace CQRS.Tests.Commands.Scenarios.ValidCommand
{
    public class MakeMeTeaCommandHandler : IHandleCommand<MakeMeTeaCommand>
    {
        private readonly IMakeMeTeaHandlerShouldInvokeMe _makeMeTeaHandlerShouldInvokeMe;

        public MakeMeTeaCommandHandler(IMakeMeTeaHandlerShouldInvokeMe makeMeTeaHandlerShouldInvokeMe)
        {
            _makeMeTeaHandlerShouldInvokeMe = makeMeTeaHandlerShouldInvokeMe;
        }

        public void Handle(MakeMeTeaCommand command)
        {
            _makeMeTeaHandlerShouldInvokeMe.Execute();
        }
    }
}
