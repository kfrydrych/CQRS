using CQRS.Query;

namespace CQRS.Tests.Queries.Scenarios.NoValidation
{
    public class GetNoValidationQueryHandler : IHandleQuery<GetNoValidationQuery, NoValidationViewModel>
    {
        public NoValidationViewModel Handle(GetNoValidationQuery query)
        {
            return new NoValidationViewModel();
        }
    }
}
