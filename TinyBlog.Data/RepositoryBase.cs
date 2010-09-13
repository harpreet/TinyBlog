namespace TinyBlog.Data
{
    public class RepositoryBase
    {
        public RepositoryBase(IQueryExecutor queryExecutor)
        {
            QueryExecutor = queryExecutor;
        }

        public IQueryExecutor QueryExecutor { get; private set; }
    }
}