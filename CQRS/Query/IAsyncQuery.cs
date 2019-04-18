namespace CQRS.Query
{
    public interface IAsyncQuery : IBaseQuery
    {
    }

    public interface IAsyncQuery<TResult> : IAsyncQuery
    {
    }
}
