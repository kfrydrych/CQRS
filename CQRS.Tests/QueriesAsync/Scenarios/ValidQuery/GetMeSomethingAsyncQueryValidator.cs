using CQRS.Validation;

namespace CQRS.Tests.QueriesAsync.Scenarios.ValidQuery
{
    public class GetMeSomethingAsyncQueryValidator : IValidateQuery<GetMeSomethingValidAsyncQuery>
    {
        public ActionPossible Execute(GetMeSomethingValidAsyncQuery query)
        {
            return ActionPossible.True();
        }
    }
}
