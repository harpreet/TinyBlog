using System.Data;
using NHibernate;

namespace TinyBlog.Data
{
    public class QueryExecutor : IQueryExecutor
    {
        protected readonly ISession _session;

        public QueryExecutor(ISession session)
        {
            _session = session;
        }

        public virtual ISession Session
        {
            get { return _session; }
        }

        #region IQueryExecutor Members

        public virtual T ExecuteQuery<T>(Query<T> query)
        {
            using (ITransaction tx = Session.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                T result = query.Invoke(Session);
                tx.Commit();
                return result;
            }
        }

        public virtual bool UpdateDelete(CUDQuery q)
        {
            Query<object> newQuery = session =>
                                         {
                                             q.Invoke(session);
                                             return null;
                                         };

            ExecuteQuery(newQuery);

            return true;
        }

        #endregion
    }
}