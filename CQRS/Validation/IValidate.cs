using CQRS.Commands;
using CQRS.Query;

namespace CQRS.Validation
{
    public interface IValidateCommand
    {
    }

    public interface IValidateCommand<in TCommand> : IValidateCommand where TCommand : IBaseCommand
    {
        ActionPossible Execute(TCommand command);
    }

    public interface IValidateQuery
    {
    }

    public interface IValidateQuery<in TQuery> : IValidateQuery where TQuery : IBaseQuery
    {
        ActionPossible Execute(TQuery query);
    }
}