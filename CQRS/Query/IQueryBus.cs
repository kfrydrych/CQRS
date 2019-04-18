using System.Threading.Tasks;

namespace CQRS.Query
{
    internal interface IQueryBus
    {
        TResult Send<TResult>(IQuery<TResult> query);

        Task<TResult> Send<TResult>(IAsyncQuery<TResult> query);
    }
}