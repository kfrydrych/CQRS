using CQRS.Validation;

namespace CQRS.Tests.QueriesAsync.Scenarios.InvalidQuery
{
    public class GetMeSomethingInvalidAsyncQueryValidator : IValidateQuery<GetMeSomethingInvalidAsyncQuery>
    {
        public ActionPossible Execute(GetMeSomethingInvalidAsyncQuery query)
        {
            return ActionPossible.Unauthorized();
        }
    }
}