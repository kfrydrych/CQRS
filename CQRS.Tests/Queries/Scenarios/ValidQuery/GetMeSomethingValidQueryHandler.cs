using CQRS.Query;

namespace CQRS.Tests.Queries.Scenarios.ValidQuery
{
    public class GetMeSomethingValidQueryHandler : IHandleQuery<GetMeSomethingValidQuery, ValidQueryViewModel>
    {
        public ValidQueryViewModel Handle(GetMeSomethingValidQuery query)
        {
            return new ValidQueryViewModel();
        }
    }
}