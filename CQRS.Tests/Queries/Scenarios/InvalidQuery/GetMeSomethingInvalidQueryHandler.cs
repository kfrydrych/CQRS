using CQRS.Query;

namespace CQRS.Tests.Queries.Scenarios.InvalidQuery
{
    public class GetMeSomethingInvalidQueryHandler : IHandleQuery<GetMeSomethingInvalidQuery, InvalidQueryViewModel>
    {
        public InvalidQueryViewModel Handle(GetMeSomethingInvalidQuery query)
        {
            return new InvalidQueryViewModel();
        }
    }
}