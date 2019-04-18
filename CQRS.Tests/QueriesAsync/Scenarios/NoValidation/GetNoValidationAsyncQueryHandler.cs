using CQRS.Query;
using System.Threading.Tasks;

namespace CQRS.Tests.QueriesAsync.Scenarios.NoValidation
{
    public class GetNoValidationAsyncQueryHandler : IHandleQueryAsync<GetNoValidationAsyncQuery, NoValidationViewModel>
    {
        public Task<NoValidationViewModel> Handle(GetNoValidationAsyncQuery query)
        {
            return Task.Factory.StartNew(BuildModel);
        }

        private NoValidationViewModel BuildModel()
        {
            return new NoValidationViewModel();
        }
    }
}