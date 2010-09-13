namespace TinyBlog.Data
{
    public interface IQueryExecutor
    {
        T ExecuteQuery<T>(Query<T> query);
        bool UpdateDelete(CUDQuery q);
    }
}