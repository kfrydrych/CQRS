using CQRS.Query;
using System.Threading.Tasks;

namespace CQRS.Tests.QueriesAsync.Scenarios.ValidQuery
{
    public class GetMeSomethingValidAsyncQueryHandler : IHandleQueryAsync<GetMeSomethingValidAsyncQuery, ValidAsyncQueryViewModel>
    {
        public Task<ValidAsyncQueryViewModel> Handle(GetMeSomethingValidAsyncQuery query)
        {
            return Task.Factory.StartNew(BuildModel);
        }

        private ValidAsyncQueryViewModel BuildModel()
        {
            return new ValidAsyncQueryViewModel();
        }
    }
}
