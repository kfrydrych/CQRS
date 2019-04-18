namespace CQRS.Query
{
    public interface IQuery : IBaseQuery
    {
    }

    public interface IQuery<TResult> : IQuery
    {
    }

}
