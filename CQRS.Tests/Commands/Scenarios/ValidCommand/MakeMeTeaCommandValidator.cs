using CQRS.Validation;

namespace CQRS.Tests.Commands.Scenarios.ValidCommand
{
    public class MakeMeTeaCommandValidator : IValidateCommand<MakeMeTeaCommand>
    {
        public ActionPossible Execute(MakeMeTeaCommand command)
        {
            return ActionPossible.True();
        }
    }
}
