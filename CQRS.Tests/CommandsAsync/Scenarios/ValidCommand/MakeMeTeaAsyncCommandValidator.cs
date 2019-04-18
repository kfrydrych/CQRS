using CQRS.Validation;

namespace CQRS.Tests.CommandsAsync.Scenarios.ValidCommand
{
    public class MakeMeTeaAsyncCommandValidator : IValidateCommand<MakeMeTeaAsyncCommand>
    {
        public ActionPossible Execute(MakeMeTeaAsyncCommand command)
        {
            return ActionPossible.True();
        }
    }
}