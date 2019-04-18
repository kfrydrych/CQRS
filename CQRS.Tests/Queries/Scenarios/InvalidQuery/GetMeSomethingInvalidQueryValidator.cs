using CQRS.Validation;

namespace CQRS.Tests.Queries.Scenarios.InvalidQuery
{
    public class GetMeSomethingInvalidQueryValidator : IValidateQuery<GetMeSomethingInvalidQuery>
    {
        public ActionPossible Execute(GetMeSomethingInvalidQuery query)
        {
            return ActionPossible.False("I cannot perform this action");
        }
    }
}