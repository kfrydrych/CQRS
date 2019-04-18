using CQRS.Validation;

namespace CQRS.Tests.Queries.Scenarios.ValidQuery
{
    public class GetMeSomethingValidQueryValidator : IValidateQuery<GetMeSomethingValidQuery>
    {
        public ActionPossible Execute(GetMeSomethingValidQuery query)
        {
            return ActionPossible.True();
        }
    }
}
