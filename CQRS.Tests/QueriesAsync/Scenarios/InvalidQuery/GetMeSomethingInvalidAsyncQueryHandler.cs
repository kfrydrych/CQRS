using System.Threading.Tasks;
using CQRS.Query;

namespace CQRS.Tests.QueriesAsync.Scenarios.InvalidQuery
{
    public class GetMeSomethingInvalidAsyncQueryHandler : IHandleQueryAsync<GetMeSomethingInvalidAsyncQuery, InvalidAsyncQueryViewModel>
    {
        public Task<InvalidAsyncQueryViewModel> Handle(GetMeSomethingInvalidAsyncQuery query)
        {
            return Task.Factory.StartNew(BuildModel);
        }

        private InvalidAsyncQueryViewModel BuildModel()
        {
            return new InvalidAsyncQueryViewModel();
        }
    }
}