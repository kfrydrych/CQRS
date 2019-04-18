using CQRS.Validation;

namespace CQRS.Tests.Commands.Scenarios.InvalidCommand
{
    public class MakeMeCoffeCommandValidator : IValidateCommand<MakeMeCoffeCommand>
    {
        public ActionPossible Execute(MakeMeCoffeCommand command)
        {
            return ActionPossible.Unauthorized();
        }
    }
}