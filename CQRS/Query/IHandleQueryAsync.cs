using System.Threading.Tasks;

namespace CQRS.Query
{
    public interface IHandleQueryAsync : IHandleQuery
    {
    }

    public interface IHandleQueryAsync<in TQuery, TResult> : IHandleQueryAsync
        where TQuery : IAsyncQuery<TResult>
    {
        Task<TResult> Handle(TQuery query);
    }
}
