namespace CQRS.Query
{
    public interface IHandleQuery
    {
    }

    public interface IHandleQuery<in TQuery, out TResult> : IHandleQuery
        where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}