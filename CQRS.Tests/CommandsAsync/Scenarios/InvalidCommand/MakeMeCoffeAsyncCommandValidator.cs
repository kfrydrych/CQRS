using CQRS.Validation;

namespace CQRS.Tests.CommandsAsync.Scenarios.InvalidCommand
{
    public class MakeMeCoffeAsyncCommandValidator : IValidateCommand<MakeMeCoffeAsyncCommand>
    {
        public ActionPossible Execute(MakeMeCoffeAsyncCommand command)
        {
            return ActionPossible.Unauthorized();
        }
    }
}