using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NHibernate;
using StructureMap;
using TinyBlog.Data;

namespace TinyBlog.Tests
{
    public class TestQueryExecutor : QueryExecutor
    {
        private readonly IContainer _container;

        public TestQueryExecutor(ISession session, StructureMap.IContainer container) : base(session)
        {
            _container = container;
        }

        public override ISession Session
        {
            get { return _container.GetInstance<SessionBuilder>().OpenSession(); }
        }
        public override T ExecuteQuery<T>(Query<T> query)
        {
            try
            {
                using (ITransaction tx = Session.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    T result = query.Invoke(Session);
                    tx.Commit();
                    return result;
                }
            }
            finally
            {
                Session.Dispose();
            }
        }
    }
}
